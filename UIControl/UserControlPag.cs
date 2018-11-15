using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelectAll.UIControl
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1 ( )
        {
            InitializeComponent( );
        }

        private void UserControl1_Load ( object sender ,EventArgs e )
        {

        }

       

        #region 页码变化触发事件

        public event EventHandler OnPageChanged;

        #endregion

        private int _pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public virtual int pageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        private int _pageSize = 1000;
        /// <summary>
        /// 每页记录数默认1000
        /// </summary>
        public virtual int pageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        private int _recordCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public virtual int recordCount
        {
            get
            {
                return _recordCount;
            }
            set
            {
                _recordCount = value;
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount
        {
            get
            {
                if ( _pageSize != 0 )
                    _pageCount = GetPageCount( );
                return _pageCount;
            }

        }

        private string _jumpText;
        /// <summary>
        /// 跳转按钮文本
        /// </summary>
        public string jmupText
        {
            get
            {
                if ( string.IsNullOrEmpty( _jumpText ) )
                    _jumpText = "Go";
                return _jumpText;
            }
            set
            {
                _jumpText = value;
            }
        }


        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <returns></returns>
        private int GetPageCount ( )
        {
            if ( pageSize == 0 )
            {
                return 0;
            }
            int pageCount = recordCount / pageSize;
            if ( recordCount % pageSize == 0 )
            {
                pageCount = recordCount / pageSize;
            }
            else
            {
                pageCount = recordCount / pageSize + 1;
            }
            return pageCount;
        }


        /// <summary>
        /// 外部调用
        /// </summary>
        /// <param name="count"></param>
        public void DrawCount ( int count )
        {
            recordCount = count;
            DrawControl( false );
        }

        private void SetFormEnable ( )
        {
            lnkFirst.Enabled = true;
            lnkPrev.Enabled = true;
            lnkNext.Enabled = true;
            lnkLast.Enabled = true;
            btnGo.Enabled = true;
        }

        /// <summary>
        /// 分页控件呈现
        /// </summary>
        /// <param name="callEvent"></param>
        private void DrawControl ( bool callEvent )
        {
            btnGo.Text = jmupText;
            lblCurrentPage.Text = pageIndex.ToString( );
            lblPageCount.Text = pageCount.ToString( );
            lblTotalCount.Text = recordCount.ToString( );
            txtPageSize.Text = pageSize.ToString( );

            if ( callEvent && OnPageChanged != null )
                OnPageChanged( this ,null );  //当前分页数字改变时  出发分页事件
            SetFormEnable( );
            if ( pageCount == 1 )
            {
                lnkFirst.Enabled = false;
                lnkPrev.Enabled = false;
                lnkNext.Enabled = false;
                lnkLast.Enabled = false;
                btnGo.Enabled = false;
            }
            else if ( pageIndex == 1 )
            {
                lnkFirst.Enabled = false;
                lnkPrev.Enabled = false;
            }
            else if ( pageIndex == pageCount )
            {
                lnkNext.Enabled = false;
                lnkLast.Enabled = false;
            }
        }

        #region 相关控件事件

        private void lnkFirst_LinkClicked ( object sender ,LinkLabelLinkClickedEventArgs e )
        {
            pageIndex = 1;
            DrawControl( true );
        }

        private void lnkPrev_LinkClicked ( object sender ,LinkLabelLinkClickedEventArgs e )
        {
            pageIndex = Math.Max( 1 ,pageIndex - 1 );
            DrawControl( true );
        }

        private void lnkNext_LinkClicked ( object sender ,LinkLabelLinkClickedEventArgs e )
        {
            pageIndex = Math.Min( pageCount ,pageIndex + 1 );
            DrawControl( true );
        }

        private void lnkLast_LinkClicked ( object sender ,LinkLabelLinkClickedEventArgs e )
        {
            pageIndex = pageCount;
            DrawControl( true );
        }

        /// <summary>
        /// enter键功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageNum_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            btnGo_Click( null ,null );
        }
        /// <summary>
        /// 跳转页数限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageNum_TextChanged ( object sender ,EventArgs e )
        {
            int num = 0;
            if ( int.TryParse( txtPageNum.Text.Trim( ) ,out num ) && num > 0 )
            {
                if ( num > pageCount )
                    txtPageNum.Text = pageCount.ToString( );
            }
        }
        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click ( object sender ,EventArgs e )
        {
            int num = 0;
            if ( int.TryParse( txtPageNum.Text.Trim( ) ,out num ) && num > 0 )
            {
                pageIndex = num;
                DrawControl( true );
            }
        }
        #endregion


        bool isTextChanged = false;
        /// <summary>
        /// 分页属性改变了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSize_TextChanged ( object sender ,EventArgs e )
        {
            int num = 0;
            if ( !int.TryParse( txtPageSize.Text.Trim( ) ,out num ) || num <= 0 )
            {
                num = 1000;
                txtPageSize.Text = "1000";
            }
            else
                isTextChanged = true;
            pageSize = num;
        }
        /// <summary>
        /// 光标离开分页属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageSize_Leave ( object sender ,EventArgs e )
        {
            if ( isTextChanged )
            {
                isTextChanged = false;
                lnkFirst_LinkClicked( null ,null );
            }
        }
    }
}
