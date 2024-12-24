using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using NotificationAPI.Models;
using NotificationAPI.Helpers;

namespace NotificationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(ILogger<NotificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("PushNotificationOnly")]
        public IActionResult PushNotificationOnly(RequestMessage request)
        {
            NotificationHelper.SendFCMWithNotificationOnlyAsync(new MessageModel() { Title = request.Title, Body = request.Body },request.Topic);
            return Ok("Ok");
        }

        [HttpPost]
        [Route("PushNotificationIncludeData")]
        public IActionResult PushNotificationIncludeData(RequestMessage request)
        {
            NotificationHelper.SendFCMWithNotificationIncludeDataAsync(new MessageModel() { Title = request.Title, Body = request.Body }, request.Topic, request.Priority);
            return Ok("Ok");
        }

        [HttpPost]
        [Route("PushWithDataOnly")]
        public IActionResult PushWithDataOnly(RequestMessage request)
        {
            NotificationHelper.SendFCMAsync(new MessageModel() { Title = request.Title, Body = request.Body }, request.Topic);
            return Ok("Ok");
        }

        [HttpPost]
        [Route("PushNotificationWithToken")]
        public IActionResult PushNotificationWithToken(RequestMessageByToken request)
        {
            NotificationHelper.SendFCMBySpecificIdAsync(new MessageModel() { Title = request.Title, Body = request.Body }, request.Token);
            return Ok("Ok");
        }

        [HttpPost]
        [Route("PushNotificationWithTokens")]
        public IActionResult PushNotificationWithTokens(RequestMessageByTokens request)
        {
            NotificationHelper.SendFCMBySpecificIdsAsync(new MessageModel() { Title = request.Title, Body = request.Body }, request.Tokens);
            return Ok("Ok");
        }
    }
}
