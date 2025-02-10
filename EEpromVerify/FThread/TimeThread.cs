using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApsMotionControl.FThread
{
    public class TimeThread
    {
        private Label timeLabel;
        private Thread thread;
        public bool threadTimeRun = false;

        public TimeThread()
        {
            this.timeLabel = Globalo.MainForm.TimeLabel;
            thread = new Thread(Run);
        }
        public void Run()
        {
            try
            {
                DateTime dTime;
                string sTime = "";
                threadTimeRun = true;
                while (threadTimeRun)
                {
                    dTime = DateTime.Now;
                    //sTime = $"{dTime:hh:mm:ss.fff}";
                    sTime = $"{dTime:hh : mm : ss}";
                    if (this.timeLabel.InvokeRequired)
                    {
                        //timeLabel.Invoke(new TimeTextCallback(setTimeLabel), sTime);        //<--사용가능 #1
                        this.timeLabel.Invoke(new MethodInvoker(delegate { setTimeLabel(sTime); }));
                        //TimeLabel.Invoke(new MethodInvoker(delegate { TimeLabel.Text = sTime; }));//<--사용가능 #2

                    }

                    Thread.Sleep(100);
                }
            }
            catch (ThreadInterruptedException err)
            {
                // Debug.WriteLine(err);
                Globalo.LogPrint("TimeThread", err.ToString());
            }
            finally
            {
                //Debug.WriteLine("time 리소스 지우기");
            }
        }
        public void setTimeLabel(string sTime)
        {
            timeLabel.Text = sTime;
        }
        public void Start()
        {
            if (thread != null)
            {
                thread.Start();
            }
        }
        public void Stop()
        {
            threadTimeRun = false;
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    thread.Abort();
                }
            }
        }
    }
}
