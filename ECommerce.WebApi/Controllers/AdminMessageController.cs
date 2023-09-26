using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IMapper _mapper;

        public AdminMessageController(IMessageService MessageService, IMapper mapper, IEmailSenderService emailSenderService)
        {
            _messageService = MessageService;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMessagesForReceiver(string userName)
        {
            var messages = await _messageService.GetMessagesForReceiver(userName);
            return Ok(messages);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMessagesForSender(string userName)
        {
            var messages = await _messageService.GetMessagesForSender(userName);
            return Ok(messages);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetMessageByIdReceiver(int id)
        {
            var message = await _messageService.TGetByIdAsync(id);
            if (!message.isReadforReceiver) 
            {
                message.isReadforReceiver = true;
                await _messageService.TUpdateAsync(message);
                return Ok(message);
            }
            return Ok(message);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetMessageByIdSender(int id)
        {
            var message = await _messageService.TGetByIdAsync(id);
            return Ok(message);
        }

        [HttpGet("[action]/{id},{forReceiver}")]
        public async Task<IActionResult> DeleteMessage(int id, bool forReceiver)
        {
            var message = await _messageService.TGetByIdAsync(id);
            if (message == null) { return BadRequest(); }
            if (forReceiver == true) 
            { 
                message.isDeletedforReceiver = true;
            }
            if (forReceiver == false)
            {
                message.isDeletedforSender = true;
            }
            if (message.isDeletedforReceiver == true && message.isDeletedforSender == true)
            {
                await _messageService.TDeleteAsync(message);
            }
            await _messageService.TUpdateAsync(message);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(CreateMessageDto createMessageDto)
        {
            var value = _mapper.Map<Message>(createMessageDto);
            await _messageService.TInsertAsync(value);
            return Ok();
        }

        [HttpPost("[action]")] 
        public async  Task<IActionResult> SendEmail(CreateMailDto createMailDto) 
        {
            await _emailSenderService.SendEmailAsync(createMailDto);
            return Ok();
        }

    }
}
