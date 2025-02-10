using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApsMotionControl.FThread
{
    public class DIoThread
    {
        private Thread thread;
        public bool threadDIoRun = false;
        public DIoThread()
        {
            thread = new Thread(Run);
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
            threadDIoRun = false;
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    thread.Abort();
                }
            }
        }
        public void Close()
        {
            Stop();
        }
        public void Run()
        {
            try
            {
                threadDIoRun = true;
                while (threadDIoRun)
                {
                    Globalo.dIoControl.ReadDWordIn(0);
                    Thread.Sleep(10);
                }
            }
            catch (ThreadInterruptedException err)
            {
                // Debug.WriteLine(err);
                Globalo.LogPrint("DIoThread", err.ToString());
            }
            finally
            {
                //Debug.WriteLine("time 리소스 지우기");
            }
        }
    }
}
