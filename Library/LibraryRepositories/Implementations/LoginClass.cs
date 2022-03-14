using LibraryEntities.Models;
namespace LibraryDb
{
    public class LoginClass : IDisposable, ILoginClass
    {
        private LibraryContext db;
        public LoginClass()
        {
            db = new LibraryContext();
        }
        public string UserLogin(string username, string password)
        {
            try
            {
                var userQuery = db.Users.Where(u => u.UsersUserName == username).SingleOrDefault();
                if (userQuery != null)
                {
                    if (userQuery.UsersPassword == password)
                    {
                        return "";
                    }
                }
                return "Invalid, Username or Password";
            }
            catch 
            {
                return "Invalid, Username or Password";
            }
        }
        public User GetUser(string username)
        {
            try
            {
                User user = new User();
                user = db.Users.Where(u => u.UsersUserName == username).SingleOrDefault();
                return user;
            }
            catch
            {
                return null;
            }
        }
        public void Dispose()
        {
            try
            {

                db.Dispose();
            }
            catch
            {
                return;
            }
        }
    }
}

