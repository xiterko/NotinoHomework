using NotinoHomework.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotinoHomework.Repositories
{
    public interface IEmailRepository
    {
        void SendEmail(string recipient, Document attachement);
    }
}
