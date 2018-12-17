using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class ProductPlanHeaderEntity
    {
        private int _idx;
        private string _prd001;
        private DateTime? _prd002;
        private bool _prd003;
        private string _prd004;
        private string _prd005;

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
        public string PRD001
        {
            set
            {
                _prd001 = value;
            }
            get
            {
                return _prd001;
            }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? PRD002
        {
            set
            {
                _prd002 = value;
            }
            get
            {
                return _prd002;
            }
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool PRD003
        {
            set
            {
                _prd003 = value;
            }
            get
            {
                return _prd003;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRD004
        {
            set
            {
                _prd004 = value;
            }
            get
            {
                return _prd004;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRD005
        {
            set
            {
                _prd005 = value;
            }
            get
            {
                return _prd005;
            }
        }
    }
}
