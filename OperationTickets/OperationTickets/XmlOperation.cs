using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;


namespace OperationTickets
{
    class XmlOperation
    {

        public bool  CreatXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);
            //根节点 
            XmlElement root = doc.CreateElement("Persons");
            doc.AppendChild(root);

            //根节点的添加独立子节点 
            XmlElement person = doc.CreateElement("Person");
            person.SetAttribute("id", "1");
            person.AppendChild(getChildNode(doc, "Name", "FlyElephant"));
            person.AppendChild(getChildNode(doc, "Age", "24"));
            root.AppendChild(person);

            //根节点的添加独立子节点 
            person = doc.CreateElement("Person");
            person.SetAttribute("id", "2");
            person.AppendChild(getChildNode(doc, "Name", "keso"));
            person.AppendChild(getChildNode(doc, "Age", "25"));
            root.AppendChild(person);

            doc.Save("person.xml");
            return true;
        }


        public XmlNode  getChildNode(XmlDocument doc, string name,string inerText)
        {

            XmlNode nameNode = doc.SelectSingleNode("person.xml");

            return nameNode ;
        }

        public void GetXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("person.xml");    //加载Xml文件 
            XmlElement root = doc.DocumentElement;   //获取根节点 
            XmlNodeList personNodes = root.GetElementsByTagName("Person"); //获取Person子节点集合 
            foreach (XmlNode node in personNodes)
            {
                string id = ((XmlElement)node).GetAttribute("id");   //获取Name属性值 
                string name = ((XmlElement)node).GetElementsByTagName("Name")[0].InnerText;  //获取Age子XmlElement集合 
                string age = ((XmlElement)node).GetElementsByTagName("Age")[0].InnerText;
                Console.WriteLine("编号:" + id + "姓名:" + name + "年龄:" + age);
            }
        }

        public void ModifyXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("person.xml");    //加载Xml文件 
            XmlElement root = doc.DocumentElement;   //获取根节点 
            XmlNodeList personNodes = root.GetElementsByTagName("Person"); //获取Person子节点集合
            foreach (XmlNode node in personNodes)
            {
                XmlElement ele = (XmlElement)node;
                if (ele.GetAttribute("id") == "3")
                {
                    XmlElement nameEle = (XmlElement)ele.GetElementsByTagName("Name")[0];
                    nameEle.InnerText = nameEle.InnerText + "修改";
                    //XmlElement selectEle = (XmlElement)root.SelectSingleNode("/Persons/Person[@id='1']");
                    //XmlElement nameEle = (XmlElement)selectEle.GetElementsByTagName("Name")[0];
                    //nameEle.InnerText = nameEle.InnerText + "修改";
                }
            }
            doc.Save("person.xml");

        }

        public void DeleteXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("person.xml");    //加载Xml文件 
            XmlElement root = doc.DocumentElement;   //获取根节点 
            XmlNodeList personNodes = root.GetElementsByTagName("Person"); //获取Person子节点集合 
            XmlNode selectNode = root.SelectSingleNode("/Persons/Person[@id='1']");
            root.RemoveChild(selectNode);
            doc.Save("person.xml");
        }

















        private string ConvertDataTableToXML(DataTable xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
        private DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
