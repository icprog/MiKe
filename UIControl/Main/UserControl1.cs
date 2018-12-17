using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Drawing;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Forms;
using System . Drawing . Drawing2D;

namespace UIControl . Main
{
    public partial class UserControl1 :UserControl
    {
        public UserControl1 ( )
        {
            InitializeComponent ( );
        }


        private void UserControl1_Paint ( object sender ,PaintEventArgs e )
        {
            //Graphics g = e . Graphics;
            //Color FColor = Color . White;
            ////Color TColor = ColorTranslator . FromHtml ( "#029FF4" );
            //Color TColor = Color . LightGreen;
            //Brush b = new LinearGradientBrush ( this . ClientRectangle ,FColor ,TColor ,LinearGradientMode . Vertical );
            //g . FillRectangle ( b ,this . ClientRectangle );
        }
    }
}
