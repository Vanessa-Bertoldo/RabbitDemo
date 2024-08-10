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
            _rabbitMQService.SendMessage(mensagem);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// get messages in queue
        /// </summary>
        /// <returns>200</returns>
        [HttpGet("get-messages")]
        public IActionResult GetMessages([FromQuery] string queueName, [FromQuery] int maxMessages)
        {
            var messages = _rabbitMQService.GetMessagesFromQueue(queueName, maxMessages);
            return Ok(messages);
        }

        /// <summary>
        /// create exchange Fanout
        /// </summary>
        /// <returns>200</returns>
        [HttpGet("create-exchange-fanout")]
        public IActionResult CreateExchangeFanout([FromQuery] string exchangeName)
        {
            var retornMethod = _rabbitMQService.CreateExchangeFanout(exchangeName);
            return Ok(retornMethod);
        }

        /// <summary>
        /// create exchange Direct
        /// </summary>
        /// <returns>200</returns>
        [HttpGet("create-exchange-direct")]
        public IActionResult CreateExchangeDirect([FromQuery] string exchangeName)
        {
            var retornMethod = _rabbitMQService.CreateExchangeDirect(exchangeName);
            return Ok(retornMethod);
        }

        /// <summary>
        /// send message to exchange
        /// </summary>
        /// <returns>200</returns>
        [HttpGet("send-message-exchange")]
        public IActionResult SendMessageExchange([FromQuery] string exchangeName, [FromQuery] string message)
        {
            var retornMethod = _rabbitMQService.SendMessageForExchange(exchangeName, message);
            return Ok(retornMethod);
        }
    }
}
