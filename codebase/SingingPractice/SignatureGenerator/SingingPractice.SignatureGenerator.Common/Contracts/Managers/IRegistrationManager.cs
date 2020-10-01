using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SingingPractice.SignatureGenerator.Common.Contracts.Managers
{
    public interface IRegistrationManager
    {
        Task RegisterAsync(string json);
    }
}
