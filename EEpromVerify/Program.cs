using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApsMotionControl
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
// TODO: 250216 2
//250212 - home / start commit1
//250210 - aps / commit1
//240712 - Motor_XYT_Move 까지 추가 in work
// TODO: 로그인 버튼 추가해
// FIXME: 버그 수정 필요
// TODO: 로그인 화면 UI 개선 필요
// FIXME: 비밀번호 검증 로직이 정상 작동하지 않음
// HACK: 임시 해결책 적용 (보안 검토 필요)
// NOTE: API 응답 속도가 느림. 원인 분석 필요
// UNDONE: 이 기능은 더 이상 사용되지 않음. 제거할지 검토 필요
// DONE: 회원가입 기능 구현 완료

// TODO (@ayoon): UI 디자인 변경 필요
// TODO (@dev1): 데이터베이스 연결 최적화