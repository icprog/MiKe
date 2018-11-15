using DevExpress . XtraEditors;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Forms;

namespace LineProductMes . ClassForMain
{
   public class CopyUtils
    {
        /// <summary>
        /// 右键复制
        /// </summary>
        /// <param name="view"></param>
        /// <param name="focuseName"></param>
        public static void copyResult ( DevExpress.XtraGrid.Views.Grid.GridView view,string focuseName )
        {
            if ( string . IsNullOrEmpty ( focuseName ) )
            {
                XtraMessageBox . Show ( "请选择复制内容" );
                return;
            }
            object result = view . GetFocusedRowCellValue ( focuseName );
            if ( result == null )
                return;

            Clipboard . SetDataObject ( result );
        }

    }
}
