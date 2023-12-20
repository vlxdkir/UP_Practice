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
        void DeleteUsername(int id);
        void CreateUser(string login, string password, int accesslevel);
        UserModel GetById(int id);
        UserModel GetByUsername(string username);
        void EditUser(string newusername, string newpassword, string newemail, string newname, string newlastname, int newaccesslevel);

        List<UserModel> GetAllUsers();
        IEnumerable<UserModel> GetByAll();
        //...
    }
}
