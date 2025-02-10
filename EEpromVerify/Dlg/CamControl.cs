using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ApsMotionControl.Dlg
{
    public partial class CamControl : UserControl
    {
        public class CDRect
        {
            public CDRect()
            {
                cnt = w = 0;
                left = right = 0.0;
                top = bottom = 0.0;
                centerX = centerY = 0.0;
            }
            public int cnt;
            public int w;
            public double left;
            public double right;
            public double top;
            public double bottom;
            public double centerX;
            public double centerY;
            public uint c;
            //COLORREF c;
        };
        public enum MOUSE_CLICK_TYPE
        {
            MOUSE_DRAG = 0, TRACK_DRAG, DISP_MOVE, MEASURE, DIST_CHECK
        };
        public enum SQUARE_TYPE
        {
            SQUARE_RESET = 0, SQUARE_CREATE, SQUARE_RESIZE, SQUARE_MOVE
        };

        public enum SQUARE_DIR
        {
            STANDARD = -1, CENTER, LEFT, TOP, RIGHT, BOTTOM, LEFTTOP, LEFTBOTTOM, RIGHTTOP, RIGHTBOTTOM
        };

        public enum MOUSE_CURSOR
        {
            MOVE_ALL, MOVE_WIDTH_LEFT, MOVE_WIDTH_RIGHT, MOVE_HEIGHT_TOP, MOVE_HEIGHT_BOTTOM,
            MOVE_NW, MOVE_NE, MOVE_SW, MOVE_SE
        };
        //public PictureBox __mCCdWindow;

        public Panel mCamWindow;
        public Panel mCCdWindow;


        public int _Width;
        public int _Height;

        //Draw Var
        MOUSE_CLICK_TYPE m_nDragFlag = MOUSE_CLICK_TYPE.MOUSE_DRAG;
        //SQUARE_TYPE m_nType = SQUARE_TYPE.SQUARE_RESET;

        //double m_dZoomFac;			// ZOOM 배율
        public const int MOUSE_DRAG_SIZE_X = 20;//10
        public const int MOUSE_DRAG_SIZE_Y = 20;//10
        public const int RESET_SIZE_X = 100;
        public const int RESET_SIZE_Y = 100;

        public int m_iNo_SFR = 0;
        public bool m_bDrawLine;
        public bool m_bClickFlag;
        public System.Drawing.Point m_clClickPoint = new System.Drawing.Point();
        public CDRect m_clDRect = new CDRect();
        public Rectangle m_clRect = new Rectangle();
        public SQUARE_DIR m_nCursorType;
        public System.Drawing.Point m_clScrollPos = new System.Drawing.Point();        // V/H Scroll 현재 값
        public bool m_bDrawFlag = false;
        public bool m_bBoxMoveFlag = false;
        public bool m_bBoxMoveFlag_CCD = false;
        public Rectangle m_rBox = new Rectangle();
        public Rectangle m_rCcdBox = new Rectangle();
        MOUSE_CURSOR m_iMoveType;
        public Rectangle m_rMarkBox = new Rectangle();
        public Rectangle m_rcFixedBox = new Rectangle();

        public Rectangle m_clRectRoi = new Rectangle();

       
        
        //
        [DllImport("user32")]
        public static extern IntPtr SetCapture();// IntPtr hWnd);

        [DllImport("user32")]
        public static extern bool ReleaseCapture();

        public CamControl(int _w, int _h)
        {
            InitializeComponent();

            setInterface();

            mCamWindow.Width = _w;
            mCamWindow.Height = _h;

            mCCdWindow.Width = _w;
            mCCdWindow.Height = _h;


        }
        

        
        
        //-----------------------------------------------------------------------------
        //
        //
        //
        //-----------------------------------------------------------------------------
        private void CamDrawRect(System.Drawing.Point clPoint, System.Drawing.Point clPtSize, int DisplayMode)
        {
            int nSx, nSy, nEx, nEy, nCx, nCy;

            double dZoomX;
            double dZoomY;

            dZoomX = dZoomY = 0.0;
            nSx = nSy = nEx = nEy = nCx = nCy = 0;

            if(DisplayMode == 0)
            {
                dZoomX = 1.0;// (double)(1.0 / oGlobal.vision.M_CamReduceFactorX);
                dZoomY = 1.0;// (double)(1.0 / oGlobal.vision.M_CamReduceFactorY);
            }
            else
            {
                dZoomX = 1.0;// oGlobal.vision.M_CcdExpandFactorX;// (double)(1.0 / oGlobal.vision.M_CcdReduceFactorX);
                dZoomY = 1.0;// oGlobal.vision.M_CcdExpandFactorY;//(double)(1.0 / oGlobal.vision.M_CcdReduceFactorY);
            }

            System.Drawing.Point clPtDrawOffset = new System.Drawing.Point();
            System.Drawing.Point clPtDrawCenter = new System.Drawing.Point();

            nSx = (int)(clPoint.X * dZoomX);
            nSy = (int)(clPoint.Y * dZoomY);
            nEx = (int)(clPtSize.X * dZoomX);
            nEy = (int)(clPtSize.Y * dZoomY);

            nCx = (int)(nSx + ((clPtSize.X >> 1) / dZoomX));// m_dZoomFac));
            nCy = (int)(nSy + ((clPtSize.Y >> 1) / dZoomY));//m_dZoomFac));

            clPtDrawOffset.X = (int)(m_clScrollPos.X / dZoomX);//m_dZoomFac);
            clPtDrawOffset.Y = (int)(m_clScrollPos.Y / dZoomY);//m_dZoomFac);

            m_clRect.X = (int)(nSx);// + clPtDrawOffset.X);
            m_clRect.Y = (int)(nSy);// + clPtDrawOffset.Y);
            m_clRect.Width = (int)(nEx);
            m_clRect.Height = (int)(nEy);

            m_clRectRoi = m_clRect;

            Globalo.vision.ClearOverlay(0, DisplayMode);

            clPtDrawCenter.X = nCx + clPtDrawOffset.X;
            clPtDrawCenter.Y = nCy + clPtDrawOffset.Y;

            Globalo.vision.DrawMOverlayBox(0, 0, m_clRect, Color.Blue, 1, true, 0, DisplayMode);
            Globalo.vision.DrawMOverlayCross(0, 0, clPtDrawCenter, 20, Color.Gray, 1, true, 9);
        }
        private void CamPanel_MouseDown(object sender, MouseEventArgs e)
        {
            int iGap = 20;
            switch (m_nDragFlag)
            {
                case MOUSE_CLICK_TYPE.MOUSE_DRAG:
                    m_clClickPoint = new System.Drawing.Point(e.X, e.Y);

                    if (m_clClickPoint.X > mCamWindow.Left && m_clClickPoint.X < mCamWindow.Right &&
                        m_clClickPoint.Y > mCamWindow.Top && m_clClickPoint.Y < mCamWindow.Bottom)
                    {
                        iGap = 20;

                        m_bDrawFlag = true;

                        if (m_clClickPoint.X > m_rBox.Left - iGap &&
                            m_clClickPoint.Y > m_rBox.Top - iGap &&
                            m_clClickPoint.X < m_rBox.Right + iGap &&
                            m_clClickPoint.Y < m_rBox.Bottom + iGap)
                        {
                            m_bBoxMoveFlag = true;
                        }

                        m_iMoveType = checkMousePos(m_clClickPoint, m_rBox, 0);
                    }
                    break;
                case MOUSE_CLICK_TYPE.DIST_CHECK:

                    break;
                case MOUSE_CLICK_TYPE.DISP_MOVE:

                    break;
                case MOUSE_CLICK_TYPE.MEASURE:

                    break;
            }
        }
        private void CamPanel_MouseUp(object sender, MouseEventArgs e)
        {
            System.Drawing.Point mUpPoint = new System.Drawing.Point(e.X, e.Y);
            m_bDrawFlag = false;
            m_bBoxMoveFlag = false;
            //m_rMarkBox = m_rBox;
            //m_rcFixedBox = m_rBox;


            ReleaseCapture();
           
        }
        public void SWAP(ref int num1 , ref int num2)
        {
            int num_swap = num1;
            num1 = num2;
            num2 = num_swap;
        }
        private void CamPanel_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point m_MovePoint = new System.Drawing.Point(e.X, e.Y);


            //빠르게 움직이면 Type가 바껴서 230702
            //if (m_MovePoint.X > mCamWindow.Left || m_MovePoint.X < mCamWindow.Right ||
            //        m_MovePoint.Y > mCamWindow.Top || m_MovePoint.Y < mCamWindow.Bottom)    // && !m_bMeasureDist)
            //    {
            //        m_iMoveType = checkMousePos(m_MovePoint, m_rBox, 0);
            //    }


            changeCursor(m_MovePoint, m_rBox, 0);

            if (m_bDrawFlag)
            {
                if (m_MovePoint.X > mCamWindow.Left &&
                    m_MovePoint.X < mCamWindow.Right &&
                    m_MovePoint.Y > mCamWindow.Top &&
                    m_MovePoint.Y < mCamWindow.Bottom)
                {
                    if (m_bBoxMoveFlag)
                    {
                        int iMoveX = (int)(m_MovePoint.X + 0.5) - (int)(m_clClickPoint.X + 0.5);
                        int iMoveY = (int)(m_MovePoint.Y + 0.5) - (int)(m_clClickPoint.Y + 0.5);

                        // 이동
                        if (m_iMoveType == MOUSE_CURSOR.MOVE_ALL)
                        {
                            m_rBox.X += iMoveX;
                            m_rBox.Y += iMoveY;
                        }
                        // 좌 크기 
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_WIDTH_LEFT)
                        {
                            m_rBox.X += iMoveX;
                            m_rBox.Width += iMoveX * -1;
                        }
                        // 우 크기 
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_WIDTH_RIGHT)
                        {
                            m_rBox.Width += iMoveX;
                        }
                        // 상 크기 
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_HEIGHT_TOP)
                        {
                            m_rBox.Y += iMoveY;
                            m_rBox.Height += iMoveY * -1;
                        }
                        // 하 크기 
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_HEIGHT_BOTTOM)
                        {
                            m_rBox.Height += iMoveY;
                        }
                        // 좌상 크기 
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_NW)
                        {
                            m_rBox.X += iMoveX;
                            m_rBox.Y += iMoveY;
                            m_rBox.Width += iMoveX * -1;
                            m_rBox.Height += iMoveY * -1;
                        }
                        // 우상 크기 
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_NE)
                        {
                            m_rBox.Y += iMoveY;
                            m_rBox.Width += iMoveX;
                            m_rBox.Height += iMoveY * -1;
                        }
                        // 좌하 크기 
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_SW)
                        {
                            m_rBox.X += iMoveX;
                            m_rBox.Width += iMoveX * -1;
                            m_rBox.Height += iMoveY;
                        }
                        // 우하 크기
                        else if (m_iMoveType == MOUSE_CURSOR.MOVE_SE)
                        {
                            m_rBox.Width += iMoveX;
                            m_rBox.Height += iMoveY;
                        }

                        m_clClickPoint = m_MovePoint;
                    }
                    else
                    {
                        m_rBox.X = (int)(m_clClickPoint.X + 0.5);
                        m_rBox.Y = (int)(m_clClickPoint.Y + 0.5);
                        m_rBox.Width = (int)((m_MovePoint.X- m_clClickPoint.X) + 0.5);
                        m_rBox.Height = (int)((m_MovePoint.Y- m_clClickPoint.Y) + 0.5);
                    }
                    if (m_rBox.Left > m_rBox.Right)
                    {
                        int xTemp = m_rBox.Left;
                        int wTemp = m_rBox.Right;
                        m_rBox.X = m_rBox.Right;
                        m_rBox.Width = xTemp - wTemp;
                        //SWAP(m_rBox.Left, m_rBox.Right);
                    }
                   
                    if (m_rBox.Top > m_rBox.Bottom)
                    {
                        int yTemp = m_rBox.Top;
                        m_rBox.Y = m_rBox.Bottom;
                        m_rBox.Height = yTemp - m_rBox.Bottom;
                        //SWAP(ref m_rBox.Top, ref m_rBox.Bottom);
                    }
                    

                    m_rcFixedBox = m_rBox;

                    if (m_rBox.Left < 1)
                    {
                        m_rBox.X = 1;
                        //m_rBox.Width = 1 + m_rcFixedBox.Width;
                    }
                    if (m_rBox.Top < 1)
                    {
                        m_rBox.Y = 1;
                        //m_rBox.Height = 1 + m_rcFixedBox.Height;
                    }

                    if (m_rBox.Right >= Globalo.vision.SMALL_CAM_SIZE_X - 1)//(oGlobal.vision.CAM_SIZE_X - 1) * dReduceFactorX)
                    {
                        m_rBox.Width = (int)(Globalo.vision.SMALL_CAM_SIZE_X - m_rBox.X);
                        m_rBox.X = (int)(Globalo.vision.SMALL_CAM_SIZE_X - m_rBox.Width);
                    }
                    if (m_rBox.Bottom >= Globalo.vision.SMALL_CAM_SIZE_Y -1)//(oGlobal.vision.CAM_SIZE_Y - 1) * dReduceFactorY)
                    {
                        m_rBox.Height = (int)(Globalo.vision.SMALL_CAM_SIZE_Y - m_rBox.Y);
                        m_rBox.Y = (int)(Globalo.vision.SMALL_CAM_SIZE_Y - m_rBox.Height);
                    }

                    System.Drawing.Point p1 = new System.Drawing.Point(m_rBox.X, m_rBox.Y);
                    System.Drawing.Point p2 = new System.Drawing.Point(m_rBox.Width, m_rBox.Height);

                    CamDrawRect(p1, p2, 0);
                    //! 현재 선택된 값에 따라 사각 영역 표시를 하는 색상을 결정한다. 
                    //COLORREF clrBoxArea = GetColor_Mouse_Box(m_iMode_Mouse_Box);

                    //! 사각 영역을 그린다. 
                    //vision.clearOverlay(m_iCurCamNo);
                    //vision.boxlist[m_iCurCamNo].addList(m_rBox, PS_SOLID, clrBoxArea);

                    //if (m_iCurCamNo == 3) ccdDlg->m_pSFRDlg->drawROI();

                    //vision.drawOverlay(m_iCurCamNo);

                    
                }
                SetCapture();
            }

            /*
            System.Drawing.Point clPtCamDragStart = new System.Drawing.Point();
            System.Drawing.Point clPtCamDragSize = new System.Drawing.Point();

            System.Drawing.Point clPtDrawOffSet = new System.Drawing.Point();

            clPtDrawOffSet.X = clPtDrawOffSet.Y = 0;

            if ((m_bClickFlag == true) && (m_nDragFlag == MOUSE_CLICK_TYPE.MOUSE_DRAG))
            {
                // 박스 리사이즈
                if (m_nType == SQUARE_TYPE.SQUARE_RESIZE && m_bClickFlag == true)
                {
                    switch (m_nCursorType)
                    {
                        case SQUARE_DIR.LEFT: m_clDRect.left = m_clClickPoint.X; break;
                        case SQUARE_DIR.LEFTTOP: m_clDRect.left = m_clClickPoint.X; m_clDRect.top = m_clClickPoint.Y; break;
                        case SQUARE_DIR.LEFTBOTTOM: m_clDRect.left = m_clClickPoint.X; m_clDRect.bottom = m_clClickPoint.Y; break;
                        case SQUARE_DIR.RIGHT: m_clDRect.right = m_clClickPoint.X; break;
                        case SQUARE_DIR.RIGHTTOP: m_clDRect.right = m_clClickPoint.X; m_clDRect.top = m_clClickPoint.Y; break;
                        case SQUARE_DIR.RIGHTBOTTOM: m_clDRect.right = m_clClickPoint.X; m_clDRect.bottom = m_clClickPoint.Y; break;
                        case SQUARE_DIR.TOP: m_clDRect.top = m_clClickPoint.Y; break;
                        case SQUARE_DIR.BOTTOM: m_clDRect.bottom = m_clClickPoint.Y; break;
                    }

                    // 박스 사이즈가 너무 작은 경우
                    if ((m_clDRect.right - m_clDRect.left) < 20 || (m_clDRect.bottom - m_clDRect.top) < 20)
                    {
                        m_bClickFlag = false;

                        // 20 * 20 이하기 됐을 경우 클릭하기 편하도록 10 PIXEL 증가
                        switch (m_nCursorType)
                        {
                            case SQUARE_DIR.LEFT:
                                m_clDRect.left = m_clClickPoint.X - RESET_SIZE_X;
                                break;
                            case SQUARE_DIR.LEFTTOP:
                                m_clDRect.left = m_clClickPoint.X - RESET_SIZE_X;
                                m_clDRect.top = m_clClickPoint.Y - RESET_SIZE_Y;
                                break;
                            case SQUARE_DIR.LEFTBOTTOM:
                                m_clDRect.left = m_clClickPoint.X - RESET_SIZE_X;
                                m_clDRect.bottom = m_clClickPoint.Y + RESET_SIZE_Y;
                                break;
                            case SQUARE_DIR.RIGHT:
                                m_clDRect.right = m_clClickPoint.X + RESET_SIZE_X;
                                break;
                            case SQUARE_DIR.RIGHTTOP:
                                m_clDRect.right = m_clClickPoint.X + RESET_SIZE_X;
                                m_clDRect.top = m_clClickPoint.Y - RESET_SIZE_Y;
                                break;
                            case SQUARE_DIR.RIGHTBOTTOM:
                                m_clDRect.right = m_clClickPoint.X + RESET_SIZE_X;
                                m_clDRect.bottom = m_clClickPoint.Y + RESET_SIZE_Y;
                                break;
                            case SQUARE_DIR.TOP:
                                m_clDRect.top = m_clClickPoint.Y - RESET_SIZE_X;
                                break;
                            case SQUARE_DIR.BOTTOM:
                                m_clDRect.bottom = m_clClickPoint.Y + RESET_SIZE_Y;
                                break;
                        }

                        m_nType = SQUARE_TYPE.SQUARE_RESET;
                    }

                    m_clDRect.centerX = m_clDRect.left + ((m_clDRect.right - m_clDRect.left) / 2);
                    m_clDRect.centerY = m_clDRect.top + ((m_clDRect.bottom - m_clDRect.top) / 2);

                    ChangeCursor(m_nCursorType);
                }
                // 박스 생성
                else if (m_nType == SQUARE_TYPE.SQUARE_CREATE)
                {
                    m_clDRect.right = m_clClickPoint.X;
                    m_clDRect.bottom = m_clClickPoint.Y;
                    m_clDRect.centerX = m_clDRect.left + ((m_clDRect.right - m_clDRect.left) / 2);
                    m_clDRect.centerY = m_clDRect.top + ((m_clDRect.bottom - m_clDRect.top) / 2);
                }
                // 박스 이동
                else if (m_nType == SQUARE_TYPE.SQUARE_MOVE)
                {
                    m_clDRect.left = m_clDRect.left - (m_clDRect.centerX - m_clClickPoint.X);
                    m_clDRect.right = m_clDRect.right - (m_clDRect.centerX - m_clClickPoint.X);
                    m_clDRect.top = m_clDRect.top - (m_clDRect.centerY - m_clClickPoint.Y);
                    m_clDRect.bottom = m_clDRect.bottom - (m_clDRect.centerY - m_clClickPoint.Y);

                    m_clDRect.centerX = m_clClickPoint.X;
                    m_clDRect.centerY = m_clClickPoint.Y;

                    ChangeCursor(m_nCursorType);
                }

                clPtCamDragStart.X = (m_clDRect.left < m_clDRect.right) ? (int)m_clDRect.left : (int)m_clDRect.right;
                clPtCamDragStart.Y = (m_clDRect.top < m_clDRect.bottom) ? (int)m_clDRect.top : (int)m_clDRect.bottom;

                clPtCamDragSize.X = (int)(Math.Abs(m_clDRect.left - m_clDRect.right));
                clPtCamDragSize.Y = (int)(Math.Abs(m_clDRect.top - m_clDRect.bottom));

                CamDrawRect(clPtCamDragStart, clPtCamDragSize);
            }
            */
        }
        
        //-----------------------------------------------------------------------------
        //
        //	커서 변경
        //
        //-----------------------------------------------------------------------------
        void changeCursor(System.Drawing.Point p, Rectangle rcTemp, int DisplayMode)
        {
            double dExpandFactorX = 0.0;
            double dExpandFactorY = 0.0;
            int iGap = 0;
            if(DisplayMode == 0)
            {
                dExpandFactorX = 1.0;// oGlobal.vision.M_CamExpandFactorX;
                dExpandFactorY = 1.0;//oGlobal.vision.M_CamExpandFactorY;
                iGap = 20;
            }
            else
            {
                dExpandFactorX = Globalo.vision.M_CcdExpandFactorX;
                dExpandFactorY = Globalo.vision.M_CcdExpandFactorY;

                iGap = (int)(dExpandFactorX * 3);
            }
            
            

            System.Drawing.Point point = p;
            
            point.X = (int)(point.X * dExpandFactorX + 0.5);
            point.Y = (int)(point.Y * dExpandFactorY + 0.5);

            // 박스 이동
            if (point.X > rcTemp.Left + iGap &&
                point.X < rcTemp.Right - iGap &&
                point.Y > rcTemp.Top + iGap &&
                point.Y < rcTemp.Bottom - iGap)
            {
                this.Cursor = Cursors.SizeAll;    
            }
            // 좌 크기
            else if (point.Y > rcTemp.Top + iGap && point.Y < rcTemp.Bottom - iGap &&
                point.X > rcTemp.Left - iGap && point.X < rcTemp.Left + iGap)
            {
                this.Cursor = Cursors.SizeWE;
            }
            // 우 크기
            else if (point.Y > rcTemp.Top + iGap && point.Y < rcTemp.Bottom - iGap && 
                point.X > rcTemp.Right - iGap && point.X < rcTemp.Right + iGap)
            {
                this.Cursor = Cursors.SizeWE;
            }
            // 상 크기
            else if (point.X > rcTemp.Left + iGap && point.X < rcTemp.Right - iGap && 
                point.Y > rcTemp.Top - iGap && point.Y < rcTemp.Top + iGap)
            {
                this.Cursor = Cursors.SizeNS;
            }
            // 하 크기
            else if (point.X > rcTemp.Left + iGap && point.X < rcTemp.Right - iGap && 
                point.Y > rcTemp.Bottom - iGap && point.Y < rcTemp.Bottom + iGap)
            {
                this.Cursor = Cursors.SizeNS;
            }
            // 좌상 크기
            else if (point.X > rcTemp.Left - iGap && point.X < rcTemp.Left + iGap && 
                point.Y > rcTemp.Top - iGap && point.Y < rcTemp.Top + iGap)
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            // 우상 크기
            else if (point.X > rcTemp.Right - iGap && point.X < rcTemp.Right + iGap && 
                point.Y > rcTemp.Top - iGap && point.Y < rcTemp.Top + iGap)
            {
                this.Cursor = Cursors.SizeNESW;
            }
            // 좌하 크기
            else if (point.X > rcTemp.Left - iGap && point.X < rcTemp.Left + iGap && 
                point.Y > rcTemp.Bottom - iGap && point.Y < rcTemp.Bottom + iGap)
            {
                this.Cursor = Cursors.SizeNESW;
            }
            // 우하 크기
            else if (point.X > rcTemp.Right - iGap && point.X < rcTemp.Right + iGap && 
                point.Y > rcTemp.Bottom - iGap && point.Y < rcTemp.Bottom + 20)
            {
                this.Cursor = Cursors.SizeNWSE;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }
        public void setInterface()
        {
            //m_dZoomFac = oGlobal.vision.M_CamReduceFactorX;// ((double)SMALL_CAM_SIZE_X / (double)CAM_SIZE_X);

            mCCdWindow = this.CcdPanel;
            mCamWindow = this.CamPanel;

            mCamWindow.Visible = false;

            mCamWindow.Location = mCCdWindow.Location;
            mCamWindow.Size = mCCdWindow.Size;

            _Width = mCCdWindow.Width;
            _Height = mCCdWindow.Height;


            //mCamPanel = this.CamPanel;
            //막대 그래프
            //FirstChart.Series[0].Points.Clear();
            //FirstChart.Series[0].Points.AddXY(10, 100);
            //FirstChart.Series[0].Points[0].Color = Color.Red;
            //FirstChart.Series[0].Points.AddXY(20, 500);
            //FirstChart.Series[0].Points.AddXY(30, 300);
            //FirstChart.Series[0].Points.AddXY(40, 400);
            ////꺽은선 그래프
            //SecondChart.Series[0].Points.Clear();
            //SecondChart.ChartAreas[0].AxisY.Maximum = 1.0;
            //SecondChart.Series[0].Points.AddXY(0.01, 0.2);
            //SecondChart.Series[0].Points.AddXY(0.02, 0.3);
            //SecondChart.Series[0].Points.AddXY(0.03, 0.4);
            //SecondChart.Series[0].Points.AddXY(0.04, 0.5);
            ////Bar 그래프

            //BarChart.Series[0].Points.Clear();
            //BarChart.ChartAreas[0].AxisY.Maximum = 1.0;
            //BarChart.Series[0].Points.AddXY(0.01, 0.2);
            //BarChart.Series[0].Points.AddXY(0.02, 0.3);
            //BarChart.Series[0].Points.AddXY(0.03, 0.4);
            //BarChart.Series[0].Points.AddXY(0.04, 0.5);


            m_rBox.X = 0;
            m_rBox.Y = 0;
            m_rBox.Width = 0;
            m_rBox.Height = 0;
            m_rCcdBox.X = 0;
            m_rCcdBox.Y = 0;
            m_rCcdBox.Width = 0;
            m_rCcdBox.Height = 0;
            


            

        }
        //--------------------------------------------------------------------------------------
        //
        //ALIGN CAM
        //
        //--------------------------------------------------------------------------------------
        private MOUSE_CURSOR checkMousePos(System.Drawing.Point p, Rectangle rcTemp, int DisplayMode)
        {
            int iGap = 0;
            MOUSE_CURSOR iRtn = MOUSE_CURSOR.MOVE_ALL;

            double dExpandFactorX = 0.0;
            double dExpandFactorY = 0.0;
            if (DisplayMode == 0)
            {
                dExpandFactorX = 1.0;// oGlobal.vision.M_CamExpandFactorX;
                dExpandFactorY = 1.0;//oGlobal.vision.M_CamExpandFactorY;
                iGap = 20;
            }
            else
            {
                dExpandFactorX = Globalo.vision.M_CcdExpandFactorX;//M_CcdExpandFactorX; M_CcdReduceFactorX
                dExpandFactorY = Globalo.vision.M_CcdExpandFactorY;//M_CcdExpandFactorY; M_CcdReduceFactorY

                iGap = (int)(dExpandFactorX * 3);
            }


            System.Drawing.Point point = p;

            point.X = (int)(point.X * dExpandFactorX + 0.5);
            point.Y = (int)(point.Y * dExpandFactorY + 0.5);


            //박스 이동
            if (point.X > rcTemp.Left + iGap &&
                point.X < rcTemp.Right - iGap &&
                point.Y > rcTemp.Top + iGap &&
                point.Y < rcTemp.Bottom - iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_ALL;
            }
            // 좌 크기
            else if (point.Y > rcTemp.Top + iGap && point.Y < rcTemp.Bottom - iGap && point.X > rcTemp.Left - iGap && point.X < rcTemp.Left + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_WIDTH_LEFT;
            }
            // 우 크기
            else if (point.Y > rcTemp.Top + iGap && point.Y < rcTemp.Bottom - iGap && point.X > rcTemp.Right - iGap && point.X < rcTemp.Right + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_WIDTH_RIGHT;
            }
            // 상 크기
            else if (point.X > rcTemp.Left + iGap && point.X < rcTemp.Right - iGap && point.Y > rcTemp.Top - iGap && point.Y < rcTemp.Top + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_HEIGHT_TOP;
            }
            // 하 크기
            else if (point.X > rcTemp.Left + iGap && point.X < rcTemp.Right - iGap && point.Y > rcTemp.Bottom - iGap && point.Y < rcTemp.Bottom + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_HEIGHT_BOTTOM;
            }
            // 좌상 크기
            else if (point.X > rcTemp.Left - iGap && point.X < rcTemp.Left + iGap && point.Y > rcTemp.Top - iGap && point.Y < rcTemp.Top + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_NW;
            }
            // 우상 크기
            else if (point.X > rcTemp.Right - iGap && point.X < rcTemp.Right + iGap && point.Y > rcTemp.Top - iGap && point.Y < rcTemp.Top + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_NE;
            }
            // 좌하 크기
            else if (point.X > rcTemp.Left - iGap && point.X < rcTemp.Left + iGap && point.Y > rcTemp.Bottom - iGap && point.Y < rcTemp.Bottom + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_SW;
            }
            // 우하 크기
            else if (point.X > rcTemp.Right - iGap && point.X < rcTemp.Right + iGap && point.Y > rcTemp.Bottom - iGap && point.Y < rcTemp.Bottom + iGap)
            {
                iRtn = MOUSE_CURSOR.MOVE_SE;
            }
            else
            {
            }

            return iRtn;
        }
        private void BTN_CAM_VIEW_CCD_Click(object sender, EventArgs e)
        {
            //CCD 화면 보여주기
            mCCdWindow.Visible = true;
            mCamWindow.Visible = false;
        }

        private void BTN_CAM_VIEW_CAM_Click(object sender, EventArgs e)
        {
            //CAM 화면 보여주기
            mCamWindow.Visible = true;
            mCCdWindow.Visible = false;
        }

        private void CcdPanel_MouseDown(object sender, MouseEventArgs e)
        {
            //double dExpandFactorX = 0.0;
            //double dExpandFactorY = 0.0;

            switch (m_nDragFlag)
            {
                case MOUSE_CLICK_TYPE.MOUSE_DRAG:
                    m_clClickPoint = new System.Drawing.Point(e.X, e.Y);


                    if (m_clClickPoint.X > mCCdWindow.Left && m_clClickPoint.X < mCCdWindow.Right &&
                        m_clClickPoint.Y > mCCdWindow.Top && m_clClickPoint.Y < mCCdWindow.Bottom)
                    {
                        m_iNo_SFR = Globalo.mCCdPanel.checkNoSFR(m_clClickPoint);
                        Debug.WriteLine(m_iNo_SFR);
                        if (m_iNo_SFR >= 0 && m_iNo_SFR < Globalo.CHART_ROI_COUNT)
                        {
                            m_bBoxMoveFlag_CCD = true;
                            m_rCcdBox.X = Globalo.mCCdPanel.m_iOffsetX_SFR[m_iNo_SFR];
                            m_rCcdBox.Y = Globalo.mCCdPanel.m_iOffsetY_SFR[m_iNo_SFR];
                            m_rCcdBox.Width = Globalo.mCCdPanel.m_iSizeX_SFR[m_iNo_SFR];
                            m_rCcdBox.Height = Globalo.mCCdPanel.m_iSizeY_SFR[m_iNo_SFR];

                            m_iMoveType = checkMousePos(m_clClickPoint, m_rCcdBox, 1);
                        }
                        else if (m_iNo_SFR >= Globalo.CHART_ROI_COUNT && m_iNo_SFR < Globalo.CHART_ROI_COUNT + 4)
                        {
                            m_bBoxMoveFlag_CCD = true;

                            m_rCcdBox = Globalo.mCCdPanel.m_rcRoiBox[m_iNo_SFR - Globalo.CHART_ROI_COUNT];

                            m_iMoveType = checkMousePos(m_clClickPoint, m_rCcdBox, 1);
                        }

                        //m_iNo_SFR = ccdDlg->m_pSFRDlg->checkNoSFR(point);
                        //if (m_iNo_SFR >= 0 && m_iNo_SFR < LAST_MARK_CNT)
                        //{
                        //    m_bBoxMoveFlag_CCD = true;

                        //    m_rBox.left = ccdDlg->m_pSFRDlg->m_iOffsetX_SFR[m_iNo_SFR];
                        //    m_rBox.top = ccdDlg->m_pSFRDlg->m_iOffsetY_SFR[m_iNo_SFR];
                        //    m_rBox.right = m_rBox.left + ccdDlg->m_pSFRDlg->m_iSizeX_SFR[m_iNo_SFR];
                        //    m_rBox.bottom = m_rBox.top + ccdDlg->m_pSFRDlg->m_iSizeY_SFR[m_iNo_SFR];

                        //    m_iMoveType = checkMousePos(point, m_rBox);
                        //}
                        //else if (m_iNo_SFR >= LAST_MARK_CNT && m_iNo_SFR < LAST_MARK_CNT + 4)
                        //{
                        //    m_bBoxMoveFlag_CCD = true;

                        //    m_rBox = ccdDlg->m_pSFRDlg->m_rcRoiBox[m_iNo_SFR - LAST_MARK_CNT];

                        //    m_iMoveType = checkMousePos(point, m_rBox);
                        //}
                    }
                 break;
            }
        }

        private void CcdPanel_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point m_MovePoint = new System.Drawing.Point(e.X, e.Y);

            double dExpandFactorX = 0.0;
            double dExpandFactorY = 0.0;

            //if(m_bBoxMoveFlag_CCD)
            //{
            //    if (m_MovePoint.X > mCCdWindow.Left || m_MovePoint.X < mCCdWindow.Right ||
            //        m_MovePoint.Y > mCCdWindow.Top || m_MovePoint.Y < mCCdWindow.Bottom)// && !m_bMeasureDist)
            //    {
            //        m_iMoveType = checkMousePos(m_MovePoint, m_rCcdBox, 1);
            //    }
            //}
            

            changeCursor(m_MovePoint, m_rCcdBox, 1);


            if (m_bBoxMoveFlag_CCD)
            {
                if (m_iNo_SFR != -1)
                {
                    if (m_MovePoint.X > mCCdWindow.Left && m_MovePoint.X < mCCdWindow.Right &&
                    m_MovePoint.Y > mCCdWindow.Top && m_MovePoint.Y < mCCdWindow.Bottom)
                    {
                        dExpandFactorX = Globalo.vision.M_CcdExpandFactorX;// M_CcdExpandFactorX;
                        dExpandFactorY = Globalo.vision.M_CcdExpandFactorY;// M_CcdExpandFactorY;


                        if (m_bBoxMoveFlag_CCD)
                        {
                            int iMoveX = (int)(m_MovePoint.X * dExpandFactorX + 0.5) - (int)(m_clClickPoint.X * dExpandFactorX + 0.5);
                            int iMoveY = (int)(m_MovePoint.Y * dExpandFactorY + 0.5) - (int)(m_clClickPoint.Y * dExpandFactorY + 0.5);

                            // 이동
                            if (m_iMoveType == MOUSE_CURSOR.MOVE_ALL)
                            {
                                m_rCcdBox.X += iMoveX;
                                m_rCcdBox.Y += iMoveY;
                            }
                            // 좌 크기 
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_WIDTH_LEFT)
                            {
                                m_rCcdBox.X += iMoveX;
                                m_rCcdBox.Width += iMoveX * -1;
                            }
                            // 우 크기 
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_WIDTH_RIGHT)
                            {
                                m_rCcdBox.Width += iMoveX;
                            }
                            // 상 크기 
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_HEIGHT_TOP)
                            {
                                m_rCcdBox.Y += iMoveY;
                                m_rCcdBox.Height += iMoveY * -1;
                            }
                            // 하 크기 
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_HEIGHT_BOTTOM)
                            {
                                m_rCcdBox.Height += iMoveY;
                            }
                            // 좌상 크기 
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_NW)
                            {
                                m_rCcdBox.X += iMoveX;
                                m_rCcdBox.Y += iMoveY;
                                m_rCcdBox.Width += iMoveX * -1;
                                m_rCcdBox.Height += iMoveY * -1;
                            }
                            // 우상 크기 
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_NE)
                            {
                                m_rCcdBox.Y += iMoveY;
                                m_rCcdBox.Width += iMoveX;
                                m_rCcdBox.Height += iMoveY * -1;
                            }
                            // 좌하 크기 
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_SW)
                            {
                                m_rCcdBox.X += iMoveX;
                                m_rCcdBox.Width += iMoveX * -1;
                                m_rCcdBox.Height += iMoveY;
                            }
                            // 우하 크기
                            else if (m_iMoveType == MOUSE_CURSOR.MOVE_SE)
                            {
                                m_rCcdBox.Width += iMoveX;
                                m_rCcdBox.Height += iMoveY;
                            }

                            m_clClickPoint = m_MovePoint;
                        }
                        else
                        {
                            m_rCcdBox.X = (int)(m_clClickPoint.X + 0.5);
                            m_rCcdBox.Y = (int)(m_clClickPoint.Y + 0.5);
                            m_rCcdBox.Width = (int)((m_MovePoint.X - m_clClickPoint.X) + 0.5);
                            m_rCcdBox.Height = (int)((m_MovePoint.Y - m_clClickPoint.Y) + 0.5);
                        }


                        if (m_rCcdBox.Left > m_rCcdBox.Right)
                        {
                            int xTemp = m_rCcdBox.Left;
                            int wTemp = m_rCcdBox.Right;
                            m_rCcdBox.X = m_rCcdBox.Right;
                            m_rCcdBox.Width = xTemp - wTemp;
                            //SWAP(m_rBox.Left, m_rBox.Right);
                        }

                        if (m_rCcdBox.Top > m_rCcdBox.Bottom)
                        {
                            int yTemp = m_rCcdBox.Top;
                            m_rCcdBox.Y = m_rCcdBox.Bottom;
                            m_rCcdBox.Height = yTemp - m_rCcdBox.Bottom;
                            //SWAP(ref m_rBox.Top, ref m_rBox.Bottom);
                        }
                        m_rcFixedBox = m_rCcdBox;

                        if (m_rCcdBox.Left < 1)
                        {
                            m_rCcdBox.X = 1;
                            //m_rBox.Width = 1 + m_rcFixedBox.Width;
                        }
                        if (m_rCcdBox.Top < 1)
                        {
                            m_rCcdBox.Y = 1;
                            //m_rBox.Height = 1 + m_rcFixedBox.Height;
                        }

                        if (m_rCcdBox.Right > (Globalo.mLaonGrabberClass.m_nWidth - 1) * dExpandFactorX)
                        {
                            m_rCcdBox.X = (int)(Globalo.mLaonGrabberClass.m_nWidth * dExpandFactorX - m_rCcdBox.Width);
                        }
                        if (m_rCcdBox.Bottom > (Globalo.mLaonGrabberClass.m_nHeight - 1) * dExpandFactorY)
                        {
                            m_rCcdBox.Y = (int)(Globalo.mLaonGrabberClass.m_nHeight * dExpandFactorY - m_rCcdBox.Height);
                        }
                        if (m_iNo_SFR < Globalo.CHART_ROI_COUNT)
                        {
                            //Chart Roi
                            Globalo.mCCdPanel.m_iOffsetX_SFR[m_iNo_SFR] = m_rCcdBox.X;
                            Globalo.mCCdPanel.m_iOffsetY_SFR[m_iNo_SFR] = m_rCcdBox.Y;

                            Globalo.mCCdPanel.m_iSizeX_SFR[m_iNo_SFR] = m_rCcdBox.Width;
                            Globalo.mCCdPanel.m_iSizeY_SFR[m_iNo_SFR] = m_rCcdBox.Height;
                        }
                        else
                        {
                            //Circle Roi
                            Globalo.mCCdPanel.m_rcRoiBox[m_iNo_SFR - Globalo.CHART_ROI_COUNT] = m_rCcdBox;
                        }


                        //! 사각 영역을 그린다. 
                        Globalo.mCCdPanel.DrawRectSfr(m_iNo_SFR);
                    }
                }
                SetCapture();
            }
        }

        private void CcdPanel_MouseUp(object sender, MouseEventArgs e)
        {
            m_bDrawFlag = false;
            m_bBoxMoveFlag = false;
            m_bBoxMoveFlag_CCD = false;

            System.Drawing.Point mUpPoint = new System.Drawing.Point(e.X, e.Y);

            if (mUpPoint.X > mCamWindow.Left &&
               mUpPoint.X < mCamWindow.Right &&
               mUpPoint.Y > mCamWindow.Top &&
               mUpPoint.Y < mCamWindow.Bottom)
            {

            }

            ReleaseCapture();
        }
    }
}
