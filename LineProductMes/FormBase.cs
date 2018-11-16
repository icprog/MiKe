using DevExpress . Skins;
using System . Configuration;

namespace LineProductMes
{
    public partial class FormBase :DevExpress . XtraEditors . XtraForm
    {
        public string strFeel=string.Empty;
        protected static DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel=new DevExpress.LookAndFeel.DefaultLookAndFeel();

        //获取Configuration对象
        Configuration config = ConfigurationManager . OpenExeConfiguration ( /*ConfigurationUserLevel.None*/System . Windows . Forms . Application . ExecutablePath );
        
        public FormBase ( )
        {
            InitializeComponent ( );

            //strFeel = config . AppSettings . Settings [ "Feed" ] . Value;
            if ( string . IsNullOrEmpty ( strFeel ) )
                strFeel = "Office 2013 Light Gray";
            defaultLookAndFeel . LookAndFeel . SkinName = strFeel;

            //Skin GridSkin = GridSkins . GetSkin ( defaultLookAndFeel . LookAndFeel );
            //LineProductMesBll . UserInfoMation . FeedColor = GridSkin [ GridSkins . SkinGridEvenRow ] . Color . BackColor;
        }
    }
}
