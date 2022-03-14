using System.Linq.Expressions;
using LibraryEntities.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDb
{
    public class BorrowRepository : IDisposable, IBorrowRepository
    {
        private LibraryContext Context;
        public BorrowRepository(LibraryContext Context)
        {
            this.Context = Context;

        }
        public void Add(Borrowing entity)
        {
            try
            {
                Context.Borrowings.Add(entity);
                var book = FindBook(b => b.BookId == entity.BorrowingBookId);
                if (book != null)
                {
                    book.BookCopys -= 1;
                }
                Save();
            }
            catch 
            {
                return;
            }
        }

        public List<Borrowing> ClientFillterBorrow(int selectedFilter, string value, User entity)
        {
            try
            {
                List<Borrowing> borrowings = null;
                switch (selectedFilter)
                {
                    case 0:
                        borrowings = GetDBClientBorrowings(entity).Where(b => b.BookName == value).ToList<Borrowing>();
                        break;
                    case 1:
                        borrowings = GetDBClientBorrowings(entity).Where(b => b.BorrowingReturnedDate == null).ToList<Borrowing>();
                        break;
                    case 2:
                        borrowings = GetDBClientBorrowings(entity).Where(b => b.BorrowingReturnedDate != null).ToList<Borrowing>();
                        break;
                    case 3:
                        borrowings = GetDBClientBorrowings(entity).Where(b => b.BorrowingDate == Convert.ToDateTime(value)).ToList<Borrowing>();
                        break;
                    case 4:
                        borrowings = GetDBClientBorrowings(entity).Where(b => b.BorrowingReturnDate == Convert.ToDateTime(value)).ToList<Borrowing>();
                        break;
                }
                return borrowings;
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
                Context.Dispose();
            }
            catch
            {
                return;
            }
        }

        public List<Borrowing> Fillter(int selectedFilter, string value)
        {
            try
            {
                List<Borrowing> borrowings = null;
                switch (selectedFilter)
                {
                    case 0:
                        borrowings = Find(b => b.BorrowingId == int.Parse(value));
                        break;
                    case 1:
                        borrowings = GetAll().Where(b => b.UserName == value).ToList<Borrowing>();
                        break;
                    case 2:
                        borrowings = GetAll().Where(b => b.BookName == value).ToList<Borrowing>();
                        break;
                    case 3:
                        borrowings = Find(b => b.BorrowingReturnedDate == null);
                        break;
                    case 4:
                        borrowings = Find(b => b.BorrowingReturnedDate != null);
                        break;
                    case 5:
                        borrowings = Find(b => b.BorrowingDate == Convert.ToDateTime(value));
                        break;
                    case 6:
                        borrowings = Find(b => b.BorrowingReturnDate == Convert.ToDateTime(value));
                        break;
                }
                return borrowings;
            }
            catch
            {
                return null;
            }
        }
        public List<Borrowing> Find(Expression<Func<Borrowing, bool>> predicate)
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

        public Book FindBook(Expression<Func<Book, bool>> predicate)
        {
            try
            {
                return Context.Set<Book>().Where(predicate).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Borrowing> GetAll()
        {
            try
            {
                return Context.Borrowings.Include(b => b.BorrowingUser).Include(b => b.BorrowingBook).ToList<Borrowing>();
            }
            catch
            {
                return null;
            }
        }

        public Borrowing GetById(int id)
        {
            try
            {
                if (Context.Borrowings.Find(id) != null)
                {
                    return Context.Borrowings.Find(id);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public List<Borrowing> GetDBClientBorrowings(User entity)
        {
            try
            {
                return Context.Borrowings.Where(b => b.BorrowingUserId == entity.UsersId).Include(b => b.BorrowingUser).Include(b => b.BorrowingBook).ToList<Borrowing>();
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
                Borrowing entity = Context.Borrowings.Find(id);
                if (entity != null)
                {
                    Context.Borrowings.Remove(entity);
                    Save();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool ReturnBook(string id)
        {
            try
            {
                var borrow = GetById(int.Parse(id));
                var book = FindBook(b => b.BookId == borrow.BorrowingBookId);
                if (borrow.BorrowingReturnedDate != null)
                {
                    return false;
                }
                if (borrow != null && book != null)
                {
                    borrow.BorrowingReturnedDate = DateTime.Now.Date;
                    book.BookCopys += 1;
                }
                Save();
                return true;
            }
            catch
            {
                return false;
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
        public void Update(Borrowing entity)
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

        //  במקרה שישנו את הספר בהשאלה וההשאלה פתוחה צריך להחזיר עותק לספר הישן ולהוריד לספר החדש ליפני עדכון ההשאלה
        public void bookChek(Borrowing entity, bool IsSame, string newBookId)
        {
            try
            {
                if (entity.BorrowingReturnedDate == null && !IsSame)
                {
                    Book Newook = FindBook(b => b.BookId == int.Parse(newBookId));
                    Newook.BookCopys -= 1;
                    Book oldBook = FindBook(b => b.BookId == entity.BorrowingBookId);
                    oldBook.BookCopys += 1;
                }
                Save();
            }
            catch
            {
                return;
            }
        }

    }
}
