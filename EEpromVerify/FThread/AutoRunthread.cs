using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApsMotionControl.FThread
{
    public class AutoRunthread : BaseThread
    {
        //public event delLogSender eLogSender;       //외부에서 호출할때 사용

        private Process.PcbProcess RunProcess = new Process.PcbProcess();
        private Process.ReadyProcess readyProcess = new Process.ReadyProcess();
        public AutoRunthread()
        {
            //thread = null;
        }
        protected override void ThreadInit()
        {

        }
        protected override void ThreadRun()
        {
            if (Globalo.taskWork.m_nCurrentStep >= Globalo.taskWork.m_nStartStep && Globalo.taskWork.m_nCurrentStep < Globalo.taskWork.m_nEndStep)
            {
                if (ProgramState.CurrentState == OperationState.Stopped)
                {
                    //Stop Auto Process
                    Globalo.MainForm.StopAutoProcess();
                    return;
                }
                //// 원점 복귀
                //if (Globalo.taskWork.m_nCurrentStep >= 10000 && Globalo.taskWork.m_nCurrentStep < 20000)
                //{
                //    Globalo.taskWork.m_nCurrentStep = readyProcess.HomeProcess(Globalo.taskWork.m_nCurrentStep);
                //}

                if (Globalo.taskWork.m_nCurrentStep >= 20000 && Globalo.taskWork.m_nCurrentStep < 30000)
                {
                    Globalo.taskWork.m_nCurrentStep = readyProcess.AutoReady(Globalo.taskWork.m_nCurrentStep, cts.Token);
                }
                else if (Globalo.taskWork.m_nCurrentStep >= 30000 && Globalo.taskWork.m_nCurrentStep < 40000)
                {
                    Globalo.taskWork.m_nCurrentStep = RunProcess.Auto_Loading(Globalo.taskWork.m_nCurrentStep);
                }

            }
            else if (Globalo.taskWork.m_nCurrentStep == -1)
            {
                //Globalo.MainForm.StopAutoProcess();
                cts.Cancel();
            }
            else if (Globalo.taskWork.m_nCurrentStep < 0)
            {
                Globalo.MainForm.PauseAutoProcess();
            }
            else
            {
                cts.Cancel();
            }
        }

    }
}
