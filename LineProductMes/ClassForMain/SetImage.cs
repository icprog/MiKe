using DevExpress . XtraEditors;
using System . Drawing;
using System . IO;
using System . Windows . Forms;

namespace LineProductMes . ClassForMain
{
    public static class SetImage
    {
        public static void setImage ( Control con ,string nameOfImage )
        {
            string imagePath = Application . StartupPath + "\\Image\\" + nameOfImage + "";
            if ( File . Exists ( imagePath ) )
            {
                con . BackgroundImage = Image . FromFile ( imagePath );
            }
        }
        public static void setImageDev ( PictureEdit con ,string nameOfImage )
        {
            string imagePath = Application . StartupPath + "\\Image\\" + nameOfImage + "";
            if ( File . Exists ( imagePath ) )
            {
                con . Image = Image . FromFile ( imagePath );
            }
        }
    }
}
