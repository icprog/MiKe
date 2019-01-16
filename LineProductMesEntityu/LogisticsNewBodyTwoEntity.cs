using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class LogisticsNewBodyTwoEntity
    {
        private int _idx;
        private string _lgp001;
        private string _lgp002;
        private string _lgp003;
        private string _lgp004;
        private string _lgp005;
        private string _lgp006;
        private DateTime? _lgp007;
        private DateTime? _lgp008;
        private DateTime? _lgp009;
        private DateTime? _lgp010;
        private decimal? _lgp011;
        private decimal? _lgp012;
        private decimal? _lgp013;
        private decimal? _lgp014;
        private decimal? _lgp015;

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
        public string LGP001
        {
            set
            {
                _lgp001 = value;
            }
            get
            {
                return _lgp001;
            }
        }
        /// <summary>
        /// 工号
        /// </summary>
        public string LGP002
        {
            set
            {
                _lgp002 = value;
            }
            get
            {
                return _lgp002;
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string LGP003
        {
            set
            {
                _lgp003 = value;
            }
            get
            {
                return _lgp003;
            }
        }
        /// <summary>
        /// 岗位
        /// </summary>
        public string LGP004
        {
            set
            {
                _lgp004 = value;
            }
            get
            {
                return _lgp004;
            }
        }
        /// <summary>
        /// 工作状态
        /// </summary>
        public string LGP005
        {
            set
            {
                _lgp005 = value;
            }
            get
            {
                return _lgp005;
            }
        }
        /// <summary>
        /// 班组
        /// </summary>
        public string LGP006
        {
            set
            {
                _lgp006 = value;
            }
            get
            {
                return _lgp006;
            }
        }
        /// <summary>
        /// 计时开工时间
        /// </summary>
        public DateTime? LGP007
        {
            set
            {
                _lgp007 = value;
            }
            get
            {
                return _lgp007;
            }
        }
        /// <summary>
        /// 计时完工时间
        /// </summary>
        public DateTime? LGP008
        {
            set
            {
                _lgp008 = value;
            }
            get
            {
                return _lgp008;
            }
        }
        /// <summary>
        /// 计件开工时间
        /// </summary>
        public DateTime? LGP009
        {
            get
            {
                return _lgp009;
            }

            set
            {
                _lgp009 = value;
            }
        }
        /// <summary>
        /// 计件完工时间
        /// </summary>
        public DateTime? LGP010
        {
            get
            {
                return _lgp010;
            }

            set
            {
                _lgp010 = value;
            }
        }
        /// <summary>
        /// 计件工时
        /// </summary>
        public decimal? LGP011
        {
            get
            {
                return _lgp011;
            }

            set
            {
                _lgp011 = value;
            }
        }
        /// <summary>
        /// 计时工时
        /// </summary>
        public decimal? LGP012
        {
            get
            {
                return _lgp012;
            }

            set
            {
                _lgp012 = value;
            }
        }
        /// <summary>
        /// 工资
        /// </summary>
        public decimal? LGP013
        {
            get
            {
                return _lgp013;
            }

            set
            {
                _lgp013 = value;
            }
        }
        /// <summary>
        /// 小时工资
        /// </summary>
        public decimal? LGP014
        {
            get
            {
                return _lgp014;
            }

            set
            {
                _lgp014 = value;
            }
        }
        /// <summary>
        /// 分配工资
        /// </summary>
        public decimal? LGP015
        {
            get
            {
                return _lgp015;
            }

            set
            {
                _lgp015 = value;
            }
        }
    }
}
