using System.Windows.Forms;

namespace NewPetProjectC_
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.NewTaskButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.FirstTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.FirstTabPage = new System.Windows.Forms.TabPage();
            this.SecondTabPage = new System.Windows.Forms.TabPage();
            this.SecondPanel = new System.Windows.Forms.Panel();
            this.SecondTextBox = new System.Windows.Forms.TextBox();
            this.TimeButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.SecondTabPage.SuspendLayout();
            this.SecondPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog";
            this.OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.GotFileFromOpenFileDialog);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(578, 450);
            this.tabControl.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 450);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Task Helper";
            this.Load += new System.EventHandler(this.MainWindowLoad);
            this.tabControl.ResumeLayout(false);
            this.SecondTabPage.ResumeLayout(false);
            this.SecondPanel.ResumeLayout(false);
            this.SecondPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private Button NewTaskButton;
        private TextBox FirstTextBox;
        protected Button ExitButton;
        private ContextMenuStrip contextMenuStrip1;
        private TabPage FirstTabPage;
        private TabPage SecondTabPage;
        protected Button button2;
        private TabControl tabControl;
        private Panel SecondPanel;
        private TextBox SecondTextBox;
        private Button TimeButton;
    }
}

