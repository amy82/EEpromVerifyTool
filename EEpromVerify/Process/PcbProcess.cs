using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApsMotionControl.Process
{
    public class PcbProcess
    {

        public int nTimeTick = 0;
        //public int[] SensorSet;
        public int[] SensorSet = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] OrgOnGoing = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private bool autoPause = true;
        public PcbProcess()
        {
            //SensorSet = new int[4];
            //textCmdPos.Text = String.Format("{0:0.000}", dCmdPosition);
            
        }

        public int Auto_Loading(int nStep)       //로딩(30000 ~ 40000)
        {
            string szLog = "";
            const int UniqueNum = 30000;
            int nRetStep = nStep;
            switch (nStep)
            {
                case UniqueNum:
                    szLog = $"[AUTO] 자동운전 TEST 1 [STEP : {nStep}]";
                    Globalo.LogPrint("AutoPrecess", szLog);
                    nRetStep = 30050;
                    Thread.Sleep(500);
                    break;

                case 30050:
                    if(autoPause)
                    {
                        autoPause = false;
                        szLog = $"[AUTO] 자동운전 TEST 2 일시정지[STEP : {nStep}]";
                        Globalo.LogPrint("AutoPrecess", szLog, Globalo.eMessageName.M_WARNING);
                        nRetStep = -30050;
                        break;
                    }



                    // 바코드 이벤트를 발생시키는 시뮬레이션
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 5000 }; // 3초 후 바코드 스캔된 것처럼 처리
                    timer.Tick += (sender, args) =>
                    {
                        timer.Stop();
                        Globalo.serialPortManager.Barcode.SimulateScan("121212");
                    };
                    timer.Start();
                    nRetStep = 30100;
                    break;
                case 30100:


                    //Globalo.yamlManager.configData.DrivingSettings.EnableAutoStartBcr
                    DialogResult result = Globalo.MessageAskPopup("제품 투입후 진행해주세요!");
                    if (result == DialogResult.Yes)
                    {
                        nRetStep = 30150;
                    }
                    else
                    {
                        szLog = $"[AUTO] 자동운전 TEST 2 일시정지[STEP : {nStep}]";
                        Globalo.LogPrint("PcbPrecess", szLog);
                        nRetStep = -30100;
                        break;
                    }
                    
                    break;
                case 30150:
                    nRetStep = 30200;
                    break;
                case 30200:
                    nRetStep = 30250;
                    break;
                case 30250:
                    nRetStep = 39000;
                    break;
                case 39000:
                    nRetStep = 40000;
                    break;
            }
            return nRetStep;
        }
        public int Auto_EEpromVerify(int nStep)       //로딩(40000 ~ 50000)
        {
            string szLog = "";
            const int UniqueNum = 40000;
            int nRetStep = nStep;
            switch (nStep)
            {
                case UniqueNum:

                    nRetStep = 40500;
                    break;
                case 40500:

                    nRetStep = 41500;
                    break;
                case 41500:

                    nRetStep = 42500;
                    break;
                case 42500:

                    nRetStep = 43000;
                    break;
                case 43000:

                    nRetStep = 49000;
                    break;
                case 49000:

                    nRetStep = 50000;
                    break;
            }
            return nRetStep;
        }

        public int Auto_Final(int nStep)       //로딩(50000 ~ 60000)
        {
            string szLog = "";
            const int UniqueNum = 50000;
            int nRetStep = nStep;
            switch (nStep)
            {
                case UniqueNum:

                    nRetStep = 50500;
                    break;
                case 50500:

                    nRetStep = 51500;
                    break;
                case 51500:

                    nRetStep = 52000;
                    break;
                case 52000:

                    nRetStep = 53000;
                    break;
                case 53000:

                    nRetStep = 59000;
                    break;
                case 59000:

                    nRetStep = 30000;
                    break;
            }
            return nRetStep;
        }

        //
        //
        //
    }
}
