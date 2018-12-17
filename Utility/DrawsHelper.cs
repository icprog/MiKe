using System;
using System . Collections . Generic;
using System . Drawing;
using System . Drawing . Drawing2D;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace Utility
{
    public class DrawsHelper
    {
        public static void Draw ( Rectangle rectangle ,Graphics g ,int _radius ,bool cusp ,Color begin_color ,Color end_color )
        {
            int span = 2;
            //抗锯齿
            g . SmoothingMode = SmoothingMode . AntiAlias;
            //渐变填充
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush ( rectangle ,begin_color ,end_color ,LinearGradientMode . Vertical );
            //填充
            g . FillPath ( myLinearGradientBrush ,DrawRoundRect ( rectangle . X ,rectangle . Y ,rectangle . Width - span ,rectangle . Height - 1 ,_radius ) );
        }

        public static GraphicsPath DrawRoundRect ( int x ,int y ,int width ,int height ,int radius )
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath ( );
            gp . AddArc ( x ,y ,radius ,radius ,180 ,90 );
            gp . AddArc ( width - radius ,y ,radius ,radius ,270 ,90 );
            gp . AddArc ( width - radius ,height - radius ,radius ,radius ,0 ,90 );
            gp . AddArc ( x ,height - radius ,radius ,radius ,90 ,90 );
            gp . CloseAllFigures ( );
            return gp;
        }
    }
}
