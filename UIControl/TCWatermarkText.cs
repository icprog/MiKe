using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace UILibrary
{
    [ToolboxBitmap( typeof ( TextBox))]
    public class TCWatermarkText : TextBox
    {
        protected string _emptyTextTip = "请输入用户名";
        protected Color _emptyTextTipColor = Color.Gray;

        public TCWatermarkText()
            : base()
        {
            SetStyle( ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        protected void WmPaintWater(ref Message msg)
        {
            using (Graphics graphics = Graphics.FromHwnd(base.Handle))
            {
                if (Text.Length == 0 && string.IsNullOrEmpty(_emptyTextTip) == false && Focused == false)
                {
                    TextFormatFlags format = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;

                    if (RightToLeft == RightToLeft.Yes)
                    {
                        format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
                    }

                    TextRenderer.DrawText(graphics , _emptyTextTip, new Font("微软雅黑",8.5f), base.ClientRectangle , _emptyTextTipColor, format);
                }
            }
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 15)
            {
                this.WmPaintWater(ref m);
            }
        }

        [Description("水印的颜色"),Category("自定义属性")]
        public Color EmptyWaterColor
        {
            get
            {
                return _emptyTextTipColor;
            }
            set
            {
                if (_emptyTextTipColor != value)
                {
                    _emptyTextTipColor = value;
                    base.Invalidate();
                }
            }    
        }
        [Description("水印文字"),Category("自定义属性")]
        public string EmptyWaterText
        {
            get
            {
                return _emptyTextTip;
            }
            set
            {
                if (_emptyTextTip != value)
                {
                    _emptyTextTip = value;
                    base.Invalidate();
                }
            }
        }
    }
}
