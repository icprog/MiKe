using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class WagesHeaderEntity
    {
        private int _idx;
        private string _wag001;
        private DateTime? _wag002;
        private bool _wag003;
        private string _wag004;
        private string _wag005;

        /// <summary>
        /// 
        /// </summary>
        public int idx
        {
            set
            {
                _idx = value;
            }
            get
            {
                return _idx;
            }
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string WAG001
        {
            set
            {
                _wag001 = value;
            }
            get
            {
                return _wag001;
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? WAG002
        {
            set
            {
                _wag002 = value;
            }
            get
            {
                return _wag002;
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        public bool WAG003
        {
            set
            {
                _wag003 = value;
            }
            get
            {
                return _wag003;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WAG004
        {
            set
            {
                _wag004 = value;
            }
            get
            {
                return _wag004;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WAG005
        {
            set
            {
                _wag005 = value;
            }
            get
            {
                return _wag005;
            }
        }
    }
}
