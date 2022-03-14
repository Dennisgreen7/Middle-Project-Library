using LibraryEntities.Models;

namespace LibraryDb
{
    public interface ILoginClass
    {
        void Dispose();
        User GetUser(string username);
        string UserLogin(string username, string password);
    }
}