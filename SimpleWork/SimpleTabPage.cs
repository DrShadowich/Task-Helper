using NewPetProjectC_.SimpleWork;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace NewPetProjectC_
{
    class SimpleTabPage
    {
        private TabPage _tabPage = null;
        private FlowLayoutPanel _flowLayoutPanel = null;
        private Panel _tempPanel = null;
        private TextBox _textBox = null;
        private Button _confirmButton = null;
        private OpenFileDialog _fileDialog = null;
        private TabControl _tabControl = null;
        private SimpleTaskPanel _panel = null;
        private string _sqlTheme = string.Empty;
        private EntityContext _context = null;
        private readonly bool _isHasSetting = false;
        private readonly bool _isFirst = false;
        private readonly bool _isTimePanel = false;


        public SimpleTabPage(ref TabControl tabControl, OpenFileDialog fileDialog, EntityContext context, string textName, bool isHasSetting)
        {
            _isHasSetting = isHasSetting;
            _tabPage = new TabPage() { Text = textName };
            _tabControl = tabControl;
            _tabControl.Controls.Add(_tabPage);
            _fileDialog = fileDialog;
            _context = context;
            InitTabPage();
        }
        public SimpleTabPage(ref TabControl tabControl, OpenFileDialog fileDialog, EntityContext context, string textName)
        {
            _tabPage = new TabPage() { Text = textName };
            tabControl.Controls.Add(_tabPage);
            _fileDialog = fileDialog;
            _context = context;
            InitTabPage();
        } 
        public SimpleTabPage(ref TabControl tabControl, OpenFileDialog fileDialog, EntityContext context, string textName, bool isHasSetting, bool isFirst)
        {
            _isFirst = isFirst;
            _isHasSetting = isHasSetting;
            _tabPage = new TabPage() { Text = textName };
            tabControl.Controls.Add(_tabPage);
            _fileDialog = fileDialog;
            _context = context;
            InitTabPage();
        }
        public SimpleTabPage(ref TabControl tabControl, EntityContext context, bool isFirst, bool isHasSetting, bool isTimePanel) 
        {
            _isFirst = isFirst;
            _isTimePanel = isTimePanel;
            _isHasSetting = isHasSetting;
            _tabPage = new TabPage() { Text = "График" };
            tabControl.Controls.Add(_tabPage);
            _context = context;
            InitTabPage();
        }

        private void InitTabPage()
        {
            if (_isHasSetting)
            {
                
                void PanelPaint(object sender, EventArgs e) { }
                void TextBoxChanged(object sender, EventArgs e) => _tabPage.Text = _textBox.Text;

                _tempPanel = new Panel()
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                };
                SimpleForms.InitFlowLayoutPanelString(ref _flowLayoutPanel, DockStyle.Top);
                _textBox = new TextBox()
                {
                    Text = "Название новой темы задач",
                    Size = new System.Drawing.Size(400, 40)
                };
                _confirmButton = new Button()
                {
                    Text = "Изменить имя",
                    Size = new System.Drawing.Size(125, 22)
                };

                _tempPanel.Paint += PanelPaint;
                _confirmButton.Click += TextBoxChanged;
                _confirmButton.Click += (object s, EventArgs e) => SimpleSound.Play(SimpleSound.Success_3);

                _flowLayoutPanel.Controls.Add(_textBox);
                _flowLayoutPanel.Controls.Add(_confirmButton);
                _tempPanel.Controls.Add(_flowLayoutPanel);
                _tabPage.Controls.Add(_tempPanel);

                CreateMethods();
            }
            else 
            {
                if(_isTimePanel == false) _sqlTheme = _tabPage.Text;
                ReplaceSettingsToWorkPage();
            }
        }

        private void CreateMethods()
        {
            void ConfirmButtonClick(object sender, EventArgs e) => ReplaceSettingsToWorkPage();
            _confirmButton.Click += ConfirmButtonClick;
            _tabControl.Click += (object s, EventArgs e) => SimpleSound.Play(SimpleSound.Click);
        }

        void ReplaceSettingsToWorkPage()
        {
            if (_sqlTheme == string.Empty && _isTimePanel == false) _sqlTheme = _textBox?.Text;
            _confirmButton?.Dispose();
            _flowLayoutPanel?.Dispose();
            _tempPanel?.Dispose();
            if (_isTimePanel == false)
            {
                _panel = new SimpleTaskPanel(ref _tabPage, _fileDialog, _sqlTheme, _context);
                _panel.LoadLabelAndButtonsStrings();
            }
            else
            {
                _panel = new TimePanel(ref _tabPage, _context);
                _panel.LoadLabelAndButtonsStrings();
            }
            if (_isFirst == false)
            {
                SimpleTabPage newTabPage = new SimpleTabPage(ref _tabControl, _fileDialog, _context, "Новая тема задач", true);
            }
        }
    }
}
