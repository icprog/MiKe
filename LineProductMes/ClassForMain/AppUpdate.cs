using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace AutoUpdate
{
    /// <summary>
    /// 应用程序升级检测类
    /// </summary>
    public class AppUpdate
    {
        public const string UPDATEFILENAME="UpdateList.xml";
        public const string TEMPNAME = "Temp_Update";
        /// <summary>
        /// 本地更新文件路径 (UpdateList.xml)
        /// </summary>
        public string LocalUpdateFile
        {
            get
            {
                string path = Application.StartupPath;
                path += path.EndsWith("\\") ?  UPDATEFILENAME : "\\"+ UPDATEFILENAME;
                return path;
            }
        }

        private Version _localVersion = null;
        /// <summary>
        /// 本地版本信息
        /// </summary>
        public Version LocalVersion
        {
            get { return _localVersion; }
            set { _localVersion = value; }
        }
        private Version _newVersion = null; 
        /// <summary>
        /// 最新版本信息
        /// </summary>
        public Version NewVersion
        {
            get { return _newVersion; }
            set { _newVersion = value; }
        }
        private bool _forceUpdate = false;
        /// <summary>
        /// 是否强制升级
        /// </summary>
        public bool ForceUpdate
        {
            get { return _forceUpdate; }
            set { _forceUpdate = value; }
        }
        /// <summary>
        /// 本地更新目录
        /// </summary>
        public string LocalFileDir
        {
            get
            {
                string path = Application.StartupPath;
                path += path.EndsWith("\\") ? TEMPNAME : "\\" + TEMPNAME;
                return path;
            }    
        }
        /// <summary>
        /// 服务端更新文件的地址( http://******/updatelist.xml )
        /// </summary>
        protected string ServerUpdateFile
        {
            get
            {
                string path =  _localVersion ==null ? "":  _localVersion.ServerUrl;
                path += path.EndsWith("/") ? UPDATEFILENAME : "/"+ UPDATEFILENAME;
                return path;
            }
        }
        /// <summary>
        /// 服务端更新文件的本地克隆文件路径
        /// </summary>
        protected string ServerUpdateFileClone
        {
            get
            {
                string path = LocalFileDir;
                path += path.EndsWith("\\") ? UPDATEFILENAME : "\\"+UPDATEFILENAME;
                return path;
            }
        }
        /// <summary>
        /// 服务端更新版本列表
        /// </summary>
        public List<Version> ServerVersions
        {
            get;
            set;
        }          
        /// <summary>
        /// 获得服务端更新版本列表,及最新的版本信息,同时去除比本地版本旧的版本信息
        /// 修改升级策略，升级最近的全量包和最新的版本。不进行增量升级
        /// 当UpdateFlag为2时，说明是全量升级包，之前的增量版本可以不升级
        /// </summary>
        /// <returns>返回更新列表</returns>
        protected List<Version> GetServerVersions(ref string msg)
        {
            try
            {
                msg = string.Empty;
                _newVersion = null;

                Version fullVersion = null;//全量升级包

                XmlFiles xmlserverfile = new XmlFiles(ServerUpdateFileClone);
                string serverUrl = xmlserverfile.GetNodeValue("//Updater/Url");
                string entryPoint = xmlserverfile.GetNodeValue("//Application/EntryPoint");

                XmlNodeList fileNodes = xmlserverfile.GetChildNodeList("//Dir");
                if (fileNodes == null || fileNodes.Count < 1)
                {
                    msg = "没有发现版本信息！";
                    Utility . LogHelper . WriteLog ( "服务端升级文件中没有包含升级版本信息!" );
                    return null; 
                }
                _newVersion = new Version();
                List<Version> serverVersions = new List<Version>();
                int versionid = -1;//记录最新的版本标记
                foreach (XmlNode node in fileNodes)
                {
                    string sysifuse = node.Attributes["sysifuse"].InnerText.Trim();
                    if (sysifuse != "1") continue;

                    string updateflag = "0";
                    try
                    {
                        updateflag = node.Attributes["updateflag"].InnerText.Trim();
                    }
                    catch
                    { updateflag = "0"; }

                    int id = 0;
                    int.TryParse(node.Attributes["value"].InnerText, out id);
                    Version version = new Version();
                    string versionstr = string.Empty;
                    try
                    {
                        versionstr = node.Attributes["ver"].InnerText;
                    }
                    catch { }
                    version.StrVersion = versionstr;
                    version.Ifuse = sysifuse;
                    version.ValueVersion = id;
                    version.Updateflag = updateflag;
                    //比较版本，获得最新版本   
                    if (versionid < id)
                    {
                        versionid = id;
                        _newVersion.StrVersion = version.StrVersion;
                        _newVersion.ValueVersion = version.ValueVersion;
                        _newVersion.Ifuse = version.Ifuse;
                        _newVersion.Updateflag = version.Updateflag;
                        _newVersion.ServerUrl = serverUrl;
                        _newVersion.EntryPoint = entryPoint;
                    }
                    //比较版本，去除老版本
                    if (id <= LocalVersion.ValueVersion) continue;

                    //当当前版本是全量升级包时，记录最新的全量包 modify 2014.11.7
                    if (updateflag.Equals("2") )
                    {
                        if (fullVersion == null)
                        {
                            fullVersion = version;
                        }
                        else if (fullVersion.ValueVersion < id)
                        {
                            fullVersion = version;
                        }                         
                    }

                    serverVersions.Add(version);
                    //当 服务端版本配置文件 中有 必须升级标记则强制升级
                    if (version.Updateflag == "1") _forceUpdate = true;
                }

                //serverVersions.Clear();

                //修改 保存 全量包的版本信息 modify 2014.11.7
                if (fullVersion != null && fullVersion.ValueVersion > LocalVersion.ValueVersion && fullVersion.ValueVersion != versionid )
                {
                    serverVersions.Clear();
                    serverVersions.Add(fullVersion);
                }

                //修改 保存 最新的版本信息 modify 2014.4.8                    
                //if (versionid > LocalVersion.ValueVersion )
                //{
                //    serverVersions.Add(_newVersion);
                //}


                return serverVersions;
            }
            catch (Exception ex)
            {
                msg = "获得服务端版本信息时发生错误："+ ex.Message;
                Utility . LogHelper . WriteException ( ex );
                return null;
            }
        }
        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <returns></returns>
        protected bool CheckFileExist( string path )
        {                     
            return File.Exists(path);
        }
        /// <summary>
        /// 下载 文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="localFilePath"></param>
        public void DownloadFile(string url , string localFilePath)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(url, localFilePath);
                client.Dispose();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 开启主程序
        /// </summary>
        public void StartMainAppExe(string appname)
        {
            string path = Application.StartupPath + "\\" + appname;
            if (CheckFileExist(path))
            {
                //System.Diagnostics.Process.Start(path);
                StartUpExe(path);
            }
            Application.Exit();
        }
        /// <summary>
        /// 以管理员权限运行程序
        /// </summary>
        /// <param name="path"></param>
        protected void StartUpExe(string path)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = path;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                   Environment.OSVersion.Version.Major > 5 &&
                   Environment.OSVersion.Version.Minor > 0)
            {//当操作系统是win7以上版本时，运行下面语句
                startInfo.Verb = "runas"; //以管理员权限运行
            }
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start(startInfo);
        }
        /// <summary>
        /// 检测程序版本信息
        /// </summary>
        /// <returns></returns>
        public bool CheckAppVersion ( ref string msg )
        {
            if ( CheckFileExist( LocalUpdateFile ) == false )
            {
                msg = "本地更新配置文件不存在,更新失败!";
                return false;
            }
            try
            {//获得本地版本信息
                _localVersion = new Version( );
                XmlFiles xmllocalfile = new XmlFiles( LocalUpdateFile );
                _localVersion.EntryPoint = xmllocalfile.GetNodeValue( "//Application/EntryPoint" );
                _localVersion.ServerUrl = xmllocalfile.GetNodeValue( "//Updater/Url" );
                _localVersion.ValueVersion = int.Parse( xmllocalfile.GetNodeValue( "//Application/VersionId" ) );
                _localVersion.StrVersion = xmllocalfile.GetNodeValue( "//Application/VersionName" );
                //XtraMessageBox.Show( xmllocalfile.GetNodeValue( "//Application/VersionId" ) );
                //XtraMessageBox.Show( "EntryPoint:" + _localVersion.EntryPoint + "\n\rServerUrl:" + _localVersion.ServerUrl + "\n\rValueVersion:" + _localVersion.ValueVersion + "\n\rStrVersion:" + _localVersion.StrVersion + "" );
            }
            catch ( Exception ex )
            {
                msg = "本地更新配置文件出错!" + Environment.NewLine + ex.Message;
                return false;
            }
            try
            {
                if ( Directory.Exists( LocalFileDir ) ) { Directory.Delete( LocalFileDir ,true ); }
                Directory.CreateDirectory( LocalFileDir );
                //下载服务端更新文件到本地客户端
                DownloadFile( ServerUpdateFile ,ServerUpdateFileClone );
                //XtraMessageBox.Show( "ServerUpdateFile:" + ServerUpdateFile + "\n\rServerUpdateFileClone:" + ServerUpdateFileClone + "" );
            }
            catch (Exception ex)
            {
                Utility . LogHelper . WriteException ( ex );
                msg = "当前主程序版本:"+ (_localVersion != null ? _localVersion.StrVersion : "") +"，自动更新与服务器连接失败!" + Environment.NewLine + "请检查您的网络或联系维护人员。";
                return false;
            }
            if (File.Exists(ServerUpdateFileClone) == false)
            {
                msg = "更新配置文件下载失败,无法更新!";
                return false;
            }
            //获得服务端更新版本列表
            msg = string.Empty;
            ServerVersions = GetServerVersions( ref msg );
            //XtraMessageBox.Show( "" + ServerVersions + "" );
            if (string.IsNullOrEmpty(msg) == false)
            {               
                return false;
            }
            //
            if ( _localVersion.StrVersion == _newVersion.StrVersion && _localVersion.ValueVersion == _newVersion.ValueVersion ||
                _localVersion.ValueVersion >= _newVersion.ValueVersion )
            {
                msg =string.Format( "当前主程序版本:{0}，已经是最新版本，无需更新。" , _localVersion.StrVersion );
                if (Directory.Exists(LocalFileDir)) { Directory.Delete(LocalFileDir, true); }
                return false;
            }

            msg = "发现最新版本";
            return true;
        }
    }
}
