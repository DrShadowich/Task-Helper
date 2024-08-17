using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using NewPetProjectC_.SimpleWork;
using System.Linq;
namespace NewPetProjectC_
{
    public partial class MainWindow : Form
    {
        private EntityContext _context = null;
        public bool CheckListOfStrings(List<string> strings, string checkingString)
        {
            foreach (string i in strings) if (i == checkingString) return false;
            return true;
        }

        public MainWindow(EntityContext context) 
        {
            InitializeComponent();
            _context = context;
        } 
        
        private void MainWindowLoad(object sender, EventArgs e)
        {
            SimpleSound.Play(SimpleSound.Start);

            SimpleTabPage timeTabPage = new SimpleTabPage(ref tabControl, _context, true, false, true);
            List<string> tempString = new List<string>();

            foreach (var task in _context.BasicTasks.ToList())
            {
                if (CheckListOfStrings(tempString, task.Theme))
                {
                    SimpleTabPage tabPage = new SimpleTabPage(ref tabControl, OpenFileDialog, _context, task.Theme, false, true);
                    tempString.Add(task.Theme);
                }
                else continue;
            }

            SimpleTabPage newTabPage = new SimpleTabPage(ref tabControl, OpenFileDialog, _context, "Новая тема задач", true);
        }

        private void GotFileFromOpenFileDialog(object sender, CancelEventArgs e) { }
        private void ExitButtonClick(object sender, EventArgs e) { }
        private void FirstTabPageClick(object sender, EventArgs e) { }

    }
}
