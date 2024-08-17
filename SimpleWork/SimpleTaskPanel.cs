using NewPetProjectC_.SimpleWork;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NewPetProjectC_
{
    class SimpleTaskPanel : Panel
    {
        protected TabPage _thisTabPage = null;
        protected Panel _panel = null;
        private readonly OpenFileDialog _fileDialog = null;
        private readonly string _sqlTheme = string.Empty;
        private bool _isWhieBackGround = true;
        protected Button _backgroundSwitchButton = null;
        protected TextBox _textBox = null;
        protected Button _newButton = null;
        protected Button _exitButton = null;
        protected Button _superDeleteButton = null;
        protected EntityContext _context = null;


        /// <summary>
        /// For TaskPanel
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="fileDialog"></param>
        /// <param name="sqlTheme"></param>
        public SimpleTaskPanel(ref TabPage tabPage, OpenFileDialog fileDialog, string sqlTheme, EntityContext context)
        {
            _context = context;
            _sqlTheme = sqlTheme;
            _thisTabPage = tabPage;
            _fileDialog = fileDialog;
            InitComponents();
        }

        /// <summary>
        /// For TimePanel
        /// </summary>
        protected SimpleTaskPanel(EntityContext context)
        {
            _context = context;
        }
        protected virtual void InitComponents()
        {
            SimpleForms.InitPanel(ref _panel);
            _textBox = new TextBox()
            {
                Dock = DockStyle.Top,
                Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Location = new Point(0, 37),
                Name = "FirstTextBox",
                Size = new Size(564, 26),
                TabIndex = 2,
                Text = "Название нового Task\'a"
            };

            _newButton = new Button
            {
                Dock = DockStyle.Top,
                Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 204),
                Location = new Point(0, 0),
                Name = "NewTaskButton",
                Size = new Size(564, 37),
                TabIndex = 0,
                Text = "New Task",
                UseVisualStyleBackColor = true
            };

            _exitButton = new Button
            {
                Dock = DockStyle.Bottom,
                Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Location = new Point(0, 385),
                Name = "ExitButton",
                Size = new Size(564, 33),
                TabIndex = 1,
                Text = "Exit",
                UseVisualStyleBackColor = true
            };

            _backgroundSwitchButton = new Button()
            {
                Text = "Black BackGround",
                Dock = DockStyle.Bottom
            };

            _superDeleteButton = new Button
            {
                Dock = DockStyle.Bottom,
                Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Location = new Point(0, 385),
                Name = "SuperDelete",
                Size = new Size(564, 33),
                TabIndex = 1,
                Text = "Delete page",
                UseVisualStyleBackColor = true
            };
            _exitButton.Click += new EventHandler(ExitButtonClick);
            _newButton.Click += new EventHandler(NewButtonClick);
            _panel.Paint += new PaintEventHandler(PanelPaint);
            _superDeleteButton.Click += new EventHandler(SuperDeleteButtonClick);
            _backgroundSwitchButton.Click += new EventHandler(SwitchBackGroundColor);

            _panel.Controls.Add(_textBox);
            _panel.Controls.Add(_newButton);
            _panel.Controls.Add(_exitButton);
            _panel.Controls.Add(_backgroundSwitchButton);
            _panel.Controls.Add(_superDeleteButton);
            _thisTabPage.Controls.Add(_panel);
        }

        private void CreateLABString(string labelTextName, string description, string filePath)
        {
            _panel.Controls.Remove(_superDeleteButton);
            TaskLabelAndButtonsString labelAndButtonsString = new TaskLabelAndButtonsString
                (
                ref _panel,
                ref _context,
                labelTextName,
                description,
                filePath,
                _sqlTheme,
                _fileDialog
                );
            _panel.Controls.Add(_superDeleteButton);
        }
        

        private void CreateLabelAndButtonsString(string labelTextName)
        {
            try
            {
                string description = string.Empty;
                string filePath = string.Empty;

                CreateLABString(labelTextName, description, filePath);

            }
            catch (Exception ex)
            {
                SimpleMessageBoxes.SentFatalErrorMessageBox(ex.Message);
            }
        }
        public virtual void LoadLabelAndButtonsStrings()
        {
            try
            {
                var tasks = _context.BasicTasks.ToList();
                foreach (var task in tasks)
                {
                    if (task.Theme == _sqlTheme)
                    {
                        bool check = false;
                        string filePath = string.Empty;
                        string description = string.Empty;
                        string labelTextName = task.Name;

                        if (task.Description != string.Empty) description = task.Description;
                        if (task.PathOfReadyProject != string.Empty) filePath = task.PathOfReadyProject;
                        if (task.IsCompleted) check = task.IsCompleted;

                        CreateLABString(labelTextName, description, filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                SimpleMessageBoxes.SentFatalErrorMessageBox(ex.Message);
            }
        }

        protected virtual void SuperDeleteButtonClick(object sender, EventArgs e)
        {
            if(DialogResult.Yes == SimpleMessageBoxes.SentQuestionMessageBox("Вы уверены удалить эту страничку?\nВсе данные, находящиеся здесь будут удалены."))
            {
                foreach(var task in _context.BasicTasks.ToList())
                {
                    if(task.Theme == _sqlTheme) _context.BasicTasks.Remove(task);
                }
                _context.SaveChanges();   
                _thisTabPage.Dispose();
                SimpleMessageBoxes.SentSuccess2MessageBox("Страница полностью удалена");
            }
        }

        private void SwitchBackGroundColor(object sender, EventArgs e)
        {
            if (_isWhieBackGround)
            {
                foreach (Control i in _panel.Controls)
                {
                    if (i.GetType() == typeof(Button)) i.BackColor = Color.Gray;
                    else if (i.GetType() == typeof(Label)) i.BackColor = Color.Gray;
                    else if (i.GetType() == typeof(CheckBox)) i.BackColor = Color.Gray;
                }
                _panel.BackColor = Color.Black;
                _isWhieBackGround = false;
            }
            else
            {
                foreach (Control i in _panel.Controls)
                {
                    if (i.GetType() == typeof(Button)) i.BackColor = SystemColors.Control;
                    else if (i.GetType() == typeof(Label)) i.BackColor = SystemColors.Control;
                    else if (i.GetType() == typeof(CheckBox)) i.BackColor = SystemColors.Control;
                }
                _panel.BackColor = SystemColors.Control; ;
                _isWhieBackGround = true;
            }
        }

        protected virtual void NewButtonClick(object sender, EventArgs e)
        {
            string stringOfNewTaskName = _textBox.Text;
            if (stringOfNewTaskName.ToLower() == "название нового task'a") SimpleMessageBoxes.SentBlockMessageBox("Назовите свой новый Task.");
            else
            {
                CreateLabelAndButtonsString(stringOfNewTaskName);
                _context.BasicTasks.Add(new TaskEntity() { Theme = _sqlTheme, Name = stringOfNewTaskName});
                _context.SaveChanges();
                SimpleMessageBoxes.SentSuccessMessageBox($"Task успешно добавлен с именем {stringOfNewTaskName}.");
            }
        }

        protected void ExitButtonClick(object sender, EventArgs e)
        {
            if(DialogResult.Yes == SimpleMessageBoxes.SentQuestionMessageBox("Выйти из приложения?"))
            {
                SimpleSound.Play(SimpleSound.Exit);
                Application.Exit();
            }
        }

        protected virtual void PanelPaint(object sender, EventArgs e) { }

        private void GotFileFromOpenFileDialog(object sender, CancelEventArgs e) { }

        protected virtual void TextBoxChanged(object sender, EventArgs e) { }

    }
}
