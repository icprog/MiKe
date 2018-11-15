using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class SupplierForMouldEntity
    {
        private int _idx;
        private string _sfm001;
        private string _sfm002;
        private string _sfm003;
        private string _sfm004;
        private string _sfm005;
        private bool _sfm006;
        private string _sfm007;
        private string _sfm008;
        private string _sfm009;
        private string _sfm010;

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
        /// 供应商编号
        /// </summary>
        public string SFM001
        {
            set
            {
                _sfm001 = value;
            }
            get
            {
                return _sfm001;
            }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SFM002
        {
            set
            {
                _sfm002 = value;
            }
            get
            {
                return _sfm002;
            }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string SFM003
        {
            set
            {
                _sfm003 = value;
            }
            get
            {
                return _sfm003;
            }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string SFM004
        {
            set
            {
                _sfm004 = value;
            }
            get
            {
                return _sfm004;
            }
        }
        /// <summary>
        /// 地址省
        /// </summary>
        public string SFM005
        {
            set
            {
                _sfm005 = value;
            }
            get
            {
                return _sfm005;
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public bool SFM006
        {
            set
            {
                _sfm006 = value;
            }
            get
            {
                return _sfm006;
            }
        }
        /// <summary>
        /// 地址市
        /// </summary>
        public string SFM007
        {
            set
            {
                _sfm007 = value;
            }
            get
            {
                return _sfm007;
            }
        }
        /// <summary>
        /// 地址区
        /// </summary>
        public string SFM008
        {
            set
            {
                _sfm008 = value;
            }
            get
            {
                return _sfm008;
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string SFM009
        {
            set
            {
                _sfm009 = value;
            }
            get
            {
                return _sfm009;
            }
        }
        /// <summary>
        /// 模具类别
        /// </summary>
        public string SFM010
        {
            set
            {
                _sfm010 = value;
            }
            get
            {
                return _sfm010;
            }
        }
    }
}
