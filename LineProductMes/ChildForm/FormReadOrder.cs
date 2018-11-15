using System;
using System . Data;
using System . Windows . Forms;
using DevExpress . XtraEditors;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes . ChildForm
{
    public partial class FormReadOrder :FormBaseChild
    {
        LineProductMesBll.Bll.ProductPlanBll _bll=null;
        DataTable tableView,tableResult,tableOther,tableViewOther;
        bool result=false;
        
        public FormReadOrder ( DataTable tableView )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . ProductPlanBll ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            this . tableViewOther = tableView;

            InitData ( );
        }
        
        void InitData ( )
        {
            tableView = _bll . getOrder ( );
            gridControl1 . DataSource = tableView;
        }
        private void btnOk_Click ( object sender ,EventArgs e )
        {
            int [ ] selectRows = gridView1 . GetSelectedRows ( );
            if ( selectRows . Length < 1 )
            {
                XtraMessageBox . Show ( "请选择需要排产的订单" );
                return;
            }
            tableResult = null;
            tableResult = tableView . Clone ( );
            DataRow row;
            foreach ( int i in selectRows )
            {
                row = gridView1 . GetDataRow ( i );
                DataRow dr = tableResult . NewRow ( );
                dr [ "IBB001" ] = row [ "IBB001" ];
                dr [ "IBB002" ] = row [ "IBB002" ];
                dr [ "IBB003" ] = row [ "IBB003" ];
                dr [ "IBB004" ] = row [ "IBB004" ];
                dr [ "IBB006" ] = row [ "IBB006" ];
                dr [ "IBB007" ] = row [ "IBB007" ];
                dr [ "IBB009" ] = row [ "IBB009" ];
                dr [ "IBB010" ] = row [ "IBB010" ];
                dr [ "IBB011" ] = row [ "IBB011" ];
                dr [ "IBB013" ] = row [ "IBB013" ];
                tableResult . Rows . Add ( dr );
            }

            if ( checkOut ( ) == false )
                return;

            result = true;

            DateTime? dtOne, dtTwo, dtTre, dtFor;
            int One, Two, Tre;

            tableOther = _bll . getArrayOrderAndProduct ( tableResult );
            if ( tableOther != null && tableOther . Rows . Count > 0 )
            {
                foreach ( DataRow ro in tableResult . Rows )
                {
                    //历史最小排期
                    dtOne = Convert . ToDateTime ( tableOther . Compute ( "MIN(PRE005)" ,"PRE002='" + ro [ "IBB001" ] + "' AND PRE003='" + ro [ "IBB002" ] + "' AND PRE004='" + ro [ "IBB003" ] + "'" ) );
                    //历史最大排期
                    dtTwo = Convert . ToDateTime ( tableOther . Compute ( "MAX(PRE005)" ,"PRE002='" + ro [ "IBB001" ] + "' AND PRE003='" + ro [ "IBB002" ] + "' AND PRE004='" + ro [ "IBB003" ] + "'" ) );
                    //现在最小排期
                    dtTre = Convert . ToDateTime ( ro [ "IBB007" ] );
                    One = Convert . ToInt32 ( ro [ "IBB009" ] );
                    Two = Convert . ToInt32 ( ro [ "IBB010" ] );
                    Tre = Two % One;
                    if ( Tre > 0 )
                        Tre = Two / One + 1;
                    else
                        Tre = Two / One;
                    //现在最大排期
                    dtFor = Convert . ToDateTime ( dtTre ) . AddDays ( Tre );
                    if ( dtOne >= dtFor || dtTwo >= dtTre )
                    {
                        XtraMessageBox . Show ( "订单号:" + ro [ "IBB001" ] + "\n\r序号:" + ro [ "IBB002" ] + "\n\r品号:" + ro [ "IBB003" ] + "\n\r排期与之前的排期交叉,请重新选择开始日期" ,"提示" );
                        result = false;
                        break;
                    }
                }
                if ( result == false )
                    return;
            }

            if ( tableViewOther != null && tableViewOther . Rows . Count > 0 )
            {
                foreach ( DataRow ro in tableResult . Rows )
                {
                    if ( tableViewOther . Select ( "PRE002='" + ro [ "IBB001" ] + "' AND PRE003='" + ro [ "IBB002" ] + "' AND PRE004='" + ro [ "IBB003" ] + "'" ) . Length > 0 )
                    {
                        dtOne = Convert . ToDateTime ( tableViewOther . Compute ( "MIN(PRE005)" ,"PRE002='" + ro [ "IBB001" ] + "' AND PRE003='" + ro [ "IBB002" ] + "' AND PRE004='" + ro [ "IBB003" ] + "'" ) );

                        dtTwo = Convert . ToDateTime ( tableViewOther . Compute ( "MAX(PRE005)" ,"PRE002='" + ro [ "IBB001" ] + "' AND PRE003='" + ro [ "IBB002" ] + "' AND PRE004='" + ro [ "IBB003" ] + "'" ) );

                        dtTre = Convert . ToDateTime ( ro [ "IBB007" ] );
                        One = Convert . ToInt32 ( ro [ "IBB009" ] );
                        Two = Convert . ToInt32 ( ro [ "IBB010" ] );
                        Tre = Two % One;
                        if ( Tre > 0 )
                            Tre = Two / One + 1;
                        else
                            Tre = Two / One;
                        dtFor = Convert . ToDateTime ( dtTre ) . AddDays ( Tre );
                        if ( dtOne <= dtFor || dtTwo >= dtTre )
                        {
                            XtraMessageBox . Show ( "订单号:" + ro [ "IBB001" ] + "\n\r序号:" + ro [ "IBB002" ] + "\n\r品号:" + ro [ "IBB003" ] + "\n\r排期与之前的排期交叉,请重新选择开始日期" ,"提示" );
                            result = false;
                            break;
                        }
                    }
                }
                if ( result == false )
                    return;
            }

            _bll . EditOrder ( tableResult );

            this . DialogResult = DialogResult . OK;
        }
        private void btnCancel_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = DialogResult . Cancel;
        }
        
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            DataRow row = gridView1 . GetDataRow ( e . RowHandle );
            if ( row == null )
                return;
            LineProductMesEntityu . ProductPlanBodyEntity model = new LineProductMesEntityu . ProductPlanBodyEntity ( );
            //交货日期
            model . PRE006 = Convert . ToDateTime ( row [ "IBB013" ] ); //排产数量
            model . PRE008 = Convert . ToInt32 ( row [ "IBB010" ] );
            //日产量
            model . PRE009 = Convert . ToInt32 ( row [ "IBB009" ] );
            //开始日期
            model . PRE005 = Convert . ToDateTime ( row [ "IBB007" ] );
            if ( model . PRE009 == 0 )
                return;
            int days = Convert . ToInt32 ( model . PRE008 / model . PRE009 );
            if ( model . PRE008 % model . PRE009 > 0 )
                days++;
            if ( ( Convert . ToDateTime ( model . PRE006 ) - Convert . ToDateTime ( model . PRE005 ) . AddDays ( days ) ) . Days != 0 )
                row [ "U0" ] = ( Convert . ToDateTime ( model . PRE006 ) - Convert . ToDateTime ( model . PRE005 ) . AddDays ( days ) ) . Days;
        }

        public DataTable getTable
        {
            get
            {
                return tableResult;
            }
        }
        bool checkOut ( )
        {
            result = true;

            if ( tableResult == null || tableResult . Rows . Count < 1 )
                return false;

            DateTime dtStart;
            int One, Two, Tre;

            string orderNum, num;

            foreach ( DataRow row in tableResult . Rows )
            {
                orderNum = row [ "IBB001" ] . ToString ( );
                num = row [ "IBB002" ] . ToString ( );
                if ( row [ "IBB007" ] == null || row [ "IBB007" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "订单号:"+orderNum+"\n\r序号:"+num+"\n\r请选择开始日期" );
                    result = false;
                    break;
                }
                dtStart = Convert . ToDateTime ( row [ "IBB007" ] );
                if ( ( dtStart - LineProductMesBll . UserInfoMation . sysTime ) . Days < 0 )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r请重新选择开始日期" );
                    result = false;
                    break;
                }
                if ( row [ "IBB013" ] == null || row [ "IBB013" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r交货日期不可为空" );
                    result = false;
                    break;
                }
                if ( row [ "IBB009" ] == null || row [ "IBB009" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r请重新录入日产量" );
                    result = false;
                    break;
                }
                if ( row [ "IBB010" ] == null || row [ "IBB010" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r请重新录入排产数量" );
                    result = false;
                    break;
                }
                One = string . IsNullOrEmpty ( row [ "IBB011" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "IBB011" ] . ToString ( ) );
                Two = Convert . ToInt32 ( row [ "IBB009" ] . ToString ( ) );
                Tre = Convert . ToInt32 ( row [ "IBB010" ] . ToString ( ) );
                if ( Two == 0 )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r请录入日产量" );
                    result = false;
                    break;
                }
                if ( Tre == 0 )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r请录入排产数量" );
                    result = false;
                    break;
                }
                if ( Two > One )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r日产量大于剩余产量,不允许" );
                    result = false;
                    break;
                }
                if ( Tre > One )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r排产数量大于剩余产量,不允许" );
                    result = false;
                    break;
                }
                if ( Two > Tre )
                {
                    XtraMessageBox . Show ( "订单号:" + orderNum + "\n\r序号:" + num + "\n\r日产量大于排产数量,不允许" );
                    result = false;
                    break;
                }
            }

            return result;
        }

    }
}