using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace OpenGOVideo
{
    static class Program
    {

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
                // Ensure that only one instance is running at a time!
                bool createdNew = true;
                using (Mutex mutex = new Mutex(true, "MyApplicationName", out createdNew))

                    if (createdNew)
                    {
                        // VS put this stuff here by default, so we'll just keep it
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Main());


                        //close preview
                        if (Meeting.Current.Job.DeviceSources.Count > 0)
                            Meeting.Current.Job.DeviceSources[0].PreviewWindow = null;
                        Meeting.Current.Job.StopEncoding();

                        Process.GetCurrentProcess().Kill();
                    }
                    else
                    {
                        Process current = Process.GetCurrentProcess();
                        foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                        {
                            if (process.Id != current.Id)
                            {
                                SetForegroundWindow(process.MainWindowHandle);
                                break;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                
                
            }
              
        
        }
    }
}
