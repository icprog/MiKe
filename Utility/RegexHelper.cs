using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Text . RegularExpressions;
using System . Threading . Tasks;

namespace Utility
{
    public class RegexHelper
    {
        /// <summary>
        /// 省份证验证
        /// </summary>
        /// <returns></returns>
        public static bool checkID ( string result )
        {
            string rule = @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
            bool isOk = Regex . IsMatch ( result ,rule ,RegexOptions . IgnoreCase );
            return isOk;
        }
    }
}
