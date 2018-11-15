using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Drawing;
using System . Data;
using System . Linq;
using System . Text;
using System . Windows . Forms;

namespace UIControl
{
    public partial class TextLine :TextBox
    {
        public TextLine ( )
        {
            InitializeComponent ( );

            this . Width = 100;
            this . BorderStyle = BorderStyle . None;
        }

        private Color _linecolor = Color.Black ;
        /// <summary>
        /// 线条颜色
        /// </summary>
        public Color LineColor
        {
            get
            {
                return this . _linecolor;
            }
            set
            {
                this . _linecolor = value;
                this . Invalidate ( );
            }
        }
        private const int WM_PAINT = 0xF;
        protected override void WndProc ( ref Message m )
        {
            base . WndProc ( ref m );
            if ( m . Msg == WM_PAINT )
            {
                DrawLine ( );
            }
        }
        private void DrawLine ( )
        {
            Graphics g = this . CreateGraphics ( );
            using ( Pen p = new Pen ( this . _linecolor ) )
            {
                g . DrawLine ( p ,0 ,this . Height - 1 ,this . Width ,this . Height - 1 );
            }
        }

    }
}
