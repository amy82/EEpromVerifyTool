using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ApsMotionControl.FThread
{
    public class LogThread : BaseThread
    {
        private ListBox logListBox;
        //private Thread thread;
        //public bool threadLogRun = false;

        public DirectoryInfo dif = new DirectoryInfo(@"C:\logg"); // 디렉토리 경로
        public string Fpath = @"LOG.txt"; // 파일 경로 


        public Queue<string> logQueue = new Queue<string>();

        private readonly object writeLock = new object();

        public LogThread()
        {
            this.logListBox = Globalo.MainForm.listBox_Log;
        }


        protected override void ProcessRun()
        {
            //threadLogRun = true;
            if (!dif.Exists) // 디렉토리 체크
            {
                dif.Create();
            }
            DateTime dTime = DateTime.Now;
            Fpath = dif + "\\" + DateTime.Now.ToString("yyyy/MM/dd") + "_ApsMotionLog.txt";

            FileInfo file = new FileInfo(Fpath); // 파일 체크 후 생성
            if (!file.Exists)
            {
                FileStream fs = file.Create();
                fs.Close();
            }
            lock (writeLock)
            {
                //while (threadLogRun)
                while (!cts.Token.IsCancellationRequested)
                {
                    if (logQueue.Count > 0)
                    {
                        using (StreamWriter fw = new StreamWriter(Fpath, append: true))
                        {
                            string LogInfo = logQueue.Dequeue();// + "\n";

                            if (this.logListBox != null)
                            {
                                if (this.logListBox.InvokeRequired)
                                {
                                    this.logListBox.Invoke(new MethodInvoker(delegate { this.logListBox.Items.Add(LogInfo); }));
                                    this.logListBox.Invoke(new MethodInvoker(delegate { this.logListBox.SelectedIndex = this.logListBox.Items.Count - 1; }));

                                }
                                else
                                {
                                    this.logListBox.Items.Add(LogInfo);
                                }
                            }
                            fw.WriteLine(LogInfo);
                        }
                    }
                    Thread.Sleep(10);
                }
            }
        }

        
    }
}
