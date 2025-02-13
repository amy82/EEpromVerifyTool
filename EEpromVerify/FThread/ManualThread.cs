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
        private readonly Action _runAction;
        protected override void ManulRun(Action runAction)
        {
            while (!cts.Token.IsCancellationRequested)
            {
                Console.WriteLine("ManualThread ProcessRun Run.");

                _runAction?.Invoke();

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


                cts.Cancel();

            }

            cts = null;
            Console.WriteLine("ManualThread stopped safely.");
        }
    }
}
