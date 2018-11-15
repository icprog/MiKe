using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace UILibrary.PushPanel
{
    /* 作者：Starts_2000
     * 日期：2010-08-10
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public class PanelColorTable
    {
        private static readonly Color _border = SystemColors.ControlDark; // Color.FromArgb(177, 221, 255);
        private static readonly Color _captionBackNormal = SystemColors.Control; //Color.FromArgb(160, 218, 255);
        private static readonly Color _captionBackHover = Color.FromArgb(123, 198, 255);
        private static readonly Color _captionBackPressed = Color.FromArgb(100, 188, 255);
        private static readonly Color _captionFore = Color.Black;
        

        public PanelColorTable() { }

        public virtual Color Border
        {
            get { return _border; }
        }

        public virtual Color CaptionBackNormal
        {
            get { return _captionBackNormal; }
        }

        public virtual Color CaptionBackHover
        {
            get { return _captionBackHover; }
        }

        public virtual Color CaptionBackPressed
        {
            get { return _captionBackPressed; }
        }

        public virtual Color CaptionFore
        {
            get { return _captionFore; }
        }
    }
}
