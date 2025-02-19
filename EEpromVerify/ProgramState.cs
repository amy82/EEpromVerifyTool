using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsMotionControl
{
    public enum OperationState
    {
        Stopped,       // 정지중
        Paused,
        AutoRunning,   // 자동운전중
        Preparing,      // 운전준비중
        PreparationComplete,   //운전준비 완료
        Originning      // 원점
    }
    // 글로벌 상태 변수를 관리하는 클래스 정의
    public static class ProgramState
    {
        // 현재 상태를 나타내는 static 변수
        public static OperationState CurrentState { get; set; } = OperationState.Stopped;

        public static bool ON_LINE_MOTOR = false;    //true
        public static bool ON_LINE_MIL = false;      //true

        //public static bool ON_LINE_CAM = false;      //true
        //public static bool ON_LINE_GRABBER = false;      //true

    }
}
