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
    public class CcdGrabThread : BaseThread
    {
        //public event delLogSender eLogSender;       //외부에서 호출할때 사용

        private IntPtr RawPtr;
        private IntPtr BmpPtr;

        private int mWidth;
        private int mHeight;

        private Mat imageItp;
        protected override void ThreadInit()
        {
            RawPtr = Marshal.UnsafeAddrOfPinnedArrayElement(Globalo.mLaonGrabberClass.m_pFrameRawBuffer, 0);
            BmpPtr = Marshal.UnsafeAddrOfPinnedArrayElement(Globalo.mLaonGrabberClass.m_pFrameBMPBuffer, 0);

            mWidth = Globalo.GrabberDll.mGetWidth();
            mHeight = Globalo.GrabberDll.mGetHeight();
            imageItp = new Mat(mHeight, mWidth, MatType.CV_8UC3);//MatType.CV_8UC3);

            //double dZoomX = 0.0;
            //double dZoomY = 0.0;
            //dZoomX = ((double)Globalo.camControl.CcdPanel.Width / (double)mWidth);
            //dZoomY = ((double)Globalo.camControl.CcdPanel.Height / (double)mHeight);
        }
        //protected override unsafe void ProcessRun()
        protected override unsafe void ThreadRun()
        {
            if (Globalo.mLaonGrabberClass.M_GrabDllLoadComplete == false)
            {
                return;
            }

            

            
            try
            {
                //mCcdThreadRun = true;
                //while (mCcdThreadRun)
                while (!cts.Token.IsCancellationRequested)
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

    }
}
