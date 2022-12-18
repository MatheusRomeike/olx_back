using Domain.Domain.Login;
using Domain.Domain.Login.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class LoginRepository : BaseRepository<Login>, ILoginRepository
    {
    }
}
