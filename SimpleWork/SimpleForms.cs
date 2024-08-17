using System.Drawing;
using System.Windows.Forms;

namespace NewPetProjectC_
{
    class SimpleForms
    {
        public static void SetButtonText(Button button, string afterButtonString) => button.Text = afterButtonString;

        public static void ScrollFix(ref TabPage tabPage, int tabPageScrollValue)
        {
            if (tabPage.AutoScroll)
            {
                if (tabPage.VerticalScroll.Value == 0) tabPage.VerticalScroll.Value = tabPageScrollValue;
                else tabPageScrollValue = tabPage.VerticalScroll.Value;
            }
        }
        public static void InitFlowLayoutPanelString(ref FlowLayoutPanel flowLayoutPanel, DockStyle dock)
        {
            flowLayoutPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = dock,
                Location = new Point(0, 450)
            };

        }

        public static void InitPanel(ref Panel panel)
        {
            panel = new Panel()
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                Location = new Point(3, 3),
                Name = "Panel",
                Size = new Size(564, 418),
                TabIndex = 0,
            };
        }

        public static void CheckMinute(ref string time)
        {
            switch (time)
            {
                case "0":
                    time = "00";
                    break;
                case "1":
                    time = "01";
                    break;
                case "2":
                    time = "02";
                    break;
                case "3":
                    time = "03";
                    break;
                case "4":
                    time = "04";
                    break;
                case "5":
                    time = "05";
                    break;
                case "6":
                    time = "06";
                    break;
                case "7":
                    time = "07";
                    break;
                case "8":
                    time = "08";
                    break;
                case "9":
                    time = "09";
                    break;
                default:
                    break;
            }
        }
    }
}
