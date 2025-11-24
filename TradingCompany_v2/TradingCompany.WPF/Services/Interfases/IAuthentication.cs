using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.WPF.Services.Interfases
{
    public interface IAuthentication
    {
      public bool  ValidateUser(string login, string password);
    }
}
