using System;
using System.ComponentModel;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace NewPetProjectC_.SimpleWork
{
    static class SimpleSound
    {
        public static string Block = "Block";
        public static string Click = "Click";
        public static string Error = "Error";
        public static string Exit = "Exit";
        public static string FatalError = "FatalError";
        public static string Notify = "Notify";
        public static string Question = "Question";
        public static string SomethingAdd = "SomethingAdd";
        public static string Start = "Start";
        public static string Success = "Success";
        public static string Success_2 = "Success_2";
        public static string Success_3 = "Success_3";
        public static string TimesUp = "TimesUp";

        public static void Play(string nameWavFile)
        {
            try
            {
                SoundPlayer sound = new SoundPlayer($"Sounds\\{nameWavFile}.wav");
                sound.Play();
            }
            catch (Exception ex) 
            {
                SimpleMessageBoxes.SentFatalErrorMessageBox(ex.Message);
            }
        }        
    }
}
