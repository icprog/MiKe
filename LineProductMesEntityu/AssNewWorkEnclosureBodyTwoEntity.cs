using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class AssNewWorkEnclosureBodyTwoEntity
    {
        private int _idx;
        private string _anv001;
        private string _anv002;
        private string _anv003;
        private string _anv004;
        private DateTime? _anv005;
        private DateTime? _anv006;
        private string _anv007;
        private string _anv008;
        private decimal? _anv009;
        private decimal? _anv010;
        
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
        public string ANV001
        {
            set
            {
                _anv001 = value;
            }
            get
            {
                return _anv001;
            }
        }
        /// <summary>
        /// 工号
        /// </summary>
        public string ANV002
        {
            set
            {
                _anv002 = value;
            }
            get
            {
                return _anv002;
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string ANV003
        {
            set
            {
                _anv003 = value;
            }
            get
            {
                return _anv003;
            }
        }
        /// <summary>
        /// 岗位
        /// </summary>
        public string ANV004
        {
            set
            {
                _anv004 = value;
            }
            get
            {
                return _anv004;
            }
        }
        /// <summary>
        /// 开工时间
        /// </summary>
        public DateTime? ANV005
        {
            set
            {
                _anv005 = value;
            }
            get
            {
                return _anv005;
            }
        }
        /// <summary>
        /// 完工时间
        /// </summary>
        public DateTime? ANV006
        {
            set
            {
                _anv006 = value;
            }
            get
            {
                return _anv006;
            }
        }
        /// <summary>
        /// 工作状态
        /// </summary>
        public string ANV007
        {
            set
            {
                _anv007 = value;
            }
            get
            {
                return _anv007;
            }
        }
        /// <summary>
        /// 班组
        /// </summary>
        public string ANV008
        {
            set
            {
                _anv008 = value;
            }
            get
            {
                return _anv008;
            }
        }
        /// <summary>
        /// 工时
        /// </summary>
        public decimal? ANV009
        {
            get
            {
                return _anv009;
            }

            set
            {
                _anv009 = value;
            }
        }
        /// <summary>
        /// 工资
        /// </summary>
        public decimal? ANV010
        {
            get
            {
                return _anv010;
            }

            set
            {
                _anv010 = value;
            }
        }
    }
}
