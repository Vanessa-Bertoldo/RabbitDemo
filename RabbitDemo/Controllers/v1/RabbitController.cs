using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitDemo.Models;
using RabbitDemo.Services;

namespace RabbitDemo.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;

        public RabbitController(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        /// <summary>
        /// create message to queue
        /// </summary>
        /// <returns>201</returns>
        [HttpPost("create-queue")]
        public IActionResult CreateQueue([FromQuery] string queueName)
        {
            _rabbitMQService.CreateQueue(queueName);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// send message to queue
        /// </summary>
        /// <returns>201</returns>
        [HttpPost("send-message")]
        public IActionResult SendMessage([FromQuery] string queueName, [FromBody] string message)
        {
            _rabbitMQService.SendMessage(queueName, message);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// send message to queue
        /// </summary>
        /// <returns>201</returns>
        [HttpPost("send-message-queue")]
        public IActionResult SendMessageForQueue(MensagemModel mensagem)
        {
            _rabbitMQService.SendMessage(mensagem.Destino, mensagem.Mensagem);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("get-messages")]
        public IActionResult GetMessages([FromQuery] string queueName, [FromQuery] int maxMessages)
        {
            var messages = _rabbitMQService.GetMessagesFromQueue(queueName, maxMessages);
            return Ok(messages);
        }
    }
}
