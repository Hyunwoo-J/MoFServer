using System;

namespace MoFModel.Models
{
    /// <summary>
    /// 서버 응답 코드
    /// </summary>
    public static class ResultCode
    {
        public static int Ok = 200;
        public static int Fail = -999;

        public static int ReviewNotExists = 1001;
        public static int TheaterExists = 2001;
    }
}
