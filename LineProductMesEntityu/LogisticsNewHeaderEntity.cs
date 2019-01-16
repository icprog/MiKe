using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class LogisticsNewHeaderEntity
    {
        private int _idx;
        private string _lgn001;
        private DateTime? _lgn002;
        private bool _lgn003;
        private bool _lgn004;
        private decimal? _lgn005;
        private decimal? _lgn006;
        private string _lgn007;
        private string _lgn008;
        private DateTime? _lgn009;
        private DateTime? _lgn010;
        private decimal? _lgn011;
        private decimal? _lgn012;

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
        public string LGN001
        {
            set
            {
                _lgn001 = value;
            }
            get
            {
                return _lgn001;
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? LGN002
        {
            set
            {
                _lgn002 = value;
            }
            get
            {
                return _lgn002;
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        public bool LGN003
        {
            set
            {
                _lgn003 = value;
            }
            get
            {
                return _lgn003;
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public bool LGN004
        {
            set
            {
                _lgn004 = value;
            }
            get
            {
                return _lgn004;
            }
        }
        /// <summary>
        /// 午休
        /// </summary>
        public decimal? LGN005
        {
            set
            {
                _lgn005 = value;
            }
            get
            {
                return _lgn005;
            }
        }
        /// <summary>
        /// 晚休
        /// </summary>
        public decimal? LGN006
        {
            get
            {
                return _lgn006;
            }

            set
            {
                _lgn006 = value;
            }
        }
        /// <summary>
        /// 工资类型
        /// </summary>
        public string LGN007
        {
            get
            {
                return _lgn007;
            }

            set
            {
                _lgn007 = value;
            }
        }
        /// <summary>
        /// 制单
        /// </summary>
        public string LGN008
        {
            get
            {
                return _lgn008;
            }

            set
            {
                _lgn008 = value;
            }
        }
        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime? LGN009
        {
            set
            {
                _lgn009 = value;
            }
            get
            {
                return _lgn009;
            }
        }
        /// <summary>
        /// 完工时间
        /// </summary>
        public DateTime? LGN010
        {
            set
            {
                _lgn010 = value;
            }
            get
            {
                return _lgn010;
            }
        }
        /// <summary>
        /// 补贴工时
        /// </summary>
        public decimal? LGN011
        {
            set
            {
                _lgn011 = value;
            }
            get
            {
                return _lgn011;
            }
        }
        /// <summary>
        /// 补贴单价
        /// </summary>
        public decimal? LGN012
        {
            set
            {
                _lgn012 = value;
            }
            get
            {
                return _lgn012;
            }
        }
    }
}
