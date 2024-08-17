using NewPetProjectC_.TasksEntity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewPetProjectC_
{
    class TimePanel : SimpleTaskPanel
    {

        private DateTime _date;

        public TimePanel(ref TabPage thisTabPage, EntityContext context) : base(context)
        {
            _thisTabPage = thisTabPage;
            InitComponents();
            CheckTimeTable();
        }


        protected override void InitComponents()
        {
            base.InitComponents();
            _newButton.Text = "New Time - Task";
            _textBox.Text = "Напишите здесь новую задачу и время, например: 12:00 Кушать";
        }

        private void CreateTimeLabelsAndButtonString(string task, string time, bool @checked)
        {
            _panel.Controls.Remove(_superDeleteButton);
            TimeLabelsAndButtonString labelsAndButtonString = new TimeLabelsAndButtonString
                (
                ref _panel,
                _context,
                task,
                time,
                @checked
                );
            labelsAndButtonString.CreateMethods();
            _panel.Controls.Add(_superDeleteButton);
        }
        public override void LoadLabelAndButtonsStrings()
        {
            try
            {
                string task = string.Empty;
                string time = string.Empty;
                bool @checked = true;

                foreach (var timeTask in _context.TimeTasks.ToList())
                {
                    task = timeTask.Task;
                    time = timeTask.Time;

                    string[] tempTime = time.Split(':');
                    SimpleForms.CheckMinute(ref tempTime[1]);
                    time = tempTime[0] + ":" + tempTime[1];

                    @checked = timeTask.Checked;
                    CreateTimeLabelsAndButtonString(task, time, @checked);
                }
            }
            catch (Exception ex)
            {
                SimpleMessageBoxes.SentFatalErrorMessageBox(ex.Message);
            }
        }

        protected override void NewButtonClick(object sender, EventArgs e)
        {
            string[] tempString = _textBox.Text.Split(' ');

            if (tempString.Length > 2) 
            {
                SimpleMessageBoxes.SentBlockMessageBox($"Неверный ввод строки. Попробуйте убрать {tempString[2]}");
                return;
            } 

            string time = string.Empty;
            string task = string.Empty;

            try
            {
                string[] tempTime = tempString[0].Split(':');
                _date = new DateTime(2024, 12, 10, int.Parse(tempTime[0]), int.Parse(tempTime[1]), 0);
                task = tempString[1];

                time = $"{_date.Hour}:{_date.Minute}";

                SimpleForms.CheckMinute(ref tempTime[1]);
                time = tempTime[0] + ":" + tempTime[1];
                _context.TimeTasks.Add(new TimeEntity() { Task = task, Time = $"{tempTime[0]}:{tempTime[1]}" });
                _context.SaveChanges();

                CreateTimeLabelsAndButtonString(task, time, true);
            }
            catch (Exception ex)
            {
                SimpleMessageBoxes.SentBlockMessageBox($"Время не записалось по причине: {ex.Message}\nПопробуйте ввести строку правильно, например: 15:00 Спать.");
            }
        }


        public async Task CheckTimeTable()
        {
            async void LocalCheckTime()
            {  
                while (true)
                {
                    using(var contextAsync = new EntityContext())
                    {
                        foreach (var timeTask in contextAsync.TimeTasks.ToList())
                        {
                            DateTime nowDate = DateTime.Now;
                            string dateTime = $"{nowDate.Hour}:{nowDate.Minute}";
                            string dataTime = timeTask.Time;

                            if (dateTime == dataTime)
                            {
                                SimpleMessageBoxes.SentTimeUpMessageBox(dateTime, timeTask.Time);
                                await Task.Delay(60000);
                            }
                        }
                    }
                }
            }

            await Task.Run(() => LocalCheckTime());
        }
        protected override void SuperDeleteButtonClick(object sender, EventArgs e)
        {
            if(DialogResult.Yes == SimpleMessageBoxes.SentQuestionMessageBox("Вы уверены удалить все данные у графика?\nВсё расписание будет стёрто."))
            {
                foreach(var timeTask in _context.TimeTasks.ToList()) _context.TimeTasks.Remove(timeTask);
                _context.SaveChanges();
                _panel.Dispose();
                InitComponents();
                SimpleMessageBoxes.SentSuccess2MessageBox("Страница полностью удалена");
            }
        }

        private void TabPageClick(object sender, EventArgs e) { }


    }
}
