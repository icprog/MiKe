using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class ProductPlanBodyEntity
    {
        private int? _idx;
        private string _pre001;
        private string _pre002;
        private string _pre003;
        private string _pre004;
        private DateTime? _pre005;
        private DateTime? _pre006;
        private int? _pre007;
        private int? _pre008;
        private int? _pre009;
        private int? _pre010;
        private int? _pre011;
        private string _pre012;

        /// <summary>
        /// 
        /// </summary>
        public int? idx
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
        public string PRE001
        {
            set
            {
                _pre001 = value;
            }
            get
            {
                return _pre001;
            }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string PRE002
        {
            set
            {
                _pre002 = value;
            }
            get
            {
                return _pre002;
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string PRE003
        {
            set
            {
                _pre003 = value;
            }
            get
            {
                return _pre003;
            }
        }
        /// <summary>
        /// 品号
        /// </summary>
        public string PRE004
        {
            set
            {
                _pre004 = value;
            }
            get
            {
                return _pre004;
            }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PRE005
        {
            set
            {
                _pre005 = value;
            }
            get
            {
                return _pre005;
            }
        }
        /// <summary>
        /// 交货日期
        /// </summary>
        public DateTime? PRE006
        {
            set
            {
                _pre006 = value;
            }
            get
            {
                return _pre006;
            }
        }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int? PRE007
        {
            set
            {
                _pre007 = value;
            }
            get
            {
                return _pre007;
            }
        }
        /// <summary>
        /// 排产数量
        /// </summary>
        public int? PRE008
        {
            set
            {
                _pre008 = value;
            }
            get
            {
                return _pre008;
            }
        }
        /// <summary>
        /// ERP计划数量
        /// </summary>
        public int? PRE009
        {
            set
            {
                _pre009 = value;
            }
            get
            {
                return _pre009;
            }
        }
        /// <summary>
        /// 日排量
        /// </summary>
        public int? PRE010
        {
            get
            {
                return _pre010;
            }

            set
            {
                _pre010 = value;
            }
        }
        /// <summary>
        /// 调整量
        /// </summary>
        public int? PRE011
        {
            get
            {
                return _pre011;
            }

            set
            {
                _pre011 = value;
            }
        }

        /// <summary>
        /// ERP计划单号
        /// </summary>
        public string PRE012
        {
            get
            {
                return _pre012;
            }

            set
            {
                _pre012 = value;
            }
        }
    }
}
