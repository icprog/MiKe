using System;

namespace LineProductMesEntityu
{
    public class BoarSetEntity
    {
        private int _idx;
        private string _bos001;
        private int? _bos002;
        private int? _bos003;
        private int? _bos004;
        private DateTime? _bos005;

        public int IDX
        {
            get
            {
                return _idx;
            }
            set
            {
                _idx = value;
            }
        }

        /// <summary>
        /// 看板车间
        /// </summary>
        public string BOS001
        {
            get
            {
                return _bos001;
            }

            set
            {
                _bos001 = value;
            }
        }

        /// <summary>
        /// 显示计划天数
        /// </summary>
        public int? BOS002
        {
            get
            {
                return _bos002;
            }

            set
            {
                _bos002 = value;
            }
        }

        /// <summary>
        /// 数据切换时间
        /// </summary>
        public int? BOS003
        {
            get
            {
                return _bos003;
            }

            set
            {
                _bos003 = value;
            }
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int? BOS004
        {
            get
            {
                return _bos004;
            }

            set
            {
                _bos004 = value;
            }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? BOS005
        {
            get
            {
                return _bos005;
            }

            set
            {
                _bos005 = value;
            }
        }

    }
}
