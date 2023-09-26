using ECommerce.DtoLayer.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(CreateMailDto createMailDto);
    }
}
