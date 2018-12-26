using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class PaintNewspaperHeaderEntity
    {
        private int _idx;
        private string _pan001;
        private string _pan002;
        private string _pan003;
        private string _pan004;
        private string _pan005;
        private DateTime? _pan006;
        private string _pan007;
        private string _pan008;
        private bool _pan009;
        private bool _pan010;
        private decimal? _pan011;
        private decimal? _pan012;
        private string _pan013;
        private string _pan014;
        private DateTime? _pan015;
        private DateTime? _pan016;

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
        public string PAN001
        {
            set
            {
                _pan001 = value;
            }
            get
            {
                return _pan001;
            }
        }
        /// <summary>
        /// 车间编号
        /// </summary>
        public string PAN002
        {
            set
            {
                _pan002 = value;
            }
            get
            {
                return _pan002;
            }
        }
        /// <summary>
        /// 车间名称
        /// </summary>
        public string PAN003
        {
            set
            {
                _pan003 = value;
            }
            get
            {
                return _pan003;
            }
        }
        /// <summary>
        /// 班组编号
        /// </summary>
        public string PAN004
        {
            set
            {
                _pan004 = value;
            }
            get
            {
                return _pan004;
            }
        }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string PAN005
        {
            set
            {
                _pan005 = value;
            }
            get
            {
                return _pan005;
            }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? PAN006
        {
            set
            {
                _pan006 = value;
            }
            get
            {
                return _pan006;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string PAN007
        {
            set
            {
                _pan007 = value;
            }
            get
            {
                return _pan007;
            }
        }
        /// <summary>
        /// 总结
        /// </summary>
        public string PAN008
        {
            set
            {
                _pan008 = value;
            }
            get
            {
                return _pan008;
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        public bool PAN009
        {
            set
            {
                _pan009 = value;
            }
            get
            {
                return _pan009;
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public bool PAN010
        {
            set
            {
                _pan010 = value;
            }
            get
            {
                return _pan010;
            }
        }
        /// <summary>
        /// 午休(小时)
        /// </summary>
        public decimal? PAN011
        {
            set
            {
                _pan011 = value;
            }
            get
            {
                return _pan011;
            }
        }
        /// <summary>
        /// 晚休(小时)
        /// </summary>
        public decimal? PAN012
        {
            set
            {
                _pan012 = value;
            }
            get
            {
                return _pan012;
            }
        }
        /// <summary>
        /// 工资类型
        /// </summary>
        public string PAN013
        {
            set
            {
                _pan013 = value;
            }
            get
            {
                return _pan013;
            }
        }
        /// <summary>
        /// 制单
        /// </summary>
        public string PAN014
        {
            get
            {
                return _pan014;
            }

            set
            {
                _pan014 = value;
            }
        }
        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime? PAN015
        {
            set
            {
                _pan015 = value;
            }
            get
            {
                return _pan015;
            }
        }
        /// <summary>
        /// 完工时间
        /// </summary>
        public DateTime? PAN016
        {
            set
            {
                _pan016 = value;
            }
            get
            {
                return _pan016;
            }
        }
    }
}
