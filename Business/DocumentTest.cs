using Microsoft.AspNetCore.Mvc;
using Moq;
using NotinoHomework.Controllers;
using NotinoHomework.Facades;
using NotinoHomework.Repositories;
using System;
using System.IO;
using Xunit;

namespace Business
{
    public class DocumentTest
    {
       DocumentRepository _documentRepository;
       EmailRepository _emailRepository;

        public DocumentTest()
        {
            _documentRepository = new DocumentRepository();
            _emailRepository = new EmailRepository();
        }

        [Fact]
        public void GetJson()
        {
            var controller = new DocumentController(_documentRepository, _emailRepository);

            var result = (OkObjectResult)controller.Get(Environment.CurrentDirectory + "\\Files\\exampleJson.json", NotinoHomework.Enums.DocumentFormats.Json);

            var fs = new FileStream(Environment.CurrentDirectory + "\\Files\\exampleJson.json", FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(fs);
            string OriginalFile = reader.ReadToEnd();

            Assert.True(File.Equals(result.Value, OriginalFile), "Files do not match");
        }

        [Fact]
        public void GetXml()
        {
            var controller = new DocumentController(_documentRepository, _emailRepository);

            var result = (OkObjectResult)controller.Get(Environment.CurrentDirectory + "\\Files\\exampleXml.xml", NotinoHomework.Enums.DocumentFormats.Xml);

            var fs = new FileStream(Environment.CurrentDirectory + "\\Files\\exampleXml.xml", FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(fs);
            string OriginalFile = reader.ReadToEnd();

            Assert.True(File.Equals(result.Value, OriginalFile), "Files do not match");
        }

        [Fact]
        public void GetJsonFileAsXml()
        {
            var controller = new DocumentController(_documentRepository, _emailRepository);

            var result = (OkObjectResult)controller.Get(Environment.CurrentDirectory + "\\Files\\exampleJson.json", NotinoHomework.Enums.DocumentFormats.Xml);

            var fs = new FileStream(Environment.CurrentDirectory + "\\Files\\JsonConvertedToXml.xml", FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(fs);
            string ConverterFile = reader.ReadToEnd();

            Assert.True(File.Equals(result.Value, ConverterFile), "Files do not match");
        }

        [Fact]
        public void GetXmlFileAsJson()
        {
            var controller = new DocumentController(_documentRepository, _emailRepository);

            var result = (OkObjectResult)controller.Get(Environment.CurrentDirectory + "\\Files\\exampleXml.xml", NotinoHomework.Enums.DocumentFormats.Json);

            var fs = new FileStream(Environment.CurrentDirectory + "\\Files\\XmlConvertedToJson.json", FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(fs);
            string ConverterFile = reader.ReadToEnd();

            Assert.True(File.Equals(result.Value, ConverterFile), "Files do not match");
        }



    }
}
