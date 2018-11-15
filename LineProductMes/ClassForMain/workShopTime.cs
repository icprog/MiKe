using System;
using System . Data;

namespace LineProductMes . ClassForMain
{
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
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) >= Convert . ToDateTime ( row [ rowName ] ) )
                        return false;
                    if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( obj ) >= Convert . ToDateTime ( row [ rowNameStart ] ) && Convert . ToDateTime ( row [ rowName ] ) <= Convert . ToDateTime ( row [ rowNameEnd ] ) )
                                return false;
                        }
                    }
                }
                if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameStart ] ) )
                        return false;
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
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowName ] ) )
                        return false;
                    if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                    {
                        if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime (row[ rowName] ) >= Convert . ToDateTime ( row [ rowNameStart ] ) && Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowNameEnd ] ) )
                                return false;
                        }
                    }
                }
                if ( row [ rowNameStart ] != null && row [ rowNameStart ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameStart ] ) )
                    {
                        if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowNameEnd ] ) )
                                return false;
                        }
                    }
                }
            }

            return true;
        }
        
        /// <summary>
        /// 计时开始
        /// </summary>
        /// <param name="row">选中列</param>
        /// <param name="obj">计时开始时间</param>
        /// <param name="rowName">计件结束</param>
        /// <param name="rowNameEnd">计时结束</param>
        /// <param name="rowEndName">计件开始</param>
        /// <returns></returns>
        public static bool startCenTime (DataRow row,object obj,string rowName,string rowNameEnd,string rowEndName )
        {
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowName ] ) )
                    {
                        if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowEndName ] ) )
                                return false;
                            if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                            {
                                if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowEndName ] ) && Convert . ToDateTime ( row [ rowNameEnd ] ) >= Convert . ToDateTime ( row [ rowName ] ) )
                                    return false;
                            }
                        }
                    }
                }
                if ( row [ rowNameEnd ] != null && row [ rowNameEnd ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowNameEnd ] ) )
                        return false;
                }
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
        public static bool endCenTime ( DataRow row ,object obj ,string rowName ,string rowStartName,string rowEndName )
        {
            if ( obj != null && obj . ToString ( ) != string . Empty )
            {
                if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowName ] ) )
                        return false;
                }
                if ( row [ rowStartName ] != null && row [ rowStartName ] . ToString ( ) != string . Empty )
                {
                    if ( Convert . ToDateTime ( obj ) > Convert . ToDateTime ( row [ rowStartName ] ) )
                    {
                        if ( row [ rowEndName ] != null && row [ rowEndName ] . ToString ( ) != string . Empty )
                        {
                            if ( Convert . ToDateTime ( obj ) <= Convert . ToDateTime ( row [ rowEndName ] ) )
                                return false;
                            if ( row [ rowName ] != null && row [ rowName ] . ToString ( ) != string . Empty )
                            {
                                if ( Convert . ToDateTime ( obj ) >= Convert . ToDateTime ( row [ rowEndName ] ) && Convert . ToDateTime ( row[rowName] ) <= Convert . ToDateTime ( row [ rowStartName ] ) )
                                    return false;
                            }
                        }
                    }
                }
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

    }
}
