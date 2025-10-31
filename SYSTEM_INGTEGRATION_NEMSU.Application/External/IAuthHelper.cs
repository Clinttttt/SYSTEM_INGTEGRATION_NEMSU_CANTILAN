using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IAuthHelper
    {
        Task SetAuthHeaderAsync(HttpClient client);
    }
}
