using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class LogisticsNewBodyOneEntity
    {
        private int _idx;
        private string _log001;
        private string _log002;
        private string _log003;
        private string _log004;
        private string _log005;
        private int? _log006;
        private decimal? _log007;
        private int? _log008;
        private decimal? _log009;

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
        public string LOG001
        {
            set
            {
                _log001 = value;
            }
            get
            {
                return _log001;
            }
        }
        /// <summary>
        /// 销货单号
        /// </summary>
        public string LOG002
        {
            set
            {
                _log002 = value;
            }
            get
            {
                return _log002;
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string LOG003
        {
            set
            {
                _log003 = value;
            }
            get
            {
                return _log003;
            }
        }
        /// <summary>
        /// 品号
        /// </summary>
        public string LOG004
        {
            set
            {
                _log004 = value;
            }
            get
            {
                return _log004;
            }
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string LOG005
        {
            set
            {
                _log005 = value;
            }
            get
            {
                return _log005;
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int? LOG006
        {
            set
            {
                _log006 = value;
            }
            get
            {
                return _log006;
            }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal? LOG007
        {
            set
            {
                _log007 = value;
            }
            get
            {
                return _log007;
            }
        }
        /// <summary>
        /// 完工数量
        /// </summary>
        public int? LOG008
        {
            set
            {
                _log008 = value;
            }
            get
            {
                return _log008;
            }
        }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal? LOG009
        {
            set
            {
                _log009 = value;
            }
            get
            {
                return _log009;
            }
        }
    }
}
