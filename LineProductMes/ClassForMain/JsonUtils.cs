using Newtonsoft . Json;
using System . Collections . Generic;
using System . IO;
using System . Text;
using System . Windows . Forms;

namespace LineProductMes . ClassForMain
{
    public static class JsonUtils
    {
        /// <summary>
        /// 把Json转为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<T> JsonStringToObj<T> ( string result ) where T : class
        {
            List<T> list = new List<T> ( );
            T t = JsonConvert . DeserializeObject<T> ( result );
            list . Add ( t );
            return list;
        }

        /// <summary>
        /// 读取本地json文件，转为字符串
        /// </summary>
        /// <returns></returns>
        public static string ReadJson ( )
        {
            StreamReader sr = new StreamReader ( Application . StartupPath + "\\JS.json" ,Encoding . UTF8 );
            string line = string . Empty;
            string jsonObj = string . Empty;
            while ( ( line = sr . ReadLine ( ) ) != null )
            {
                jsonObj += line . ToString ( );
            }
            return jsonObj;
        }

        /// <summary>
        /// 把Json转为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<T> JsonStringToVar<T> ( string result )
        {
            JsonSerializer serializer = new JsonSerializer ( );
            StreamReader sr = new StreamReader ( Application . StartupPath + "\\JS.json" ,Encoding . UTF8 );
            List<T> list = ( List<T> ) serializer . Deserialize ( new JsonTextReader ( sr ) ,typeof ( List<T> ) );
            return list;
        }

    }
}
