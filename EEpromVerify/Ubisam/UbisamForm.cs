using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using System.Xml.Linq;
using UbiCom.Net.Structure;
using UbiGEM.Net.Structure;
using UbiGEM.Net.Utility.Logger;
using System.IO;
using System.Windows;

namespace ApsMotionControl.Ubisam
{
    public partial class UbisamForm : Form
    {
        private readonly string DATETIME_TEXT_FORMAT = "{0} [{1}] {2}" + Environment.NewLine;
        private const string PROGRAM_TITLE_FORMAT = "{0} - {1}";
        private const string PROGRAM_STATUS_FORMAT = "{0} - {1}:{2}";

        private const string UGC_FILE_FILTER = "UbiGEM Configuration File (*.ugc)|*.ugc|All files (*.*)|*.*";
        private const string PROGRAM_DEFAULT_TITLE = "UbiSam.GEM.Sample.CSharp";

        private const int LOG_LINE_MAX_COUNT = 100;
        private int uCtTimeOutData = 2;
        private UbiGEM.Net.Driver.GemDriver _gemDriver;
        private List<long> _setAlarmList;
        private int _ack = 0;
        private string _ugcFileName;
        public UbisamForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            Initialize();
            UpdateTitle();

            
        }
        public void UbisamClose()
        {
            OnMnuStop();
            _gemDriver.Dispose();
        }
        private void Initialize()
        {
            _gemDriver = new UbiGEM.Net.Driver.GemDriver();
            _setAlarmList = new List<long>();

            //[Communication Event]
            _gemDriver.OnCommunicationStateChanged += GemDriver_OnCommunicationStateChanged;
            _gemDriver.OnControlStateChanged += GemDriver_OnControlStateChanged;
            _gemDriver.OnEquipmentProcessState += GemDriver_OnEquipmentProcessState;
            _gemDriver.OnGEMConnected += GemDriver_OnGEMConnected;
            _gemDriver.OnGEMSelected += GemDriver_OnGEMSelected;
            _gemDriver.OnGEMDeselected += GemDriver_OnGEMDeselected;
            _gemDriver.OnGEMDisconnected += GemDriver_OnGEMDisconnected;
            _gemDriver.OnControlStateOnlineChangeFailed += GemDriver_OnControlStateOnlineChangeFailed;


            //[Received Message Event]
            _gemDriver.OnReceivedRequestOffline += GemDriver_OnReceivedRequestOffline;
            _gemDriver.OnReceivedRequestOnline += GemDriver_OnReceivedRequestOnline;
            _gemDriver.OnReceivedDefineReport += GemDriver_OnReceivedDefineReport;
            _gemDriver.OnReceivedLinkEventReport += GemDriver_OnReceivedLinkEventReport;
            _gemDriver.OnReceivedEnableDisableEventReport += GemDriver_OnReceivedEnableDisableEventReport;
            _gemDriver.OnReceivedRemoteCommand += GemDriver_OnReceivedRemoteCommand;
            /////
            _gemDriver.OnReceivedEnhancedRemoteCommand += GemDriver_OnReceivedEnhancedRemoteCommand;
            //
            _gemDriver.OnReceivedNewECVSend += GemDriver_OnReceivedNewECVSend;
            _gemDriver.OnReceivedEnableDisableAlarmSend += GemDriver_OnReceivedEnableDisableAlarmSend;
            _gemDriver.OnReceivedTerminalMessage += GemDriver_OnReceivedTerminalMessage;
            _gemDriver.OnReceivedTerminalMultiMessage += GemDriver_OnReceivedTerminalMultiMessage;
            _gemDriver.OnReceivedPPRequest += GemDriver_OnReceivedPPRequest;
            _gemDriver.OnReceivedPPSend += GemDriver_OnReceivedPPSend;
            _gemDriver.OnReceivedPPLoadInquire += GemDriver_OnReceivedPPLoadInquire;
            _gemDriver.OnReceivedDeletePPSend += GemDriver_OnReceivedDeletePPSend;
            _gemDriver.OnReceivedFmtPPRequest += GemDriver_OnReceivedFmtPPRequest;
            _gemDriver.OnReceivedFmtPPSend += GemDriver_OnReceivedFmtPPSend;
            _gemDriver.OnReceivedCurrentEPPDRequest += GemDriver_OnReceivedCurrentEPPDRequest;
            _gemDriver.OnReceivedDateTimeRequest += GemDriver_OnReceivedDateTimeRequest;
            _gemDriver.OnReceivedDateTimeSetRequest += GemDriver_OnReceivedDateTimeSetRequest;
            _gemDriver.OnReceivedLoopback += GemDriver_OnReceivedLoopback;
            _gemDriver.OnReceivedEstablishCommunicationsRequest += GemDriver_OnReceivedEstablishCommunicationsRequest;
            _gemDriver.OnUserPrimaryMessageReceived += GemDriver_OnUserPrimaryMessageReceived;
            _gemDriver.OnUserSecondaryMessageReceived += GemDriver_OnUserSecondaryMessageReceived;
            _gemDriver.OnReceivedUnknownMessage += GemDriver_OnReceivedUnknownMessage;
            _gemDriver.OnInvalidMessageReceived += GemDriver_OnInvalidMessageReceived;
            _gemDriver.OnReceivedInvalidRemoteCommand += GemDriver_OnReceivedInvalidRemoteCommand;
            _gemDriver.OnReceivedInvalidEnhancedRemoteCommand += GemDriver_OnReceivedInvalidEnhancedRemoteCommand;

            //[Response Message Event]
            _gemDriver.OnResponseTerminalRequest += GemDriver_OnResponseTerminalRequest;
            _gemDriver.OnResponsePPRequest += GemDriver_OnResponsePPRequest;
            _gemDriver.OnResponsePPSend += GemDriver_OnResponsePPSend;
            _gemDriver.OnResponsePPLoadInquire += GemDriver_OnResponsePPLoadInquire;
            _gemDriver.OnResponseFmtPPRequest += GemDriver_OnResponseFmtPPRequest;
            _gemDriver.OnResponseFmtPPSend += GemDriver_OnResponseFmtPPSend;
            _gemDriver.OnResponseFmtPPVerification += GemDriver_OnResponseFmtPPVerification;
            _gemDriver.OnResponseDateTimeRequest += GemDriver_OnResponseDateTimeRequest;
            _gemDriver.OnResponseLoopback += GemDriver_OnResponseLoopback;
            _gemDriver.OnResponseEventReportAcknowledge += GemDriver_OnResponseEventReportAcknowledge;

            //[Request Message Event]
            _gemDriver.OnVariableUpdateRequest += GemDriver_OnVariableUpdateRequest;
            _gemDriver.OnUserGEMMessageUpdateRequest += GemDriver_OnUserGEMMessageUpdateRequest;
            _gemDriver.OnTraceDataUpdateRequest += GemDriver_OnTraceDataUpdateRequest;


            //[Log Event]
            _gemDriver.OnWriteLog += GemDriver_OnWriteLog;
            //_gemDriver.OnSECS1Log += GemDriver_OnSECS1Log;
            //_gemDriver.OnSECS2Log += GemDriver_OnSECS2Log;

        }
        public int UbisamUgcLoad()
        {
            _ugcFileName = Globalo.yamlManager.ugcSetFile.ugcFilePath;
            UpdateTitle();
            OnMnuInitilaize();

            GemDriverError driverResult = _gemDriver.Start();
            WriteLog(LogLevel.Error, $"Driver Start Result : {driverResult}");
            return 0;
        }
        private void reportCommonSet()
        {
            
        }
        public bool EventReportSendFn(string strCEID, string parameter = "")
        {
            VariableInfo dataMainList;
            VariableInfo dataValue;



            DateTime currentTime = DateTime.Now;
            string timeData = string.Format("{0:yyMMddHHmmss}", currentTime);


            dataValue = new VariableInfo() { VID = "10001", Format = SECSItemFormat.A, Name = "m_sEquipmentID", Value = Globalo.dataManage.mesData.m_sEquipmentID };
            _gemDriver.SetVariable(dataValue);
            dataValue = new VariableInfo() { VID = "10002", Format = SECSItemFormat.A, Name = "EquipmentName", Value = Globalo.dataManage.mesData.m_sEquipmentName };
            _gemDriver.SetVariable(dataValue);
            dataValue = new VariableInfo() { VID = "10003", Format = SECSItemFormat.A, Name = "Time", Value = timeData };
            _gemDriver.SetVariable(dataValue);
            dataValue = new VariableInfo() { VID = "10008", Format = SECSItemFormat.A, Name = "OperatorID", Value = Globalo.dataManage.mesData.m_sMesOperatorID };
            _gemDriver.SetVariable(dataValue);


            if (strCEID == ReportConstants.OFFLINE_CHANGED_REPORT_10102)
            {
                dataValue = new VariableInfo() { VID = "10005", Format = SECSItemFormat.U1, Name = "PreviousControlState", Value = Globalo.dataManage.mesData.m_dEqupControlState[0] };
                _gemDriver.SetVariable(dataValue);
                dataValue = new VariableInfo() { VID = "10004", Format = SECSItemFormat.U1, Name = "CurrentControlState", Value = Globalo.dataManage.mesData.m_dEqupControlState[1] };
                _gemDriver.SetVariable(dataValue);
                dataValue = new VariableInfo() { VID = "10006", Format = SECSItemFormat.A, Name = "ControlStateChangeReasonCode", Value = Globalo.dataManage.mesData.rCtrlState_Chg_Req.Change_Code };
                _gemDriver.SetVariable(dataValue);
                dataValue = new VariableInfo() { VID = "10007", Format = SECSItemFormat.A, Name = "ControlStateChangeReasonText", Value = Globalo.dataManage.mesData.rCtrlState_Chg_Req.Change_Text };
                _gemDriver.SetVariable(dataValue);
                dataValue = new VariableInfo() { VID = "10010", Format = SECSItemFormat.U1, Name = "ControlStateChangeOrderType", Value = Globalo.dataManage.mesData.m_dControlStateChangeOrder };
                _gemDriver.SetVariable(dataValue);


            }
            else if (strCEID == "10104")//Online Remote Changed Report  xx
            {
                _gemDriver.SetVariable("10005", Globalo.dataManage.mesData.m_dEqupControlState[0]);
                _gemDriver.SetVariable("10004", Globalo.dataManage.mesData.m_dEqupControlState[1]);
                _gemDriver.SetVariable("10010", Globalo.dataManage.mesData.m_dControlStateChangeOrder);
            }
            else if (strCEID == "10201")//Equipment Constant Changed Report
            {

                dataMainList = new VariableInfo() { VID = "10020", Format = SECSItemFormat.L, Name = "ChangedECList" };

                VariableInfo dataSubList1 = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "ChangedECInfo", Value = "" };
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "ECID", Value = 107 };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "ECV", Value = 0 };
                dataSubList1.ChildVariables.Add(dataValue);
                dataMainList.ChildVariables.Add(dataSubList1);

                VariableInfo dataSubList2 = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "ChangedECInfo", Value = "" };
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "ECID", Value = 108 };
                dataSubList2.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "ECV", Value = 5 };
                dataSubList2.ChildVariables.Add(dataValue);
                dataMainList.ChildVariables.Add(dataSubList2);


                _gemDriver.SetVariable(dataMainList);
                _gemDriver.SetVariable("10021", 1);
            }
            else if (strCEID == ReportConstants.EQUIPMENT_OPERATION_MODE_CHANGED_REPORT_10301)
            {
                dataValue = new VariableInfo() { VID = "10012", Format = SECSItemFormat.A, Name = "EquipmentOperationMode", Value = Globalo.dataManage.mesData.m_dEqupOperationMode[0] };//EquipmentOperationMode 1, 9 주로 사용
                _gemDriver.SetVariable(dataValue);
                dataValue = new VariableInfo() { VID = "10013", Format = SECSItemFormat.A, Name = "EqpOperationModeChangeOrderType", Value = Globalo.dataManage.mesData.m_dEqupOperationMode[1] };
                _gemDriver.SetVariable(dataValue);
            }
            else if (strCEID == ReportConstants.PROCESS_STATE_CHANGED_REPORT_10401)
            {
                dataMainList = new VariableInfo() { VID = "10012", Format = SECSItemFormat.L, Name = "ProcerssStateInfo"};

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "CurrentProcessState", Value = Globalo.dataManage.mesData.m_dProcessState[1] };
                dataMainList.ChildVariables.Add(dataValue);



                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "PreviousProcessState", Value = Globalo.dataManage.mesData.m_dProcessState[0] };
                dataMainList.ChildVariables.Add(dataValue);

                _gemDriver.SetVariable(dataMainList);

                dataMainList = new VariableInfo() { VID = "10022", Format = SECSItemFormat.L, Name = "AlarmSetList" };
                for (int i = 0; i < Globalo.dataManage.mesData.m_uAlarmList.Count; i++)
                {
                    dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U4, Name = "ALID", Value = Globalo.dataManage.mesData.m_uAlarmList[i] };
                    dataMainList.ChildVariables.Add(dataValue);
                }
                _gemDriver.SetVariable(dataMainList);
            }
            else if (strCEID == ReportConstants.IDLE_REASON_REPORT_10402)
            {
                dataMainList = new VariableInfo() { VID = "10037", Format = SECSItemFormat.L, Name = "IdleReasonInfo" };

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "OperatorID", Value = Globalo.dataManage.mesData.m_sMesOperatorID };
                dataMainList.ChildVariables.Add(dataValue);

                string[] values = parameter.Split(',');
                string[] idelStrList = new string[5] { "IDLECODE", "IDLETEXT", "IDLESTARTTIME", "IDLEENDTIME", "IDLENOTE" };

                string idleVal = "";
                for (int i = 0; i < idelStrList.Length; i++)
                {
                    idleVal = "";
                    if (i < values.Length - 1)
                    {
                        idleVal = values[i];
                    }

                    dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = idelStrList[i], Value = idleVal };
                    dataMainList.ChildVariables.Add(dataValue);
                }

                _gemDriver.SetVariable(dataMainList);
            }
            else if (strCEID == ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601)
            {
                dataMainList = new VariableInfo() { VID = "10016", Format = SECSItemFormat.L, Name = "PPStateChangedPPIDInfo" };

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "PPStateChangedPPID", Value = parameter };
                dataMainList.ChildVariables.Add(dataValue);

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "PPStateChangedPPIDVersion", Value = Globalo.dataManage.mesData.m_sMesRecipeRevision };
                dataMainList.ChildVariables.Add(dataValue);

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "PPChangedState", Value = Globalo.dataManage.mesData.m_dPPChangeArr[0] };
                dataMainList.ChildVariables.Add(dataValue);

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "PPChangeOrderType", Value = Globalo.dataManage.mesData.m_dPPChangeArr[1] };
                dataMainList.ChildVariables.Add(dataValue);

                _gemDriver.SetVariable(dataMainList);
            }
            else if (strCEID == ReportConstants.OBJECT_ID_REPORT_10701)
            {
                //10024	10024	LotInfo
            }
            else if (strCEID == ReportConstants.PP_SELECTED_REPORT_10702 || strCEID == ReportConstants.PP_UPLOAD_COMPLETED_REPORT_10703)
            {
                dataMainList = new VariableInfo() { VID = "10026", Format = SECSItemFormat.L, Name = "CarrierInfo" };

                //CarrierInfo 는 사용 안함

                _gemDriver.SetVariable(dataMainList);

            }
            else if (strCEID == ReportConstants.LOT_PROCESSING_STARTED_REPORT_10704)
            {
                //10024	10024	LotInfo
            }
            else if (strCEID == ReportConstants.LOT_PROCESSING_COMPLETED_REPORT_10710)
            {
                VariableInfo dataSubList1;
                string lotId = "";
                string pcId = "";
                string pdId = "";

                dataMainList = new VariableInfo() { VID = "10051", Format = SECSItemFormat.L, Name = "LotList" };

                dataSubList1 = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "LotInfo" };

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U4, Name = "PortID", Value = 1 };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "LotID", Value = lotId };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "CarrierID", Value = "" };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "PocketID", Value = "" };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "ModuleID", Value = Globalo.dataManage.TaskWork.m_szChipID };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "ProcessID", Value = pcId };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "ProductID", Value = pdId };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "ModuleJudgeCode"};

                if (Globalo.dataManage.mesData.m_nMesFinalResult == 1)
                {
                    dataValue.Value = "OK";
                }
                else
                {
                    dataValue.Value = "NG";
                }
                dataSubList1.ChildVariables.Add(dataValue);

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "ModuleDefectCode", Value = Globalo.dataManage.mesData.m_dEqpDefectCode };
                dataSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "EndType"};
                if (Globalo.dataManage.mesData.m_nMesFinalResult == 1)
                {
                    dataValue.Value = 0;
                }
                else
                {
                    dataValue.Value = 5;
                }
                dataSubList1.ChildVariables.Add(dataValue);

                //
                //

                VariableInfo dataSubSubList1 = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "ModuleProcessedUnitList" };
                dataSubList1.ChildVariables.Add(dataSubSubList1);

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U4, Name = "ModuleProcessedUnitID", Value = 0 };
                dataSubSubList1.ChildVariables.Add(dataValue);

                //
                VariableInfo dataSubSubList2 = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "UserDataList" };
                dataSubList1.ChildVariables.Add(dataSubSubList2);

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U4, Name = "UserData", Value = "" };
                dataSubSubList2.ChildVariables.Add(dataValue);

                //
                VariableInfo dataSubSubList3 = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "UsedMaterialList" }; //설비에서 사용하는 자재 리스트
                dataSubList1.ChildVariables.Add(dataSubSubList3);


                VariableInfo dataSubSubSubList1 = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "KeyMaterialLocationInfo" };
                dataSubSubList3.ChildVariables.Add(dataSubSubSubList1);

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U4, Name = "UnitNo", Value = 1 };
                dataSubSubSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "SlotNo", Value = 1 };
                dataSubSubSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "MaterialID", Value = Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialId };
                dataSubSubSubList1.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "MaterialType", Value = Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialType };
                dataSubSubSubList1.ChildVariables.Add(dataValue);

                _gemDriver.SetVariable(dataMainList);

            }
            else if (strCEID == ReportConstants.LOT_APD_REPORT_10711)
            {
                int vidCnt = 40002;
                string strVid = "";
                //string kk = (10000 + i).ToString();

                for (int i = 0; i < SecsGemData.LOT_APD_INFO.Length; i++)
                {
                    strVid = (vidCnt + i).ToString();
                    dataMainList = new VariableInfo() { VID = strVid, Format = SECSItemFormat.L, Name = "LotAPDInfo" };

                    dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "DATANAME", Value = SecsGemData.LOT_APD_INFO[i] };
                    dataMainList.ChildVariables.Add(dataValue);

                    dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "DATAVALUE"};
                    if(i < Globalo.dataManage.mesData.vMesApdData.Count)
                    {
                        dataValue.Value = Globalo.dataManage.mesData.vMesApdData[i];
                    }
                    else
                    {
                        dataValue.Value = "0.00000";
                    }
                    dataMainList.ChildVariables.Add(dataValue);

                    _gemDriver.SetVariable(dataMainList);
                }

                
            }
            else if (strCEID == ReportConstants.ABORTED_REPORT_10712)
            {
                //10024	10024	LotInfo
            }
            else if (strCEID == ReportConstants.MATERIAL_ID_REPORT_10713 || strCEID == ReportConstants.MATERIAL_CHANGE_COMPLETED_REPORT_10714)
            {
                dataMainList = new VariableInfo() { VID = "10035", Format = SECSItemFormat.L, Name = "ChangedMaterialInfo" };

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "MaterialID", Value = Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialId };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U4, Name = "UnitNo", Value = 1 };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "SlotNo", Value = 1 };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "MaterialType", Value = Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialType };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "RemainUseData", Value = Globalo.dataManage.mesData.rMaterial_Id_Confirm.RemainData };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "ExchangeReason", Value = "" };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "ProductID", Value = "" }; //v2.2.7 버전에서 추가 250214
                dataMainList.ChildVariables.Add(dataValue);

                _gemDriver.SetVariable(dataMainList);

                _gemDriver.SetVariable("10053", "");
            }
            else if (strCEID == ReportConstants.OP_RECOGNIZED_OP_CALL_REPORT_10801)
            {
                dataMainList = new VariableInfo() { VID = "10038", Format = SECSItemFormat.L, Name = "LastOperatorCallInfo" };

                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "OPCALL_TYPE", Value = Globalo.dataManage.mesData.rCtrlOp_Call.CallType };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "OPCALL_CODE", Value = Globalo.dataManage.mesData.rCtrlOp_Call.OpCall_Code };
                dataMainList.ChildVariables.Add(dataValue);
                dataValue = new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "OPCALL_TEXT", Value = Globalo.dataManage.mesData.rCtrlOp_Call.OpCall_Text };
                dataMainList.ChildVariables.Add(dataValue);

                _gemDriver.SetVariable(dataMainList);
            }
            else if (strCEID == ReportConstants.OP_RECOGNIZED_TERMINAL_REPORT_10901)
            {
                _gemDriver.SetVariable("10039", parameter);
            }
            else if (strCEID == ReportConstants.T3_TIMEOUT_REPORT_11001)
            {
                _gemDriver.SetVariable("10017", 1);
            }
            else if (strCEID == ReportConstants.CT_TIMEOUT_REPORT_11002)
            {
                _gemDriver.SetVariable("10018", uCtTimeOutData);
            }

            // ReportCollectionEvent(string) API의 사용은 미리 정의된 Collection Event를 보고할 경우 입니다.
            // OnVariableUpdateRequest Event 발생 합니다.
            // OnVariableUpdateRequest Event 내에서 Variable의 값을 설정 하는것도 가능합니다.

            GemDriverError driverResult = _gemDriver.ReportCollectionEvent(strCEID, true);
            //GemDriverError driverResult = _gemDriver.ReportCollectionEvent(ceInfo);
            WriteLog(LogLevel.Error, $"Report Collection Event Result : {driverResult}");

            return true;
        }

        #region [Communication Event]
        /// <summary>
        /// Communication State가 변경될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="communicationState"></param>
        private void GemDriver_OnCommunicationStateChanged(CommunicationState communicationState)
        {
            WriteLog(LogLevel.Information, $"OnCommunicationStateChanged - {communicationState}");

            WriteLog(LogLevel.Information, $"ControlState - {_gemDriver.ControlState}");
            string str = communicationState.ToString() + " / " + _gemDriver.ControlState.ToString();


            Globalo.mMainPanel.setControlState((int)communicationState, (int)_gemDriver.ControlState);
        }

        /// <summary>
        /// Control State가 변경될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="controlState"></param>
        private void GemDriver_OnControlStateChanged(ControlState controlState)
        {
            WriteLog(LogLevel.Information, $"OnControlStateChanged - {controlState}");
            Globalo.mMainPanel.setControlState((int)_gemDriver.CommunicationState, (int)controlState);
        }

        /// <summary>
        /// EquipmentProcess State가 변경될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="equipmentProcessState"></param>
        private void GemDriver_OnEquipmentProcessState(byte equipmentProcessState)
        {
            WriteLog(LogLevel.Information, $"OnEquipmentProcessState - {equipmentProcessState}");
        }

        private void GemDriver_OnGEMConnected(string ipAddress, int portNo)
        {
            UpdateTitle("Connected", ipAddress, portNo);
        }

        private void GemDriver_OnGEMSelected(string ipAddress, int portNo)
        {
            UpdateTitle("Selected", ipAddress, portNo);
        }

        private void GemDriver_OnGEMDeselected(string ipAddress, int portNo)
        {
            UpdateTitle("Deselected", ipAddress, portNo);
        }

        private void GemDriver_OnGEMDisconnected(string ipAddress, int portNo)
        {
            UpdateTitle("Disconnected", ipAddress, portNo);
        }

        private void GemDriver_OnControlStateOnlineChangeFailed()
        {
            WriteLog(LogLevel.Error, "OnControlStateOnlineChangeFailed");
        }
        #endregion

        #region [Received Message Event]
        /// <summary>
        /// Host에서 S1F15(Offline Request)가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="systemBytes"></param>
        private void GemDriver_OnReceivedRequestOffline(uint systemBytes)
        {
            WriteLog(LogLevel.Information, "Received Request Offline");

            _gemDriver.ReplyRequestOfflineAck(systemBytes, _ack);

            if(Globalo.dataManage.mesData.m_dEqupControlState[0] == (int)eCURRENT_CONTROL_STATE.eOnlineLocal ||
                Globalo.dataManage.mesData.m_dEqupControlState[0] == (int)eCURRENT_CONTROL_STATE.eOnlineRemote)
            {
                Globalo.dataManage.mesData.m_dControlStateChangeOrder = 1;      //by Host

                _gemDriver.SetVariable("10006", "HOST001");
                _gemDriver.SetVariable("10007", "Host에서 OFF-LINE 전환 하였습니다.");

                OnMnuOffLIne();
                //m_pWrapper->SetVariable(_T("10006"), _T("HOST001"));    //ControlStateChangeReasonCode
                //m_pWrapper->SetVariable(_T("10007"), _T("Host에서 OFF-LINE 전환 하였습니다."));  //ControlStateChangeReasonText
                //OnBnClickedButtonUbigemCsOffline();
            }
        }

        /// <summary>
        /// Host에서 S1F17(Online Request)가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        private void GemDriver_OnReceivedRequestOnline(uint systemBytes)
        {
            WriteLog(LogLevel.Information, "Received Request Online");

            _gemDriver.ReplyRequestOnlineAck(systemBytes, _ack);
        }

        /// <summary>
        /// S2F33(Define Report)가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        private void GemDriver_OnReceivedDefineReport()
        {
            WriteLog(LogLevel.Information, "Received Define Report");
        }

        /// <summary>
        /// S2F35(Link Event Report)가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        private void GemDriver_OnReceivedLinkEventReport()
        {
            WriteLog(LogLevel.Information, "Received LinkEvent Report");
        }

        /// <summary>
        /// S2F37(Event Report Enable/Disable)이 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        private void GemDriver_OnReceivedEnableDisableEventReport()
        {
            WriteLog(LogLevel.Information, "Received Enable Disable Event Send");
        }

        /// <summary>
        /// Host에서 S2F41(Remote Command)가 수신될 때 발생하는 이벤트입니다.
        /// RemoteCommandInfo 의 아이템을 순회하는 코드입니다.
        /// RemoteCommandParameterResult 에 IRemoteCommandParameterResult 를 추가하여 parameter 별 ack를 구성할 수 있습니다.
        /// </summary>
        /// <param name="remoteCommandInfo"></param>
        private void GemDriver_OnReceivedRemoteCommand(RemoteCommandInfo remoteCommandInfo)
        {
            RemoteCommandResult result = new RemoteCommandResult();
            RemoteCommandParameterResult paramResult;
            string logText;

            result.HostCommandAck = _ack;

            foreach (CommandParameterInfo paramInfo in remoteCommandInfo.CommandParameter.Items)
            {
                paramResult = new RemoteCommandParameterResult(paramInfo.Name, (int)CPACK.IllegalFormatSpecifiedForCPVAL);
                result.Items.Add(paramResult);
            }

            logText = $"[RemoteCommand={remoteCommandInfo.RemoteCommand}]{Environment.NewLine}";

            foreach (CommandParameterInfo paramInfo in remoteCommandInfo.CommandParameter.Items)
            {
                logText += $": [CPNAME={paramInfo.Name},Format={paramInfo.Format},CPVAL={paramInfo.Value}]{Environment.NewLine}";
            }

            if (logText.Length > 0)
            {
                logText = logText.Substring(0, logText.Length - Environment.NewLine.Length);
            }

            WriteLog(LogLevel.Information, $"OnReceivedRemoteCommand : {logText}");

            //S2F42 Reply이전 호스트에서 받은 S2F41(EnhancedRemoteCommand)에 대한 Validation Check가 필요합니다.

            _gemDriver.ReplyRemoteCommandAck(remoteCommandInfo, result);

            //S2F42 Reply이후 관련된 Logic을 넣으면 됩니다.
        }

        /// <summary>
        /// Host에서 S2F49(Enhanced Remote Command)가 수신될 때 발생하는 이벤트입니다.
        /// EnhancedRemoteCommandInfo 의 아이템을 순회하는 코드입니다.
        /// RemoteCommandResult 에 RemoteCommandParameterResult 를 추가하여 parameter 별 ack를 구성할 수 있습니다.
        /// </summary>
        /// <param name="remoteCommandInfo"></param>
        private void GemDriver_OnReceivedEnhancedRemoteCommand(EnhancedRemoteCommandInfo remoteCommandInfo)
        {
            RemoteCommandResult result = new RemoteCommandResult();
            RemoteCommandParameterResult paramResult;
            string logText = "";
            string strErrCode = "";
            string strErrText = "";
            result.HostCommandAck = _ack;

            logText = $"[RemoteCommand={remoteCommandInfo.RemoteCommand}]{Environment.NewLine}";


            if(remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_OFFLINE_REASON)
            {
                Globalo.dataManage.mesData.vOfflineReason.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_IDLE_REASON)
            {
                Globalo.dataManage.mesData.vIdleReason.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_MATERIAL_EXCHANGE)
            {
                Globalo.dataManage.mesData.vMaterialExchange.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_MODEL_LIST)
            {
                Globalo.dataManage.mesData.vModelLIst.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_SELECT)
            {
                Globalo.dataManage.mesData.vPPSelect.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_LOT_ID_FAIL)
            {
                Globalo.dataManage.mesData.vLotIdFail.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_CONFIRM)
            {
                Globalo.dataManage.mesData.vPPUploadConfirm.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_FAIL)
            {
                Globalo.dataManage.mesData.vPPUploadFail.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_LOT_START)
            {
                Globalo.dataManage.mesData.vLotStart.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_EEPROM_DATA)
            {
                Globalo.dataManage.mesData.VMesEEpromData.Clear();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_EEPROM_FAIL)
            {

            }

            //
            //
            //
            //
            foreach (EnhancedCommandParameterInfo paramInfo in remoteCommandInfo.EnhancedCommandParameter.Items)
            {
                if (paramInfo.Format == SECSItemFormat.L)
                {
                    logText += $": [CPNAME={paramInfo.Name},Format={paramInfo.Format},Count={paramInfo.Items.Count}]{Environment.NewLine}";
                    paramResult = new RemoteCommandParameterResult(paramInfo.Name);

                    foreach (EnhancedCommandParameterItem item in paramInfo.Items)
                    {
                        Data.RcmdParameter parameter;
                        logText += CheckValidationParameterItem(1, item, paramResult, out parameter);


                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_LOT_ID_FAIL)
                        {
                            Globalo.dataManage.mesData.vLotIdFail.Add(parameter);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_SELECT)
                        {
                            Globalo.dataManage.mesData.vPPSelect.Add(parameter);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_CONFIRM)
                        {
                            Globalo.dataManage.mesData.vPPUploadConfirm.Add(parameter);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_OFFLINE_REASON)
                        {
                            Data.RcmdParam1 rcmdP1 = new Data.RcmdParam1();
                            rcmdP1.CpName = item.Name;
                            rcmdP1.CepVal = item.Value;
                            Globalo.dataManage.mesData.vOfflineReason.Add(rcmdP1);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_IDLE_REASON)
                        {
                            Data.RcmdParam1 rcmdP1 = new Data.RcmdParam1();
                            rcmdP1.CpName = item.Name;
                            rcmdP1.CepVal = item.Value;
                            Globalo.dataManage.mesData.vIdleReason.Add(rcmdP1);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_MODEL_LIST)
                        {
                            Data.RcmdParam1 rcmdP1 = new Data.RcmdParam1();
                            rcmdP1.CpName = item.Name;
                            rcmdP1.CepVal = item.Value;
                            Globalo.dataManage.mesData.vModelLIst.Add(rcmdP1);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_FAIL)
                        {
                            Data.RcmdParam1 rcmdP1 = new Data.RcmdParam1();
                            rcmdP1.CpName = item.Name;
                            rcmdP1.CepVal = item.Value;
                            Globalo.dataManage.mesData.vPPUploadFail.Add(rcmdP1);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_MATERIAL_EXCHANGE)
                        {
                            Globalo.dataManage.mesData.vMaterialExchange.Add(parameter);
                        }
                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_LOT_START)
                        {
                            Globalo.dataManage.mesData.vLotStart.Add(parameter);
                        }


                        if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_EEPROM_DATA)
                        {
                            Data.EEpromCsvData tempData = new Data.EEpromCsvData();
                            foreach (EnhancedCommandParameterItem subitem in item.ChildParameterItem.Items)
                            {
                                if (subitem.Name == "ADDRESS")
                                {
                                    tempData.ADDRESS = subitem.Value;
                                }
                                else if (subitem.Name == "DATA_SIZE")
                                {
                                    tempData.DATA_SIZE = subitem.Value;
                                }
                                else if (subitem.Name == "DATA_FORMAT")
                                {
                                    tempData.DATA_FORMAT = subitem.Value;
                                }
                                else if (subitem.Name == "BYTE_ORDER")
                                {
                                    tempData.BYTE_ORDER = subitem.Value;
                                }
                                else if (subitem.Name == "FIX_YN")
                                {
                                    tempData.FIX_YN = subitem.Value;
                                }
                                else if (subitem.Name == "ITEM_VALUE")
                                {
                                    tempData.ITEM_VALUE = subitem.Value;
                                }
                                else if (subitem.Name == "ITEM_CODE")
                                {
                                    tempData.ITEM_CODE = subitem.Value;
                                }
                                else if (subitem.Name == "CRC_START")
                                {
                                    tempData.CRC_START = subitem.Value;
                                }
                                else if (subitem.Name == "CRC_END")
                                {
                                    tempData.CRC_END = subitem.Value;
                                }
                                else if (subitem.Name == "PAD_VALUE")
                                {
                                    tempData.PAD_VALUE = subitem.Value;
                                }
                                else if (subitem.Name == "PAD_POSITION")
                                {
                                    tempData.PAD_POSITION = subitem.Value;
                                }
                            }
                            Globalo.dataManage.mesData.VMesEEpromData.Add(tempData);

                        }

                    }
                }
                else
                {
                    logText += $": [CPNAME={paramInfo.Name},Format={paramInfo.Format},CPVAL={paramInfo.Value}]{Environment.NewLine}";

                    if (paramInfo.Name == "EQPID")
                    {
                        Globalo.dataManage.mesData.m_sEquipmentID = paramInfo.Value;
                    }
                    if (paramInfo.Name == "EQPNAME")
                    {
                        Globalo.dataManage.mesData.m_sRecipeId = paramInfo.Value;
                    }
                    if (paramInfo.Name == "RECIPEID")
                    {

                    }
                    //
                    //---------------
                    //---------------
                    //
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_CTRLSTATE_CHG_REQ)
                    {
                        if (paramInfo.Name == "CONFIRMFLAG")
                        {
                            Globalo.dataManage.mesData.rCtrlState_Chg_Req.ConfirmFlag = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "CONTROLSTATE")
                        {
                            Globalo.dataManage.mesData.rCtrlState_Chg_Req.ControlState = (int)paramInfo.Value;
                        }
                        else if (paramInfo.Name == "CHANGE_CODE")
                        {
                            Globalo.dataManage.mesData.rCtrlState_Chg_Req.Change_Code = paramInfo.Value;
                            _gemDriver.SetVariable("10006", paramInfo.Value);
                        }
                        else if (paramInfo.Name == "CHANGE_TEXT")
                        {
                            Globalo.dataManage.mesData.rCtrlState_Chg_Req.Change_Text = paramInfo.Value;
                            _gemDriver.SetVariable("10007", paramInfo.Value);
                        }
                        else if (paramInfo.Name == "RESULT_CODE")
                        {
                            Globalo.dataManage.mesData.rCtrlState_Chg_Req.Result_Code = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "RESULT_TEXT")
                        {
                            Globalo.dataManage.mesData.rCtrlState_Chg_Req.Result_Text = paramInfo.Value;
                        }
                    }
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_OP_CALL)
                    {
                        if (paramInfo.Name == "CALLTYPE")
                        {
                            Globalo.dataManage.mesData.rCtrlOp_Call.CallType = (int)paramInfo.Value;
                        }
                        else if (paramInfo.Name == "OPCALL_CODE")
                        {
                            Globalo.dataManage.mesData.rCtrlOp_Call.OpCall_Code = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "OPCALL_TEXT")
                        {
                            Globalo.dataManage.mesData.rCtrlOp_Call.OpCall_Text = paramInfo.Value;
                        }
                    }
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_LOT_ID_FAIL)
                    {
                        if (paramInfo.Name == "CODE")
                        {
                            strErrCode = paramInfo.Value;
                            Globalo.dataManage.mesData.m_sErcmdCode = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "TEXT")
                        {
                            strErrText = paramInfo.Value;
                            Globalo.dataManage.mesData.m_sErcmdText = paramInfo.Value;
                        }
                    }
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_CONFIRM)
                    {

                    }
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_FAIL)
                    {
                        if (paramInfo.Name == "CODE")
                        {
                            strErrCode = paramInfo.Value;
                            Globalo.dataManage.mesData.m_sErcmdCode = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "TEXT")
                        {
                            strErrText = paramInfo.Value;
                            Globalo.dataManage.mesData.m_sErcmdText = paramInfo.Value;
                        }
                    }
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_MATERIAL_ID_CONFIRM)
                    {
                        if (paramInfo.Name == "MATERIALID")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialId = paramInfo.Value;      //저장?
                        }
                        else if (paramInfo.Name == "MATERIALTYPE")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialType = paramInfo.Value;        //저장?
                        }
                        else if (paramInfo.Name == "MATERIALTYPE_TEXT")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialType_Text = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "UNITNO")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Confirm.UnitNo = (int)paramInfo.Value;
                        }
                        else if (paramInfo.Name == "SLOTNO")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Confirm.SlotNo = (int)paramInfo.Value;
                        }
                        else if (paramInfo.Name == "REMAINDATA")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Confirm.RemainData = paramInfo.Value;
                        }
                    }
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_MATERIAL_ID_FAIL)
                    {
                        if (paramInfo.Name == "MATERIALID")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Fail.MaterialId = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "MATERIALTYPE")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Fail.MaterialType = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "MATERIALTYPE_TEXT")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Fail.MaterialType_Text = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "UNITNO")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Fail.UnitNo = (int)paramInfo.Value;
                        }
                        else if (paramInfo.Name == "SLOTNO")
                        {
                            Globalo.dataManage.mesData.rMaterial_Id_Fail.SlotNo = (int)paramInfo.Value;
                        }
                        else if (paramInfo.Name == "CODE")
                        {
                            strErrCode = paramInfo.Value;
                            Globalo.dataManage.mesData.rMaterial_Id_Fail.Code = paramInfo.Value;
                            Globalo.dataManage.mesData.m_sErcmdCode = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "TEXT")
                        {
                            strErrText = paramInfo.Value;
                            Globalo.dataManage.mesData.rMaterial_Id_Fail.Text = paramInfo.Value;
                            Globalo.dataManage.mesData.m_sErcmdText = paramInfo.Value;
                        }
                    }
                    if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PAUSE)
                    {
                        if (paramInfo.Name == "PAUSE_CODE")
                        {
                            Globalo.dataManage.mesData.rLgit_Pause.PauseCode = paramInfo.Value;
                        }
                        else if (paramInfo.Name == "PAUSE_TEXT")
                        {
                            Globalo.dataManage.mesData.rLgit_Pause.PauteText = paramInfo.Value;
                        }
                    }
                }


                
                paramResult = new RemoteCommandParameterResult(paramInfo.Name, (int)CPACK.IllegalFormatSpecifiedForCPVAL);
                result.Items.Add(paramResult);
            }

            if (logText.Length > 0)
            {
                logText = logText.Substring(0, logText.Length - Environment.NewLine.Length);
            }

            WriteLog(LogLevel.Information, $"OnReceivedEnhancedRemoteCommand : {logText}");

            //S2F50 Reply이전 호스트에서 받은 S2F49(EnhancedRemoteCommand)에 대한 Validation Check가 필요합니다.

            _gemDriver.ReplyEnhancedRemoteCommandAck(remoteCommandInfo, result);

            //S2F50 Reply이후 관련된 Logic을 넣으면 됩니다.
            //
            //
            //
            //Next Step
            //
            //
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_OP_CALL)
            {
                //string LogInfo = $"[{rCtrlOp_Call.OpCall_Text}] Message Test";

                string LogInfo = Globalo.dataManage.mesData.rCtrlOp_Call.OpCall_Text;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => Globalo.ShowOpCallMessageDialog(LogInfo)));

                    //this.Invoke(new Action(() => Globalo.ShowOpCallMessageDialog(LogInfo)));  //반환값 없음
                    //this.Invoke(new Action(() => {Globalo.ShowOpCallMessageDialog(LogInfo); })); //반환값 미사용

                    //Rtn = (bool)this.Invoke(new Func<bool>(() =>Globalo.ShowAskMessageDialog(LogInfo)));     //반환값 사용
                }
                else
                {
                    Globalo.ShowOpCallMessageDialog(LogInfo);
                }

            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_SETCODE_MODEL_LIST)
            {
                //Main Model Grid Refresh
                //Globalo.yamlManager.MesData.SecGemData.Modellist.Count();
                int cnt = Globalo.dataManage.mesData.vModelLIst.Count;
                if (cnt > 0)
                {
                    Globalo.yamlManager.MesData.SecGemData.ModelNo = 0;
                    Globalo.yamlManager.MesData.SecGemData.Modellist.Clear();

                    for (int i = 0; i < cnt; i++)
                    {
                        Globalo.yamlManager.MesData.SecGemData.Modellist.Add(Globalo.dataManage.mesData.vModelLIst[i].CepVal);
                    }

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(Globalo.mMainPanel.ShowModelGrid));
                    }
                    else
                    {
                        Globalo.mMainPanel.ShowModelGrid();
                    }
                    Globalo.yamlManager.MesSave();
                }
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PAUSE)
            {
                Globalo.dataManage.mesData.m_bLgit_Pause_req = true;	//eLGIT_PAUSE
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_SELECT)
            {
                //Recipe ID Check 
                if (Globalo.dataManage.mesData.m_sRecipeId == Globalo.dataManage.mesData.m_sMesPPID)
                {
                    Globalo.dataManage.TaskWork.bRecv_Lgit_Pp_select = 0;		//Recv
                }
                else
                {
                    Globalo.dataManage.TaskWork.bRecv_Lgit_Pp_select = 1;	//Recv
                }
            }

            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_LOT_ID_FAIL)
            {
                Globalo.dataManage.TaskWork.bRecv_S2F49_LG_Lot_Start = 1;
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_CONFIRM)
            {
                Globalo.dataManage.TaskWork.bRecv_S2F49_PP_UpLoad_Confirm = 0;		//LGIT_PP_UPLOAD_CONFIRM
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_PP_UPLOAD_FAIL)
            {
                Globalo.dataManage.TaskWork.bRecv_S2F49_PP_UpLoad_Confirm = 1;		//LGIT_PP_UPLOAD_FAIL
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_LOT_START)
            {
                Globalo.dataManage.TaskWork.bRecv_S2F49_LG_Lot_Start = 0;
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_MATERIAL_ID_CONFIRM)
            {
                //g_clMesCommunication[m_nUnit].secsGemSave();
                Globalo.yamlManager.MesData.SecGemData.MaterialId = Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialId;
                Globalo.yamlManager.MesData.SecGemData.MaterialType = Globalo.dataManage.mesData.rMaterial_Id_Confirm.MaterialType;
                Globalo.yamlManager.MesSave();
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_MATERIAL_ID_FAIL)
            {
                string LogInfo = Globalo.dataManage.mesData.rMaterial_Id_Fail.MaterialId;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => Globalo.ShowMaterialMessageDialog(LogInfo)));
                }
                else
                {
                    Globalo.ShowMaterialMessageDialog(LogInfo);
                }
                
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_EEPROM_DATA)
            {
                Globalo.dataManage.TaskWork.bRecv_S2F49_LG_EEprom_Data = 1;
                
            }
            if (remoteCommandInfo.RemoteCommand == SecsGemData.LGIT_EEPROM_FAIL)
            {
                Globalo.dataManage.TaskWork.bRecv_S2F49_LG_EEprom_Fail = 1;
            }

        }//end

        /// <summary>
        /// Host에서 S2F15(New ECV Send)가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="newEcInfo"></param>
        private void GemDriver_OnReceivedNewECVSend(uint systemBytes, VariableCollection newEcInfo)
        {
            WriteLog(LogLevel.Information, "Received New ECV Send");

            //for(int i = 0; i < newEcInfo.Items.Count; i++)
            //{

            //}
            foreach (var item in newEcInfo.Items)
            {
                Console.WriteLine($"item: " + item.VID + item.Value);
                if(item.VID == "101")
                {
                    Globalo.dataManage.mesData.EstablishCommunicationsTimeout = item.Value;
                }
                else if (item.VID == "102")
                {
                    Globalo.dataManage.mesData.HeartBeatRate = item.Value;
                }
                else if (item.VID == "103")
                {
                    Globalo.dataManage.mesData.DefaultCommState = item.Value;
                }
                else if (item.VID == "104")
                {
                    Globalo.dataManage.mesData.DefaultCtrlState = item.Value;
                }
                else if (item.VID == "105")
                {
                    Globalo.dataManage.mesData.DefaultOfflineSubstate = item.Value;
                }
                else if (item.VID == "106")
                {
                    Globalo.dataManage.mesData.DefCtrlOfflineState = item.Value;
                }
                else if (item.VID == "107")
                {
                    Globalo.dataManage.mesData.TimeFormat = item.Value;
                }
                else if (item.VID == "108")
                {
                    Globalo.dataManage.mesData.DefaultOnlineSubState = item.Value;
                }
                else if (item.VID == "109")
                {
                    Globalo.dataManage.mesData.ConversationTimeoutCount = item.Value;
                }
                else if (item.VID == "201")
                {
                    Globalo.dataManage.mesData.m_sEquipmentID = item.Value;
                    _gemDriver.SetVariable(item.VID, Globalo.dataManage.mesData.m_sEquipmentID);
                }
                else if (item.VID == "202")
                {
                    Globalo.dataManage.mesData.m_sEquipmentName = item.Value;
                    _gemDriver.SetVariable(item.VID, Globalo.dataManage.mesData.m_sEquipmentName);
                }
                else if (item.VID == "221")
                {
                    Globalo.dataManage.mesData.IdleReasonReportUsage = false;
                    if(item.Value == "Y")
                    {
                        Globalo.dataManage.mesData.IdleReasonReportUsage = true;
                    }
                }
                else if (item.VID == "222")
                {
                    Globalo.dataManage.mesData.IdleSetTimeInterval = item.Value;
                }
            }
            _gemDriver.ReplyNewEquipmentConstantSend(systemBytes, newEcInfo, _ack);
        }

        /// <summary>
        /// S5F3(Alarm Enable/Disable Send)가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        private void GemDriver_OnReceivedEnableDisableAlarmSend()
        {
            WriteLog(LogLevel.Information, "Received Enable Disable Alarm Send");
        }

        /// <summary>
        /// Host에서 S10F3(Terminal Message Single)이 수신될 때 발생하는 이벤트입니다
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="tid"></param>
        /// <param name="terminalMessage"></param>
        private void GemDriver_OnReceivedTerminalMessage(uint systemBytes, int tid, string terminalMessage)
        {
            WriteLog(LogLevel.Information, $"Received Terminal Message: TID={tid}, Text={terminalMessage ?? string.Empty}");

            _gemDriver.ReplyTerminalMessageAck(systemBytes, _ack);
        }

        /// <summary>
        /// Host에서 S10F5(Terminal Message Multi)가 수신될 때 발생하는 이벤트입니다
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="tid"></param>
        /// <param name="terminalMessages"></param>
        private void GemDriver_OnReceivedTerminalMultiMessage(uint systemBytes, int tid, List<string> terminalMessages)
        {
            string logText;

            logText = string.Empty;

            foreach (string terminalMessage in terminalMessages)
            {
                logText += terminalMessage + Environment.NewLine;
            }

            if (logText.Length > 0)
            {
                logText = logText.Substring(0, logText.Length - Environment.NewLine.Length);
            }

            WriteLog(LogLevel.Information, $"Received Terminal Multi Message: TID={tid}{Environment.NewLine}{logText}");

            _gemDriver.ReplyTerminalMultiMessageAck(systemBytes, _ack);
        }

        /// <summary>
        /// S7F5(PP Reqeust)가 수신될 경우 발생하는 이벤트입니다
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="ppid"></param>
        private void GemDriver_OnReceivedPPRequest(uint systemBytes, string ppid)
        {
            bool result;
            List<byte> ppbody;
            result = MakePPBody(out ppbody);

            WriteLog(LogLevel.Information, "Received PP Request");

            _gemDriver.ReplyPPRequestAck(systemBytes, ppid, ppbody, result);
        }

        /// <summary>
        /// Host에서 S7F3(PP Send)가 수신될 때 발생하는 이벤트입니다
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="ppid"></param>
        /// <param name="ppbody"></param>
        private void GemDriver_OnReceivedPPSend(uint systemBytes, string ppid, List<byte> ppbody)
        {
            WriteLog(LogLevel.Information, "Received PP Send");

            _gemDriver.ReplyPPSendAck(systemBytes, _ack);
        }

        /// <summary>
        /// Host에서 S7F1(PP Load Inquire)가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="ppid"></param>
        /// <param name="length"></param>
        private void GemDriver_OnReceivedPPLoadInquire(uint systemBytes, string ppid, int length)
        {
            WriteLog(LogLevel.Information, "Received PP Load Inquire");

            _gemDriver.ReplyPPLoadInquireAck(systemBytes, _ack);
        }

        /// <summary>
        /// S7F17(Delete PP Send)가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="ppids"></param>
        private void GemDriver_OnReceivedDeletePPSend(uint systemBytes, List<string> ppids)
        {
            string logText;

            logText = string.Empty;

            if (ppids.Count == 0)
            {
                //Delete All PPIDs
            }
            else
            {
                foreach (string ppid in ppids)
                {
                    //Delete Existing PPID.

                    logText += ppid + ",";
                }

                if (logText.Length > 0)
                {
                    logText = logText.Substring(0, logText.Length - 1);
                }
            }

            WriteLog(LogLevel.Information, $"Received Delette PP Send: ppids={logText}");

            _gemDriver.ReplyPPDeleteAck(systemBytes, _ack);
        }

        /// <summary>
        /// S7F25(Formatted PP Reqeust)가 수신될 경우 발생하는 이벤트입니다
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="ppid"></param>
        private void GemDriver_OnReceivedFmtPPRequest(uint systemBytes, string ppid)
        {
            FmtPPCollection fmtPPCollection;

            Globalo.yamlManager.vPPRecipeSpecEquip = Globalo.yamlManager.RecipeLoad(Globalo.dataManage.mesData.m_sMesPPID);
            bool rtn = true;
            if(Globalo.yamlManager.vPPRecipeSpecEquip == null)
            {
                rtn = false;
            }
            bool result = ProcessProgramParsing(ppid, rtn, out fmtPPCollection);


            WriteLog(LogLevel.Information, "Received FMT PP Request");

            _gemDriver.ReplyFmtPPRequestAck(systemBytes, ppid, fmtPPCollection, result);

            Globalo.dataManage.TaskWork.bRecv_S7F25_Formatted_Process_Program = 0;

        }

        /// <summary>
        /// Host에서 S7F23(Formatted PP Send)가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="fmtPPCollection"></param>
        private void GemDriver_OnReceivedFmtPPSend(uint systemBytes, FmtPPCollection fmtPPCollection)
        {
            string lotText = string.Empty;

            lotText += $"[PPID={fmtPPCollection.PPID}]{Environment.NewLine}";

            foreach (FmtPPCCodeInfo ppcodeInfo in fmtPPCollection.Items)
            {
                lotText += $": [CCODE={ppcodeInfo.CommandCode}]{Environment.NewLine}";

                foreach (FmtPPItem ppitem in ppcodeInfo.Items)
                {
                    lotText += $":    [PPNAME={ppitem.PPName},FORMAT={ppitem.Format}]{Environment.NewLine},PPVALUE={ppitem.PPValue}";
                }
            }

            if (lotText.Length > 0)
            {
                lotText = lotText.Substring(0, lotText.Length - Environment.NewLine.Length);
            }

            WriteLog(LogLevel.Information, $"OnReceivedFmtPPSend : {lotText}");

            _gemDriver.ReplyFmtPPSendAck(systemBytes, _ack);
        }

        private void GemDriver_OnReceivedCurrentEPPDRequest(uint systemBytes)
        {
            List<string> ppids = new List<string>();

            //Add PP List to ppids

            WriteLog(LogLevel.Information, "Received Current EPPD Request");

            _gemDriver.ReplyCurrentEPPDRequestAck(systemBytes, ppids, true);
        }

        /// <summary>
        /// S2F17(Date Time Reqeust)가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="systemBytes"></param>
        private void GemDriver_OnReceivedDateTimeRequest(uint systemBytes)
        {
            DateTime timeData = DateTime.Now;
            WriteLog(LogLevel.Information, "Received Date Time Request");

            _gemDriver.ReplyDateTimeRequest(systemBytes, timeData);
        }

        /// <summary>
        /// S2F31(Date Time Set Request)가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="systemBytes"></param>
        /// <param name="timeData"></param>
        private void GemDriver_OnReceivedDateTimeSetRequest(uint systemBytes, DateTime timeData)
        {
            WriteLog(LogLevel.Information, $"Received Date Time Set Request DateTime={timeData:yyyy-MM-dd HH:mm:ss.fff}");

            _gemDriver.ReplyDateTimeSetRequest(systemBytes, _ack, timeData);
        }

        /// <summary>
        /// Host에서 S2F25(Loopback)이 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="receiveData"></param>
        private void GemDriver_OnReceivedLoopback(List<byte> receiveData)
        {
            string strReceiveData = string.Empty;

            foreach (byte data in receiveData)
            {
                strReceiveData += data + " ";
            }

            if (strReceiveData.Length > 0)
            {
                strReceiveData = strReceiveData.Substring(0, strReceiveData.Length - 1);
            }

            WriteLog(LogLevel.Information, $"Received Loopback Data={strReceiveData}");
        }

        /// <summary>
        /// Host에서 S1F13(Establish Communication)이 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="mdln"></param>
        /// <param name="sofRev"></param>
        /// <returns></returns>
        private int GemDriver_OnReceivedEstablishCommunicationsRequest(string mdln, string sofRev)
        {
            WriteLog(LogLevel.Information, "Received Establish Communication Request");

            return _ack;
        }

        /// <summary>
        /// 사용자 정의 Message로 등록한 Stream, Function 중 Primary 메시지가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="message"></param>
        private void GemDriver_OnUserPrimaryMessageReceived(SECSMessage message)
        {
            WriteLog(LogLevel.Information, "User PrimaryMessage Received");
        }

        /// <summary>
        /// 사용자 정의 Message로 등록한 Stream, Function 중 Secondary 메시지가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="primaryMessage"></param>
        /// <param name="secondaryMessage"></param>
        private void GemDriver_OnUserSecondaryMessageReceived(SECSMessage primaryMessage, SECSMessage secondaryMessage)
        {
            WriteLog(LogLevel.Information, "User SecondaryMessage Received");
        }

        private void GemDriver_OnReceivedUnknownMessage(SECSMessage message)
        {
            WriteLog(LogLevel.Information, "Received Unknown Message");
        }

        private void GemDriver_OnInvalidMessageReceived(MessageValidationError error, SECSMessage message)
        {
            WriteLog(LogLevel.Information, "Received Invalid Message");
        }

        /// <summary>
        /// UbiGEM Configuration 파일에 정의되지 않은 Remote Command 가 수신될 경우 발생합니다.
        /// </summary>
        /// <param name="remoteCommandInfo"></param>
        private void GemDriver_OnReceivedInvalidRemoteCommand(RemoteCommandInfo remoteCommandInfo)
        {
            WriteLog(LogLevel.Information, "Received Invalid Remote Command");
        }

        /// <summary>
        /// UbiGEM Configuration 파일에 정의되지 않은 Enhanced Remote Command 가 수신될 경우 발생합니다.
        /// </summary>
        /// <param name="remoteCommandInfo"></param>
        private void GemDriver_OnReceivedInvalidEnhancedRemoteCommand(EnhancedRemoteCommandInfo remoteCommandInfo)
        {
            WriteLog(LogLevel.Information, "Received Invalid Enhanced Remote Command");
        }
        #endregion

        #region [Response Message Event]
        private void GemDriver_OnResponseTerminalRequest(int ack)
        {
            WriteLog(LogLevel.Information, "Response Terminal Request");
        }

        /// <summary>
        /// S7F5(PP Request)를 발송 후 Host에서 S7F6이 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="ppid"></param>
        /// <param name="ppbody"></param>
        private void GemDriver_OnResponsePPRequest(string ppid, List<byte> ppbody)
        {
            WriteLog(LogLevel.Information, "Response PP Request");
        }

        /// <summary>
        /// S7F3(PP Send)를 발송 후 Host에서 S7F4가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="ack"></param>
        /// <param name="ppid"></param>
        private void GemDriver_OnResponsePPSend(int ack, string ppid)
        {
            WriteLog(LogLevel.Information, "Response PP Send");
        }

        /// <summary>
        /// S7F1(PP Load Inquire)를 발송 후 Host에서 S7F2가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="ppgnt"></param>
        /// <param name="ppid"></param>
        private void GemDriver_OnResponsePPLoadInquire(int ppgnt, string ppid)
        {
            WriteLog(LogLevel.Information, "Response PP Load Inquire");
        }

        /// <summary>
        /// S2F23(Formatted PP Request) 발송 후 Host에서 S2F24가 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="fmtPPCollection"></param>
        private void GemDriver_OnResponseFmtPPRequest(FmtPPCollection fmtPPCollection)
        {
            Globalo.yamlManager.vPPRecipeSpec__Host = Globalo.yamlManager.RecipeLoad(Globalo.dataManage.mesData.m_sMesPPID);

            Data.RootRecipe ppRs = null;
            if (Globalo.yamlManager.vPPRecipeSpec__Host != null)
            {
                ppRs = Globalo.yamlManager.vPPRecipeSpec__Host;

                int rRecvCount = fmtPPCollection.Items.Count();
                if (rRecvCount > 0)
                {
                    foreach (FmtPPCCodeInfo ppcodeInfo in fmtPPCollection.Items)
                    {
                        foreach (FmtPPItem ppitem in ppcodeInfo.Items)
                        {
                            if (ppRs.RECIPE.ParamMap.TryGetValue(ppitem.PPName, out var value))
                            {
                                ppRs.RECIPE.ParamMap[ppitem.PPName] = new Data.Param { value = ppitem.PPValue, use = ppRs.RECIPE.ParamMap[ppitem.PPName].use };
                            }

                            //lotText += $":    [PPNAME={ppitem.PPName},FORMAT={ppitem.Format}]{Environment.NewLine},PPVALUE={ppitem.PPValue}";
                        }
                    }


                    // vPPRecipeSpec__Host.clear();
                    //vPPRecipeSpec__Host.push_back(ppRs);

                    Globalo.yamlManager.vPPRecipeSpec__Host = ppRs;

                    bool Rtn = false;
                    if (InvokeRequired)
                    {
                        Rtn = (bool)this.Invoke(new Func<bool>(() =>
                            Globalo.ShowAskMessageDialog("Apply Host Recipe Parameter Value?")
                        ));
                    }
                    else
                    {
                        Rtn = Globalo.ShowAskMessageDialog("Apply Host Recipe Parameter Value?");
                    }


                    if (Rtn)
                    {
                        Globalo.dataManage.mesData.m_dPPChangeArr[0] = (int)Ubisam.ePP_CHANGE_STATE.eEdited;            //2 (Edited)
                        Globalo.dataManage.mesData.m_dPPChangeArr[1] = (int)Ubisam.ePP_CHANGE_ORDER_TYPE.eOperator;       //1 = Host, 2 = Operator
                                                                                                                          //현재 사용중인 레시피일 경우 vPPRecipeSpecEquip 에 저장해야된다.
                        Globalo.yamlManager.RecipeSave(Globalo.yamlManager.vPPRecipeSpec__Host);
                        if (Globalo.dataManage.mesData.m_sMesPPID == fmtPPCollection.PPID)
                        {
                            Globalo.yamlManager.vPPRecipeSpecEquip = Globalo.yamlManager.vPPRecipeSpec__Host;
                        }
                        //if (this.InvokeRequired)
                        //{
                        //    this.Invoke(new Action(Globalo.mMainPanel.ShowRecipeGrid));     //Recipe parameter 갱신
                        //}
                        //else
                        //{
                        //    Globalo.mMainPanel.ShowRecipeGrid();     //Recipe parameter 갱신
                        //}

                        EventReportSendFn(ReportConstants.PROCESS_PROGRAM_STATE_CHANGED_REPORT_10601, Globalo.yamlManager.vPPRecipeSpec__Host.RECIPE.Ppid); //SEND
                    }
                }
                else
                {
                    string logData = $"[{fmtPPCollection.PPID}] RECIPE ITEM EMPTY!";
                    if (InvokeRequired)
                    {
                        Invoke(new Action(() => Globalo.LogPrint("MainControl", logData, Globalo.eMessageName.M_ERROR)));
                    }
                    else
                    {
                        Globalo.LogPrint("MainControl", logData, Globalo.eMessageName.M_ERROR);
                    }

                }
            }
            else
            {
                //ppRs.RECIPE.Ppid = fmtPPCollection.PPID;
                //ppRs.RECIPE.Version = "1";

                string logData = $"[{fmtPPCollection.PPID}] RECIPE EMPTY!";
                if (InvokeRequired)
                {
                    Invoke(new Action(() => Globalo.LogPrint("MainControl", logData, Globalo.eMessageName.M_ERROR)));
                }
                else
                {
                    Globalo.LogPrint("MainControl", logData, Globalo.eMessageName.M_ERROR);
                }
            }

            
            WriteLog(LogLevel.Information, "Response FMT PP Request");
        }

        /// <summary>
        /// S7F25(Formatted PP Send)를 발송 후 Host에서 S7F26이 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="ack"></param>
        /// <param name="fmtPPCollection"></param>
        private void GemDriver_OnResponseFmtPPSend(int ack, FmtPPCollection fmtPPCollection)
        {
            WriteLog(LogLevel.Information, "Response FMT PP Send");
        }

        /// <summary>
        /// S2F27(Formatted PP Verification Send)를 발송 후 Host에서 S2F28이 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="fmtPPVerificationCollection"></param>
        private void GemDriver_OnResponseFmtPPVerification(FmtPPVerificationCollection fmtPPVerificationCollection)
        {
            WriteLog(LogLevel.Information, "Response FMT Verification Ack");
        }

        /// <summary>
        /// S7F17(Date Time Reqeust)를 발송 후 Host에서 S7F18이 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="timeData"></param>
        /// <returns></returns>
        private bool GemDriver_OnResponseDateTimeRequest(DateTime timeData)
        {
            bool result = true;

            WriteLog(LogLevel.Information, $"Response Date Time Request DateTime={timeData:yyyy-MM-dd HH:mm:ss.fff}");

            return result;
        }

        /// <summary>
        /// S2F25(Loopback)을 발송 후 Host에서 S2F26이 수신될 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="receiveData"></param>
        /// <param name="sendData"></param>
        private void GemDriver_OnResponseLoopback(List<byte> receiveData, List<byte> sendData)
        {
            bool result = false;

            if (receiveData.Count == sendData.Count)
            {
                int count = receiveData.Count;

                for (int i = 0; i < count; i++)
                {
                    if (receiveData[i] != sendData[i])
                    {
                        result = false;
                        break;
                    }
                }
            }

            WriteLog(LogLevel.Information, $"Response Loopback : Receive Data={string.Join(",", receiveData)} : Send Data={string.Join(",", sendData)} : Result={result}");
        }

        /// <summary>
        /// S6F11(Event Report)의 Secondary Message(S6F12)가 수신될 경우 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="ceid"></param>
        /// <param name="ack"></param>
        private void GemDriver_OnResponseEventReportAcknowledge(string ceid, int ack)
        {
            if (ceid == "10001" && ack == (int)ACKC6.Accepted)
            {
                // Collection Event의 Ack값에 따라 정의할 시나리오 작성
            }
        }
        #endregion

        #region [Request Message Event]
        /// <summary>
        /// Variable 정보의 Update가 필요할 때 발생하는 이벤트입니다.
        /// </summary>
        /// <param name="updateType"></param>
        /// <param name="variables"></param>
        private void GemDriver_OnVariableUpdateRequest(VariableUpdateType updateType, List<VariableInfo> variables)
        {
            /* <OnVariableUpdateRequest Event의 경우>
             * 1. 호스트가 S1F3 Message를 Send 하였을 때
             * 2. 호스트가 S6F19 Message를 Send 하였을 때
             * 3. ReportCollectionEvent(string) API를 사용할 경우.
             */

            // List Type Variable의 데이터 설정 방법
            // VID=2000 이고 구조가 아래와 같고, n = 5, m = 4 인 경우
            // Ln DataList
            //    L3 DataInfo
            //       A DataID
            //       U1 SubDataCount
            //       Lm SubDataList
            //          L2 SubDataInfo
            //              A SubDataID
            //              U1 SubDataNo

            /*
            VariableInfo dataList = new VariableInfo() { VID = "2000", Format = SECSItemFormat.L, Name = "DataList" };

            for (int i = 0; i < 5; i++)
            {
                int subCount = 4;

                VariableInfo dataInfo = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "DataInfo" };

                dataList.ChildVariables.Add(dataInfo);

        		dataInfo.ChildVariables.Add(new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "DataID", Value = "DataID" });
                dataInfo.ChildVariables.Add(new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "SubDataCount", Value = subCount });
                
                VariableInfo subDataList = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "SubDataList" };
		        dataInfo.ChildVariables.Add(subDataList);

		        for(int j = 0; j < subCount; j++)
		        {
			        VariableInfo subDataInfo = new VariableInfo() { VID = "", Format = SECSItemFormat.L, Name = "SubDataInfo" };
			        subDataList.ChildVariables.Add(subDataInfo);

                    subDataInfo.ChildVariables.Add(new VariableInfo() { VID = "", Format = SECSItemFormat.A, Name = "SubDataID", Value = "SubDataID" });

                    subDataInfo.ChildVariables.Add(new VariableInfo() { VID = "", Format = SECSItemFormat.U1, Name = "SubDataNo", Value = j });
		        }
	        }

            _gemDriver.SetVariable(dataList);
            */

            foreach (VariableInfo variableInfo in variables)
            {
                if (variableInfo.VID == Ubisam.DefinedV.Alarmset)
                {
                    // Format 'L'의 ChildVariable 'n'개 값 설정 방법
                    VariableInfo alarmSet = _gemDriver.Variables[Ubisam.DefinedV.Alarmset];
                    VariableInfo alarmID;

                    // 상위 Variable의 ChildVariables을 Clear
                    alarmSet.ChildVariables.Clear();

                    // 하위 Variable의 개수 만큼 상위 Variable ChildVariables에 추가
                    foreach (long alid in _setAlarmList)
                    {
                        // 하위 Variable 생성 방법
                        // 1. ugc file에서 정의한 Variable의 정보를 Copy해서 생성
                        alarmID = _gemDriver.Variables[Ubisam.DefinedV.ALID].CopyTo();
                        alarmID.Value = alid;

                        // 2. 직접 Variable 객체를 생성
                        alarmID = new VariableInfo()
                        {
                            VID = Ubisam.DefinedV.ALID,
                            Name = "ALID",
                            Format = SECSItemFormat.A,
                            Length = 1,
                            Value = alid
                        };

                        alarmSet.ChildVariables.Add(alarmID);
                    }
                }
            }

            WriteLog(LogLevel.Information, "Variable Update Request");
        }

        /// <summary>
        /// 사용자 정의 GEM Message의 업데이트가 필요할 경우 발생합니다.
        /// </summary>
        /// <param name="message"></param>
        private void GemDriver_OnUserGEMMessageUpdateRequest(SECSMessage message)
        {
            WriteLog(LogLevel.Information, "GEM Message Update Request");
        }

        /// <summary>
        /// Trace Data를 발송하기 위해 Variable의 Update가 필요한 경우 발생합니다.
        /// </summary>
        /// <param name="variables"></param>
        private void GemDriver_OnTraceDataUpdateRequest(List<VariableInfo> variables)
        {
            WriteLog(LogLevel.Information, "Trace Data Update Request");
        }
        #endregion

        #region [Process Program]
        private bool MakePPBody(out List<byte> ppbody)
        {
            bool result = true;
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            byte[] arrPPBody;
            int count;

            ppbody = new List<byte>();
            count = rand.Next(0, 1000);

            arrPPBody = new byte[count];

            rand.NextBytes(arrPPBody);

            foreach (byte data in arrPPBody)
            {
                ppbody.Add(data);
            }

            return result;
        }
        private bool ProcessProgramParsing(string ppid, bool withoutValue, out FmtPPCollection fmtPPCollection)
        {
            bool result = true;
            fmtPPCollection = new FmtPPCollection(ppid);
            if (withoutValue == false)
            {
                return false;
            }
            FmtPPCCodeInfo info = new FmtPPCCodeInfo();
            
            int subCnt = Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap.Count();

            string name = "";
            string value = "";

            foreach (var kvp in Globalo.yamlManager.vPPRecipeSpecEquip.RECIPE.ParamMap)
            {
                if(kvp.Value.use)
                {
                    name = kvp.Key;
                    value = kvp.Value.value;

                    info.Add(name, value, SECSItemFormat.A);
                }
            }
            fmtPPCollection.Items.Add(info);
            return result;

            //XElement root;
            //XElement element;
            //XElement subElement;
            //fmtPPCollection = new FmtPPCollection(ppid);

            //if (ppid == "MGL19SS06MD")
            //{
            //FmtPPCCodeInfo info;

            //try
            //{
            //    root = XElement.Load(new System.IO.StringReader(Properties.Resources.MGL19SS06MD));

            //    element = root.Element("CCodeInfoInfos");

            //    if (element != null)
            //    {
            //        foreach (XElement tempCCodeInfo in element.Elements("CCodeInfo"))
            //        {
            //            info = new FmtPPCCodeInfo
            //            {
            //                CommandCode = tempCCodeInfo.Attribute("CommandCode") != null ? tempCCodeInfo.Attribute("CommandCode").Value : string.Empty
            //            };

            //            subElement = tempCCodeInfo.Element("PPItems");

            //            if (subElement != null)
            //            {
            //                foreach (XElement tempPPARM in subElement.Elements("PPItem"))
            //                {
            //                    if (withoutValue == true)
            //                    {
            //                        string value;
            //                        SECSItemFormat format;

            //                        value = tempPPARM.Attribute("PPValue") != null ? tempPPARM.Attribute("PPValue").Value : string.Empty;
            //                        format = tempPPARM.Attribute("Format").Value != null ? ((SECSItemFormat)Enum.Parse(typeof(SECSItemFormat), tempPPARM.Attribute("Format").Value)) : SECSItemFormat.A;

            //                        info.Add(value, format);
            //                    }
            //                    else
            //                    {
            //                        string name;
            //                        string value;
            //                        SECSItemFormat format;

            //                        name = tempPPARM.Attribute("PPName") != null ? tempPPARM.Attribute("PPName").Value : string.Empty;
            //                        value = tempPPARM.Attribute("PPValue") != null ? tempPPARM.Attribute("PPValue").Value : string.Empty;
            //                        format = tempPPARM.Attribute("Format").Value != null ? ((SECSItemFormat)Enum.Parse(typeof(SECSItemFormat), tempPPARM.Attribute("Format").Value)) : SECSItemFormat.A;

            //                        info.Add(name, value, format);
            //                    }
            //                }
            //            }

            //            fmtPPCollection.Items.Add(info);
            //        }
            //    }
            //}
            //catch
            //{
            //    result = false;
            //}
            //}

            //return result;
        }
        #endregion

        #region [Log Event]
        private void GemDriver_OnWriteLog(LogLevel logLevel, string logText)
        {
            // Driver Log를 남길 때
            logText = logText.Substring(30);
            logText = logText.Substring(0, logText.Length - 2);
            WriteLog(logLevel, logText);
        }

        private void GemDriver_OnSECS1Log(LogLevel logLevel, string logText)
        {
            // SECS 1 Log를 남길 때
        }

        private void GemDriver_OnSECS2Log(LogLevel logLevel, string logText)
        {
            // SECS 2 Log를 남길 때
            logText = logText.Substring(30);
            logText = logText.Substring(0, logText.Length - 2);
            WriteLog(logLevel, logText);
        }
        #endregion


        #region [Other]
        private void UpdateTitle()
        {
            //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            this.Invoke(new Action(() =>
            {
                if (string.IsNullOrEmpty(_ugcFileName) == true)
                {
                    this.Text = PROGRAM_DEFAULT_TITLE;
                }
                else
                {
                    this.Text = string.Format(PROGRAM_TITLE_FORMAT, PROGRAM_DEFAULT_TITLE, _ugcFileName);
                }
            }));
        }

        private void UpdateTitle(string connectionState, string ipAddress, int portNo)
        {
            //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            this.Invoke(new Action(() =>
            {
                this.Text = string.Format(PROGRAM_TITLE_FORMAT, PROGRAM_DEFAULT_TITLE, _ugcFileName)
                + " - "
                + string.Format(PROGRAM_STATUS_FORMAT, connectionState, ipAddress, portNo);
            }));
        }
        private void WriteLog(LogLevel logLevel, string logText)
        {
            //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            this.Invoke(new Action(() =>
            {
                int lineCount;

                if (txtLogs != null)
                {
                    lineCount = GetLineCount();

                    if (LOG_LINE_MAX_COUNT <= lineCount)
                    {
                        //txtLogs.Document.Blocks.Remove(txtLogs.Document.Blocks.FirstBlock);
                        int lineIndex = txtLogs.GetLineFromCharIndex(0);
                        int lineLength = txtLogs.Lines[lineIndex].Length;

                        txtLogs.Select(0, lineLength);
                        txtLogs.SelectedText = ""; // 선택된 텍스트를 제거
                    }


                    //TextRange tr = new TextRange(txtLogs.Document.ContentEnd, txtLogs.Document.ContentEnd)
                    //{
                    //    Text = string.Format(DATETIME_TEXT_FORMAT, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), logLevel.ToString(), logText)
                    //};

                    string logTextFormatted = string.Format(DATETIME_TEXT_FORMAT, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), logLevel.ToString(), logText);
                    txtLogs.AppendText(logTextFormatted + Environment.NewLine);

                    

                    try
                    {
                        //txtLogs.ScrollToEnd();
                        // 스크롤을 마지막으로 이동
                        txtLogs.ScrollToCaret();
                    }
                    catch { }
                }
            }));
        }
        private int GetLineCount()
        {
            int lineCount;

            if (string.IsNullOrWhiteSpace(GetAsText()))
            {
                return 0;
            }

            lineCount = Regex.Matches(GetAsRTF(), Regex.Escape(@"\par")).Count - 1;

            return lineCount;
        }
        private string GetAsRTF()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                this.Invoke(new Action(() =>
                {
                    //TextRange textRange = new TextRange(txtLogs.Document.ContentStart, txtLogs.Document.ContentEnd);
                    //textRange.Save(memoryStream, DataFormats.Rtf);
                    // memoryStream.Seek(0, SeekOrigin.Begin);
                    byte[] rtfBytes = Encoding.ASCII.GetBytes(txtLogs.Rtf);
                    memoryStream.Write(rtfBytes, 0, rtfBytes.Length);
                    memoryStream.Seek(0, SeekOrigin.Begin);  // 스트림의 시작으로 되돌림
                }));

                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        private string GetAsText()
        {
            //return new TextRange(txtLogs.Document.ContentStart, txtLogs.Document.ContentEnd).Text;
            return txtLogs.Text;
        }
        private string CheckValidationParameterItem(int level, EnhancedCommandParameterItem enhancedCommandParameterItem, 
            RemoteCommandParameterResult paramResult, out Data.RcmdParameter parameter)
        {
            string logText = ":";
            RemoteCommandParameterResult itemResult;

            for (int i = 0; i < level; i++)
            {
                logText += " ";
            }
            level++;


            parameter.name = enhancedCommandParameterItem.Name;
            parameter.value = enhancedCommandParameterItem.Value;
            parameter.Children = new List<Data.RcmdParameter>();
            if (enhancedCommandParameterItem.Format == SECSItemFormat.L)
            {
                logText += $"[CPNAME={enhancedCommandParameterItem.Name},Format={enhancedCommandParameterItem.Format},CEPVAL={enhancedCommandParameterItem.Value}]{Environment.NewLine}";

                if (string.IsNullOrEmpty(enhancedCommandParameterItem.Name) == true)
                {
                    itemResult = new RemoteCommandParameterResult((int)CPACK.IllegalFormatSpecifiedForCPVAL);
                }
                else
                {
                    itemResult = new RemoteCommandParameterResult(enhancedCommandParameterItem.Name, (int)CPACK.IllegalFormatSpecifiedForCPVAL);
                }

                foreach (EnhancedCommandParameterItem item in enhancedCommandParameterItem.ChildParameterItem.Items)
                {
                    Data.RcmdParameter childParameter = new Data.RcmdParameter();

                    logText += CheckValidationParameterItem(level, item, itemResult, out childParameter);
                    parameter.Children.Add(childParameter);
                }
            }
            else
            {
                logText += $"[CPNAME={enhancedCommandParameterItem.Name},Format={enhancedCommandParameterItem.Format},CEPVAL={enhancedCommandParameterItem.Value}]{Environment.NewLine}";
                itemResult = new RemoteCommandParameterResult(enhancedCommandParameterItem.Name, (int)CPACK.IllegalFormatSpecifiedForCPVAL);
            }

            paramResult.ParameterListAck.Add(itemResult);

            return logText;
        }
        #endregion

        private void button_Initlalize_Click(object sender, EventArgs e)
        {
            OnMnuInitilaize();
            
        }
        private void OnMnuInitilaize()
        {
            string errorText;
            GemDriverError driverResult = _gemDriver.Initialize(_ugcFileName, out errorText);

            WriteLog(LogLevel.Error, $"Initialize Result : {driverResult}");

            if (driverResult == GemDriverError.Ok)
            {
                //cbbECID.ItemsSource = _gemDriver.Variables.ECV.Items.Where(t => t.Format != SECSItemFormat.L).ToList();
                // _gemDriver.Variables.ECV.Items에서 SECSItemFormat.L을 제외한 항목을 필터링
                var ecidItems = _gemDriver.Variables.ECV.Items.Where(t => t.Format != SECSItemFormat.L).ToList();

                // ComboBox에 항목을 추가
                cbbECID.Items.Clear();  // 기존 항목을 제거 (선택사항)
                cbbVID.Items.Clear();  // 기존 항목을 제거 (선택사항)
                cbbCE.Items.Clear();  // 기존 항목을 제거 (선택사항)

                foreach (var item in ecidItems)
                {
                    cbbECID.Items.Add(item);  // 항목 추가
                }
                //cbbVID.ItemsSource = _gemDriver.Variables.Variables.Items.Where(t => t.Format != SECSItemFormat.L).ToList();
                var vidItems = _gemDriver.Variables.Variables.Items.Where(t => t.Format != SECSItemFormat.L).ToList();
                foreach (var item in vidItems)
                {
                    cbbVID.Items.Add(item);  // 항목 추가
                }
                //cbbCE.ItemsSource = _gemDriver.CollectionEvents.Items;
                var ceItems = _gemDriver.CollectionEvents.Items;
                foreach (var item in ceItems)
                {
                    cbbCE.Items.Add(item);  // 항목 추가
                }
                //cbbUserMessage.ItemsSource = _gemDriver.UserMessage.MessageInfo;
            }
        }
        private void OnMnuStart()
        {
            // Initialize EQP Data to Driver before Connecting to Communication

            GemDriverError driverResult = _gemDriver.Start();
            WriteLog(LogLevel.Error, $"Driver Start Result : {driverResult}");
        }
        private void OnMnuStop()
        {
            _gemDriver.Stop();
        }
        private void button_Start_Click(object sender, EventArgs e)
        {
            OnMnuStart();
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            OnMnuStop();
            
        }

        private void button_OpenUgc_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = UGC_FILE_FILTER
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _ugcFileName = openFileDialog.FileName;
                UpdateTitle();
            }
        }

        private void btnSetECIDValue_Click(object sender, EventArgs e)
        {
            VariableInfo varInfo;

            if (cbbECID.SelectedItem != null)
            {
                varInfo = cbbECID.SelectedItem as VariableInfo;

                if (varInfo != null)
                {
                    GemDriverError driverResult = _gemDriver.SetEquipmentConstant(varInfo.VID, txtECIDValue.Text);

                    WriteLog(LogLevel.Information, $"Set ECID : {varInfo.VID}, Value : {varInfo.Value}, Result : {driverResult}");
                }
            }
        }

        private void btnListECIDValue_Click(object sender, EventArgs e)
        {
            // SetEquipmentConstant(List<string>, List<object>) 사용은 ECV의 Value 변경되었음을 List로 보고 할 경우입니다.

            List<string> ecids = new List<string>();
            List<object> values = new List<object>();

            // 변경된 ECV의 ID와 값을 쌍으로 구성하여 보고

            GemDriverError driverResult = _gemDriver.SetEquipmentConstant(ecids, values);

            WriteLog(LogLevel.Error, $"Set ECID Value List Result={driverResult}");
        }

        private void btnSetVIDValue_Click(object sender, EventArgs e)
        {
            // SetVariable(string, object) API사용은 DVVAL/SV의 Value 변경되었음을 보고 할 경우입니다.
            VariableInfo varInfo;

            varInfo = cbbVID.SelectedItem as VariableInfo;

            if (varInfo != null)
            {
                GemDriverError driverResult = _gemDriver.SetVariable(varInfo.VID, txtVIDValue.Text);

                WriteLog(LogLevel.Information, $"Set VID : {varInfo.VID}, Value : {varInfo.Value}, Result : {driverResult}");
            }
        }
        private void btnSetVariableList_Click(object sender, EventArgs e)
        {
            // SetVariable(List<string>, List<object>) API사용은 DVVAL/SV의 Value 변경되었음을 List로 보고 할 경우입니다.

            List<string> vids = new List<string>();
            List<object> values = new List<object>();

            // 변경된 SV/DVVAL의 ID와 값을 쌍으로 구성하여 보고

            GemDriverError driverResult = _gemDriver.SetVariable(vids, values);

            WriteLog(LogLevel.Error, $"Set Variable Value List Result : {driverResult}");
        }
        private void btnReport1_Click(object sender, EventArgs e)
        {
            // ReportCollectionEvent(string) API의 사용은 미리 정의된 Collection Event를 보고할 경우 입니다.
            // OnVariableUpdateRequest Event 발생 합니다.
            // OnVariableUpdateRequest Event 내에서 Variable의 값을 설정 하는것도 가능합니다.

            CollectionEventInfo ceInfo;
            KeyValuePair<string, CollectionEventInfo> selectedItem;

            if (cbbCE.SelectedItem != null)
            {
                selectedItem = (KeyValuePair<string, CollectionEventInfo>)cbbCE.SelectedItem;
                ceInfo = selectedItem.Value;
                // EquipmentConstantChanged 관련 Collection Event 는 직접적으로 호출하면 안됩니다.
                if (ceInfo != null && ceInfo.Name != "EquipmentConstantChanged" && ceInfo.Name != "EquipmentConstantChangedbyhost")
                {
                    // OnVariableUpdateRequest Event에서 값을 설정 하지 않고, ReportCollectionEvent(string) 호출 이전에 설정해도 됩니다.
                     _gemDriver.Variables[DefinedV.ControlState].Value = 5;

                    GemDriverError driverResult = _gemDriver.ReportCollectionEvent(ceInfo.CEID);

                    WriteLog(LogLevel.Error, $"Report Collection Event Result : {driverResult}");
                }
            }
        }

        private void btnReport2_Click(object sender, EventArgs e)
        {
            // ReportCollectionEvent(CollectionEventInfo) API는 Report 하려는 Collection Event의 구조가 복잡할 경우 사용하기 좋습니다.
            // Collection Event를 Code로 구성하여 Report 합니다.
            // ※ 호스트에서 DefineReport를 사용하는 업체는 ReportCollectionEvent(CollectionEventInfo) API를 사용하실 경우, Code 수정이 불가피 합니다.

            // Variable Value 값 설정은 두가지 방법을 제시합니다.
            // 1. new로 새로운 VariableInfo 생성
            //  ▶ new VariableInfo() { VID = "", Name = "", Format = SECSItemFormat.A, Value = "Value" }
            // ※ Name은 Log를 찍을 때 표시됩니다.

            // 2. GEM Driver 내 VariableCollection에서 해당 Variable CopyTo()
            //  ▶ GemDriver.Variables[VID].Value = "Value";
            //  ▶ ReportInfo.Variables.Add(GemDriver.Variables[VID].CopyTo());

            if (cbbCE.SelectedItem != null)
            {
                KeyValuePair<string, CollectionEventInfo> selectedItem = (KeyValuePair<string, CollectionEventInfo>)cbbCE.SelectedItem;

                // CollectionEvent를 완전히 새로 구성하기 때문에 새로운 객체를 생성합니다.
                CollectionEventInfo ceInfo = new CollectionEventInfo() { CEID = selectedItem.Value.CEID, IsUse = true, Enabled = true };

                // EquipmentConstantChanged 관련 Collection Event는 직접적으로 호출하면 안됩니다.
                if (ceInfo != null && ceInfo.Name != "EquipmentConstantChanged" && ceInfo.Name != "EquipmentConstantChangedbyhost")
                {
                    // Collection Event 구조 생성
                    ReportInfo rptInfo;

                    rptInfo = new ReportInfo() { ReportID = "1" };

                    rptInfo.Variables.Add(new VariableInfo() { Name = "DeviceID", Format = SECSItemFormat.A, Value = "0" });
                    rptInfo.Variables.Add(new VariableInfo() { Name = "ControlState", Format = SECSItemFormat.U1, Value = 5 });

                    ceInfo.Reports.Add(rptInfo);

                    GemDriverError driverResult = _gemDriver.ReportCollectionEvent(ceInfo);
                    WriteLog(LogLevel.Error, $"Report Collection Event Result : {driverResult}");
                }
            }
        }
        private void btnReportTest_Click(object sender, EventArgs e)
        {
            EventReportSendFn(ReportConstants.OFFLINE_CHANGED_REPORT_10102, "");
        }
        private void btnProcessingStateChange_Click(object sender, EventArgs e)
        {
            // ReportEquipmentProcessingState(byte) API사용은 Equipment Processing States(장비 프로세싱 상태)에 변경이 있어 호스트로 보고 할 경우입니다.
            byte processState;
            if (byte.TryParse(txtEQPProcessingState.Text, out processState))
            {
                GemDriverError driverResult = _gemDriver.ReportEquipmentProcessingState(processState);
                WriteLog(LogLevel.Error, $"Report Equipment Processing State Result : {driverResult}");
            }
        }

        private void btnSetAlarm_Click(object sender, EventArgs e)
        {
            long alarmID;
            if (long.TryParse(txtAlarm.Text, out alarmID) == true)
            {
                GemDriverError driverResult = _gemDriver.ReportAlarmSet(alarmID);

                WriteLog(LogLevel.Error, $"Set Alarm Result : {driverResult}");

                if (driverResult == GemDriverError.Ok)
                {
                    UpdateSetAlarmList(alarmID, true);
                }
            }
        }

        private void btnClearAlarm_Click(object sender, EventArgs e)
        {
            long alarmID;
            if (long.TryParse(txtAlarm.Text, out alarmID) == true)
            {
                GemDriverError driverResult = _gemDriver.ReportAlarmClear(alarmID);
                WriteLog(LogLevel.Error, $"Clear Alarm Result : {driverResult}");

                if (driverResult == GemDriverError.Ok)
                {
                    UpdateSetAlarmList(alarmID, false);
                }
            }
        }
        private void UpdateSetAlarmList(long alarmID, bool isSet)
        {
            // GEM Driver는 Set Alarm List를 관리하지 않습니다.

            if (isSet == true)
            {
                _setAlarmList.Add(alarmID);
            }
            else
            {
                _setAlarmList.Remove(alarmID);
            }
        }

        private void btnReportTerminalMessage_Click(object sender, EventArgs e)
        {
            int tid;
            if (int.TryParse(txtTerminalTID.Text, out tid) == true)
            {
                GemDriverError driverResult = _gemDriver.ReportTerminalMessage(tid, txtTerminalMessage.Text);

                WriteLog(LogLevel.Error, $"Report Terminal Message Result : {driverResult}");
            }
        }
        #region [Terminal Message]
        private void btnReportTerminalMessage_Click(object sender, RoutedEventArgs e)
        {
            int tid;
            if (int.TryParse(txtTerminalTID.Text, out tid) == true)
            {
                GemDriverError driverResult = _gemDriver.ReportTerminalMessage(tid, txtTerminalMessage.Text);

                WriteLog(LogLevel.Error, $"Report Terminal Message Result : {driverResult}");
            }
        }
        #endregion

        private void  OnMnuOffLIne()
        {
            GemDriverError driverResult = _gemDriver.RequestOffline();
            WriteLog(LogLevel.Error, $"Request Offline Result={driverResult}");
        }
        private void button_Offline_Click(object sender, EventArgs e)
        {
            OnMnuOffLIne();
        }

        private void button_OnlineRemote_Click(object sender, EventArgs e)
        {
            GemDriverError driverResult = _gemDriver.RequestOnlineRemote();

            WriteLog(LogLevel.Error, $"Request Online Remote Result={driverResult}");
        }

        public void RequestOfflineFn()
        {
            GemDriverError driverResult = _gemDriver.RequestOffline();
        }
        public void RequestOnlineRemoteFn()
        {
            GemDriverError driverResult = _gemDriver.RequestOnlineRemote();
        }

        private void button_Ubisam_Close_Click(object sender, EventArgs e)
        {
            Globalo.MainForm.Enabled = true;
            this.Visible = false;
        }

        private void UbisamForm_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
            {
                // 화면 중앙에 폼 표시
                // 폼을 항상 위에 표시
                this.TopMost = true;
                this.CenterToScreen();
            }
        }

        private void button_PPRequest_Click(object sender, EventArgs e)
        {
            string ppid;

            ppid = string.IsNullOrEmpty(txtPPID.Text) == true ? "MGL19SS06MD" : txtPPID.Text;

            GemDriverError driverResult = _gemDriver.RequestPPRequest(ppid);

            WriteLog(LogLevel.Error, $"Request PP Request Result : {driverResult}");
        }

        private void button_PPSend_Click(object sender, EventArgs e)
        {
            string ppid;
            List<byte> ppbody;

            ppid = string.IsNullOrEmpty(txtPPID.Text) == true ? "MGL19SS06MD" : txtPPID.Text;

            MakePPBody(out ppbody);

            GemDriverError driverResult = _gemDriver.RequestPPSend(ppid, ppbody);

            WriteLog(LogLevel.Error, $"Request PP Send Result : {driverResult}");
        }

        private void button_PPLoadInquire_Click(object sender, EventArgs e)
        {
            string ppid;
            List<byte> ppbody;

            ppid = string.IsNullOrEmpty(txtPPID.Text) == true ? "MGL19SS06MD" : txtPPID.Text;

            MakePPBody(out ppbody);

            GemDriverError driverResult = _gemDriver.RequestPPLoadInquire(ppid, ppbody.Count);
            WriteLog(LogLevel.Error, $"Request PP Load Inquire Result : {driverResult}");
        }

        private void button_PPChanged_Click(object sender, EventArgs e)
        {
            string ppid = string.Empty;
            int ppstate = (int)ProcessProgramChangeState.Credited;

            //ispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            this.Invoke((MethodInvoker)delegate
            {
                ppid = string.IsNullOrEmpty(txtPPID.Text) == true ? "MGL19SS06MD" : txtPPID.Text;
            });

            GemDriverError driverResult = _gemDriver.RequestPPChanged(ppstate, ppid);
            WriteLog(LogLevel.Error, $"Request PP Changed Result : {driverResult}");
        }
        public void FormattedProcessProgramRequest(string ppId)
        {
            GemDriverError driverResult = _gemDriver.RequestFmtPPRequest(ppId);
            WriteLog(LogLevel.Error, $"Request Fmt PP Request Result : {driverResult}");
        }
        private void btnRequestFmtPPRequest_Click(object sender, EventArgs e)
        {
            string ppid = string.IsNullOrEmpty(txtFMTPPID.Text) == true ? "MGL19SS06MD" : txtFMTPPID.Text;
            


            FormattedProcessProgramRequest(ppid);
        }

        private void btnRequestFmtPPChanged_Click(object sender, EventArgs e)
        {
            string ppid = string.Empty;
            int fmtPPstate = (int)ProcessProgramChangeState.Credited;

            //Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            this.Invoke((MethodInvoker)delegate
            {
                ppid = string.IsNullOrEmpty(txtFMTPPID.Text) == true ? "MGL19SS06MD" : txtFMTPPID.Text;
            });

            GemDriverError driverResult = _gemDriver.RequestFmtPPChanged(fmtPPstate, ppid);
            WriteLog(LogLevel.Error, $"Request Fmt PP Changed Result : {driverResult}");
        }

        private void btnRequestFmtPPSendWithoutValue_Click(object sender, EventArgs e)
        {
            FmtPPCollection fmtPPCollection;
            string ppid = string.IsNullOrEmpty(txtFMTPPID.Text) == true ? "MGL19SS06MD" : txtFMTPPID.Text;
            ProcessProgramParsing(ppid, true, out fmtPPCollection);

            GemDriverError driverResult = _gemDriver.RequestFmtPPSendWithoutValue(fmtPPCollection);
            WriteLog(LogLevel.Error, $"Request Fmt PP Send Without Value Result : {driverResult}");
        }

        private void btnRequestFmtPPSend_Click(object sender, EventArgs e)
        {
            FmtPPCollection fmtPPCollection;
            string ppid = string.IsNullOrEmpty(txtFMTPPID.Text) == true ? "MGL19SS06MD" : txtFMTPPID.Text;
            ProcessProgramParsing(ppid, false, out fmtPPCollection);

            GemDriverError driverResult = _gemDriver.RequestFmtPPSend(fmtPPCollection);
            WriteLog(LogLevel.Error, $"Request Fmt PP Send Result : {driverResult}");
        }

        private void btnRequestFmtPPVerificationSend_Click(object sender, EventArgs e)
        {
            string ppid = string.IsNullOrEmpty(txtFMTPPID.Text) == true ? "MGL19SS06MD" : txtFMTPPID.Text;
            FmtPPVerificationCollection fmtPPCollection = new FmtPPVerificationCollection(ppid);
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < 10; i++)
            {
                FmtPPVerificationInfo info = new FmtPPVerificationInfo
                {
                    ACK = _ack,
                    SeqNum = rand.Next(0, 1000),
                    ErrW7 = $"ERR{rand.Next(0, 1000)}",
                };

                fmtPPCollection.Items.Add(info);
            }

            GemDriverError driverResult = _gemDriver.RequestFmtPPVerificationSend(fmtPPCollection);
            WriteLog(LogLevel.Error, $"Request Fmt PP Verification Send Result : {driverResult}");
        }
    }
}
