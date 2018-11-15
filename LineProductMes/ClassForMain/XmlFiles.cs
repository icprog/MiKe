using System;
using System.IO;
using System.Xml;

namespace AutoUpdate
{
	/// <summary>
	/// XmlFiles  文件操作类
	/// </summary>
	public class XmlFiles : XmlDocument
	{
		#region 字段与属性
		private string _xmlFilePath;
		public string XmlFilePath
		{
            set { _xmlFilePath = value; }
            get { return _xmlFilePath; }
		}
		#endregion

		public XmlFiles(string xmlFile)
		{
            XmlFilePath = xmlFile;			
			this.Load(xmlFile);
		}
		/// <summary>
		/// 给定一个节点的xPath表达式并返回一个节点
		/// </summary>
		public XmlNode FindNode(string xPath)
		{
			XmlNode xmlNode = this.SelectSingleNode(xPath);
			return xmlNode;           
		}
		/// <summary>
		/// 给定一个节点的xPath表达式返回其值
		/// </summary>
		/// <param name="xPath"></param>
		/// <returns></returns>
		public string GetNodeValue(string xPath)
		{
			XmlNode xmlNode = this.SelectSingleNode(xPath);
            if (xmlNode == null) return string.Empty;
			return xmlNode.InnerText;
		}
		/// <summary>
		/// 给定一个节点的表达式返回此节点下的孩子节点列表
		/// </summary>
		/// <param name="xPath"></param>
		/// <returns></returns>
		public XmlNodeList GetChildNodeList(string xPath)
		{
            XmlNode curNode = this.SelectSingleNode(xPath);
            if (curNode == null) return null;
			XmlNodeList nodeList = curNode.ChildNodes;
			return nodeList;
		}
        public XmlNodeList GetNodeList(string xPath)
        {
            XmlNodeList nodeList = this.SelectNodes(xPath);
            return nodeList;
        }
	}
}
