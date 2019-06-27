using Impulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Impulse.Data.BLL.Interface
{
    public interface ICitizen
    {
        Task<bool> SaveCitizenDetails(Citizen itm);
        Task<Citizen> getCitizenDetails(int UserId);
    }
}
