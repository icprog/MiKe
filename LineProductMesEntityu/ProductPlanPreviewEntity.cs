using System;

namespace LineProductMesEntityu
{
    public class ProductPlanPreviewEntity
    {
        private string _prf001;
        private DateTime? _prf002;
        private int? _prf003;

        /// <summary>
        /// 品号
        /// </summary>
        public string PRF001
        {
            get
            {
                return _prf001;
            }

            set
            {
                _prf001 = value;
            }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PRF002
        {
            get
            {
                return _prf002;
            }

            set
            {
                _prf002 = value;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int? PRF003
        {
            get
            {
                return _prf003;
            }

            set
            {
                _prf003 = value;
            }
        }

    }
}
