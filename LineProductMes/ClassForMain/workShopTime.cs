using StudentMgr;
using System;
using System . Data;
using System . Text;

namespace LineProductMes . ClassForMain
{
    //[start ,end] [start1,end1]  start < end1 && end > start1
    
    public class workShopTime
    {
        /// <summary>
        /// 计件开始
        /// </summary>
        /// <param name="row">选中列</param>
        /// <param name="obj">计件开始时间</param>
        /// <param name="rowName">计件结束</param>
        /// <param name="rowNameStart">计时开始</param>
        /// <param name="rowNameEnd">计时结束</param>
        /// <returns></returns>
        public static bool startTime ( DataRow row ,object obj,string rowName ,string rowNameStart,string rowNameEnd)
        {
            //[计件开始,计件结束] [计时开始,计时结束]  计件开始<计时结束  && 计件结束>计时开始
            //[obj,rowName] [rowNameStart,rowNameEnd]       obj<rowNameEnd  && rowName>rowNameStart   有交集 
            //                                              obj>rowNameEnd || rowName<rowNameStart    无交集
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowName ] ) )
                        return false;
                    if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowNameEnd ] ) && Convert . ToDateTime ( row [ rowName ] ) > Convert . ToDateTime ( row [ rowNameStart ] ) )
                                return false;
                        }
                    }
                }
                if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                {
                    //if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameStart ] ) )
                    //    return false;
                    if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameStart ] ) && Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowNameEnd ] ) )
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 计件结束
        /// </summary>
        /// <param name="row">选中列</param>
        /// <param name="obj">计件结束时间</param>
        /// <param name="rowName">计件开始</param>
        /// <param name="rowNameStart">计时开始</param>
        /// <param name="rowNameEnd">计时结束</param>
        /// <returns></returns>
        public static bool endTime ( DataRow row ,object obj ,string rowName ,string rowNameStart ,string rowNameEnd)
        {
            //[计件开始,计件结束] [计时开始,计时结束]  计件开始<计时结束  && 计件结束>计时开始
            //[rowName,obj] [rowNameStart,rowNameEnd]       rowName<rowNameEnd  && obj>rowNameStart   有交集 
            //                                              rowName>rowNameEnd || obj<rowNameStart    无交集
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowName ] ) )
                        return false;
                    if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( row [ rowName ] ) < Convert . ToDateTime ( row [ rowNameEnd ] ) && Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameStart ] ) )
                                return false;
                        }
                    }
                }

                if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                {
                    //if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameStart ] ) )
                    //{
                        if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowNameEnd ] ) && Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameStart ] ) )
                                return false;
                        }
                    //}
                }
            }

            return true;
        }
        
        /// <summary>
        /// 计时开始
        /// </summary>
        /// <param name="row">选中列</param>
        /// <param name="obj">计时开始时间</param>
        /// <param name="rowName">计件结束     6</param>
        /// <param name="rowNameEnd">计时结束  8</param>
        /// <param name="rowEndName">计件开始  5</param>
        /// <returns></returns>
        public static bool startCenTime (DataRow row,object obj,string rowName,string rowNameEnd,string rowEndName )
        {
            //[计件开始,计件结束] [计时开始,计时结束]  计件开始<计时结束  && 计件结束>计时开始
            //[rowEndName,rowName] [obj,rowNameEnd]    rowEndName<rowNameEnd  && rowName>obj   有交集 
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameEnd ] ) )
                        return false;
                    if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( rowEndName ) < Convert . ToDateTime ( row [ rowNameEnd ] ) && Convert . ToDateTime ( row [ rowName ] ) > Convert . ToDateTime ( obj ) )
                                return false;
                        }
                    }
                }

                if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                {
                    if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowName ] ) && Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowEndName ] ) )
                            return false;
                    }
                }

                //if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                //{
                //    if ( Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowName ] ) )
                //    {
                //        if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                //        {
                //            if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowEndName ] ) )
                //                return false;
                //            if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                //            {
                //                if ( Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowEndName ] ) && Convert . ToDateTime ( row [ rowNameEnd ] ) > Convert . ToDateTime ( row [ rowName ] ) )
                //                    return false;
                //            }
                //        }
                //    }
                //}
                //if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                //{
                //    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameEnd ] ) )
                //        return false;
                //}
            }

            return true;
        }
        
        /// <summary>
        /// 计时结束
        /// </summary>
        /// <param name="row">选中列</param>
        /// <param name="obj">计时结束时间</param>
        /// <param name="rowName">计时开始</param>
        /// <param name="rowStartName">计件开始</param>
        /// <param name="rowEndName">计件结束</param>
        /// <returns></returns>
        public static bool endCenTime ( DataRow row ,object obj ,string rowName ,string rowStartName ,string rowEndName )
        {
            //[计件开始,计件结束] [计时开始,计时结束]  计件开始<计时结束  && 计件结束>计时开始
            //[rowStartName,rowEndName] [rowName,obj]  rowStartName<obj  && rowEndName>rowName   有交集 

            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowName ] ) )
                        return false;

                    if ( row [ rowStartName ] != null && row [ rowStartName ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( row [ rowStartName ] ) < Convert . ToDateTime ( obj ) && Convert . ToDateTime ( row [ rowEndName ] ) > Convert . ToDateTime ( row [ rowName ] ) )
                                return false;
                        }
                    }

                }

                if ( row [ rowStartName ] != null && row [ rowStartName ] . ToString ( ) != string . Empty )
                {
                    if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                    {
                        if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowStartName ] ) && Convert . ToDateTime ( obj ) < Convert . ToDateTime ( row [ rowEndName ] ) )
                            return false;
                    }
                }

                //if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                //{
                //    if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowName ] ) )
                //        return false;
                //}
                //if ( row [ rowStartName ] != null && row [ rowStartName ] . ToString ( ) != string . Empty )
                //{
                //    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowStartName ] ) )
                //    {
                //        if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                //        {
                //            if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowEndName ] ) )
                //                return false;
                //            if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                //            {
                //                if ( Convert . ToDateTime ( obj ) >= Convert . ToDateTime ( row [ rowEndName ] ) && Convert . ToDateTime ( row[rowName] ) <= Convert . ToDateTime ( row [ rowStartName ] ) )
                //                    return false;
                //            }
                //        }
                //    }
                //}
            }

            return true;
        }

        /// <summary>
        /// 附件记工单开始日期
        /// </summary>
        /// <param name="row"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool startTimeFJ ( DataRow row,object obj )
        {
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ "ANV006" ] != null && row [ "ANV006" ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) >= Convert . ToDateTime ( row [ "ANV006" ] ) )
                        return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// 附件记工单结束日期
        /// </summary>
        /// <param name="row"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool endTimeFJ ( DataRow row ,object obj )
        {
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ "ANV005" ] != null && row [ "ANV005" ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ "ANV005" ] ) )
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 注塑报工单
        /// </summary>
        /// <param name="obj">人员编号</param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool startTimeZS ( object obj ,DataTable table )
        {
            bool result = true;
            if ( table == null || table . Rows . Count < 1 )
                return true;
            DataRow [ ] rows = table . Select ( "IJB002='" + obj + "'" );
            if ( rows . Length < 1 )
                return true;
            DataRow row1, row2;

            for ( int i = 0 ; i < rows . Length ; i++ )
            {
                row1 = rows [ i ];
                if ( row1 == null )
                    continue;
                for ( int j = 1 ; j < rows . Length ; j++ )
                {
                    if ( i == j )
                        continue;
                    row2 = rows [ j ];
                    if ( row2 == null )
                        continue;
                    //if ( startTime ( row1 ,row2 [ "IJB016" ] ,"IJB017" ,"IJB018" ,"IJB019" ) == false )
                    //{
                    //    result = false;
                    //    break;
                    //}
                    //if ( endTime ( row1 ,row2 [ "IJB017" ] ,"IJB016" ,"IJB018" ,"IJB019" ) == false )
                    //{
                    //    result = false;
                    //    break;
                    //}
                    //if ( startCenTime ( row1 ,row2 [ "IJB018" ] ,"IJB017" ,"IJB019" ,"IJB016" ) == false )
                    //{
                    //    result = false;
                    //    break;
                    //}
                    //if ( endCenTime ( row1 ,row2 [ "IJB019" ] ,"IJB018" ,"IJB016" ,"IJB017" ) == false )
                    //{
                    //    result = false;
                    //    break;
                    //}
                    if ( NumForPrice ( row1 ,row2 [ "IJB016" ] ,row2 [ "IJB017" ] ) == false )
                    {
                        result = false;
                        break;
                    }
                    if ( TimeFor ( row1 ,row2 [ "IJB018" ] ,row2 [ "IJB019" ] ) == false )
                    {
                        result = false;
                        break;
                    }

                }

                if ( result == false )
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 计件
        /// </summary>
        /// <param name="row"></param>
        /// <param name="num1">计件开始</param>
        /// <param name="num2">计件结束</param>
        /// <returns></returns>
        private static bool NumForPrice (DataRow row,object num1,object num2 )
        {
            //[计件开始,计件结束] [计时开始,计时结束]  计件开始<计时结束  && 计件结束>计时开始
            //[obj,rowName] [rowNameStart,rowNameEnd]       obj<rowNameEnd  && rowName>rowNameStart   有交集 

            if ( num1 != null && num1 . ToString ( ) != string . Empty && num2 != null && num2 . ToString ( ) != string . Empty )
            {
                //计件
                if ( row [ "IJB016" ] != null && row [ "IJB016" ] . ToString ( ) != string.Empty && row [ "IJB017" ] != null && row [ "IJB017" ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( num1 ) < Convert . ToDateTime ( row [ "IJB017" ] ) && Convert . ToDateTime ( num2 ) > Convert . ToDateTime ( row [ "IJB016" ] ) )
                        return false;
                }
                //计时
                if ( row [ "IJB018" ] != null && row [ "IJB018" ] . ToString ( ) != string . Empty && row [ "IJB019" ] != null && row [ "IJB019" ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( num1 ) < Convert . ToDateTime ( row [ "IJB019" ] ) && Convert . ToDateTime ( num2 ) > Convert . ToDateTime ( row [ "IJB018" ] ) )
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 计时
        /// </summary>
        /// <param name="row"></param>
        /// <param name="num1">计时开始</param>
        /// <param name="num2">计时结束</param>
        /// <returns></returns>
        private static bool TimeFor ( DataRow row ,object num1 ,object num2 )
        {
            //[计件开始,计件结束] [计时开始,计时结束]  计件开始<计时结束  && 计件结束>计时开始
            //[obj,rowName] [rowNameStart,rowNameEnd]       obj<rowNameEnd  && rowName>rowNameStart   有交集 

            if ( num1 != null && num1 . ToString ( ) != string . Empty && num2 != null && num2 . ToString ( ) != string . Empty )
            {
                //计件
                if ( row [ "IJB016" ] != null && row [ "IJB016" ] . ToString ( ) != string . Empty && row [ "IJB017" ] != null && row [ "IJB017" ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( num1 ) < Convert . ToDateTime ( row [ "IJB017" ] ) && Convert . ToDateTime ( num2 ) > Convert . ToDateTime ( row [ "IJB016" ] ) )
                        return false;
                }
                //计时
                if ( row [ "IJB018" ] != null && row [ "IJB018" ] . ToString ( ) != string . Empty && row [ "IJB019" ] != null && row [ "IJB019" ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( num1 ) < Convert . ToDateTime ( row [ "IJB019" ] ) && Convert . ToDateTime ( num2 ) > Convert . ToDateTime ( row [ "IJB018" ] ) )
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 工号列名
        /// </summary>
        private static  string columnUse;
        /// <summary>
        /// 计时开始
        /// </summary>
        private static string columnStartTime;
        /// <summary>
        /// 计时结束
        /// </summary>
        private static string columnEndTime;
        /// <summary>
        /// 计件开始
        /// </summary>
        private static string columnStart;
        /// <summary>
        /// 计件结束
        /// </summary>
        private static string columnEnd;
        /// <summary>
        /// 工号
        /// </summary>
        private static string numForUser;
        /// <summary>
        /// 提示信息
        /// </summary>
        private static string messageInfo;

        /// <summary>
        /// 哪个表
        /// </summary>
        private static string workOrderState;

        /// <summary>
        /// 工号
        /// </summary>
        private static string userCode;
        /// <summary>
        /// 单号
        /// </summary>
        private static string codeOdd;
        /// <summary>
        /// 计时开始
        /// </summary>
        private static string sTime;
        /// <summary>
        /// 计时结束
        /// </summary>
        private static string eTime;
        /// <summary>
        /// 计件开始
        /// </summary>
        private static string sjTime;
        /// <summary>
        /// 计件结束
        /// </summary>
        private static string ejTime;
        /// <summary>
        /// 车间
        /// </summary>
        private static string work;

        /// <summary>
        /// 报工时检查此人是否在其他车间同时段报工
        /// </summary>
        /// <param name="time">制单日期</param>
        /// <param name="table">数据</param>
        /// <param name="workOrder">车间</param>
        /// <param name="oddNum">单号</param>
        /// <returns></returns>
        public static string checkUserForOtherWork ( string time ,DataTable table ,string workOrder ,string oddNum )
        {
            messageInfo = columnUse = columnStartTime = columnEndTime = columnStart = columnEnd = numForUser = string . Empty;
            workOrderState = workOrder;
            switch ( workOrder )
            {
                case "组装":
                columnUse = "ANX002";
                columnStartTime = "ANX007";
                columnEndTime = "ANX008";
                columnStart = "ANX005";
                columnEnd = "ANX006";
                break;
                case "组装附件":
                columnUse = "ANV002";
                columnStartTime = "ANV005";
                columnEndTime = "ANV006";
                columnStart = "ANV013";
                columnEnd = "ANV014";
                break;
                case "五金":
                columnUse = "HAX002";
                columnStartTime = "HAX009";
                columnEndTime = "HAX010";
                columnStart = "HAX011";
                columnEnd = "HAX012";
                break;
                case "注塑计件":
                columnUse = "IJB002";
                columnStartTime = "IJB018";
                columnEndTime = "IJB019";
                columnStart = "IJB016";
                columnEnd = "IJB017";
                break;
                case "注塑计时":
                columnUse = "IJD002";
                columnStartTime = "IJD006";
                columnEndTime = "IJD007";
                columnStart = string . Empty;
                columnEnd = string . Empty;
                break;
                case "LED":
                columnUse = "LEG002";
                columnStartTime = "LEG008";
                columnEndTime = "LEG009";
                columnStart = "LEG005";
                columnEnd = "LEG006";
                break;
                case "面板":
                columnUse = "LED002";
                columnStartTime = "LED008";
                columnEndTime = "LED009";
                columnStart = "LED005";
                columnEnd = "LED006";
                break;
                case "物流组":
                columnUse = "LGP002";
                columnStartTime = "LGP007";
                columnEndTime = "LGP008";
                columnStart = "LGP009";
                columnEnd = "LGP010";
                break;
                case "喷漆":
                columnUse = "PPA002";
                columnStartTime = "PPA007";
                columnEndTime = "PPA008";
                columnStart = "PPA005";
                columnEnd = "PPA006";
                break;
            }

            foreach ( DataRow row in table . Rows )
            {
                if ( string . IsNullOrEmpty ( numForUser ) )
                    numForUser = "'" + row [ columnUse ] + "'";
                else
                    numForUser = numForUser + "," + "'" + row [ columnUse ] + "'";
            }

            messageInfo = assNewWork ( time ,table ,oddNum );
            if ( messageInfo != string . Empty )
                return messageInfo;

            return messageInfo;
        }
        
        //[计件开始,计件结束] [计时开始,计时结束]  计件开始<计时结束  && 计件结束>计时开始
        /// <summary>
        /// 组装
        /// </summary>
        /// <param name="time"></param>
        /// <param name="table"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        static string assNewWork ( string time ,DataTable table ,string oddNum )
        {
            string code = oddNum;
            messageInfo = string . Empty;

            DataTable tableOne = new DataTable ( );
            if ( !workOrderState . Equals ( LineProductMesBll.ObtainInfo.codeOne ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableOne ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeOne;
                userCode = "ANX002";
                codeOdd = "ANX001";
                sTime = "ANX007";
                eTime = "ANX008";
                sjTime = "ANX005";
                ejTime = "ANX006";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( !workOrderState . Equals ( LineProductMesBll.ObtainInfo.codeTwo ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableTwo ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeTwo;
                userCode = "ANV002";
                codeOdd = "ANV001";
                sTime = "ANV005";
                eTime = "ANV006";
                sjTime = "ANV013";
                ejTime = "ANV014";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( !workOrderState . Equals ( LineProductMesBll.ObtainInfo.codeTre ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableTre ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeTre;
                userCode = "HAX002";
                codeOdd = "HAX001";
                sTime = "HAX009";
                eTime = "HAX010";
                sjTime = "HAX011";
                ejTime = "HAX012";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( !workOrderState . Equals ( LineProductMesBll . ObtainInfo . codeFor ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableFor ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeFor;
                userCode = "IJB002";
                codeOdd = "IJB001";
                sTime = "IJB018";
                eTime = "IJB019";
                sjTime = "IJB016";
                ejTime = "IJB017";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( !workOrderState . Equals ( LineProductMesBll . ObtainInfo . codeFiv ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableFiv ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeFiv;
                userCode = "IJD002";
                codeOdd = "IJD001";
                sTime = "IJD006";
                eTime = "IJD007";
                sjTime = "IJD014";
                ejTime = "IJD015";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if (!workOrderState . Equals ( LineProductMesBll . ObtainInfo . codeSix ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableSix ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeSix;
                userCode = "LEG002";
                codeOdd = "LEG001";
                sTime = "LEG008";
                eTime = "LEG009";
                sjTime = "LEG005";
                ejTime = "LEG006";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( !workOrderState . Equals ( LineProductMesBll . ObtainInfo . codeSev ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableSev ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeSev;
                userCode = "LED002";
                codeOdd = "LED001";
                sTime = "LED008";
                eTime = "LED009";
                sjTime = "LED005";
                ejTime = "LED006";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( !workOrderState . Equals ( LineProductMesBll . ObtainInfo . codeEgi ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableEgi ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeEgi;
                userCode = "LGP002";
                codeOdd = "LGP001";
                sTime = "LGP007";
                eTime = "LGP008";
                sjTime = "LGP009";
                ejTime = "LGP010";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( !workOrderState . Equals ( LineProductMesBll . ObtainInfo . codeNin ) )
                code = string . Empty;
            tableOne = LineProductMesBll . workShopTimeBll . getTableNin ( code ,time ,numForUser );
            if ( tableOne != null && tableOne . Rows . Count > 0 )
            {
                work = LineProductMesBll . ObtainInfo . codeNin;
                userCode = "PPA002";
                codeOdd = "PAP001";
                sTime = "PPA007";
                eTime = "PPA008";
                sjTime = "PPA005";
                ejTime = "PPA006";
                checkTime ( table ,tableOne );
                if ( !string . IsNullOrEmpty ( messageInfo ) )
                    return messageInfo;
            }

            code = oddNum;
            if ( tableOne == null || tableOne . Rows . Count < 1 )
                return string . Empty;
            checkTime ( table ,tableOne );
            return messageInfo;
        }

        static void checkTime ( DataTable table ,DataTable tableOne )
        {
            foreach ( DataRow row in table . Rows )
            {
                DataRow [ ] rows = tableOne . Select ( ""+userCode+"='" + row [ columnUse ] + "'" );
                if ( rows . Length > 0 )
                {
                    DataRow r = rows [ 0 ];
                    if ( !string . IsNullOrEmpty ( columnStartTime ) )
                    {
                        if ( row [ columnStartTime ] != null && row [ columnStartTime ] . ToString ( ) != string . Empty && row [ columnEndTime ] != null && row [ columnEndTime ] . ToString ( ) != string . Empty )
                        {
                            if ( r [ sjTime ] != null && r [ sjTime ] . ToString ( ) != string . Empty && r [ ejTime ] != null && r [ ejTime ] . ToString ( ) != string . Empty )
                            {
                                if ( checkTimeTwo ( Convert . ToDateTime ( r [ sjTime ] ) ,Convert . ToDateTime ( r [ ejTime ] ) ,Convert . ToDateTime ( row [ columnStartTime ] ) ,Convert . ToDateTime ( row [ columnEndTime ] ) ) == false )
                                {
                                    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计件时间段,请核实";
                                    break;
                                }
                                //if ( Convert . ToDateTime ( r [ sjTime ] ) <= Convert . ToDateTime ( row [ columnEndTime ] ) && Convert . ToDateTime ( r [ ejTime ] ) >= Convert . ToDateTime ( row [ columnStartTime ] ) && Convert . ToDateTime ( r [ ejTime ] ) <= Convert . ToDateTime ( row [ columnEndTime ] ) )
                                //{
                                //    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计件时间段,请核实";
                                //    break;
                                //}
                            }
                            if ( r [ sTime ] != null && r [ sTime ] . ToString ( ) != string . Empty && r [ eTime ] != null && r [ eTime ] . ToString ( ) != string . Empty )
                            {
                                if ( checkTimeTwo ( Convert . ToDateTime ( r [ sTime ] ) ,Convert . ToDateTime ( r [ eTime ] ) ,Convert . ToDateTime ( row [ columnStartTime ] ) ,Convert . ToDateTime ( row [ columnEndTime ] ) ) == false )
                                {
                                    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计件时间段,请核实";
                                    break;
                                }

                                //if ( Convert . ToDateTime ( r [ sTime ] ) <= Convert . ToDateTime ( row [ columnEndTime ] ) && Convert . ToDateTime ( r [ eTime ] ) >= Convert . ToDateTime ( row [ columnStartTime ] ) && Convert . ToDateTime ( r [ eTime ] ) <= Convert . ToDateTime ( row [ columnEndTime ] ) )
                                //{
                                //    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计件时间段,请核实";
                                //    break;
                                //}
                            }
                        }
                    }

                    if ( !string . IsNullOrEmpty ( columnStart ) )
                    {
                        if ( row [ columnStart ] != null && row [ columnStart ] . ToString ( ) != string . Empty && row [ columnEnd ] != null && row [ columnEnd ] . ToString ( ) != string . Empty )
                        {
                            if ( r [ sTime ] != null && r [ sTime ] . ToString ( ) != string . Empty && r [ eTime ] != null && r [ eTime ] . ToString ( ) != string . Empty )
                            {
                                if ( checkTimeTwo ( Convert . ToDateTime ( r [ sTime ] ) ,Convert . ToDateTime ( r [ eTime ] ) ,Convert . ToDateTime ( row [ columnStart ] ) ,Convert . ToDateTime ( row [ columnEnd ] ) ) == false )
                                {
                                    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计件时间段,请核实";
                                    break;
                                }
                                //if ( Convert . ToDateTime ( r [ sTime ] ) <= Convert . ToDateTime ( row [ columnEnd ] ) && Convert . ToDateTime ( r [ eTime ] ) >= Convert . ToDateTime ( row [ columnStart ] ) && Convert . ToDateTime ( r [ eTime ] ) <= Convert . ToDateTime ( row [  columnEnd ] ) )
                                //{
                                //    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计时时间段,请核实";
                                //    break;
                                //}
                            }
                            if ( r [ sjTime ] != null && r [ sjTime ] . ToString ( ) != string . Empty && r [ ejTime ] != null && r [ ejTime ] . ToString ( ) != string . Empty )
                            {
                                if ( checkTimeTwo ( Convert . ToDateTime ( r [ sjTime ] ) ,Convert . ToDateTime ( r [ ejTime ] ) ,Convert . ToDateTime ( row [ columnStart ] ) ,Convert . ToDateTime ( row [ columnEnd ] ) ) == false )
                                {
                                    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计件时间段,请核实";
                                    break;
                                }
                                //if ( Convert . ToDateTime ( r [  sjTime ] ) <= Convert . ToDateTime ( row [ columnEnd ] ) && Convert . ToDateTime ( r [ ejTime ] ) >= Convert . ToDateTime ( row [ columnStart ] ) && Convert . ToDateTime ( r [ ejTime ] ) <= Convert . ToDateTime ( row [  columnEnd ] ) )
                                //{
                                //    messageInfo = "" + work + "单号:" + r [ codeOdd ] + "\n\r工号:" + r [ userCode ] + "\n\r已经存在计时时间段,请核实";
                                //    break;
                                //}
                            }
                        }
                    }
                }

            }
        }

        static bool checkTimeTwo ( DateTime a ,DateTime b ,DateTime c ,DateTime d )
        {
            //[a,b] [c,d]
            if ( a > c && a < d )
                return false;
            if ( b > c && b < d )
                return false;
            if ( c > a && c < b )
                return false;
            if ( d > a && d < b )
                return false;
            if ( a == c && b == d )
                return false;

            return true;
        }

    }
}
