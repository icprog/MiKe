using System . Drawing;
using System . Windows . Forms;
using DevExpress . XtraBars . Helpers;
using LineProductMesBll;
using System . Collections . Generic;
using DevExpress . XtraTreeList;
using System . Reflection;
using DevExpress . XtraBars . Ribbon;
using System;
using System . Configuration;
using System . Threading . Tasks;
using System . Data;
using DevExpress . XtraTreeList . Nodes;
using System . ComponentModel;
using DevExpress . XtraEditors;
using LineProductMes.ClassForMain;

namespace LineProductMes
{
    public partial class FormMain :RibbonForm
    {
        protected static DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel=new DevExpress.LookAndFeel.DefaultLookAndFeel();
        LineProductMesBll.Bll.MainBll _bll=null;
        //List<string> mdiChildForm=null;
        string proId=string.Empty,idPro = string . Empty;

        UIControl . Main . ALLControl control;
        UIControl . Main . ERPControl erpCon;
        UIControl . Main . BaseControl baseCon;
        UIControl . Main . PlanControl planCon;
        UIControl . Main . WorkControl workCon;
        UIControl . Main . BoardControl boardCon;
        UIControl . Main . ReportControl reportCon;

        public FormMain ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . MainBll ( );
            //设置窗体皮肤
            SkinHelper . InitSkinGallery ( ribbonGalleryBarItem1 );

            this . Location = new Point ( -1000 ,-1000 );

            Task task = new Task ( getPowList );
            task . Start ( );

            Login ( );
        }

        private void FormMain_Load ( object sender ,EventArgs e )
        {
            if ( UserInfoMation . userName . Equals ( "组装看板" ) )
            {
                FormAssBoard from = new FormAssBoard ( );
                from . WindowState = FormWindowState . Maximized;
                //from . TopMost = true;
                from . Show ( );

                this . WindowState = FormWindowState . Minimized;
            }
            else if ( UserInfoMation . userName . Equals ( "五金看板" ) )
            {
                FormHardBoard from = new FormHardBoard ( );
                from . WindowState = FormWindowState . Maximized;
                //from . TopMost = true;
                from . Show ( );

                this . WindowState = FormWindowState . Minimized;
            }
            else if ( UserInfoMation . userName . Equals ( "注塑看板" ) )
            {
                FormInjectionBoard from = new FormInjectionBoard ( );
                from . WindowState = FormWindowState . Maximized;
                //from . TopMost = true;
                from . Show ( );

                this . WindowState = FormWindowState . Minimized;
            }
            else if ( UserInfoMation . userName . Equals ( "LED看板" ) )
            {
                FormLEGBoard from = new FormLEGBoard ( );
                from . WindowState = FormWindowState . Maximized;
                //from . TopMost = true;
                from . Show ( );

                this . WindowState = FormWindowState . Minimized;
            }
            else if ( UserInfoMation . userName . Equals ( "面板看板" ) )
            {
                FormLEEBoard from = new FormLEEBoard ( );
                from . WindowState = FormWindowState . Maximized;
                //from . TopMost = true;
                from . Show ( );

                this . WindowState = FormWindowState . Minimized;
            }
            else if ( UserInfoMation . userName . Equals ( "喷漆看板" ) )
            {
                FormPaintBoard from = new FormPaintBoard ( );
                from . WindowState = FormWindowState . Maximized;
                //from . TopMost = true;
                from . Show ( );

                this . WindowState = FormWindowState . Minimized;
            }
            else if ( UserInfoMation . userName . Equals ( "采购看板" ) )
            {
                FormPurchaseBoard from = new FormPurchaseBoard ( );
                from . WindowState = FormWindowState . Maximized;
                //from . TopMost = true;
                from . Show ( );

                this . WindowState = FormWindowState . Minimized;
            }

            addControl ( );
        }

