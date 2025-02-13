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
        
        public FThread.AutoRunthread autoRunthread;

        //CCD thread
        public FThread.CcdColorThread ccdColorThread;
        public FThread.CcdGrabThread ccdGrabThread;

        //ON_LINE_MOTOR
        public FThread.DIoThread dIoThread;

        public FThread.ManualThread manualThread;

        public ThreadControl()
        {
            logThread = new FThread.LogThread();
            timeThread = new FThread.TimeThread();
            autoRunthread = new FThread.AutoRunthread();

            if (ProgramState.ON_LINE_MIL)
            {
                ccdColorThread = new FThread.CcdColorThread();
                ccdGrabThread = new FThread.CcdGrabThread();
            }
            if (ProgramState.ON_LINE_MOTOR)
            {
                dIoThread = new FThread.DIoThread();
            }

            manualThread = new FThread.ManualThread();
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
            if (ProgramState.ON_LINE_MIL)
            {
                ccdColorThread.Stop();
                ccdGrabThread.Stop();
            }
           
            if (ProgramState.ON_LINE_MOTOR)
            {
                dIoThread.Stop();
            }
            manualThread.Stop();
        }
    }
}
