namespace UILibrary
{
    //using CCWin.SkinClass;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    internal class RenderHelper
    {
        internal static Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int num = colorBase.A;
            int num2 = colorBase.R;
            int num3 = colorBase.G;
            int num4 = colorBase.B;
            if ((a + num) > 0xff)
            {
                a = 0xff;
            }
            else
            {
                a = Math.Max(0, a + num);
            }
            if ((r + num2) > 0xff)
            {
                r = 0xff;
            }
            else
            {
                r = Math.Max(0, r + num2);
            }
            if ((g + num3) > 0xff)
            {
                g = 0xff;
            }
            else
            {
                g = Math.Max(0, g + num3);
            }
            if ((b + num4) > 0xff)
            {
                b = 0xff;
            }
            else
            {
                b = Math.Max(0, b + num4);
            }
            return Color.FromArgb(a, r, g, b);
        }

        internal static void RenderAlphaImage(Graphics g, Image image, Rectangle imageRect, float alpha)
        {
            using (ImageAttributes attributes = new ImageAttributes())
            {
                ColorMap map = new ColorMap {
                    OldColor = Color.FromArgb(0xff, 0, 0xff, 0),
                    NewColor = Color.FromArgb(0, 0, 0, 0)
                };
                ColorMap[] mapArray = new ColorMap[] { map };
                attributes.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
                float[][] numArray2 = new float[5][];
                float[] numArray3 = new float[5];
                numArray3[0] = 1f;
                numArray2[0] = numArray3;
                float[] numArray4 = new float[5];
                numArray4[1] = 1f;
                numArray2[1] = numArray4;
                float[] numArray5 = new float[5];
                numArray5[2] = 1f;
                numArray2[2] = numArray5;
                float[] numArray6 = new float[5];
                numArray6[3] = alpha;
                numArray2[3] = numArray6;
                float[] numArray7 = new float[5];
                numArray7[4] = 1f;
                numArray2[4] = numArray7;
                float[][] newColorMatrix = numArray2;
                ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(image, imageRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
        }

        internal static void RenderArrowInternal(Graphics g, Rectangle dropDownRect, ArrowDirection direction, Brush brush)
        {
            Point point = new Point(dropDownRect.Left + (dropDownRect.Width / 2), dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] { new Point(point.X + 1, point.Y - 4), new Point(point.X + 1, point.Y + 4), new Point(point.X - 2, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] { new Point(point.X - 4, point.Y + 1), new Point(point.X + 4, point.Y + 1), new Point(point.X, point.Y - 2) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] { new Point(point.X - 2, point.Y - 4), new Point(point.X - 2, point.Y + 4), new Point(point.X + 1, point.Y) };
                    break;

                default:
                    points = new Point[] { new Point(point.X - 4, point.Y - 1), new Point(point.X + 4, point.Y - 1), new Point(point.X, point.Y + 2) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        internal static void RenderBackgroundInternal(Graphics g, Rectangle rect, Color baseColor, Color borderColor, Color innerBorderColor, RoundStyle style, bool drawBorder, bool drawGlass, LinearGradientMode mode)
        {
            RenderBackgroundInternal(g, rect, baseColor, borderColor, innerBorderColor, style, 8, drawBorder, drawGlass, mode);
        }

        internal static void RenderBackgroundInternal(Graphics g, Rectangle rect, Color baseColor, Color borderColor, Color innerBorderColor, RoundStyle style, int roundWidth, bool drawBorder, bool drawGlass, LinearGradientMode mode)
        {
            RenderBackgroundInternal(g, rect, baseColor, borderColor, innerBorderColor, style, 8, 0.45f, drawBorder, drawGlass, mode);
        }

        internal static void RenderBackgroundInternal(Graphics g, Rectangle rect, Color baseColor, Color borderColor, Color innerBorderColor, RoundStyle style, int roundWidth, float basePosition, bool drawBorder, bool drawGlass, LinearGradientMode mode)
        {
            if (drawBorder)
            {
                rect.Width--;
                rect.Height--;
            }
            if ((rect.Width != 0) && (rect.Height != 0))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Transparent, Color.Transparent, mode))
                {
                    Color[] colorArray = new Color[] { GetColor(baseColor, 0, 0x23, 0x18, 9), GetColor(baseColor, 0, 13, 8, 3), baseColor, GetColor(baseColor, 0, 0x23, 0x18, 9) };
                    ColorBlend blend = new ColorBlend();
                    float[] numArray = new float[4];
                    numArray[1] = basePosition;
                    numArray[2] = basePosition + 0.05f;
                    numArray[3] = 1f;
                    blend.Positions = numArray;
                    blend.Colors = colorArray;
                    brush.InterpolationColors = blend;
                    if (style != RoundStyle.None)
                    {
                        using (GraphicsPath path = GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                        {
                            g.FillPath(brush, path);
                        }
                        if (drawGlass)
                        {
                            if (baseColor.A > 80)
                            {
                                Rectangle rectangle = rect;
                                if (mode == LinearGradientMode.Vertical)
                                {
                                    rectangle.Height = (int) (rectangle.Height * basePosition);
                                }
                                else
                                {
                                    rectangle.Width = (int) (rect.Width * basePosition);
                                }
                                using (GraphicsPath path2 = GraphicsPathHelper.CreatePath(rectangle, roundWidth, RoundStyle.Top, false))
                                {
                                    using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(0x80, 0xff, 0xff, 0xff)))
                                    {
                                        g.FillPath(brush2, path2);
                                    }
                                }
                            }
                            RectangleF glassRect = rect;
                            if (mode == LinearGradientMode.Vertical)
                            {
                                glassRect.Y = rect.Y + (rect.Height * basePosition);
                                glassRect.Height = (rect.Height - (rect.Height * basePosition)) * 2f;
                            }
                            else
                            {
                                glassRect.X = rect.X + (rect.Width * basePosition);
                                glassRect.Width = (rect.Width - (rect.Width * basePosition)) * 2f;
                            }
                            ControlPaintEx.DrawGlass(g, glassRect, 170, 0);
                        }
                        if (!drawBorder)
                        {
                            return;
                        }
                        using (GraphicsPath path3 = GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                        {
                            using (Pen pen = new Pen(borderColor))
                            {
                                g.DrawPath(pen, path3);
                            }
                        }
                        rect.Inflate(-1, -1);
                        using (GraphicsPath path4 = GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                        {
                            using (Pen pen2 = new Pen(innerBorderColor))
                            {
                                g.DrawPath(pen2, path4);
                            }
                            return;
                        }
                    }
                    g.FillRectangle(brush, rect);
                    if (drawGlass)
                    {
                        if (baseColor.A > 80)
                        {
                            Rectangle rectangle2 = rect;
                            if (mode == LinearGradientMode.Vertical)
                            {
                                rectangle2.Height = (int) (rectangle2.Height * basePosition);
                            }
                            else
                            {
                                rectangle2.Width = (int) (rect.Width * basePosition);
                            }
                            using (SolidBrush brush3 = new SolidBrush(Color.FromArgb(0x80, 0xff, 0xff, 0xff)))
                            {
                                g.FillRectangle(brush3, rectangle2);
                            }
                        }
                        RectangleF ef2 = rect;
                        if (mode == LinearGradientMode.Vertical)
                        {
                            ef2.Y = rect.Y + (rect.Height * basePosition);
                            ef2.Height = (rect.Height - (rect.Height * basePosition)) * 2f;
                        }
                        else
                        {
                            ef2.X = rect.X + (rect.Width * basePosition);
                            ef2.Width = (rect.Width - (rect.Width * basePosition)) * 2f;
                        }
                        ControlPaintEx.DrawGlass(g, ef2, 200, 0);
                    }
                    if (drawBorder)
                    {
                        using (Pen pen3 = new Pen(borderColor))
                        {
                            g.DrawRectangle(pen3, rect);
                        }
                        rect.Inflate(-1, -1);
                        using (Pen pen4 = new Pen(innerBorderColor))
                        {
                            g.DrawRectangle(pen4, rect);
                        }
                    }
                }
            }
        }


        internal static void RenderVerticalText(
            Graphics g,
            string text,
            Rectangle textRect,
            Font textFont,
            Color foreColor,
            StringFormat format,
            bool left)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            if (left)
            {
                g.TranslateTransform(textRect.X, textRect.Bottom);
                g.RotateTransform(270F);
            }
            else
            {
                g.TranslateTransform(textRect.Right, textRect.Y);
                g.RotateTransform(90F);
            }

            Rectangle transformRect = new Rectangle(
                0, 0, textRect.Height, textRect.Width);

            try
            {
                using (Brush brush = new SolidBrush(foreColor))
                {
                    g.DrawString(
                        text,
                        textFont,
                        brush,
                        transformRect,
                        format);
                }
            }
            finally
            {
                g.ResetTransform();
            }
        }
    }
}

