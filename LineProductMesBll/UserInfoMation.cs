using System;
using System . Collections . Generic;
using System . Data;
using System . Drawing;
using System . Text;
using LineProductMesEntityu;

namespace LineProductMesBll
{
    public static class UserInfoMation
    {
        /// <summary>
        /// 人员姓名
        /// </summary>
        public static string userName=string.Empty;
        /// <summary>
        /// 人员编号
        /// </summary>
        public static string userNum=string.Empty;
        /// <summary>
        /// 是否记住密码
        /// </summary>
        public static string sign=string.Empty;
        /// <summary>
        /// 程序名称
        /// </summary>
        public static string programName=string.Empty;
        /// <summary>
        /// 程序Text
        /// </summary>
        public static string programText=string.Empty;
        /// <summary>
        /// 系统操作
        /// </summary>
        public static string TypeOfOper=string.Empty;
        /// <summary>
        /// 系统皮肤颜色
        /// </summary>
        public static Color FeedColor=Color.White;

        /// <summary>
        /// 获取系统时间
        /// </summary>
        public static DateTime sysTime=ObtainInfo.getTime();

        /// <summary>
        /// 新增单据单号
        /// </summary>
        public static string oddNum=string.Empty;

        private static List<LineProductMesEntityu.PowerEntity> powList;

        /// <summary>
        /// 获取权限信息
        /// </summary>
        public static List<PowerEntity> PowList
        {
            get
            {
                return powList;
            }

            set
            {
                powList = value;
            }
        }

    }
}
