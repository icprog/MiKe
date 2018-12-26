using DevExpress . XtraCharts;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Forms;

namespace LineProductMes . ClassForMain
{
    public static class GridChartStyle
    {
        public static void setChartStyle ( ChartControl [ ] charts )
        {
            foreach ( ChartControl chart in charts )
            {
                if ( chart . Diagram . GetType ( ) == typeof ( XYDiagram ) )
                {
                    XYDiagram xys = ( XYDiagram ) chart . Diagram;

                    xys . AxisX . Color = System . Drawing . Color . White;
                    xys . AxisX . GridLines . Color = System . Drawing . Color . White;
                    xys . AxisX . Interlaced = true;
                    xys . AxisX . Label . BackColor = System . Drawing . Color . DarkBlue;
                    xys . AxisX . Label . TextColor = System . Drawing . Color . White;
                    xys . AxisY . Color = System . Drawing . Color . White;
                    xys . AxisY . GridLines . MinorVisible = true;
                    xys . AxisY . Label . BackColor = System . Drawing . Color . DarkBlue;
                    xys . AxisY . Label . TextColor = System . Drawing . Color . White;
                    xys . DefaultPane . BackColor = System . Drawing . Color . DarkBlue;
                }
                if ( chart . Legend . GetType ( ) == typeof ( Legend ) )
                {
                    Legend leg = ( Legend ) chart . Legend;
                    leg . BackColor = System . Drawing . Color . DarkBlue;
                    leg . TextColor = System . Drawing . Color . White;
                    leg . Border . Color = System . Drawing . Color . Red;
                    leg . Border . Thickness = 2;
                }
                foreach ( ChartTitle title in chart . Titles )
                {
                    ChartTitle tit = title as ChartTitle;
                    tit . TextColor = System . Drawing . Color . White;
                    tit . Font = new System . Drawing . Font ( "微软雅黑" ,12F ,System . Drawing . FontStyle . Bold ,System . Drawing . GraphicsUnit . Point ,( ( byte ) ( 134 ) ) );
                }
                chart . BackColor = System . Drawing . Color . DarkBlue;
            }
        }
    }
}
