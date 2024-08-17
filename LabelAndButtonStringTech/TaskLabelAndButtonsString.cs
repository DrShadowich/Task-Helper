using NewPetProjectC_.SimpleWork;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NewPetProjectC_
{
    class TaskLabelAndButtonsString : ILabelAndButtonString
    {
        private readonly Panel _panel = null;
        public FlowLayoutPanel NewFlowLayoutPanel = null;
        public Label label = null;
        public Button DescriptionButton = null;
        public Button PathButton = null;
        public Button DeleteButton = null;
        public CheckBox CheckBox = null;
        private readonly OpenFileDialog _openFileDialog = null;
        private readonly EntityContext _context = null;        
        private readonly string _nameLabel = null;
        private readonly string _sqlTheme = null;
        private string _description = null;
        private string _filePath = null;
        /// <summary>
        /// Initialize the whole string that include Label and Buttons with checkBox
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="sqlDB"></param>
        /// <param name="labelTextName"></param>
        /// <param name="description"></param>
        /// <param name="filePath"></param>
        /// <param name="fileDialog"></param>
        public TaskLabelAndButtonsString
            (
            ref Panel panel,
            ref EntityContext context,
            string labelTextName,
            string description,
            string filePath,
            string sqlTheme,
            OpenFileDialog fileDialog
            )
        {
            _context = context;
            _nameLabel = labelTextName;
            _description = description;
            _filePath = filePath;
            _sqlTheme = sqlTheme;
            _openFileDialog = fileDialog;
            _panel = panel;
            InitLabelAndButtons();
        }
        #region INITSTRING
        public void InitLabelAndButtons()
        {
            label = new Label()
            {
                Text = _nameLabel,
                Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(130, 20)
            };

            SimpleForms.InitFlowLayoutPanelString(ref NewFlowLayoutPanel, DockStyle.Bottom);

            DescriptionButton = new Button()
            {
                Text = "Добавить описание",
                Size = new Size(130, 23)
            };
            PathButton = new Button()
            {
                Text = "Добавить решение",
                Size = new Size(130, 23)
            };
            DeleteButton = new Button() { Text = "Удалить" };
            CheckBox = new CheckBox();

            CreateMethods();

            foreach(var task in _context.BasicTasks.ToList())
            {
                if(task.Name == _nameLabel)
                {
                    task.Theme = _sqlTheme;
                    _context.BasicTasks.Update(task);
                    _context.SaveChanges();
                }
            }

            NewFlowLayoutPanel.Controls.Add(label);
            NewFlowLayoutPanel.Controls.Add(DescriptionButton);
            NewFlowLayoutPanel.Controls.Add(PathButton);
            NewFlowLayoutPanel.Controls.Add(DeleteButton);
            NewFlowLayoutPanel.Controls.Add(CheckBox);

            _panel.Controls.Add(NewFlowLayoutPanel);
        }

        public void CreateMethods()
        {

            void LocalOpenFileDialogForPathButton(object sender, EventArgs e)
            {
                if (OnOpenFileDialog("EXE файл (*.exe) | *.exe", ref _filePath))
                {
                    SimpleForms.SetButtonText(PathButton, "Готовое решение");
                    SimpleMessageBoxes.SentSuccessMessageBox("Путь до .exe файла успешно установлен.");
                    PathButton.Click -= new EventHandler(LocalOpenFileDialogForPathButton);
                    PathButton.Click += PathButtonClick;
                }
            }
            void LocalOpenFileDialogForDescriptionButton(object sender, EventArgs e)
            {
                if (OnOpenFileDialog("TXT файлы для описания (*.txt) | *.txt", ref _description))
                {
                    SimpleMessageBoxes.SentSuccessMessageBox("Описание успешно установлено");
                    SimpleForms.SetButtonText(DescriptionButton, "Описание");
                    DescriptionButton.Click -= LocalOpenFileDialogForDescriptionButton;
                    DescriptionButton.Click += DescriptionButtonClick;
                }
            }

            if (_description != null && _description != string.Empty)
            {
                DescriptionButton.Click += DescriptionButtonClick;
                SimpleForms.SetButtonText(DescriptionButton, "Описание");
            }
            else DescriptionButton.Click += LocalOpenFileDialogForDescriptionButton;
            if (_filePath != null && _filePath != string.Empty)
            {
                PathButton.Click += PathButtonClick;
                PathButton.Click += (object s, EventArgs e) => SimpleSound.Play(SimpleSound.Success_3);
                SimpleForms.SetButtonText(PathButton, "Готовое решение");
            }
            else PathButton.Click += LocalOpenFileDialogForPathButton;
            DeleteButton.Click += DeleteButtonClick;
            if (_description != null && _description != string.Empty && _filePath != null && _filePath != string.Empty) CheckBox.Checked = true;

            CheckBox.Click += CheckBoxClick;

        }
        #endregion
        #region COMPONENTSMETHODS

        void DeleteButtonClick(object sender, EventArgs e)
        {
            if(DialogResult.Yes == SimpleMessageBoxes.SentQuestionMessageBox($"Вы уверены удалить {_sqlTheme} {_nameLabel}?"))
            {
                foreach(var task in _context.BasicTasks.ToList())
                {
                    if (task.Name == _nameLabel && task.Theme == _sqlTheme) 
                    {
                        _context.BasicTasks.Remove(task);
                        _context.SaveChanges();
                    }
                }
                NewFlowLayoutPanel.Dispose();
                SimpleMessageBoxes.SentSuccess2MessageBox($"{_sqlTheme} {_nameLabel} успешно удален");
            }
        }
        private void PathButtonClick(object sender, EventArgs e)
        {
            foreach(var task in _context.BasicTasks.ToList())
            {
                if (task.Name == _nameLabel && task.Theme == _sqlTheme) _filePath = task.PathOfReadyProject;
            }
            if (_filePath != null) 
            {
                Process.Start(_filePath);
            } 
            else SimpleMessageBoxes.SentErrorMessageBox("Неверный путь к файлу, или пустой путь к файлу");
        }
        private void DescriptionButtonClick(object sender, EventArgs e) => MessageBox.Show(_description, $"Описание {_sqlTheme} {_nameLabel}.");
        private bool OnOpenFileDialog(string filter, ref string newString)
        {
            SimpleSound.Play(SimpleSound.SomethingAdd);

            _openFileDialog.Filter = filter;

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = _openFileDialog.FileName;
                if (filter.Split()[0] == "TXT")
                {
                    foreach(var task in _context.BasicTasks.ToList())
                    {
                        if (task.Name == _nameLabel && task.Theme == _sqlTheme) 
                        {
                            newString = File.ReadAllText(filePath);
                            task.Description = newString;
                            _context.BasicTasks.Update(task);
                            _context.SaveChanges();
                            return true;
                        } 
                    }
                }
                else if (filter.Split()[0] == "EXE")
                {
                    newString = filePath;
                    foreach(var task in _context.BasicTasks.ToList())
                    {
                        if(task.Name == _nameLabel && task.Theme == _sqlTheme)
                        {
                            task.PathOfReadyProject = newString;
                            _context.BasicTasks.Update(task);
                            _context.SaveChanges();
                            return true;
                        }
                    }
                }
                else SimpleMessageBoxes.SentErrorMessageBox("Использование неправильного фильтра");
                return false;
            }
            else return false;
        }
        private void CheckBoxClick(object sender, EventArgs e)
        {
            if (_description != null && _filePath != null)
            {
                foreach(var task in _context.BasicTasks.ToList())
                {
                    if(task.Name == _nameLabel && task.Theme == _sqlTheme)
                    {
                        task.IsCompleted = true;
                        _context.BasicTasks.Update(task);
                        _context.SaveChanges();
                        CheckBox.Checked = true;
                        SimpleMessageBoxes.SentSuccessMessageBox("Задача решена");
                    }
                }
            }
            else
            {
                CheckBox.Checked = false;
                SimpleMessageBoxes.SentBlockMessageBox("Вы ещё не решили задачу.");
            }
        }
        #endregion
    }
}
