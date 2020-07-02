using ArtigoAzureServiceBus.API.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ArtigoAzureServiceBus.API.Controllers
{
    [Route("api/{controller}")]
    public class FollowersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "email-queue";

        public FollowersController(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration.GetSection("ServiceBus:ConnectionString").Value;

            _queueClient = new QueueClient(connectionString, QUEUE_NAME);
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] UserFollowingInputModel userFollowingInputModel)
        {
            var inputModelJsonString = JsonConvert.SerializeObject(userFollowingInputModel);

            var messageBytes = Encoding.UTF8.GetBytes(inputModelJsonString);

            var message = new Message(messageBytes);

            await _queueClient.SendAsync(message);

            return Ok();
        }
    }
}
