using DevExpress . XtraGrid . Views . Grid;

namespace LineProductMes . ClassForMain
{
    public static class GrivColumnStyle
    {
        public static void setColumnStyle ( GridView [ ] grids )
        {
            foreach ( GridView gv in grids )
            {
                foreach ( DevExpress . XtraGrid . Columns . GridColumn co in gv . Columns )
                {
                    co . OptionsFilter . FilterPopupMode = DevExpress . XtraGrid . Columns . FilterPopupMode . CheckedList;
                    co . AppearanceCell . Font = new System . Drawing . Font ( "微软雅黑" ,10F  );
                    co . AppearanceCell . Options . UseFont = true;
                    co . AppearanceCell . Options . UseTextOptions = true;
                    co . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                    co . AppearanceCell . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                    co . AppearanceHeader . Font = new System . Drawing . Font ( "微软雅黑" ,10F );
                    co . AppearanceHeader . Options . UseFont = true;
                    co . AppearanceHeader . Options . UseTextOptions = true;
                    co . AppearanceHeader . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                    co . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                }
            }
        }

        public static void setColumnStyle ( DevExpress . XtraTreeList . TreeList tree )
        {
            foreach ( DevExpress .XtraTreeList.Columns.TreeListColumn co in tree . Columns )
            {
                co . OptionsFilter . FilterPopupMode = DevExpress . XtraTreeList . FilterPopupMode . CheckedList;
                co . AppearanceCell . Font = new System . Drawing . Font ( "微软雅黑" ,10F );
                co . AppearanceCell . Options . UseFont = true;
                co . AppearanceCell . Options . UseTextOptions = true;
                co . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                co . AppearanceCell . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                co . AppearanceHeader . Font = new System . Drawing . Font ( "微软雅黑" ,10F );
                co . AppearanceHeader . Options . UseFont = true;
                co . AppearanceHeader . Options . UseTextOptions = true;
                co . AppearanceHeader . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                co . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
            }
        }

    }
}
