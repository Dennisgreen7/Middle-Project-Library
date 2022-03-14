using System.Linq.Expressions;
using LibraryEntities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDb
{
    public class UserRepository:IDisposable,IUserRepository
    {
        private LibraryContext Context;
        public UserRepository(LibraryContext Context)
        {
            this.Context = Context;
        }
        public void Add(User entity)
        {
            try
            {
                Context.Users.Add(entity);
                Save();
            }
            catch 
            {
                return;
            }
        }
        public void Dispose()
        {
            try
            {
                Context.Dispose();
            }
            catch
            {
                return;
            }
        }

        public List<User> Fillter(int selectedFilter, string value)
        {
            try
            {
                List<User> users = null;
                switch (selectedFilter)
                {
                    case 0:
                        users = Find(u => u.UsersId == int.Parse(value));
                        break;
                    case 1:
                        users = Find(u => u.UsersFirstName == value);
                        break;
                    case 2:
                        users = Find(u => u.UsersLastName == value);
                        break;
                    case 3:
                        users = Find(u => u.UsersUserName == value);
                        break;
                    case 4:
                        users = Find(u => u.UsersRole == value);
                        break;
                    case 5:
                        users = Find(u => u.UserTzId == value);
                        break;
                }
                return users;
            }
            catch
            {
                return null;
            }
        }

        public List<User> Find(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return Context.Set<User>().Where(predicate).ToList<User>();
            }
            catch
            {
                return null;
            }
        }
        public Borrowing FindBorrow(Expression<Func<Borrowing, bool>> predicate)
        {
            try
            {
                return Context.Set<Borrowing>().Where(predicate).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Borrowing> FindBorrows(Expression<Func<Borrowing, bool>> predicate)
        {
            try
            {
                return Context.Set<Borrowing>().Where(predicate).ToList<Borrowing>();
            }
            catch
            {
                return null;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return Context.Users.ToList<User>();
            }
            catch
            {
                return null;
            }
        }

        public User GetById(int id)
        {
            try
            {
                if (Context.Users.Find(id) != null)
                {
                    return Context.Users.Find(id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var openBorrowing = FindBorrow(b => b.BorrowingUserId == id && b.BorrowingReturnedDate == null);
                if (openBorrowing != null)
                {
                    return false;
                }
                var borrows = FindBorrows(b => b.BorrowingUserId == id);
                User entity = Context.Users.Find(id);
                if (borrows != null && entity != null)
                {
                    for (int i = 0; i < borrows.Count; i++)
                    {
                        Context.Borrowings.Remove(borrows[i]);
                    }
                    Context.Users.Remove(entity);
                    Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void RemoveBorrow(Borrowing entity)
        {
            try
            {
                Context.Borrowings.Remove(entity);
            }
            catch
            {
                return;
            }
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch
            {
                return;
            }
        }

        public void Update(User entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                Save();
            }
            catch
            {
                return;
            }
        }
    }
}
