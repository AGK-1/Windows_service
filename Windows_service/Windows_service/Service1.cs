using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Runtime.InteropServices;

namespace Windows_service
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
      
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartCalculator();
            WriteToFile("Service is started " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Enabled = true;
            timer.Interval = 1000;

        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            StartCalculator();
            WriteToFile("Service is recall " + DateTime.Now);        
        }

        private void WriteToFile(string Message)
        {
            
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if(!Directory.Exists(path))
             {
                Directory.CreateDirectory(path);
             }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServicesLogs"+DateTime.Now.Date.ToShortDateString().Replace('/','_') + ".txt";
            if(File.Exists(filepath)) 
            { 
                using (StreamWriter sw = File.CreateText(filepath)) 
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped " + DateTime.Now);
        }

        private async void  StartCalculator()
        {
            string filepath = @"C:\Users\wikto\OneDrive\Desktop\1.txt";
            if (File.Exists(filepath))
            {
                while (true)
                {
                    // Append text to the file
                    File.AppendAllText(filepath, "Windows Service 1989 " + DateTime.Now);

                    // Wait for 5 seconds
                    await Task.Delay(5000);
                }
            }
            else
            {
                // Create the file if it does not exist
                File.WriteAllText(filepath, "Windows Service 1989 " + DateTime.Now);
            }

            // Optional delay before finishing the program
            await Task.Delay(5000);
            //while (true)
            //{
            //    File.AppendAllText(@"C:\Users\wikto\OneDrive\Desktop\1.txt", "Windows Serviice 1989");
            //    await Task.Delay(5000); 
            //};
        }
    }
}
