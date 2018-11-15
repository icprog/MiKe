using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connection
{
    public partial class Form1 : Form
    {
        public Form1 ( )
        {
            InitializeComponent( );
        }

        ConnectionBll.Bll.ConnecBll _bll = new ConnectionBll.Bll.ConnecBll( );
        string sign = "";

        private void Form1_Load ( object sender ,EventArgs e )
        {
            comboBox1.Text = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"host" );
            if ( comboBox1.Text != "." )
                comboBox1.Items.Add( "." );
            textBox2.Text = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"user" );
            textBox1.Text = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"password" );
            comboBox3.Text = Utility.IniUtil.ReadIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"dbname" );
        }
        //Password
        private void textBox1_TextChanged ( object sender ,EventArgs e )
        {
            DataTable da = new DataTable( );
            try
            {
                bool result = _bll.TestConnection( "Data Source=" + comboBox1.Text + ";Initial Catalog=master;User Id=" + textBox2.Text + ";Password=" + textBox1.Text + "" );
                if ( result == true )
                {
                    da = _bll.GetDataTable( "Data Source=" + comboBox1.Text + ";Initial Catalog=master;User Id=" + textBox2.Text + ";Password=" + textBox1.Text + "" );
                    if ( da != null && da.Rows.Count > 0 )
                    {
                        comboBox3.DataSource = da;
                        comboBox3.DisplayMember = "name";
                    }
                    sign = "1";
                }
                else
                    sign = "";
            }
            catch
            {
            }
        }
        //TestConnect
        private void button3_Click ( object sender ,EventArgs e )
        {
            bool result = _bll.TestConnection( "Data Source=" + comboBox1.Text + ";Initial Catalog=master;User Id=" + textBox2.Text + ";Password=" + textBox1.Text + "" );
            if ( result == true )
            {
                MessageBox.Show( "成功连接数据库" );
                sign = "1";
            }
            else
            {
                MessageBox.Show( "连接数据库失败,请重试" );
                sign = "";
            }
        }
        //Sure
        private void button1_Click ( object sender ,EventArgs e )
        {
            if ( string.IsNullOrEmpty( comboBox1.Text ) )
            {
                MessageBox.Show( "请选择或填写数据库名称" );
                return;
            }
            if ( string.IsNullOrEmpty( textBox2.Text ) )
            {
                MessageBox.Show( "请填写用户名" );
                return;
            }
            if ( string.IsNullOrEmpty( comboBox3.Text ) )
            {
                MessageBox.Show( "请选择数据库" );
                return;
            }
            if ( sign == "" )
            {
                MessageBox.Show( "连接数据库失败,请重试" );
                return;
            }
            Utility.IniUtil.WriteIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"host" ,comboBox1.Text );
            Utility.IniUtil.WriteIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"dbname" ,comboBox3.Text );
            Utility.IniUtil.WriteIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"user" ,textBox2.Text );
            Utility.IniUtil.WriteIniValue( AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini" ,"DB" ,"password" ,textBox1.Text );
            sign = "";
            this.Close( );
        }
        //Cancel
        private void button2_Click ( object sender ,EventArgs e )
        {
            this.Close( );
        }
    }
}
