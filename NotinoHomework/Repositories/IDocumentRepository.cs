using NotinoHomework.Dto;
using NotinoHomework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotinoHomework.Repositories
{
    public interface IDocumentRepository
    {
        Document Get(string path, DocumentFormats? format);
    }
}
