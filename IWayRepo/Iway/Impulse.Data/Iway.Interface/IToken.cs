using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Iway.Interface
{
    public interface IToken
    {
        Task<string> GetToken();
    }
}
