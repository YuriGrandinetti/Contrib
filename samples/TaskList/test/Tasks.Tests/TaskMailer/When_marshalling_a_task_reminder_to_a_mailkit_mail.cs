#region Licence
/* The MIT License (MIT)
Copyright � 2014 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the �Software�), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using System;
using MimeKit;
using NUnit.Framework;
using Tasks.Model;
using Tasks.Ports;

namespace Tasks.Tests.TaskMailer
{
    [TestFixture]
    public class TaskMailerTests
    {
        private MimeMessage s_mailMessage;
        private TaskReminder s_taskReminder;
        private EmailAddress s_recipient;
        private EmailAddress s_copyTo;
        private TaskName s_taskName;
        private DateTime s_dueDate;

        [SetUp]
        public void Establish ()
        {
            s_dueDate = DateTime.UtcNow.AddDays(1);
            s_recipient = new EmailAddress("ian.hammond.cooper@gmail.com");
            s_copyTo = new EmailAddress("ian@huddle.net");
            s_taskName = new TaskName("My Task");

            s_taskReminder = new TaskReminder(
                taskName: new TaskName(s_taskName),
                dueDate: s_dueDate,
                reminderTo: s_recipient,
                copyReminderTo: s_copyTo);
        }

        [Test]
        public void When_marshalling_a_task_reminder_to_a_mailkit_mail()
        {
            s_mailMessage = new MailTranslator().Translate(s_taskReminder);

            //_should_have_the_correct_subject
            Assert.AreEqual(string.Format("Task Reminder! Task {0} is due on {1}", s_taskName, s_dueDate), s_mailMessage.Subject);
            //_should_have_the_correct_recipient_addressee
            Assert.AreEqual((string) s_recipient, s_mailMessage.To[0].Name);
            //_should_have_the_correct_to_address
            Assert.AreEqual((string) s_copyTo, s_mailMessage.Cc[0].Name);
        }
    }
}