        #region Method
        protected void Login ( )
        {
            this . Hide ( );
            FormLogin form = new FormLogin ( );
            this . StartPosition = FormStartPosition . WindowsDefaultBounds;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                this . Location = new Point ( 0 ,0 );
                this . Show ( );
                this . BringToFront ( );
                this . barUserInfo . Caption = barUserInfo . Caption + UserInfoMation . userName;
                this . barLoginTime . Caption = barLoginTime . Caption + UserInfoMation . sysTime . ToString ( "yyyy/MM/dd hh:mm" ) + "  " + UserInfoMation . DataBase;

                ShowMenuByUser ( );
            }
            else
                this . Close ( );
        }
        private void ShowMenuByUser ( )
        {
            Func<List<LineProductMesEntityu . MainEntity>> funStr = initData;
            IAsyncResult result = funStr . BeginInvoke ( new AsyncCallback ( setTableView ) ,null );
        }
        List<LineProductMesEntityu . MainEntity> getModel=null;
        delegate void AsynUpdateUIBase ( );
        void setTableView ( IAsyncResult result )
        {
            if ( InvokeRequired )
            {
                this . Invoke ( new AsynUpdateUIBase ( delegate ( )
                {
                    treeList1 . DataSource = getModel;
                    //findAllBtn ( );
                } ) );
            }
        }
        protected List<LineProductMesEntityu . MainEntity> initData ( )
        {
            getModel = _bll . GetModel ( UserInfoMation . userNum );
            return getModel;
        }
        void getPowList ( )
        {
            LineProductMesBll . UserInfoMation . PowList = _bll . getPowerList ( );
        }
        #endregion

        #region Control
        void addControl ( )
        {
            control = new UIControl . Main . ALLControl ( );
            this . Controls . Add ( control );
            control . Dock = DockStyle . Fill;
            control . button1 . Click += Button1_Click;
            control . button2 . Click += Button2_Click;
            control . button3 . Click += Button3_Click;
            control . button4 . Click += Button4_Click;
            control . button5 . Click += Button5_Click;
            control . button6 . Click += Button6_Click;
        }
        private void Button1_Click ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( control );
            addERPControl ( );
        }
        private void Button2_Click ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( control );
            addBaseControl ( );
        }
        private void Button3_Click ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( control );
            addPlanControl ( );
        }
        private void Button4_Click ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( control );
            addWorkControl ( );
        }
        private void Button5_Click ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( control );
            addBoardCon ( );
        }
        private void Button6_Click ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( control );
            addReportCon ( );
        }
        void addERPControl ( )
        {
            erpCon = new UIControl . Main . ERPControl ( );
            this . Controls . Add ( erpCon );
            erpCon . Dock = DockStyle . Fill;
            erpCon . roundButton1 . Click += RoundButton1_Click;
            findAllBtn ( erpCon );
        }
        private void RoundButton1_Click ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( erpCon );
            addControl ( );
        }
        void addBaseControl ( )
        {
            baseCon = new UIControl . Main . BaseControl ( );
            this . Controls . Add ( baseCon );
            baseCon . Dock = DockStyle . Fill;
            baseCon . roundButton1 . Click += RoundButton1_Click1;
            baseCon . button1 . Click += btnUser_Click;
            baseCon . button3 . Click += btnArt_Click;
            baseCon . button5 . Click += btnMac_Click;
            baseCon . button4 . Click += btnMould_Click;
            baseCon . button6 . Click += btnSuMod_Click;
            findAllBtn ( baseCon );
        }
        private void RoundButton1_Click1 ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( baseCon );
            addControl ( );
        }
        void addPlanControl ( )
        {
            planCon = new UIControl . Main . PlanControl ( );
            this . Controls . Add ( planCon );
            planCon . Dock = DockStyle . Fill;
            planCon . roundButton1 . Click += RoundButton1_Click2;
            planCon . button1 . Click += btnPlan_Click;
            planCon . button2 . Click += btnSiPlan_Click;
            planCon . button3 . Click += btnPur_Click;
            findAllBtn ( planCon );
        }
        private void RoundButton1_Click2 ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( planCon );
            addControl ( );
        }
        void addWorkControl ( )
        {
            workCon = new UIControl . Main . WorkControl ( );
            this . Controls . Add ( workCon );
            workCon . Dock = DockStyle . Fill;
            workCon . roundButton1 . Click += RoundButton1_Click3;
            workCon . button1 . Click += btnHar_Click;
            workCon . button2 . Click += btnInj_Click;
            workCon . button3 . Click += btnPai_Click;
            workCon . button4 . Click += btnAss_Click;
            workCon . button5 . Click += btnNew_Click;
            workCon . button6 . Click += btnLog_Click;
            workCon . button7 . Click += btnLEG_Click;
            workCon . button8 . Click += btnLED_Click;
            findAllBtn ( workCon );
        }
        private void RoundButton1_Click3 ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( workCon );
            addControl ( );
        }
        void addBoardCon ( )
        {
            boardCon = new UIControl . Main . BoardControl ( );
            this . Controls . Add ( boardCon );
            boardCon . Dock = DockStyle . Fill;
            boardCon . roundButton1 . Click += RoundButton1_Click4;
            boardCon . button1 . Click += btnHardBoard_Click;
            boardCon . button2 . Click += btnInjectionBoard_Click;
            boardCon . button3 . Click += btnPaintBoard_Click;
            boardCon . button4 . Click += btnLEGBoard_Click;
            boardCon . button5 . Click += btnAssBoard_Click;
            boardCon . button6 . Click += btnPurchaseBoard_Click;
            findAllBtn ( boardCon );
        }
        private void RoundButton1_Click4 ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( boardCon );
            addControl ( );
        }
        void addReportCon ( )
        {
            reportCon = new UIControl . Main . ReportControl ( );
            this . Controls . Add ( reportCon );
            reportCon . Dock = DockStyle . Fill;
            reportCon . roundButton1 . Click += RoundButton1_Click5;
            reportCon . button2 . Click += btnWages_Click;
            findAllBtn ( reportCon );
        }
        private void RoundButton1_Click5 ( object sender ,EventArgs e )
        {
            this . Controls . Remove ( reportCon );
            addControl ( );
        }
        void findAllBtn ( Control control )
        {
            foreach ( Control btn in control . Controls )
            {
                if ( btn . GetType ( ) == typeof ( Button ) )
                {
                    if ( btn . Tag != null )
                    {
                        LineProductMesEntityu . MainEntity model = getModel . Find ( ( x ) =>
                        {
                            return x . FOR003 == btn . Tag . ToString ( );
                        } );
                        if ( model == null )
                            btn . Enabled = false;
                    }
                }
            }
        }
        #endregion

        #region Event
        private void treeList1_CustomDrawNodeCell ( object sender ,DevExpress . XtraTreeList . CustomDrawNodeCellEventArgs e )
        {
            if ( e . Node . Selected )
            {
                e . Appearance . BackColor = Color . LightSteelBlue;
            }
        }
        private void treeList1_CustomDrawNodeImages ( object sender ,DevExpress . XtraTreeList . CustomDrawNodeImagesEventArgs e )
        {
            if ( e . Node . Nodes . Count > 0 )
            {
                if ( e . Node . Expanded )
                {
                    e . SelectImageIndex = 1;
                    return;
                }
                e . SelectImageIndex = 0;
            }
            else
                e . SelectImageIndex = 0;
        }
        private void treeList1_FocusedNodeChanged ( object sender ,FocusedNodeChangedEventArgs e )
        {
            //if ( treeList1 . Nodes . Count != 0 && e . Node . Id >= 0 )
            //{
            //    proId = e . Node . GetValue ( "FOR003" ) . ToString ( );
            //    if ( proId == string . Empty )
            //        return;
            //    if ( mdiChildForm . Contains ( proId ) )
            //        return;
            //    mdiChildForm . Add ( proId );
            //    UserInfoMation . programName = proId;
            //    proId = "LineProductMes." + proId;
            //    Assembly asm = Assembly . GetExecutingAssembly ( );
            //    Form doc = ( Form ) asm . CreateInstance ( proId );
            //    doc . MdiParent = this;
            //    //doc . WindowState = FormWindowState . Maximized;
            //    doc . Show ( );
            //    treeList1 . FocusedNode = treeList1 . Nodes [ 0 ];
            //}
        }
        private void treeList1_Click ( object sender ,EventArgs e )
        {
            idPro = string . Empty;
            TreeListNode node = null;
            node = treeList1 . FocusedNode;
            if ( treeList1 . Nodes . Count != 0 && node . Id >= 0 )
            {
                idPro = proId = node . GetValue ( "FOR003" ) . ToString ( );

                openTheForm ( proId ,idPro );
            }
        }
        //口令设置
        private void btnPassW_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            FormSetPW form = new FormSetPW ( );
            form . StartPosition = FormStartPosition . CenterScreen;
            form . Show ( );
        }
        //退出
        private void btnClose_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            this . Close ( );
        }
        //重新登录
        private void btnRe_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            Application . Restart ( );
        }
        //关于
        private void btnAbout_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            FormAbout form = new FormAbout ( );
            form . StartPosition = FormStartPosition . CenterScreen;
            form . Show ( );
        }
        //选择页签
        private void xtraTabbedMdiManager1_SelectedPageChanged ( object sender ,System . EventArgs e )
        {
            //if ( xtraTabbedMdiManager1 == null )
            //    return;
            //RibbonForm form = this . xtraTabbedMdiManager1 . MdiParent as RibbonForm;
            //if ( form . ActiveMdiChild == null )
            //    return;
            //UserInfoMation . programName = form . ActiveMdiChild . Name;
            //UserInfoMation . programText = form . ActiveMdiChild . Text;
        }
        //选择皮肤
        private void ribbonGalleryBarItem1_GalleryItemClick ( object sender ,GalleryItemClickEventArgs e )
        {
            string name = string . Empty;
            if ( ribbonGalleryBarItem1 . Gallery == null )
                return;
            name = ribbonGalleryBarItem1 . Gallery . GetCheckedItems ( ) [ 0 ] . Caption;
            //获取Configuration对象
            Configuration config = ConfigurationManager . OpenExeConfiguration ( Application . ExecutablePath );
            //删除<add>元素
            config . AppSettings . Settings . Remove ( "Feed" ); //增加<add> 元素
            config . AppSettings . Settings . Add ( "Feed" ,name );
            //一定要记得保存，写不带参数的config.Save()也可以
            config . Save ( ConfigurationSaveMode . Modified );
            //刷新，否则程序读取的还是之前的值（可能已装入内存）
            System . Configuration . ConfigurationManager . RefreshSection ( "appSettings" );
        }
        private void xtraTabbedMdiManager1_PageRemoved ( object sender ,DevExpress . XtraTabbedMdi . MdiTabPageEventArgs e )
        {
            string formName = e . Page . MdiChild . Name;
            if ( FormClosingState . mdiChildForm . Contains ( formName ) )
                FormClosingState . mdiChildForm . Remove ( formName );
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( FormClosingState . mdiChildForm . Count > 0 )
            {
                if ( ClassForMain . FormClosingState . formClost == true )
                {
                    if ( XtraMessageBox . Show ( "还有其他窗体未关闭,确认关闭?" ,"提示" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                        e . Cancel = true;
                }
            }

            base . OnClosing ( e );
        }
        //打开窗体
        void openTheForm ( string proId ,string idPro )
        {
            if ( proId == string . Empty )
                return;
            if ( FormClosingState . mdiChildForm . Contains ( idPro ) )
                return;

            if ( proId . Equals ( "FormAbout" ) )
            {
                btnAbout_ItemClick ( null ,null );
                return;
            }
            else if ( proId . Equals ( "Close" ) )
            {
                btnClose_ItemClick ( null ,null );
                return;
            }
            else if ( proId . Equals ( "FormSetPW" ) )
            {
                btnPassW_ItemClick ( null ,null );
                return;
            }
            else if ( proId . Equals ( "Restart" ) )
            {
                btnRe_ItemClick ( null ,null );
                return;
            }

            UserInfoMation . programName = proId;
            proId = "LineProductMes." + proId;
            Assembly asm = Assembly . GetExecutingAssembly ( );
            Form doc = ( Form ) asm . CreateInstance ( proId );
            if ( !doc . Name . Contains ( "Board" ) )
            {
                FormClosingState . mdiChildForm . Add ( idPro );
                //doc . MdiParent = this;
            }
            else
                doc . WindowState = FormWindowState . Maximized;
            doc . Show ( );
        }
        //工艺信息
        private void btnArt_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormArt";
            openTheForm ( proId ,idPro );
        }
        //人员信息
        private void btnUser_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormEmployee";
            openTheForm ( proId ,idPro );
        }
        //机台信息
        private void btnMac_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormMachinePlat";
            openTheForm ( proId ,idPro );
        }
        //模具信息
        private void btnMould_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormMouldInformation";
            openTheForm ( proId ,idPro );
        }
        //模具供应商
        private void btnSuMod_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormSupplierForMould";
            openTheForm ( proId ,idPro );
        }
        //成品主计划
        private void btnPlan_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormProductPlanPreview";
            openTheForm ( proId ,idPro );
        }
        //半成品计划一览表
        private void btnSiPlan_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormSemiPlanView";
            openTheForm ( proId ,idPro );
        }
        //采购计划一览表
        private void btnPur_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormPurProductView";
            openTheForm ( proId ,idPro );
        }
        //组装车间产线计划
        private void btnLine_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormLineForAssPlan";
            openTheForm ( proId ,idPro );
        }
        //组装报工单
        private void btnAss_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormAssNewWork";
            openTheForm ( proId ,idPro );
        }
        //组装附件报工单
        private void btnNew_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormAssNewWorkEnclosure";
            openTheForm ( proId ,idPro );
        }
        //物流组报工单
        private void btnLog_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormLogisticsNew";
            openTheForm ( proId ,idPro );
        }
        //五金报工单
        private void btnHar_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormHardWareWork";
            openTheForm ( proId ,idPro );
        }
        //注塑报工单
        private void btnInj_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormInjectionMolding";
            openTheForm ( proId ,idPro );
        }
        //面板报工单
        private void btnLED_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormLEDNewsPaper";
            openTheForm ( proId ,idPro );
        }
        //喷漆报工单
        private void btnPai_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormPaintNewspaper";
            openTheForm ( proId ,idPro );
        }
        //LED报工单
        private void btnLEG_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormLEGNews";
            openTheForm ( proId ,idPro );
        }
        //组装车间看板
        private void btnAssBoard_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormAssBoard";
            openTheForm ( proId ,idPro );
        }
        //采购看板
        private void btnPurchaseBoard_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormPurchaseBoard";
            openTheForm ( proId ,idPro );
        }
        //喷漆车间看板
        private void btnPaintBoard_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormPaintBoard";
            openTheForm ( proId ,idPro );
        }
        //五金看板
        private void btnHardBoard_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormHardBoard";
            openTheForm ( proId ,idPro );
        }
        //注塑看板
        private void btnInjectionBoard_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormInjectionBoard";
            openTheForm ( proId ,idPro );
        }
        //面板看板
        private void btnLEEBoard_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormLEEBoard";
            openTheForm ( proId ,idPro );
        }
        //LED看板
        private void btnLEGBoard_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormLEGBoard";
            openTheForm ( proId ,idPro );
        }
        //员工工资报表
        private void btnWages_Click ( object sender ,EventArgs e )
        {
            idPro = proId = "FormWages";
            openTheForm ( proId ,idPro );
        }
        #endregion

        /* xtraTabbedMdiManager1 窗体
         * http://makaidong.com/ws1996/15953_552258.html
         */
    }
}