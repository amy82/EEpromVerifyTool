using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApsMotionControl.FThread
{
    public class DIoThread : BaseThread
    {
        public DIoThread()
        {
        }
        protected override void ProcessRun()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                Globalo.dIoControl.ReadDWordIn(0);
                Thread.Sleep(10);
            }
        }
    }
}
