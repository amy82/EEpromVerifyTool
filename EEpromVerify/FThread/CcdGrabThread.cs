using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCvSharp;
using System.Diagnostics;

namespace ApsMotionControl.FThread
{
    public class CcdGrabThread
    {
        public event delLogSender eLogSender;       //외부에서 호출할때 사용
        public bool mCcdThreadRun = false;
        private Thread thread = null;
        private bool m_bPause = false;
        private CancellationTokenSource cts;

        unsafe public void FuncGrabRun(CancellationToken token)
        {
            if (Globalo.mLaonGrabberClass.M_GrabDllLoadComplete == false)
            {
                return;
            }

            IntPtr RawPtr = Marshal.UnsafeAddrOfPinnedArrayElement(Globalo.mLaonGrabberClass.m_pFrameRawBuffer, 0);
            IntPtr BmpPtr = Marshal.UnsafeAddrOfPinnedArrayElement(Globalo.mLaonGrabberClass.m_pFrameBMPBuffer, 0);

            int mWidth = Globalo.GrabberDll.mGetWidth();
            int mHeight = Globalo.GrabberDll.mGetHeight();
            Mat imageItp = new Mat(mHeight, mWidth, MatType.CV_8UC3);//MatType.CV_8UC3);

            double dZoomX = 0.0;
            double dZoomY = 0.0;
            dZoomX = ((double)Globalo.camControl.CcdPanel.Width / (double)mWidth);
            dZoomY = ((double)Globalo.camControl.CcdPanel.Height / (double)mHeight);
            try
            {
                mCcdThreadRun = true;
                //while (mCcdThreadRun)
                while (!token.IsCancellationRequested)
                {
                    if (Globalo.mLaonGrabberClass.M_bOpen == false) continue;

                    if (Globalo.GrabberDll.mGetFrame((byte*)RawPtr.ToPointer(), (byte*)BmpPtr.ToPointer()) == true)
                    {
                        IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(Globalo.mLaonGrabberClass.m_pFrameBMPBuffer, 0);

                        Globalo.mLaonGrabberClass.imageItp = new Mat(mHeight, mWidth, MatType.CV_8UC3, Globalo.mLaonGrabberClass.m_pFrameBMPBuffer);
                        //(int rows, int cols, MatType type, IntPtr data, long step = 0);

                        //IntPtr matData = Globalo.mLaonGrabberClass.imageItp.Data;
                        //Marshal.Copy(Globalo.mLaonGrabberClass.m_pFrameBMPBuffer, 0, matData, (mWidth * mHeight * 3));


                        // Grab 버퍼에 저장
                        Globalo.mLaonGrabberClass.m_pGrabBuff[Globalo.vision.m_nGrabIndex[0]] = Globalo.mLaonGrabberClass.imageItp.Clone();
                        Globalo.vision.m_nCvtColorReadyIndex[0] = Globalo.vision.m_nGrabIndex[0];
                        Globalo.vision.m_nGrabIndex[0]++;
                        if (Globalo.vision.m_nGrabIndex[0] >= 3)
                        {
                            Globalo.vision.m_nGrabIndex[0] = 0;
                        }
                        continue;
                    }
                    Thread.Sleep(5);
                }
            }
            catch (ThreadInterruptedException err)
            {
                Debug.WriteLine(err);
            }
            finally
            {
                Debug.WriteLine("time 리소스 지우기");
            }

        }

        public bool GetThreadRun()
        {
            if (thread != null)
            {
                ////return thread?.IsAlive ?? false;
                return thread.IsAlive;    //thread 동작 중
            }

            return false;
        }
        public void Stop()
        {
            if (thread != null && cts != null)
            {
                Console.WriteLine("CcdGrab thread stop 1");
                cts.Cancel();
                cts = null;
                m_bPause = false;
                Console.WriteLine("CcdGrab thread stop 2");
            }
        }
        public bool StopCheck()
        {
            if (thread == null)
            {
                return true;
            }
            bool brtn = thread.Join(3000);
            if (brtn)
            {
                thread = null;
            }
            return brtn;
        }
        public void Pause()
        {
            m_bPause = true;
        }
        public bool Start()
        {
            try
            {
                if (m_bPause == false)   //정지 상태일때만
                {
                    cts = new CancellationTokenSource();
                    thread = new Thread(() => FuncGrabRun(cts.Token));
                    thread.Start();
                }
                m_bPause = false;

                Console.WriteLine("CcdGrab thread start.");
            }
            catch (ThreadStateException ex)
            {
                // Console.WriteLine($"ThreadStateException: {ex.Message}");
                eLogSender("AutoRunthread", $"[ERR] ThreadStateException: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}
