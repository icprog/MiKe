using System;
using System . Drawing;
using System . Drawing . Drawing2D;

namespace LineProductMes . ClassForMain
{
    public static class Graph
    {

        public static void gra ( DevExpress . XtraTab . XtraTabControl sp ,string fontName )
        {
            sp . TabPages [ 0 ] . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll.UserInfoMation.FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,3 );
                        gp . DrawEllipse ( pen ,600 ,10 ,60 ,45 );
                        Font myFont = new Font ( "宋体" ,18 ,FontStyle . Bold );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,600 ,20 );
                    }
                    //else
                    //{
                    //    Pen pen = new Pen ( sp . BackColor ,3 );
                    //    gp . DrawEllipse ( pen ,sp . Location . X ,sp . Location . Y ,sp . Width ,sp . Height );
                    //    Font myFont = new Font ( "宋体" ,18 ,FontStyle . Bold );
                    //    Brush bush = new SolidBrush ( sp . BackColor );
                    //    gp . DrawString ( "" ,myFont ,bush ,600 ,20 );
                    //}
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

        public static void gra ( DevExpress . XtraEditors .SplitGroupPanel sp ,string fontName )
        {
            sp . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll . UserInfoMation . FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,2 );
                        gp . DrawEllipse ( pen ,10 ,sp.Location.Y+10 ,60 ,110 );
                        Font myFont = new Font ( "楷体" ,18 );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,25 ,sp . Location . Y + 30 ,new StringFormat ( StringFormatFlags . DirectionVertical ) );
                    }
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

        public static void grb ( DevExpress . XtraEditors . SplitGroupPanel sp ,string fontName )
        {
            sp . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll . UserInfoMation . FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,2 );
                        gp . DrawEllipse ( pen ,10 ,sp . Location . Y + 10 ,60 ,110 );
                        Font myFont = new Font ( "楷体" ,18 );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,25 ,sp . Location . Y + 30 ,new StringFormat ( StringFormatFlags . DirectionVertical ) );
                    }
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

        /// <summary>
        /// 面板报工 LED
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="fontName"></param>
        public static void grPic ( DevExpress . XtraEditors . PictureEdit sp ,string fontName )
        {
            sp . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll . UserInfoMation . FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,2 );
                        Rectangle rec = new Rectangle ( sp . Width  / 8 ,sp . Width / 8 ,40 ,60 );
                        gp . DrawPath ( pen ,DrawRoundRect ( rec ,12 ) );
                        //gp . DrawEllipse ( pen ,10 ,sp . Location . Y + 15 ,120 ,60 );
                        Font myFont = new Font ( "隶书" ,15 ,FontStyle . Bold );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,sp . Width / 8 + 1 ,sp . Height / 8 + 1 ,new StringFormat ( StringFormatFlags . DirectionVertical ) );
                    }
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

        /// <summary>
        /// 组装  五金  注塑
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="fontName"></param>
        public static void grPicZ ( DevExpress . XtraEditors . PictureEdit sp ,string fontName )
        {
            sp . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll . UserInfoMation . FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,2 );
                        Rectangle rec = new Rectangle ( sp . Width / 4 ,sp . Height / 4 ,50 ,90 );
                        //gp . DrawRectangle ( pen ,rec );
                        gp . DrawPath ( pen ,DrawRoundRect ( rec ,12 ) );
                        //gp . DrawEllipse ( pen ,10 ,sp . Location . Y - 5 ,60 ,120 );
                        Font myFont = new Font ( "隶书" ,15 ,FontStyle . Bold );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,sp . Width / 4 + 2 ,sp . Height / 4 + 5 ,new StringFormat ( StringFormatFlags . DirectionVertical ) );
                    }
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

        public static GraphicsPath DrawRoundRect ( Rectangle rec ,int radius )
        {
            //四边圆角
            GraphicsPath gp = new GraphicsPath ( );
            gp . AddArc ( rec.X ,rec . Y ,radius ,radius ,180 ,90 );
            gp . AddArc ( rec.Width - radius ,rec.Y ,radius ,radius ,270 ,90 );
            gp . AddArc ( rec.Width - radius ,rec.Height - radius ,radius ,radius ,0 ,90 );
            gp . AddArc ( rec.X ,rec.Height - radius ,radius ,radius ,90 ,90 );
            gp . CloseAllFigures ( );
            return gp;
        }

        /// <summary>
        /// 组装附件报工单  注塑  喷漆
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="fontName"></param>
        public static void grPicS ( DevExpress . XtraEditors . PictureEdit sp ,string fontName )
        {
            sp . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll . UserInfoMation . FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,4 );
                        gp . DrawEllipse ( pen ,10 ,sp . Location . Y  ,120 ,60 );
                        Font myFont = new Font ( "楷体" ,19 ,FontStyle . Bold );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,105 ,sp . Location . Y + 15 ,new StringFormat ( StringFormatFlags . DirectionRightToLeft ) );
                    }
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

        /// <summary>
        /// 物流组报工
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="fontName"></param>
        public static void grPicW ( DevExpress . XtraEditors . PictureEdit sp ,string fontName )
        {
            sp . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll . UserInfoMation . FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,4 );
                        gp . DrawEllipse ( pen ,10 ,sp . Location . Y - 5 ,80 ,40 );
                        Font myFont = new Font ( "楷体" ,18 ,FontStyle . Bold );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,85 ,sp . Location . Y + 5 ,new StringFormat ( StringFormatFlags . DirectionRightToLeft ) );
                    }
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

        /// <summary>
        /// 成品主计划
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="fontName"></param>
        public static void grPicSMIN ( DevExpress . XtraEditors . PictureEdit sp ,string fontName )
        {
            sp . Paint += ( s ,e ) =>
            {
                try
                {
                    Point p = sp . PointToScreen ( sp . Location );
                    Graphics gp = e . Graphics;
                    gp . Clear ( LineProductMesBll . UserInfoMation . FeedColor );
                    if ( !fontName . Contains ( "反" ) )
                    {
                        //画填充圆
                        //SolidBrush s = new SolidBrush ( Color . Red );
                        //画空心圆
                        Pen pen = new Pen ( Color . Red ,2 );
                        gp . DrawEllipse ( pen ,5 ,sp . Location . Y - 8 ,110 ,45 );
                        Font myFont = new Font ( "楷体" ,18 );
                        Brush bush = new SolidBrush ( Color . Red );
                        gp . DrawString ( fontName ,myFont ,bush ,95 ,sp . Location . Y + 5 ,new StringFormat ( StringFormatFlags . DirectionRightToLeft ) );
                    }
                }
                catch ( Exception ex )
                {
                    Utility . LogHelper . WriteLog ( ex . Message );
                }
            };
        }

    }
}
