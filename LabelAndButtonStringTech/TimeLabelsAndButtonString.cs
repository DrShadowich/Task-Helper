using NewPetProjectC_.SimpleWork;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NewPetProjectC_
{
    class TimeLabelsAndButtonString : ILabelAndButtonString
    {
        private Label _timeLabel = null;
        private Label _taskLabel = null;
        private Button _deleteButton = null;
        private CheckBox _checkBox = null;
        private FlowLayoutPanel _flowLayoutPanel = null;
        private Panel _panel = null;
        private bool _checked = true;

        private readonly EntityContext _context = null;

        private readonly string _timeText = string.Empty;
        private readonly string _taskText = string.Empty;

        public CheckBox CheckBox { get { return _checkBox; } }
        
        public TimeLabelsAndButtonString
            (
            ref Panel panel,
            EntityContext context,
            string taskText,
            string timeText,
            bool Checked
            )
        {
            _panel = panel;
            _taskText = taskText;
            _timeText = timeText;
            _context = context;
            _checked = Checked;
            InitLabelAndButtons();
        }

        public void InitLabelAndButtons()
        {
            _timeLabel = new Label() 
            {
                Text = _timeText,
                Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(60, 20)
            };
            _taskLabel = new Label()
            {
                Text = _taskText,
                Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(180, 20)
            };
            _deleteButton = new Button()
            {
                Text = "Удалить"
            };

            _checkBox = new CheckBox();

            if(_checked == true) _checkBox.Checked = true;
            else _checkBox.Checked = false;

            SimpleForms.InitFlowLayoutPanelString(ref _flowLayoutPanel, DockStyle.Bottom);

            _flowLayoutPanel.Controls.Add(_timeLabel);
            _flowLayoutPanel.Controls.Add(_taskLabel);
            _flowLayoutPanel.Controls.Add(_deleteButton);
            _flowLayoutPanel.Controls.Add(_checkBox);

            _panel.Controls.Add(_flowLayoutPanel);
        }

        public void CreateMethods()
        {  
            _deleteButton.Click += new EventHandler(DeleteButtonClick);
            _checkBox.Click += new EventHandler(CheckBoxClick);
        }

        void DeleteButtonClick(object sender, EventArgs e)
        {
            if (SimpleMessageBoxes.SentQuestionMessageBox($"Вы уверены удалить: {_timeText} {_taskText}?") == DialogResult.Yes)
            {
                foreach(var timeTask in _context.TimeTasks.ToList())
                {
                    if(timeTask.Task == _taskText && timeTask.Time == _timeText)
                    {
                        _context.TimeTasks.Remove(timeTask);
                        _flowLayoutPanel.Dispose();
                        _context.SaveChanges();
                    }
                }
            }
        }

        void CheckBoxClick(object sender, EventArgs e)
        {
            if (_checkBox.Checked) _checkBox.Checked = false;
            else _checkBox.Checked = true;
            if (_checkBox.Checked)
            {
                if (SimpleMessageBoxes.SentQuestionMessageBox($"Выключить {_timeText} {_taskText}?") == DialogResult.Yes)
                {
                    _checkBox.Checked = false;
                    foreach(var timeTask in _context.TimeTasks.ToList())
                    {
                        if(timeTask.Task == _taskText && timeTask.Time == _timeText)
                        {
                            timeTask.Checked = false;
                            _context.SaveChanges();
                        }
                    }
                }
            }
            else if (!_checkBox.Checked)
            {
                if (SimpleMessageBoxes.SentQuestionMessageBox($"Включить обратно {_timeText} {_taskText}?") == DialogResult.Yes)
                {
                    _checkBox.Checked = true;
                    foreach (var timeTask in _context.TimeTasks.ToList())
                    {
                        if (timeTask.Task == _taskText && timeTask.Time == _timeText)
                        {
                            timeTask.Checked = true;
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
