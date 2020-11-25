using LocalOfferts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalOfferts.Service
{
    public interface IRegistrationService
    {
        Task<bool> CreateRegistration(Registration registration);
        Task<IEnumerable<Registration>> GetRegistrationList();
    }
}