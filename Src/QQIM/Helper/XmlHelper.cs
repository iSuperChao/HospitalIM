using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;

namespace ClientView
{
    /// <summary>
    /// XML文件操作类
    /// </summary>
    class XmlHelper
    {
        #region 变量
        
        const string STAR_PATH = "../../Xml/StarInfo.xml";
        const string BLOOD_PATH = "../../Xml/BloodTypeInfo.xml";
        XmlDocument _xmlDocument = new XmlDocument();

        #endregion

        #region 方法
        
        /// <summary>
        /// 获取星座信息
        /// </summary>
        /// <returns>星座集合</returns>
        public List<Star> GetStarInfo()
        {
            List<Star> starList = new List<Star>();
            if (File.Exists(STAR_PATH))//判断指定的文件是否存在
            {
                this._xmlDocument.Load(STAR_PATH);
            }
            else//从程序集中加载嵌入资源
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream("ClientView.Xml.StarInfo.xml");
                this._xmlDocument.Load(stream);
            }
            XmlNode xmlRoot = this._xmlDocument.DocumentElement;
            foreach (XmlNode xmlNode in xmlRoot.ChildNodes)
            {
                Star star = new Star() 
                {
                    Id = Convert.ToInt32(xmlNode.Attributes["Code"].InnerText),
                    StarName = xmlNode.Attributes["Name"].InnerText
                };
                starList.Add(star);
            }
            return starList;
        }

        /// <summary>
        /// 获取血型信息
        /// </summary>
        /// <returns>血型集合</returns>
        public List<BloodType> GetBloodTypeInfo()
        {
            List<BloodType> bloodList = new List<BloodType>();
            if (File.Exists(BLOOD_PATH))//判断指定的文件是否存在
            {
                this._xmlDocument.Load(BLOOD_PATH);
            }
            else//从程序集中加载嵌入资源
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream("ClientView.Xml.BloodTypeInfo.xml");
                this._xmlDocument.Load(stream);
            }
            XmlNode xmlRoot = this._xmlDocument.DocumentElement;
            foreach (XmlNode xmlNode in xmlRoot.ChildNodes)
            {
                BloodType blood = new BloodType()
                {
                    Id = Convert.ToInt32(xmlNode.Attributes["Code"].InnerText),
                    BloodTypeName = xmlNode.Attributes["Name"].InnerText
                };
                bloodList.Add(blood);
            }
            return bloodList;
        }

        #endregion
    }
}
