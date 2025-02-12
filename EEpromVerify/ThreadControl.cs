using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsMotionControl
{
    public class ThreadControl
    {
        public FThread.LogThread logThread;
        public FThread.TimeThread timeThread;
        public FThread.DIoThread dIoThread;
        public FThread.AutoRunthread autoRunthread;

        //CCD thread
        public FThread.CcdColorThread ccdColorThread;
        public FThread.CcdGrabThread ccdGrabThread;

        //public FThread.AutoReadyThread readyRunthread;

        //public FThread.ReadyThread readyThread;


        public FThread.TaskAutoRun taskAutoRun;

        public ThreadControl()
        {
            logThread = new FThread.LogThread();
            timeThread = new FThread.TimeThread();
            autoRunthread = new FThread.AutoRunthread();
            ccdColorThread = new FThread.CcdColorThread();
            ccdGrabThread = new FThread.CcdGrabThread();

            //readyThread = new FThread.ReadyThread();
            if (ProgramState.ON_LINE_MOTOR)
            {
                dIoThread = new FThread.DIoThread();
            }
            taskAutoRun = new FThread.TaskAutoRun();
        }
        public void AllThreadStart()
        {
            logThread.Start();
            timeThread.Start();
            if (ProgramState.ON_LINE_MOTOR)
            {
                dIoThread.Start();
            }
        }
        public void AllClose()
        {
            logThread.Stop();
            timeThread.Stop();
            autoRunthread.Stop();
            ccdColorThread.Stop();
            ccdGrabThread.Stop();

            //readyThread.Stop();
            if (ProgramState.ON_LINE_MOTOR)
            {
                dIoThread.Stop();
            }

            taskAutoRun.Stop();
        }
    }
}
