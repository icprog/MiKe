using System;

namespace LineProductMesEntityu
{
    public class LineForAssPlanEntity
    {
        private int _idx;
        private string _prg001;
        private DateTime? _prg002;
        private int? _prg003;
        private string _prg004;
        private string _prg005;
        private int _prg006;

        public int Idx
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
        /// 品号
        /// </summary>
        public string PRG001
        {
            get
            {
                return _prg001;
            }

            set
            {
                _prg001 = value;
            }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? PRG002
        {
            get
            {
                return _prg002;
            }

            set
            {
                _prg002 = value;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int? PRG003
        {
            get
            {
                return _prg003;
            }

            set
            {
                _prg003 = value;
            }
        }

        /// <summary>
        /// 产线编号
        /// </summary>
        public string PRG004
        {
            get
            {
                return _prg004;
            }

            set
            {
                _prg004 = value;
            }
        }

        /// <summary>
        /// 产线名称
        /// </summary>
        public string PRG005
        {
            get
            {
                return _prg005;
            }

            set
            {
                _prg005 = value;
            }
        }

        /// <summary>
        /// 日排量
        /// </summary>
        public int PRG006
        {
            get
            {
                return _prg006;
            }
            set
            {
                _prg006 = value;
            }
        }

    }
}
