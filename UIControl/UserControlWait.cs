using System . Windows . Forms;
using System . IO;
using System . Drawing;

namespace UIControl
{
    public partial class UserControlWait :UserControl
    {
        public UserControlWait ( )
        {
            InitializeComponent ( );

            string imagePath = Application . StartupPath + "\\Image\\wait.gif";
            if ( File . Exists ( imagePath ) )
            {
                pictureBox1 . Image = Image . FromFile ( imagePath );
            }
        }
    }
}
