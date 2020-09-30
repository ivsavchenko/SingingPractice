using System;
using System.Collections.Generic;
using System.Text;
using SingingPractice.SignatureGenerator.Common.Contracts.Managers;
using SingingPractice.SignatureGenerator.Common.Contracts.Services;

namespace SingingPractice.SignatureGenerator.Logic.Managers
{
    public class RegistrationManager : IRegistrationManager
    {
        private readonly INotificationSender notificationSender;

        public RegistrationManager(INotificationSender notificationSender)
        {
            this.notificationSender = notificationSender;
        }
    }
}
