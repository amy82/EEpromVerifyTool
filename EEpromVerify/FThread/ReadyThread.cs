using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApsMotionControl.FThread
{
    public class ReadyThread
    {
        public event delLogSender eLogSender;       //외부에서 호출할때 사용
        private Thread thread;
        private bool threadRun = false;
        private bool m_bPause = false;
        private Process.PcbProcess PcbProcess = new Process.PcbProcess();
        public ReadyThread()
        {
            
        }

        public void Run()
        {
            while (threadRun)
            {
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
                        break;
                    }
                    // 원점 복귀
                    if (Globalo.taskWork.m_nCurrentStep >= 10000 && Globalo.taskWork.m_nCurrentStep < 20000)
                    {
                        Globalo.taskWork.m_nCurrentStep = PcbProcess.HomeProcess(Globalo.taskWork.m_nCurrentStep);
                    }
                    //	운전 준비
                    else if (Globalo.taskWork.m_nCurrentStep >= 20000 && Globalo.taskWork.m_nCurrentStep < 30000)
                    {
                        Globalo.taskWork.m_nCurrentStep = PcbProcess.AutoReady(Globalo.taskWork.m_nCurrentStep);
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
                        ProgramState.CurrentState = OperationState.Stopped;
                        Stop();
                    }

                }
                else
                {
                    ProgramState.CurrentState = OperationState.Stopped;
                    Stop();
                }
                Thread.Sleep(10);
            }
        }
        public bool GetThreadRun()
        {
            if (thread != null)
            {
                if (thread.IsAlive || threadRun)
                {
                    return true;
                }
            }
            return false;
        }
        public bool GetThreadPause()
        {
            if (m_bPause == true && thread.IsAlive == true)
            {
                return true;
            }
            return false;
        }
        public void Pause(bool bSetPause)
        {
            m_bPause = bSetPause;
        }
        public bool Start()
        {
            if (thread == null || thread.IsAlive == false)
            {
                thread = new Thread(Run);
            }

            if (thread != null)
            {
                
                try
                {
                    // 이미 시작된 스레드를 다시 시작하려고 하면 예외가 발생합니다.
                    threadRun = true;
                    m_bPause = false;
                    thread.Start();
                    return true;
                }
                catch (ThreadStateException ex)
                {
                    threadRun = false;
                    // Console.WriteLine($"ThreadStateException: {ex.Message}");
                    eLogSender("AutoRunthread", $"[ERR] ThreadStateException: {ex.Message}");
                    return false;
                }
            }
            return false;
        }
        public void Stop()
        {
            threadRun = false;
            m_bPause = false;
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    //thread.Abort();
                    if (thread.Join(1000))
                    {
                        eLogSender("AutoRunthread", $"[ERR] ReadyThread Join TimeEnd");
                    }
                    else
                    {
                        eLogSender("AutoRunthread", $"[ERR] ReadyThread Join TimeOver");
                    }
                }
            }
        }
    }
}
