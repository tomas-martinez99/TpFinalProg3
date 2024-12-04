using FinalProg3.Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProg3.Application.Interfaces
{
    public interface ICustomAuthenticateService
    {
        string Authenticate(AuthenticateRequest user);
    }
}
