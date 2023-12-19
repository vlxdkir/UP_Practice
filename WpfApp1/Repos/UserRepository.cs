using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using System.Security.Permissions;

namespace WpfApp1.Repos
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where Username=@Username and [Password]=@Password";
                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void CreateUser(string login, string password, int accessLevel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into [User] (Username , Password, AccessLevel) " + "values (@Username, @Password, @AccessLevel)";
                command.Parameters.AddWithValue("@Username", login);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@AccessLevel", accessLevel);
                command.ExecuteNonQuery();

            }
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }
        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from [User] where username=@username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader[0].ToString(),
                            Username = reader[1].ToString(),
                            Password = string.Empty,
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                            AccessLevel = Convert.ToInt32(reader["AccessLevel"])

                        };
                    }
                }
            }
            return user;
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT * FROM [User]", connection))
            {
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new UserModel
                        {
                            Id = reader["Id"] == DBNull.Value ? null : reader["Id"].ToString(),
                            Username = reader["Username"] == DBNull.Value ? null : reader["Username"].ToString(),
                            Password = reader["Password"] == DBNull.Value ? null : reader["Password"].ToString(),
                            Name = reader["Name"] == DBNull.Value ? null : reader["Name"].ToString(),
                            LastName = reader["Lastname"] == DBNull.Value ? null : reader["Lastname"].ToString(),
                            Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                            AccessLevel = reader["AccessLevel"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AccessLevel"])
                        };
                        users.Add(user);
                    }
                }
            }
            
            return users;
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
