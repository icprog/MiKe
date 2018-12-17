using System;
using System . Data;

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
                if ( row [ "IJB016" ] != null && row [ "IJB016" ] . ToString ( ) != null && row [ "IJB017" ] != null && row [ "IJB017" ] . ToString ( ) != null )
                {
                    if ( Convert . ToDateTime ( num1 ) < Convert . ToDateTime ( row [ "IJB017" ] ) && Convert . ToDateTime ( num2 ) > Convert . ToDateTime ( row [ "IJB016" ] ) )
                        return false;
                }
                //计时
                if ( row [ "IJB018" ] != null && row [ "IJB018" ] . ToString ( ) != null && row [ "IJB019" ] != null && row [ "IJB019" ] . ToString ( ) != null )
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
                if ( row [ "IJB016" ] != null && row [ "IJB016" ] . ToString ( ) != null && row [ "IJB017" ] != null && row [ "IJB017" ] . ToString ( ) != null )
                {
                    if ( Convert . ToDateTime ( num1 ) < Convert . ToDateTime ( row [ "IJB017" ] ) && Convert . ToDateTime ( num2 ) > Convert . ToDateTime ( row [ "IJB016" ] ) )
                        return false;
                }
                //计时
                if ( row [ "IJB018" ] != null && row [ "IJB018" ] . ToString ( ) != null && row [ "IJB019" ] != null && row [ "IJB019" ] . ToString ( ) != null )
                {
                    if ( Convert . ToDateTime ( num1 ) < Convert . ToDateTime ( row [ "IJB019" ] ) && Convert . ToDateTime ( num2 ) > Convert . ToDateTime ( row [ "IJB018" ] ) )
                        return false;
                }
            }

            return true;
        }

    }
}
