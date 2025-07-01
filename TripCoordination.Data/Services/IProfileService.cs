using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Services
{
    // IProfileService.cs
    public interface IProfileService
    {
        Task<bool> HasProfileAsync(string userID);
    }
}
