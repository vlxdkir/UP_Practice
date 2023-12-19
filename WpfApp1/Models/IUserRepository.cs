using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Edit(UserModel userModel);
        void Remove(int id);
        void CreateUser(string login, string password, int accesslevel);
        UserModel GetById(int id);
        UserModel GetByUsername(string username);

        List<UserModel> GetAllUsers();
        IEnumerable<UserModel> GetByAll();
        //...
    }
}
