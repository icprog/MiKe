using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMes . ClassForMain
{
    public static class ToolBarContain
    {
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="barTool"></param>
        /// <param name="items"></param>
        public static void ToolbarsC ( DevExpress . XtraBars . Bar barTool ,DevExpress . XtraBars . BarButtonItem [ ] items )
        {
            foreach ( DevExpress . XtraBars . BarButtonItem item in items )
            {
                if ( barTool . LinksPersistInfo . Count > item . Id && barTool . LinksPersistInfo [ item . Id ] . Item . Name == item . Name )
                    barTool . LinksPersistInfo . RemoveAt ( item . Id );
            }
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="barTool"></param>
        /// <param name="items"></param>
        public static void ToolbarC ( DevExpress . XtraBars . Bar barTool ,DevExpress . XtraBars . BarButtonItem item )
        {
            if ( barTool . LinksPersistInfo . Count > item . Id && barTool . LinksPersistInfo [ item . Id ] . Item . Name == item . Name )
                barTool . LinksPersistInfo . RemoveAt ( item . Id );
        }
    }
}
