using DevExpress . XtraEditors;
using System;
using System . Windows . Forms;

namespace LineProductMes . ChildForm
{
    public partial class PowerEdit :FormBaseChild
    {
        LineProductMesBll.Bll.PowerBll _bll=null;
        LineProductMesEntityu.PowerEntity _model=null;
        string sign=string.Empty,nameOfPerson=string.Empty,nameOfProgram=string.Empty;
        
        public PowerEdit ( string text ,LineProductMesEntityu . PowerEntity entity ,string nameOfPerson ,string nameOfProgram ,string sign )
        {
            InitializeComponent ( );
            
            _bll = new LineProductMesBll . Bll . PowerBll ( );
            _model = new LineProductMesEntityu . PowerEntity ( );

            this . Text = text;
            this . sign = sign;
            this . _model = entity;
            this . nameOfPerson = nameOfPerson;
            this . nameOfProgram = nameOfProgram;

            
            if ( sign . Equals ( "edit" ) )
                lupNamePerson . Enabled = lupNamePro . Enabled = false;
            else
                lupNamePerson . Enabled = lupNamePro . Enabled = true;
            lupNamePerson . Properties . DataSource = _bll . GetPerson ( );
            lupNamePerson . Properties . DisplayMember = "EMP002";
            lupNamePerson . Properties . ValueMember = "EMP001";
            lupNamePro . Properties . DataSource = _bll . GetProgram ( );
            lupNamePro . Properties . DisplayMember = "FOR004";
            lupNamePro . Properties . ValueMember = "FOR003";
        }
        
        private void btnOk_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( sign ) )
            {
                XtraMessageBox . Show ( "请重新选择新增或编辑" );
                return;
            }
            errorProvider1 . Clear ( );
            bool isOk = true;
            if ( string . IsNullOrEmpty ( lupNamePerson . Text ) )
            {
                errorProvider1 . SetError ( lupNamePerson ,"人员姓名不可为空" );
                isOk = false;
            }
            if ( string . IsNullOrEmpty ( lupNamePro . Text ) )
            {
                errorProvider1 . SetError ( lupNamePro ,"程序名称不可为空" );
                isOk = false;
            }
            if ( isOk == false )
                return;

            _model = new LineProductMesEntityu . PowerEntity ( );
            _model . idx = cekRun . Tag == null ? 0 : int . Parse ( cekRun . Tag . ToString ( ) );
            _model . POW002 = lupNamePerson . EditValue . ToString ( );
            _model . POW003 = lupNamePro . EditValue . ToString ( );
            _model . POW013 = cekRun . Checked == true ? true : false;
            _model . POW004 = cekQuery . Checked == true ? true : false;
            _model . POW005 = cekAdd . Checked == true ? true : false;
            _model . POW006 = cekDelete . Checked == true ? true : false;
            _model . POW007 = cekEdit . Checked == true ? true : false;
            _model . POW008 = cekReview . Checked == true ? true : false;
            _model . POW009 = cekExamin . Checked == true ? true : false;
            _model . POW010 = cekSave . Checked == true ? true : false;
            _model . POW011 = cekCancel . Checked == true ? true : false;
            _model . POW012 = cekCancell . Checked == true ? true : false;
            _model . POW016 = cekPrint . Checked == true ? true : false;
            _model . POW017 = cekExample . Checked == true ? true : false;

            if ( sign .Equals( "add") )
            {
                isOk = _bll . Exsits ( _model );
                if ( isOk == true )
                {
                    XtraMessageBox . Show ( "" + lupNamePerson . Text + "已经存在" + lupNamePro . Text + "的权限分配记录" );
                    return;
                }
                _model . idx = _bll . Add ( _model );
                if ( _model . idx > 0 )
                {
                    XtraMessageBox . Show ( "新增成功" );
                    this . DialogResult = System . Windows . Forms . DialogResult . OK;
                }
                else
                    XtraMessageBox . Show ( "新增失败,请重试" );
            }
            else if ( sign.Equals( "edit" ))
            {
                
                //int idx = _bll . EditIdx ( _model );
                //if ( _model . idx != idx )
                //{
                //    XtraMessageBox . Show ( "" + lupNamePerson . Text + "已经存在" + lupNamePro . Text + "的权限分配记录" );
                //    return;
                //}
                isOk = _bll . Edit ( _model );
                if ( isOk == true )
                {
                    XtraMessageBox . Show ( "编辑成功" );
                    this . DialogResult = DialogResult . OK;
                }
                else
                    XtraMessageBox . Show ( "编辑失败,请重试" );
            }
        }

        private void btnCan_Click ( object sender ,EventArgs e )
        {
            this . DialogResult = System . Windows . Forms . DialogResult . Cancel;
        }

        private void cekQuery_CheckedChanged ( object sender ,EventArgs e )
        {
            if ( cekRun . Checked == false )
                cekRun . Checked = true;
        }

        private void btnCheckAll_Click ( object sender ,EventArgs e )
        {
            cekRun . Checked = true;
            cekQuery . Checked = true;
            cekAdd . Checked = true;
            cekDelete . Checked = true;
            cekEdit . Checked = true;
            cekReview . Checked = true;
            cekExamin . Checked = true;
            cekSave . Checked = true;
            cekCancel . Checked = true;
            cekCancell . Checked = true;
            cekPrint . Checked = true;
            cekExample . Checked = true;
        }

        private void cekRun_CheckedChanged ( object sender ,EventArgs e )
        {
            if ( cekRun . Checked == false )
            {
                //cekRun . Checked =  false;
                cekQuery . Checked = false;
                cekAdd . Checked = false;
                cekDelete . Checked = false;
                cekEdit . Checked = false;
                cekReview . Checked = false;
                cekExamin . Checked = false;
                cekSave . Checked = false;
                cekCancel . Checked = false;
                cekCancell . Checked = false;
                cekPrint . Checked = false;
                cekExample . Checked = false;
            }
        }

        private void PowerEdit_Load ( object sender ,EventArgs e )
        {
            lupNamePerson . Text = nameOfPerson;
            lupNamePro . Text = nameOfProgram;
            if ( _model == null )
                return;
            cekRun . Tag = _model . idx;
            cekRun . Checked = _model . POW013 == true ? true : false;
            cekQuery . Checked = _model . POW004 == true ? true : false;
            cekAdd . Checked = _model . POW005 == true ? true : false;
            cekDelete . Checked = _model . POW006 == true ? true : false;
            cekEdit . Checked = _model . POW007 == true ? true : false;
            cekReview . Checked = _model . POW008 == true ? true : false;
            cekExamin . Checked = _model . POW009 == true ? true : false;
            cekSave . Checked = _model . POW010 == true ? true : false;
            cekCancel . Checked = _model . POW011 == true ? true : false;
            cekCancell . Checked = _model . POW012 == true ? true : false;
            cekPrint . Checked = _model . POW016 == true ? true : false;
            cekExample . Checked = _model . POW017 == true ? true : false;
        }

    }
}
