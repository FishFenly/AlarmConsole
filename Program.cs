using System;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Wave;

namespace AlarmConsole
{
    class Program
    {
        public static bool runTimer = true;
        public static WaveOutEvent player;
        public static WaveFileReader wavFile;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void BringConsoleToFront()
        {
            SetForegroundWindow(GetConsoleWindow()); 
        }
        public static void Alarm()
        {
            BringConsoleToFront();
            try 
            {
                for (int i = 0; i < 3; i++)
                {
                    player = new WaveOutEvent();
                    wavFile = new WaveFileReader(@"C:\WINDOWS\Media\Alarm01.wav");           
                    player.Init(wavFile);
                    player.Play();
                    Thread.Sleep(6000);
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }          
            // Pause the program for a minute so that it doesnt keep looping         
            Thread.Sleep(60000);
        }
        public static void RunTimer()
        {
            DateTime currentTime = DateTime.Now;
            DateTime dinnerTime = DateTime.Today.AddHours(13);
            DateTime homeTime = DateTime.Today.AddHours(15).AddMinutes(45);

            if (currentTime.ToShortTimeString()  == dinnerTime.ToShortTimeString())
            {
                Console.WriteLine("Its dinner time");
                Alarm();
            }

            if (currentTime.ToShortTimeString()  == homeTime.ToShortTimeString())
            {
                Console.WriteLine("Its home time");
                Alarm();
            }
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(50,10);
            Console.WriteLine("-#- Joseph's alarm app -#-");
            Console.WriteLine("");
            Console.WriteLine("Dinner time alarm set for 13:00");
            Console.WriteLine("Home time set for 15:45");
            while (runTimer)
            { 
                RunTimer();
            }
        }
    }
}