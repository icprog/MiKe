using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FishClient.UIControls
{
    public partial class InputControl : UserControl
    {
        public event EventHandler ClickEvent = null;

      

        private int _underlineWidth=1;
        public int UnderlineWidth
        {
            get { return _underlineWidth; }
            set
            {
                if (_underlineWidth == value) return;
                _underlineWidth = value;
                this.panel1.Height = value;
                //this.Invalidate();
            }
        }
        private Color _underlineColor = Color.Black;
        public Color UnderlineColor
        {
            get { return _underlineColor; }
            set { _underlineColor = value; this.panel1.BackColor = value; }
        }

        public Color TextColor
        {
            get { return textBox1.ForeColor; }
            set { textBox1.ForeColor = value; }
        }

        public string Text
        {
            get { return textBox1.Text.Trim(); }
            set { textBox1.Text = value; }
        }

        public bool ReadOnly
        {
            get { return textBox1.ReadOnly; }
            set { textBox1.ReadOnly = value; }
        }

        public Cursor Cursor
        {
            get { return textBox1.Cursor; }
            set { textBox1.Cursor = value; }
        }

        public InputControl()
        {
            InitializeComponent();

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (ClickEvent != null)
            {
                ClickEvent(this, e);
            }
        }
    }
}
