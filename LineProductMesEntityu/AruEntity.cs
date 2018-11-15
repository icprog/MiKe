using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class AruEntity
    {
        private int _idx;
        private string _aru001;
        private string _aru002;
        private string _aru003;
        private string _aru004;
        private string _aru005;
        private string _aru006;

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
        /// 品号
        /// </summary>
        public string ARU001
        {
            set
            {
                _aru001 = value;
            }
            get
            {
                return _aru001;
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string ARU002
        {
            set
            {
                _aru002 = value;
            }
            get
            {
                return _aru002;
            }
        }
        /// <summary>
        /// 模具编号
        /// </summary>
        public string ARU003
        {
            set
            {
                _aru003 = value;
            }
            get
            {
                return _aru003;
            }
        }
        /// <summary>
        /// 模具名称
        /// </summary>
        public string ARU004
        {
            set
            {
                _aru004 = value;
            }
            get
            {
                return _aru004;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ARU005
        {
            set
            {
                _aru005 = value;
            }
            get
            {
                return _aru005;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ARU006
        {
            set
            {
                _aru006 = value;
            }
            get
            {
                return _aru006;
            }
        }
    }
}
