using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Drawing;
using System . Data;
using System . Text;
using System . Linq;
using System . Windows . Forms;
using DevExpress . XtraEditors;
using System . Drawing . Drawing2D;
using UILibrary;

namespace UIControl
{
    public partial class buttonRePaint :Button
    {
        public buttonRePaint ( ) : base ( )
        {
            InitializeComponent ( );

            this . SetStyle ( ControlStyles . UserPaint |//控制自行绘制，而不使用操作系统的绘制
                          ControlStyles . AllPaintingInWmPaint | //忽略擦出的消息，减少闪烁
                          ControlStyles . OptimizedDoubleBuffer | //绘制在缓冲区，不直接绘制在屏幕上，减少闪烁
                          ControlStyles . ResizeRedraw | //控件大小发生变化时重绘
                          ControlStyles . SupportsTransparentBackColor ,true );
        }

        //基颜色
        private Color _baseColor=Color.FromArgb(51,161,224);
        //控制状态
        private ControlState _controlState;
        private int _imageWidth=18;
        //圆角
        private RoundStyle _roundStyle=RoundStyle.All;
        //圆角半径
        private int _radius=8;


        [DefaultValue ( typeof ( Color ) ,"51,161,224" )]
        public Color BaseColor
        {
            get
            {
                return _baseColor;
            }
            set
            {
                _baseColor = value;
                base . Invalidate ( );
            }
        }

        [DefaultValue ( 18 )]//默认为18x  最小为12x
        public int ImageWidth
        {
            get
            {
                return _imageWidth;
            }
            set
            {
                if ( value != _imageWidth )
                {
                    _imageWidth = value < 12 ? 12 : value;
                    base . Invalidate ( );
                }
            }
        }

        [DefaultValue ( typeof ( RoundStyle ) ,"1" )]//默认全部都是圆角
        public RoundStyle RoundStyle
        {
            get
            {
                return _roundStyle;
            }
            set
            {
                if ( _roundStyle != value )
                {
                    _roundStyle = value;
                    base . Invalidate ( );
                }
            }
        }

        [DefaultValue ( 8 )]//设置圆角半径 默认值是8 最小值为4px
        public int Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if ( _radius != value )
                {
                    _radius = value < 4 ? 4 : value;
                    base . Invalidate ( );
                }
            }
        }

        internal ControlState ControlState //控件的状态
        {
            get
            {
                return _controlState;
            }
            set
            {
                if ( _controlState != value )
                {
                    _controlState = value;
                    base . Invalidate ( );
                }
            }
        }

        protected override void OnMouseEnter ( EventArgs e )//鼠标进入
        {
            base . OnMouseEnter ( e );
            ControlState = ControlState . Hover;//正常
        }

        protected override void OnMouseLeave ( EventArgs e )//鼠标离开
        {
            base . OnMouseLeave ( e );
            ControlState = ControlState . Normal;//正常
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle">表示矩形的大小和位置的数组</param>
        /// <param name="ga">画笔</param>
        /// <param name="_radius">圆的角度</param>
        /// <param name="cusp">是否画尖角</param>
        /// <param name="biginColor">渐变色的起始</param>
        /// <param name="endColor">渐变色的结束颜色</param>
        private void Draw (Rectangle rectangle,Graphics ga,int _radius,bool cusp,Color biginColor,Color endColor )
        {
            int span = 2;
            ga . SmoothingMode = System . Drawing . Drawing2D . SmoothingMode . AntiAlias;
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush ( rectangle ,biginColor ,endColor ,LinearGradientMode . Vertical );
            //画尖角
            if ( cusp )
            {
                span = 10;
                PointF p1 = new PointF ( rectangle . Width - 12 ,rectangle . Y + 10 );
                PointF p2 = new PointF ( rectangle . Width - 12 ,rectangle . Y + 30 );
                PointF p3 = new PointF ( rectangle . Width ,rectangle . Y + 20 );
                PointF [ ] ptsArray = new PointF [ ] { p1 ,p2 ,p3 };
                ga . FillPolygon ( myLinearGradientBrush ,ptsArray );
            }
            //填充
            ga . FillPath ( myLinearGradientBrush ,DrawRoundRect ( rectangle . X ,rectangle . Y ,rectangle . Width - span ,rectangle . Height - 1 ,_radius ) );
        }

        private static GraphicsPath DrawRoundRect ( int x ,int y ,int with ,int heigh ,int radius )
        {
            //四边圆角
            GraphicsPath g = new GraphicsPath ( );
            g . AddArc ( x ,y ,radius ,radius ,180 ,90 );
            g . AddArc ( with - radius ,y ,radius ,radius ,270 ,90 );
            g . AddArc ( with - radius ,heigh - radius ,radius ,radius ,0 ,90 );
            g . AddArc ( x ,heigh - radius ,radius ,radius ,90 ,90 );
            g . CloseAllFigures ( );
            return g;
        }

        private void buttonRePaint_Paint ( object sender ,PaintEventArgs e )
        {
            Draw ( e . ClipRectangle ,e . Graphics ,18 ,true ,Color . FromArgb ( 90 ,143 ,0 ) ,Color . FromArgb ( 41 ,67 ,0 ) );
            base . OnPaint ( e );
            Graphics g = e . Graphics;
            g . DrawString ( "你好" ,new Font ( "宋体" ,9 ,FontStyle . Regular ) ,new SolidBrush ( Color . White ) ,new PointF ( 10 ,10 ) );
        }

    }
}
