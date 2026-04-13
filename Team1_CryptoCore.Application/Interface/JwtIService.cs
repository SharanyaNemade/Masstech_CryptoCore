using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1_CryptoCore.Application.Interface
{
    public interface JwtIService
    {
        string GenerateToken(int userId);
    }
}
