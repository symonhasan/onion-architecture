using OA.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Service
{
    public interface IUserService
    {
        User GetUser(string email);
    }
}
