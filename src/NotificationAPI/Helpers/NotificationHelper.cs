using FirebaseAdmin.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using NotificationAPI.Models;

namespace NotificationAPI.Helpers
{
    public class NotificationHelper
    {
        public static async Task<bool> SendFCMWithNotificationIncludeDataAsync(MessageModel messageModel, string topic, string priority)
        {
            var notification = new Notification()
            {
                Title = messageModel.Title,
                Body = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
            };

            var messageData = ConvertToMessageData(messageModel);

            var message = new Message()
            {
                Data = messageData,
                Notification = notification,
                Topic = topic,
                Android = ConvertToAndroidData(priority)
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            Console.WriteLine(result);

            return true;
        }

        public static async Task<bool> SendFCMWithNotificationOnlyAsync(MessageModel messageModel, string topic)
        {
            var notification = new Notification()
            {
                Title = messageModel.Title,
                Body = messageModel.Body
            };


            var message = new Message()
            {
                //Token = "",

                Notification = notification,
                Topic = topic
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            Console.WriteLine(result);

            return true;
        }

        public static async Task<bool> SendFCMAsync(MessageModel messageModel, string topic)
        {
            var messageData = ConvertToMessageData(messageModel);

            var message = new Message()
            {
                //Token = "",

                Data = messageData,
                Topic = topic
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            Console.WriteLine(result);

            return true;
        }

        public static async Task<bool> SendFCMBySpecificIdAsync(MessageModel messageModel, string specificId)
        {
            var messageData = ConvertToMessageData(messageModel);

            var notification = new Notification()
            {
                Title = messageModel.Title,
                Body = messageModel.Body
            };

            var message = new Message()
            {
                Token = specificId,
                Data = messageData,
                Notification = notification
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            Console.WriteLine(result);

            return true;
        }

        public static async Task<bool> SendFCMBySpecificIdsAsync(MessageModel messageModel, List<string> specificIds)
        {
            var messageData = ConvertToMessageData(messageModel);

            var notification = new Notification()
            {
                Title = messageModel.Title,
                Body = messageModel.Body
            };

            var message = new MulticastMessage()
            {
                Tokens = specificIds,
                Data = messageData,
                Notification = notification
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendMulticastAsync(message);
            Console.WriteLine(result);

            return true;
        }

        private static Dictionary<string, string> ConvertToMessageData(MessageModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var dictionary = new Dictionary<string, string>();

            foreach (var propertyInfo in model.GetType().GetProperties())
            {
                dictionary[propertyInfo.Name] = propertyInfo.GetValue(model).ToString();
            }

            return dictionary;
        }

        private static AndroidConfig ConvertToAndroidData(string priority)
        {
            if(priority.ToLower() == "high")
                return new AndroidConfig() { Priority = Priority.High};
            else
                return new AndroidConfig() { Priority = Priority.Normal};
        }
    }
}
