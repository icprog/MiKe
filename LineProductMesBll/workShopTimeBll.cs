using StudentMgr;
using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll
{
    public class workShopTimeBll
    {
        /// <summary>
        /// 获取组装报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableOne ( string oddNum,string time,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT ANX001,ANX002,ANX005,ANX006,ANX007,ANX008 FROM MIKANW A INNER JOIN MIKANX B ON A.ANW001=B.ANX001 WHERE ANW022='{0}' AND ANX002 in ({1}) " ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT ANX001,ANX002,ANX005,ANX006,ANX007,ANX008 FROM MIKANW A INNER JOIN MIKANX B ON A.ANW001=B.ANX001 WHERE ANW022='{0}' AND ANX002 in ({1}) AND ANX001!='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );        
        }

        /// <summary>
        /// 获取组装附件报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableTwo ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT ANV001,ANV002,ANV005,ANV006,ANV013,ANV014 FROM MIKANT A INNER JOIN MIKANV B ON A.ANT001=B.ANV001 WHERE ANT008='{0}' AND ANV002 IN ({1}) " ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT ANV001,ANV002,ANV005,ANV006,ANV013,ANV014 FROM MIKANT A INNER JOIN MIKANV B ON A.ANT001=B.ANV001 WHERE ANT008='{0}' AND ANV002 IN ({1}) AND ANV001 !='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取五金报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableTre ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT HAX001,HAX002,HAX009,HAX010,HAX011,HAX012 FROM MIKHAW A INNER JOIN MIKHAX B ON A.HAW001=B.HAX001 WHERE HAW010='{0}' AND HAX002 IN ({1}) " ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT HAX001,HAX002,HAX009,HAX010,HAX011,HAX012 FROM MIKHAW A INNER JOIN MIKHAX B ON A.HAW001=B.HAX001 WHERE HAW010='{0}' AND HAX002 IN ({1}) AND HAX001 !='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取注塑计件报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableFor ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT IJB001,IJB002,IJB016,IJB017,IJB018,IJB019 FROM MIKIJA A INNER JOIN MIKIJB B ON A.IJA001=B.IJB001 WHERE IJA007='{0}' AND IJB002 IN ({1}) " ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT IJB001,IJB002,IJB016,IJB017,IJB018,IJB019 FROM MIKIJA A INNER JOIN MIKIJB B ON A.IJA001=B.IJB001 WHERE IJA007='{0}' AND IJB002 IN ({1}) AND IJB001 !='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取注塑计时报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableFiv ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );
            
            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT IJD001,IJD002,IJD007,IJD006,IJD014,IJD015 FROM MIKIJA A INNER JOIN MIKIJD B ON A.IJA001=B.IJD001 WHERE IJA007='{0}' AND IJD002 IN ({1}) " ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT IJD001,IJD002,IJD007,IJD006,IJD014,IJD015 FROM MIKIJA A INNER JOIN MIKIJD B ON A.IJA001=B.IJD001 WHERE IJA007='{0}' AND IJD002 IN ({1}) AND IJD001 !='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取LED报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableSix ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT LEG001,LEG002,LEG005,LEG006,LEG008,LEG009 FROM MIKLEF A INNER JOIN MIKLEG B ON A.LEF001=B.LEG001 WHERE LEF013='{0}' AND LEG002 IN ({1}) " ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT LEG001,LEG002,LEG005,LEG006,LEG008,LEG009 FROM MIKLEF A INNER JOIN MIKLEG B ON A.LEF001=B.LEG001 WHERE LEF013='{0}' AND LEG002 IN ({1}) AND LEG001 !='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取面板报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableSev ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT LED001,LED002,LED005,LED006,LED008,LED009 FROM MIKLEC A INNER JOIN MIKLED B ON A.LEC001=B.LED001 WHERE LEC013='{0}' AND LED002 IN ({1}) " ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT LED001,LED002,LED005,LED006,LED008,LED009 FROM MIKLEC A INNER JOIN MIKLED B ON A.LEC001=B.LED001 WHERE LEC013='{0}' AND LED002 IN ({1}) AND LED001 !='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取物流组报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableEgi ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT LGP001,LGP002,LGP007,LGP008,LGP009,LGP010 FROM MIKLGN A INNER JOIN MIKLGP B ON A.LGN001=B.LGP001 WHERE LGN002='{0}' AND LGP002 IN ({1})" ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT LGP001,LGP002,LGP007,LGP008,LGP009,LGP010 FROM MIKLGN A INNER JOIN MIKLGP B ON A.LGN001=B.LGP001 WHERE LGN002='{0}' AND LGP002 IN ({1}) AND LGP001 !='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取喷漆报工单的报工数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <param name="time"></param>
        /// <param name="numForUser"></param>
        /// <returns></returns>
        public static DataTable getTableNin ( string oddNum ,string time ,string numForUser )
        {
            StringBuilder strSql = new StringBuilder ( );

            if ( oddNum == string . Empty )
                strSql . AppendFormat ( "SELECT PAP001,PPA002,PPA005,PPA006,PPA007,PPA008 FROM MIKPAN A INNER JOIN MIKPAP B ON A.PAN001=B.PAP001 WHERE PAN006='{0}' AND PPA002 IN ({1})" ,time ,numForUser );
            else
                strSql . AppendFormat ( "SELECT PAP001,PPA002,PPA005,PPA006,PPA007,PPA008 FROM MIKPAN A INNER JOIN MIKPAP B ON A.PAN001=B.PAP001 WHERE PAN006='{0}' AND PPA002 IN ({1}) AND PAP001!='{2}' " ,time ,numForUser ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
