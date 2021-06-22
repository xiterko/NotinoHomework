using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotinoHomework.Enums;
using NotinoHomework.Repositories;

namespace NotinoHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IEmailRepository _emailRepository;

        public DocumentController(
            IDocumentRepository documentRepository,
            IEmailRepository emailRepository)
        {
            _documentRepository = documentRepository;
            _emailRepository = emailRepository;
        }

        [HttpGet]
        public ActionResult Get(string path, DocumentFormats? format)
        {
            return Ok(_documentRepository.Get(path, format).Text);
        }

        [HttpPost]
        public async Task<ActionResult> Create(string directory, IFormFile file)
        {
            if (file.Length > 0)
            {
                directory = directory ?? Environment.CurrentDirectory+"\\Files";
                directory += $"\\{file.FileName}";

               using Stream fileStream = new FileStream(directory, FileMode.Create, FileAccess.Write);
               await file.CopyToAsync(fileStream);
            }
            return Ok();
        }

        [HttpGet("SendEmail")]
        public ActionResult SendEmail(string recipientEmail, string fileLocation, DocumentFormats format = DocumentFormats.Xml)
        {
            var attachement = _documentRepository.Get(fileLocation, format);
            _emailRepository.SendEmail(recipientEmail, attachement);
            return Ok();
        }

    }
}
