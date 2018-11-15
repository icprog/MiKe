/* ==================================
 * Author    :Coder.Yan
 * CreateTime:2012/8/30 22:10:12
 * Copyright :CY©2012
 * ==================================*/
using System . Drawing;

namespace CYControls
{
    [ToolboxBitmap(typeof(EllipseShape),"Resources.EllipseShape.png")]
    public class EllipseShape:CYBaseControl
    {
        protected override void OnPaint ( System . Windows . Forms . PaintEventArgs e )
        {
            base . OnPaint ( e );
            e . Graphics . FillEllipse ( BackgroundBrush ,this . ClientRectangle );
            e . Graphics . DrawEllipse ( ForegroundPen ,this . DrawRect );
            Font myFont = new Font ( "宋体" ,9 ,FontStyle . Bold );
            Brush bush = new SolidBrush ( Color . Red );
            e . Graphics . DrawString ( "注销" ,myFont ,bush ,e . ClipRectangle . X + 10 ,e . ClipRectangle . Y + 10 );
        }

    }
}
