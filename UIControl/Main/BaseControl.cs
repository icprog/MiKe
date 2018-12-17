using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Drawing;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Forms;
using Utility;

namespace UIControl . Main
{
    public partial class BaseControl :UserControl1
    {
        public BaseControl ( )
        {
            InitializeComponent ( );
            DrawBtn ( );
        }

        void DrawBtn ( )
        {
            foreach ( Control control in this . Controls )
            {
                if ( control . GetType ( ) == typeof ( Button ) )
                    control . Paint += Control_Paint;
            }
        }

        private void Control_Paint ( object sender ,PaintEventArgs e )
        {
            DrawsHelper . Draw ( e . ClipRectangle ,e . Graphics ,18 ,false ,Color . DarkSeaGreen ,Color . LightGreen );

            base . OnPaint ( e );

            Graphics g = e . Graphics;

            Button btn = sender as Button;
            StringFormat strF = new StringFormat ( );
            strF . Alignment = StringAlignment . Center;
            strF . LineAlignment = StringAlignment . Center;
            g . DrawString ( btn . Text ,new Font ( "微软雅黑" ,14 ,FontStyle . Bold ) ,new SolidBrush ( Color . Black ) ,btn . ClientRectangle ,strF );
        }

        private void BaseControl_SizeChanged ( object sender ,EventArgs e )
        {
            DrawLocation ( );
        }

        void DrawLocation ( )
        {
            int x = this . Width / 2;
            int y = 20;
            labelControl1 . Location = new Point ( x - labelControl1 . Width / 2 ,y + labelControl1 . Height / 2 );
            button1 . Location = new Point ( x - 280 ,y + labelControl1 . Height + 100 );
            button3 . Location = new Point ( x + 160 ,y + labelControl1 . Height + 100 );
            button4 . Location = new Point ( x - 60 ,y + labelControl1 . Height + 280 );
            button5 . Location = new Point ( x - 280 ,y + labelControl1 . Height + 280 );
            button6 . Location = new Point ( x + 160 ,y + labelControl1 . Height + 280 );
        }


    }
}
