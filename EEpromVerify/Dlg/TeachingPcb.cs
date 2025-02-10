using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApsMotionControl.Dlg
{
    public partial class TeachingPcb : UserControl
    {
        private MotorControl motorControl;
        //public DataGridView PcbTeachGridView = new DataGridView();
        private int[] inGridWid = new int[] { 150, 80, 80, 80, 80, 80, 80 };
        private const int nGridSensorRowCount = 6;
        private const int nGridSpeedRowCount = 3;
        private int nGridRowCount = Data.TeachingData.MAX_TEACHPOS_COUNT + nGridSensorRowCount + nGridSpeedRowCount;
        private Timer TeachingPcbTimer;
        private Button[] TeachPcbBtnArr = new Button[6];
        public int SelectPcbAxis = -1;
        int dGridStartX = 180;
        int dGridStartY = 30;

        int dRowSensorHeight = 30;
        int dRowHeight = 45;//35;

        public TeachingPcb()
        {
            InitializeComponent();
           
            motorControl = Globalo.motorControl;

            TeachPcbGridInit();
            TeachPcbUiSet();
            TeachingPcbTimer = new Timer();
            TeachingPcbTimer.Interval = 300; // 1초 (1000밀리초) 간격 설정
            TeachingPcbTimer.Tick += new EventHandler(TeachingPcb_Timer_Tick);
            changeMotorNo((int)MotorControl.ePcbMotor.PCB_X);
        }
        public void TeachPcbUiSet()
        {
            int i = 0;

            TeachPcbBtnArr[0] = BTN_TEACH_PCB_X;
            TeachPcbBtnArr[1] = BTN_TEACH_PCB_Y;
            TeachPcbBtnArr[2] = BTN_TEACH_PCB_Z;
            TeachPcbBtnArr[3] = BTN_TEACH_PCB_TH;
            TeachPcbBtnArr[4] = BTN_TEACH_PCB_TX;
            TeachPcbBtnArr[5] = BTN_TEACH_PCB_TY;

            for (i = 0; i < TeachPcbBtnArr.Length; i++)
            {
                TeachPcbBtnArr[i].Text = motorControl.PCB_MOTOR_NAME[i];
                TeachPcbBtnArr[i].BackColor = ColorTranslator.FromHtml("#C3A279");
                TeachPcbBtnArr[i].ForeColor = Color.White;
            }

            BTN_TEACH_SERVO_ON.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SERVO_ON.ForeColor = Color.White;

            BTN_TEACH_SERVO_OFF.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SERVO_OFF.ForeColor = Color.White;

            BTN_TEACH_SERVO_RESET.BackColor = ColorTranslator.FromHtml("#C3A279");
            BTN_TEACH_SERVO_RESET.ForeColor = Color.White;

            // MotorBtnArr[i].FlatAppearance.BorderColor = ColorTranslator.FromHtml("#BBBBBB");
        }
        public void TeachPcbGridInit()
        {
            //GRID
            int i = 0;
            int dGridWidth = 0;
            int dGridHeight = (nGridSpeedRowCount * dRowHeight) + (nGridSensorRowCount * dRowSensorHeight) + (Data.TeachingData.MAX_TEACHPOS_COUNT * dRowHeight);
            int scrollWidth = 3;// 20;
            //
            this.groupTeachPcb.Controls.Add(PcbTeachGridView);
            dGridWidth = inGridWid[0] + inGridWid[1] + inGridWid[2] + inGridWid[3] + inGridWid[4] + inGridWid[5] + inGridWid[6];


            PcbTeachGridView.ColumnCount = MotorControl.PCB_UNIT_COUNT + 1;// oGlobal.MAX_MOTOR_COUNT + 1;
            PcbTeachGridView.EnableHeadersVisualStyles = false;
            PcbTeachGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; //사이즈 조절 막기
            PcbTeachGridView.RowCount = nGridRowCount;
            //PcbTeachGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            //PcbTeachGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            PcbTeachGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            PcbTeachGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Yellow;
            PcbTeachGridView.ColumnHeadersDefaultCellStyle.Font = new Font(PcbTeachGridView.Font, FontStyle.Bold);
            //PcbTeachGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;    //마우스 사이즈 조절 막기 Height
            //PcbTeachGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            PcbTeachGridView.AllowUserToResizeRows = false;
            PcbTeachGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            PcbTeachGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            PcbTeachGridView.Name = "PcbTeachGridView";
            PcbTeachGridView.Location = new Point(dGridStartX, dGridStartY);
            // PcbTeachGridView.Size = new Size(dGridWidth + 3, dRowHeight * (nGridRowCount + 2) - dRowHeight);//(nGridRowCount + 2)));
            //PcbTeachGridView.Size = new Size(dGridWidth + scrollWidth, dRowHeight * nGridRowCount + dRowHeight + 2);//(nGridRowCount + 2)));
            PcbTeachGridView.Size = new Size(dGridWidth + scrollWidth, dGridHeight + dRowHeight + 2);

            

            PcbTeachGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            PcbTeachGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            PcbTeachGridView.GridColor = Color.Black;
            PcbTeachGridView.RowHeadersVisible = false;
            /// PcbTeachGridView.SelectionMode = DataGridViewSelectionMode.
            PcbTeachGridView.CellClick += TeachGrid_CellClick;
            PcbTeachGridView.CellDoubleClick += TeachGrid_CellDoubleClick;
            //InGridContentChange(oGlobal.Maindata.dCurReadModuleCh);

            for (i = 0; i < MotorControl.PCB_UNIT_COUNT + 1; i++)
            {
                if (i > 0)
                {
                    PcbTeachGridView.Columns[i].Name = motorControl.PcbMotorAxis[i - 1].Name;
                    PcbTeachGridView.Columns[i].DefaultCellStyle.Format = "N3";     //소수점 3째자리 표현
                }
                PcbTeachGridView.Columns[i].Resizable = DataGridViewTriState.False;
                PcbTeachGridView.Columns[i].Width = inGridWid[i];
                PcbTeachGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                PcbTeachGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            PcbTeachGridView.ColumnHeadersHeight = dRowHeight;
            for (i = 0; i < nGridRowCount; i++)
            {
                if (i < 6)
                {
                    PcbTeachGridView.Rows[i].Height = dRowSensorHeight;
                }
                else
                {
                    PcbTeachGridView.Rows[i].Height = dRowHeight;
                }
            }
            int index = 0;
            PcbTeachGridView.Rows[0].SetValues("원점상태");
            PcbTeachGridView.Rows[1].SetValues("ServoOn");
            PcbTeachGridView.Rows[2].SetValues("Alarm");
            PcbTeachGridView.Rows[3].SetValues("Limit(+)");
            PcbTeachGridView.Rows[4].SetValues("HOME");
            PcbTeachGridView.Rows[5].SetValues("Limit(-)");
            PcbTeachGridView.Rows[6].SetValues("속도(mm/s)");
            PcbTeachGridView.Rows[7].SetValues("가속도(sec)");

            for (i = 0; i < 6; i++)
            {
                //row header 선택 색 변화 금지
                PcbTeachGridView.Rows[i].DefaultCellStyle.SelectionBackColor = PcbTeachGridView.DefaultCellStyle.BackColor;
                PcbTeachGridView.Rows[i].DefaultCellStyle.SelectionForeColor = PcbTeachGridView.DefaultCellStyle.ForeColor;
            }
            for (i = 0; i < Data.TeachingData.MAX_TEACHPOS_COUNT; i++)
            {
                PcbTeachGridView.Rows[i + 8].SetValues(Globalo.dataManage.teachingData.TEACH_POS_NAME[index]);//TEACH_POS_NAME[index]);
                index++;


            }


            
            PcbTeachGridView.Rows[nGridRowCount - 1].SetValues("현재위치");
            PcbTeachGridView[1, nGridRowCount - 1].Value = "11.1";
            PcbTeachGridView[2, nGridRowCount - 1].Value = "11.2";
            PcbTeachGridView[3, nGridRowCount - 1].Value = "11.3";
            PcbTeachGridView[4, nGridRowCount - 1].Value = "11.4";
            PcbTeachGridView[5, nGridRowCount - 1].Value = "11.5";
            PcbTeachGridView[6, nGridRowCount - 1].Value = "11.6";

            PcbTeachGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            PcbTeachGridView.ReadOnly = true;
            PcbTeachGridView.CurrentCell = null;
            //PcbTeachGridView.DefaultCellStyle.SelectionBackColor = PcbTeachGridView.DefaultCellStyle.BackColor;
            //PcbTeachGridView.DefaultCellStyle.SelectionForeColor = PcbTeachGridView.DefaultCellStyle.ForeColor;

            //PcbTeachGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            PcbTeachGridView.MultiSelect = false;
        }
        public void showPanel()
        {
            if (ProgramState.ON_LINE_MOTOR == true)
            {
                TeachingPcbTimer.Start();
            }
            ShowTeachingData();
        }
        public void hidePanel()
        {
            TeachingPcbTimer.Stop();
        }

        public void getDataPanel()
        {
            GetTeachData();
        }
        private void TeachingPcb_Timer_Tick(object sender, EventArgs e)
        {
            int i = 0;
            // 현재 시간을 Label에 표시
            //timeLabel.Text = "Current Time: " + DateTime.Now.ToString("HH:mm:ss");
            //모터 센서 감지 상태
            //모터 현재 위치

            // PcbTeachGridView[i + 1, nGridRowCount - 1]
            //PcbTeachGridView[2, 6].Value = $"Cell Chage";   //[MotorIndex 옆으로 , Grid아래로 Row]
            for (i = 0; i < MotorControl.PCB_UNIT_COUNT; i++)
            {
                //PcbTeachGridView[i + 1, 0] = 원점 상태
                if (motorControl.PcbMotorAxis[i].bOrgState == true)
                {
                    PcbTeachGridView[i + 1, 0].Style.BackColor = Color.LightGreen;
                }
                else
                {
                    PcbTeachGridView[i + 1, 0].Style.BackColor = Color.White;
                }
                if (motorControl.PcbMotorAxis[i].GetServoOn() == true)
                {
                    PcbTeachGridView[i + 1, 1].Style.BackColor = Color.LightGreen;
                }
                else
                {
                    PcbTeachGridView[i + 1, 1].Style.BackColor = Color.White;
                }
                if (motorControl.PcbMotorAxis[i].GetAmpFault() == true)
                {
                    PcbTeachGridView[i + 1, 2].Style.BackColor = Color.Red;
                }
                else
                {
                    PcbTeachGridView[i + 1, 2].Style.BackColor = Color.White;
                }
                if (motorControl.PcbMotorAxis[i].GetPosiSensor() == true)
                {
                    PcbTeachGridView[i + 1, 3].Style.BackColor = Color.Red;
                }
                else
                {
                    PcbTeachGridView[i + 1, 3].Style.BackColor = Color.White;
                }
                if (motorControl.PcbMotorAxis[i].GetHomeSensor() == true)
                {
                    PcbTeachGridView[i + 1, 4].Style.BackColor = Color.Green;
                }
                else
                {
                    PcbTeachGridView[i + 1, 4].Style.BackColor = Color.White;
                }
                if (motorControl.PcbMotorAxis[i].GetNegaSensor() == true)
                {
                    PcbTeachGridView[i + 1, 5].Style.BackColor = Color.Red;
                }
                else
                {
                    PcbTeachGridView[i + 1, 5].Style.BackColor = Color.White;
                }

                PcbTeachGridView[i + 1, nGridRowCount - 1].Value = motorControl.PcbMotorAxis[i].GetEncoderPos();
            }

        }
        private void changeMotorNo(int MotorNo, int nRow = -1)
        {
            int i = 0;
            if (MotorNo < 0)
            {
                return;
            }
            

            for (i = 0; i < TeachPcbBtnArr.Length; i++)
            {
                TeachPcbBtnArr[i].BackColor = ColorTranslator.FromHtml("#C3A279");
                TeachPcbBtnArr[i].ForeColor = Color.White;
            }
            TeachPcbBtnArr[MotorNo].BackColor = ColorTranslator.FromHtml("#4C4743");

            for (i = 0; i < Data.TeachingData.MAX_TEACHPOS_COUNT + 3; i++)
            {
                PcbTeachGridView[SelectPcbAxis + 1, 6 + i].Style.BackColor = Color.White;
            }
            SelectPcbAxis = (int)MotorNo;
            for (i = 0; i < Data.TeachingData.MAX_TEACHPOS_COUNT + 3; i++)
            {
                PcbTeachGridView[SelectPcbAxis + 1, 6 + i].Style.BackColor = ColorTranslator.FromHtml("#E1E0DF");   //E1E0DF , FFB230
                //Color.BurlyWood;
            }

            //PcbTeachGridView[1, 0].Style.BackColor = Color.LightSkyBlue;
            //
            for (i = 0; i < MotorControl.PCB_UNIT_COUNT + 1; i++)//for (i = 0; i < MotorControl.MAX_MOTOR_COUNT + 1; i++)
            {
                PcbTeachGridView.Columns[i].HeaderCell.Style.BackColor = Color.White; //ColorTranslator.FromHtml("#E1E0DF");
                //Color.Aqua;
            }

            //int nCol = (int)MotorNo + 1;
           // PcbTeachGridView.Columns[nCol].HeaderCell.Style.BackColor = Color.BurlyWood;
        }
        private void ShowTeachingData()
        {
            int i = 0;
            int j = 0;
            double dpos = 0.0;
            string formattedValue = "";
            for (i = 0; i < MotorControl.PCB_UNIT_COUNT; i++)        //모터 수
            {
                formattedValue = Globalo.dataManage.teachingData.PcbMotorData.dMotorVel[i].ToString("0.00#");
                PcbTeachGridView[i + 1, 6].Value = formattedValue;  //속도
                formattedValue = Globalo.dataManage.teachingData.PcbMotorData.dMotorAcc[i].ToString("0.00#");
                PcbTeachGridView[i + 1, 7].Value = formattedValue;     //가속도


                for (j = 0; j < Data.TeachingData.MAX_TEACHPOS_COUNT; j++)
                {
                    dpos = 10.0 + j;  //속도;
                    formattedValue = Globalo.dataManage.teachingData.PcbTeachData[j].dPos[i].ToString("0.00#");//dpos.ToString("0.00");//("0.0##");

                    PcbTeachGridView[i + 1, 8 + j].Value = formattedValue;
                }
            }
            //PcbTeachGridView[i + 1, 8] = 8부터 티칭값
        }
        private void GetTeachData()
        {
            int i = 0;
            int j = 0;
            double doubleValue = 0.0;
            string cellValue = "";
            for (i = 6; i < nGridRowCount - 1; i++)
            {
                for (j = 0; j < MotorControl.PCB_UNIT_COUNT; j++)
                {
                    cellValue = PcbTeachGridView.Rows[i].Cells[j + 1].Value.ToString();

                    if (double.TryParse(cellValue, out doubleValue))
                    {
                        switch (i)
                        {

                            case 6: //속도(mm/s)
                                Globalo.dataManage.teachingData.PcbMotorData.dMotorVel[j] = (int)doubleValue;

                                break;
                            case 7: //가속도(sec)
                                Globalo.dataManage.teachingData.PcbMotorData.dMotorAcc[j] = doubleValue;

                                break;
                            case 8: //WaitPos
                            case 9: //alignPos
                            case 10: //LaserPos
                            case 11: //ChartPos
                            case 12: //OcPos
                                Globalo.dataManage.teachingData.PcbTeachData[i - 8].dPos[j] = doubleValue;

                                break;
                        }
                    }
                }
            }
        }

        private void TeachGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int nRow = e.RowIndex;      //세로줄 티칭위치
            int nCol = e.ColumnIndex;   //가로줄 모터
            string cellStr = "";

            int RowLimit = 0;
            RowLimit = nGridSensorRowCount + 2;// MotorControl.PCB_UNIT_COUNT;

            if ((nRow >= RowLimit &&nRow < nGridRowCount - 1) && nCol == 0)
            {
                cellStr = PcbTeachGridView.Rows[nGridRowCount - 1].Cells[SelectPcbAxis + 1].Value.ToString();

                PcbTeachGridView[SelectPcbAxis + 1, e.RowIndex].Value = cellStr;
                //sData = m_clGridTeach.GetItemText(GridRow - 1, curCol);
                //m_clGridTeach.SetItemText(nRow, curCol, sData);
                ////
                //m_clGridTeach.SetItemBkColor(nRow, curCol, GRID_COLOR_GREEN);
                ////
                //m_clGridTeach.Invalidate();
            }
        }
        private void TeachGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int nRow = e.RowIndex;      //세로줄 티칭위치
            int nCol = e.ColumnIndex;   //가로줄 모터
            string cellStr = "";
            changeMotorNo(nCol - 1, nRow);        //Grid Cell Click
            int RowLimit = 0;
            RowLimit = nGridSensorRowCount - 1;

            if ((nRow > RowLimit && nRow < nGridRowCount - 1) && nCol >= 1)//if ((nRow >= 6 && nRow < nGridRowCount - 1) && nCol >= 1)
            {
                //var cellValue = PcbTeachGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                cellStr = PcbTeachGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();


                decimal decimalValue = 0;
                if (decimal.TryParse(cellStr, out decimalValue))
                {
                    // 소수점 형식으로 변환
                    string formattedValue = decimalValue.ToString("0.00");//("0.0##");
                    NumPadForm popupForm = new NumPadForm(formattedValue);
                    if (popupForm.ShowDialog() == DialogResult.OK)
                    {
                        double dNumData = popupForm.Result;
                        switch (nRow)
                        {
                            case 6: //속도
                                if (dNumData < 1.0)
                                {
                                    dNumData = 1.0;
                                }
                                if (dNumData > 100.0)
                                {
                                    dNumData = 100.0;
                                }
                                break;
                            case 7: //가속도
                                if (dNumData <= 0.0)
                                {
                                    dNumData = 0.1;
                                }
                                if (dNumData > 1.0)
                                {
                                    dNumData = 1.0;
                                }
                                break;
                        }
                        PcbTeachGridView[e.ColumnIndex, e.RowIndex].Value = dNumData.ToString("0.00"); ;

                    }
                }
            }
        }

        private void BTN_TEACH_SERVO_ON_Click(object sender, EventArgs e)
        {
            motorControl.PcbMotorAxis[SelectPcbAxis].AmpEnable();
        }

        private void BTN_TEACH_SERVO_OFF_Click(object sender, EventArgs e)
        {
            motorControl.PcbMotorAxis[SelectPcbAxis].AmpDisable();
        }

        private void BTN_TEACH_SERVO_RESET_Click(object sender, EventArgs e)
        {
            motorControl.PcbMotorAxis[SelectPcbAxis].AmpFaultReset();
        }

        private void BTN_TEACH_PCB_X_Click(object sender, EventArgs e)
        {
            changeMotorNo((int)MotorControl.ePcbMotor.PCB_X);
        }

        private void BTN_TEACH_PCB_Y_Click(object sender, EventArgs e)
        {
            changeMotorNo((int)MotorControl.ePcbMotor.PCB_Y);
        }

        private void BTN_TEACH_PCB_Z_Click(object sender, EventArgs e)
        {
            changeMotorNo((int)MotorControl.ePcbMotor.PCB_Z);
        }

        private void BTN_TEACH_PCB_TH_Click(object sender, EventArgs e)
        {
            changeMotorNo((int)MotorControl.ePcbMotor.PCB_TH);
        }

        private void BTN_TEACH_PCB_TX_Click(object sender, EventArgs e)
        {
            changeMotorNo((int)MotorControl.ePcbMotor.PCB_TX);
        }

        private void BTN_TEACH_PCB_TY_Click(object sender, EventArgs e)
        {
            changeMotorNo((int)MotorControl.ePcbMotor.PCB_TY);
        }
    }
}
