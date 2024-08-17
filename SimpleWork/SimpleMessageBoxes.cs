using NewPetProjectC_.SimpleWork;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewPetProjectC_
{
    static class SimpleMessageBoxes
    {
        public static void SentFatalErrorMessageBox(string exMessage)
        {
            SimpleSound.Play(SimpleSound.FatalError);       
            MessageBox.Show($"Приложение не будет запущено по причине: {exMessage}\nПриложение будет закрыто",
                    "Фатальная ошибка", MessageBoxButtons.OK);
            Application.Exit();
        }
        
        public static void SentErrorMessageBox(string exMessage)
        {
            SimpleSound.Play(SimpleSound.Error);
            DialogResult erResult = MessageBox.Show($"{exMessage} Желаете выйти из приложения?", "Исключение", MessageBoxButtons.YesNo);
            if (erResult == DialogResult.Yes) Application.Exit();
        }
        public static void SentSuccessMessageBox(string message) 
        {
            SimpleSound.Play(SimpleSound.Success);
            MessageBox.Show(message, "Успешно");
        }

        public static void SentSuccess2MessageBox(string message)
        {
            SimpleSound.Play(SimpleSound.Success_2);
            MessageBox.Show(message, "Успешно");
        }

        public static void SentSuccess3MessageBox(string message)
        {
            SimpleSound.Play(SimpleSound.Success_3);
            MessageBox.Show(message, "Успешно");
        }

        public static void SentBlockMessageBox(string message)
        {
            SimpleSound.Play(SimpleSound.Block);
            MessageBox.Show(message, "Ай-ай-ай, нельзя");
        }

        public static DialogResult SentQuestionMessageBox(string message) 
        {
            SimpleSound.Play(SimpleSound.Question);
            return MessageBox.Show(message, "Вы уверены?", MessageBoxButtons.YesNo);
        } 
        public async static Task SentTimeUpMessageBox(string time, string task) 
        {
            SimpleSound.Play(SimpleSound.TimesUp);
            await Task.Run(() => MessageBox.Show($"Время {time}.\nПора начинать {task}.", "График"));
        } 
    }
}
