using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class ArsEntity
    {
        private int? _idx;
        private string _ars001;
        private string _ars002;
        private string _ars003;
        private string _ars004;
        private string _ars005;
        private string _ars006;
        private string _ars007;
        private decimal? _ars008;
        private string _ars009;
        private bool _ars010;
        private decimal? _ars011;

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
        /// 品号
        /// </summary>
        public string ARS001
        {
            set
            {
                _ars001 = value;
            }
            get
            {
                return _ars001;
            }
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string ARS002
        {
            set
            {
                _ars002 = value;
            }
            get
            {
                return _ars002;
            }
        }
        /// <summary>
        /// 仓库
        /// </summary>
        public string ARS003
        {
            set
            {
                _ars003 = value;
            }
            get
            {
                return _ars003;
            }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string ARS004
        {
            set
            {
                _ars004 = value;
            }
            get
            {
                return _ars004;
            }
        }
        /// <summary>
        /// 材质
        /// </summary>
        public string ARS005
        {
            set
            {
                _ars005 = value;
            }
            get
            {
                return _ars005;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string ARS006
        {
            set
            {
                _ars006 = value;
            }
            get
            {
                return _ars006;
            }
        }
        /// <summary>
        /// 主要来源
        /// </summary>
        public string ARS007
        {
            set
            {
                _ars007 = value;
            }
            get
            {
                return _ars007;
            }
        }
        /// <summary>
        /// 单位克重
        /// </summary>
        public decimal? ARS008
        {
            set
            {
                _ars008 = value;
            }
            get
            {
                return _ars008;
            }
        }
        /// <summary>
        /// 生产部门名称
        /// </summary>
        public string ARS009
        {
            set
            {
                _ars009 = value;
            }
            get
            {
                return _ars009;
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public bool ARS010
        {
            set
            {
                _ars010 = value;
            }
            get
            {
                return _ars010;
            }
        }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal? ARS011
        {
            get
            {
                return _ars011;
            }

            set
            {
                _ars011 = value;
            }
        }

    }
}
