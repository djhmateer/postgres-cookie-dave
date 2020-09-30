﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostgresCookieDave.Web.Services;

namespace CookieDave.Web.IntegrationTests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public List<Email> SentEmails = new List<Email>();

        public Task SendAsync(string sender, string recipient, string title, string content)
        {
            SentEmails.Add(new Email(sender, recipient, title, content));
            return Task.CompletedTask;
        }

        public class Email
        {
            public string Sender { get; }
            public string Recipient { get; }
            public string Title { get; }
            public string Content { get; }

            public Email(string sender, string recipient, string title, string content)
            {
                Sender = sender;
                Recipient = recipient;
                Title = title;
                Content = content;
            }
        }
    }
}
