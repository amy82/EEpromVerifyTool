using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApsMotionControl.FThread
{
    public class BaseThread
    {
        protected Thread thread;
        protected bool m_bPause = false;
        protected CancellationTokenSource cts;
        public event Action<bool> ThreadCompleted; // 쓰레드 종료 이벤트
        private bool _result;

        public int threadCount = 0;

        public BaseThread()
        {
            thread = null;
            cts = null;
        }

        protected virtual void ThreadInit() { }
        protected virtual void ThreadRun(){ }

        private void ProcessRun()
        {
            try
            {
                ThreadInit();
                while (!cts.Token.IsCancellationRequested)
                {
                    if (m_bPause == false)
                    {
                        ThreadRun();
                    }
                    Thread.Sleep(Globalo.BASE_THREAD_INTERVAL);
                }
                
                _result = true;
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("Thread - caught ThreadAbortException - resetting.");
                Console.WriteLine("Exception message: {0}", e.Message);
                _result = false;
            }
            finally
            {
                cts = null;
                thread = null;
                Console.WriteLine("ProcessRun mesfinallysage");
            }


            Console.WriteLine("Thread 종료됨.");
            ThreadCompleted?.Invoke(_result); // 쓰레드 종료 시 이벤트 호출
        }
        protected virtual void ManulRun(Action runAction)
        {
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (m_bPause) continue;


                    Thread.Sleep(10);
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("Thread - caught ThreadAbortException - resetting.");
                Console.WriteLine("Exception message: {0}", e.Message);
            }
            finally
            {
                Console.WriteLine("ProcessRun mesfinallysage");

            }


            Console.WriteLine("Thread 종료됨.");
        }


        public void Pause()
        {
            m_bPause = true;
        }
        
        
        public bool Start()
        {
            try
            {
                if (thread == null)
                {

                    cts = null;
                    cts = new CancellationTokenSource();
                    Console.WriteLine("Thread Start #1.");
                    threadCount++;
                    thread = new Thread(() => ProcessRun());//cts.Token));
                    thread.Start();
                    Console.WriteLine("Thread Start #2.");
                }
                else
                {
                    if (m_bPause == false)
                    {
                        if (thread.IsAlive)
                        {
                            Console.WriteLine($"thread.IsAlive {thread.IsAlive}");
                            return false;
                        }
                        else if (thread != null)
                        {
                            Console.WriteLine($"thread not null");
                            return false;
                        }
                        else
                        {
                            //운전준비 완료하고 thread 가 null 이 아니고 , IsAlive는 false 일때
                            bool Rtn = thread.Join(100);  // 쓰레드가 종료될 때까지 기다림
                            if (Rtn)
                            {
                                cts = null;
                                cts = new CancellationTokenSource();
                                thread = null;  // 종료 후 thread를 null로 설정
                                thread = new Thread(() => ProcessRun());// cts.Token));
                                thread.Start();
                            }
                            else
                            {
                                Abort();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        Resume();   // 정지 상태일 때만 실행
                        return true;
                    }
                }
                
                
            }
            catch (ThreadStateException ex)
            {
                Console.WriteLine($"[ERR] ThreadStateException: {ex.Message}");
                return false;
            }
            return true;
        }
        public void Stop()
        {
            if (thread != null && cts != null)
            {
                Console.WriteLine("Base Thread Stop() #1");
                
                cts.Cancel();
                m_bPause = false;       //일시정지 해제 cts.Cancel 보다 m_bPause를 먼저하면 ThreadRun 에서 일시 정지로 빠진다.

                Console.WriteLine("Base Thread Stop() #End");
            }
        }
        //public void Stop()
        //{
        //    if (thread != null && cts != null)
        //    {
        //        Console.WriteLine("Thread Stop() #1");
        //        cts.Cancel();

        //        Console.WriteLine("Thread Stop() Join #1");

        //        bool bRtn = thread.Join(100);  // 🔹 1초 동안 스레드 종료 대기 200ms 뒤에 빠져나와서 추가해도 괜찮음

        //        Console.WriteLine("Thread Stop() Join #2");
        //        if (bRtn == false)
        //        {
        //            bRtn = thread.Join(50);
        //            if (bRtn == false)
        //            {
        //                Abort();
        //            }

        //        }

        //        if (!thread.IsAlive) // 🔹 스레드가 종료되었는지 확인
        //        {
        //            thread = null;
        //            cts = null;
        //        }
        //        m_bPause = false;       //일시정지 해제

        //        Console.WriteLine("Thread Stop() #2");
        //    }
        //}
        public bool GetThreadRun()
        {
            if (thread != null)
            {
                Console.WriteLine($"GetThreadRun() : {thread.IsAlive}");

                return thread.IsAlive;    //thread 동작 중
            }
            return false;
            //return thread?.IsAlive ?? false;  // thread가 null이면 false 반환
        }

        public bool GetThreadPause()
        {
            return m_bPause;
        }
        private void Resume()
        {
            m_bPause = false;
            Console.WriteLine("thread Resume call");
        }
        private void Abort()
        {
            thread.Abort();
            Console.WriteLine("thread Abort call");
        }
    }
}
