using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApsMotionControl.FThread
{
    public class ManualThread : BaseThread
    {
        private int nType = -1;
        public void runfn(int _nType = -1)
        {
            nType = _nType;
            base.Start();
        }
        protected override void ThreadInit()
        {

        }
        protected override void ThreadRun()
        {
            Console.WriteLine("추가 작업 실행 중...");
            if (nType == -1)
            {
                Console.WriteLine("ManualThread ProcessRun out.");
                cts.Cancel();
            }
            Console.WriteLine("ManualThread ProcessRun Run.");
            if (nType == 1)
            {
                //---------------------------------------------------------------------------------------
                //Thread Fn Run
                //
                Globalo.mCCdPanel.EEpromRead();


                //---------------------------------------------------------------------------------------

            }
            if (nType == 2)
            {
                Globalo.mLaonGrabberClass.OpenDevice();
            }
            if (nType == 3)
            {
                Globalo.mLaonGrabberClass.CloseDevice();
            }

            cts.Cancel();
            Console.WriteLine("ManualThread stopped safely.");
        }
        //protected override void ProcessRun()
        //{
           // while (!cts.Token.IsCancellationRequested)
            //{
                //if(nType == -1)
                //{
                //    Console.WriteLine("ManualThread ProcessRun out.");
                //    cts.Cancel();
                //    break;
                //}
                //Console.WriteLine("ManualThread ProcessRun Run.");
                //if(nType == 1)
                //{
                //    Globalo.mCCdPanel.EEpromRead();
                //}

                //for (int i = 0; i < 50; i++)  // 긴 반복문
                //{
                //    if (token.IsCancellationRequested)  // 중간에 취소 요청 확인
                //    {
                //        Console.WriteLine($"ManualThread token.IsCancellationRequested");
                //        break;
                //    }

                //    Console.WriteLine($"ManualThread Processing {i}");
                //    Thread.Sleep(300); // 작업 시간 가정
                //}


                //cts.Cancel();

            //}

            //nType = -1;
            //Console.WriteLine("ManualThread stopped safely.");
        //}
    }
}
