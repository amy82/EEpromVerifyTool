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
        public PcbProcess()
        {
            //SensorSet = new int[4];
            //textCmdPos.Text = String.Format("{0:0.000}", dCmdPosition);
            
        }
        public int Auto_Verify(int nStep)       //EEPROM VERIFY(40000 ~ 50000)
        {
            string szLog = "";
            const int UniqueNum = 40000;
            int nRetStep = nStep;
            switch (nStep)
            {
                case UniqueNum:
                    szLog = $"[AUTO]  [STEP : {nStep}]";
                    break;
                case 40050:

                    break;

            }
            return nRetStep;
        }
        public int Auto_Final(int nStep)       //EEPROM FINAL(50000 ~ 60000)
        {
            string szLog = "";
            const int UniqueNum = 50000;
            int nRetStep = nStep;
            switch (nStep)
            {
                case UniqueNum:
                    szLog = $"[AUTO]  [STEP : {nStep}]";
                    break;
                case 50050:

                    break;

            }
            return nRetStep;
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
                    break;
                case 30050:

                    nRetStep = 30100;
                    break;
                case 30100:
                    MessagePopUpForm messagePopUp3 = new MessagePopUpForm("", "YES", "NO");
                    messagePopUp3.MessageSet(Globalo.eMessageName.M_ASK, "제품 투입후 진행해주세요!");

                    DialogResult result = messagePopUp3.ShowDialog();
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
                    szLog = $"[AUTO] 자동운전 TEST 2 [STEP : {nStep}]";
                    Globalo.LogPrint("AutoPrecess", szLog);
                    nRetStep = 39000;
                    break;
                case 39000:
                    szLog = $"[AUTO] 자동운전 TEST End [STEP : {nStep}]";
                    Globalo.LogPrint("AutoPrecess", szLog);
                    nRetStep = 40000;
                    break;
            }
            return nRetStep;
        }
    }
}
