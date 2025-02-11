using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApsMotionControl.FThread
{
    public class AutoRunthread
    {
        public event delLogSender eLogSender;       //외부에서 호출할때 사용
        private Thread thread;
        private bool m_bPause = false;
        private CancellationTokenSource cts;
        private Process.PcbProcess RunProcess = new Process.PcbProcess();
        public AutoRunthread()
        {
            //thread = new Thread(Run);
            thread = null;
        }
        public bool GetThreadRun()
        {
            if(thread != null)
            {
                return thread.IsAlive;    //thread 동작 중

                ////return thread?.IsAlive ?? false;
            }

            return false;
        }
        public bool GetThreadPause()
        {
            return m_bPause;
        }
        public void ProcessRun(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                //Console.WriteLine("Worker thread running...");
                if (m_bPause)
                {
                    continue;
                }
                if (Globalo.taskWork.m_nCurrentStep >= Globalo.taskWork.m_nStartStep && Globalo.taskWork.m_nCurrentStep < Globalo.taskWork.m_nEndStep)
                {
                    if (ProgramState.CurrentState == OperationState.Stopped)
                    {
                        //Stop Auto Process
                        Globalo.MainForm.StopAutoProcess();
                        return;
                    }
                    if (Globalo.taskWork.m_nCurrentStep >= 20000 && Globalo.taskWork.m_nCurrentStep < 30000)
                    {
                        Globalo.taskWork.m_nCurrentStep = RunProcess.AutoReady(Globalo.taskWork.m_nCurrentStep);
                    }

                }
                else if (Globalo.taskWork.m_nCurrentStep == -1)
                {
                    Globalo.MainForm.StopAutoProcess();
                }
                else if (Globalo.taskWork.m_nCurrentStep < 0)
                {
                    Globalo.MainForm.PauseAutoProcess();
                }
                else
                {
                    Stop();
                }
                Thread.Sleep(10);
            }

            Console.WriteLine("Worker thread stopped safely.");
        }
        public bool Start()
        {
            if (thread != null && thread.IsAlive)
            {
                eLogSender("AutoRunthread", $"[ERR] 자동 운전 중입니다.");
                return false;
            }
            m_bPause = false;
            try
            {

                //thread = new Thread(ProcessRun);
                cts = new CancellationTokenSource();
                thread = new Thread(() => ProcessRun(cts.Token));
                thread.Start();

                Console.WriteLine("Worker thread start.");
            }
            catch (ThreadStateException ex)
            {
                // Console.WriteLine($"ThreadStateException: {ex.Message}");
                eLogSender("AutoRunthread", $"[ERR] ThreadStateException: {ex.Message}");
                return false;
            }

            return true;
        }
        public void Stop()
        {
            if (thread != null && cts != null)
            {
                cts.Cancel();
                thread.Join();
                thread = null;
                cts = null;
                Console.WriteLine("Worker thread stop.");
            }
        }
    }
}
