using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Helpers
{
    public static class XMLHelper
    {
        public static bool Save(object obj)
        {
            string fileName = obj.GetType().Name;
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\{Path.GetFileNameWithoutExtension(fileName).ToLower()}.xml";

            var xmlSerializer = new XmlSerializer(obj.GetType(), obj.GetType().GetNestedTypes());
            using (var stream = new MemoryStream())
            {
                using (var xw = XmlWriter.Create(stream,
                                    new XmlWriterSettings()
                                    {
                                        Encoding = new UTF8Encoding(false),
                                        Indent = true,
                                        NewLineOnAttributes = true,
                                        OmitXmlDeclaration = true
                                    }))
                {
                    xmlSerializer.Serialize(xw, obj);
                    var bytes = stream.ToArray();
                    var fileContent = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    File.AppendAllText(filePath, fileContent);
                }
            }
            return true;
        }

        public static T Get<T>()
        {
            string fileName = typeof(T).Name;
            var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\App_Data\\{fileName}.xml";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            var xmlSerializer = new XmlSerializer(typeof(T), typeof(T).GetNestedTypes());
            T result;

            var xmlStr = File.ReadAllText(filePath);

            using (TextReader reader = new StringReader(xmlStr))
            {
                try
                {
                    result = (T)xmlSerializer.Deserialize(reader);
                }
                catch
                {
                    result = Activator.CreateInstance<T>();
                }
            }
            return result;
        }
    }
}