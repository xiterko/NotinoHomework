using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using NotinoHomework.Dto;
using NotinoHomework.Enums;
using NotinoHomework.Repositories;

namespace NotinoHomework.Facades
{
    public class DocumentRepository : IDocumentRepository
    {

        private Document GetDocument(string path, out string fileType)
        {
            try
            {
                using FileStream sourceStream = File.Open(path, FileMode.Open);
                var fi = new FileInfo(path);
                fileType = fi.Extension;
                var reader = new StreamReader(sourceStream);
                var result = reader.ReadToEnd();
                return new Document
                {
                    Title = fi.Name,
                    Text = result
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Document Get(string path, DocumentFormats? format)
        {
            var doc = GetDocument(path, out string fileType);

            switch (format)
            {
                case DocumentFormats.Xml:
                    {
                        if (fileType.ToLower() == ".json")
                        {
                            doc.Text = JsonConvert.DeserializeXNode(doc.Text, "Root").ToString();
                            doc.Title = doc.Title.Remove(doc.Title.Length - 4) + "xml";
                            return doc;
                        }
                        goto default;
                    }
                case DocumentFormats.Json:
                    {
                        if (fileType.ToLower() == ".xml")
                        {
                            XmlDocument docXml = new XmlDocument();
                            docXml.LoadXml(doc.Text);
                            doc.Text = JsonConvert.SerializeXmlNode(docXml).ToString();
                            doc.Title = doc.Title.Remove(doc.Title.Length - 3) + "json";
                            return doc;
                        }
                        goto default;
                    }
                default: return doc;
            }
        }
    }
}
