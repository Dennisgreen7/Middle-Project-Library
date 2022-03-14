using LibraryEntities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryDb
{
    public class AuthorRepository : IDisposable, IAuthorRepository
    {
        private LibraryContext Context;
        public AuthorRepository(LibraryContext Context)
        {
            this.Context = Context;
        }

        public void Add(Author entity)
        {
            try
            {
                Context.Authors.Add(entity);
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

        public List<Author> Fillter(int selectedFilter, string value)
        {
            try
            {
                List<Author> authors = null;
                switch (selectedFilter)
                {
                    case 0:
                        authors = Find(a => a.AuthorId == int.Parse(value));
                        break;
                    case 1:
                        authors = Find(a => a.AuthorName == value);
                        break;
                    case 2:
                        authors = Find(a => a.AuthorCountry == value);
                        break;
                }
                return authors;
            }
            catch
            {
                return null;
            }
        }
        public List<Author> Find(Expression<Func<Author, bool>> predicate)
        {
            try
            {
                return Context.Set<Author>().Where(predicate).ToList<Author>();
            }
            catch
            {
                return null;
            }
        }

        public List<Book> FindBooks(Expression<Func<Book, bool>> predicate)
        {
            try
            {
                return Context.Set<Book>().Where(predicate).ToList<Book>();
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

        public List<Author> GetAll()
        {
            try
            {
                return Context.Authors.ToList<Author>();
            }
            catch
            {
                return null;
            }
        }
        public Author GetById(int id)
        {
            try
            {
                if (Context.Authors.Find(id) != null)
                {
                    return Context.Authors.Find(id);
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
                var books = FindBooks(b => b.AuthorId == id).ToList<Book>();
                List<Borrowing> borrows = null;
                Borrowing openBorrow;
                for (int i = 0; i < books.Count; i++)
                {
                    openBorrow = FindBorrow(b => b.BorrowingBookId == books[i].BookId && b.BorrowingReturnedDate == null);
                    if (openBorrow != null)
                    {
                        return false;
                    }
                    borrows = FindBorrows(b => b.BorrowingBookId == books[i].BookId).ToList<Borrowing>();
                    if (borrows != null)
                    {
                        for (int j = 0; j < borrows.Count; j++)
                        {
                            RemoveBorrow(borrows[j]);
                        }
                    }
                }
                Author entity = Context.Authors.Find(id);
                if (books != null && entity != null)
                {
                    for (int i = 0; i < books.Count; i++)
                    {
                        RemoveBook(books[i]);
                    }
                    Context.Authors.Remove(entity);
                    Save();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RemoveBook(Book entity)
        {
            try
            {
                Context.Books.Remove(entity);
            }
            catch
            {
                return;
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

        public void Update(Author entity)
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
