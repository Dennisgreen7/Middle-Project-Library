using LibraryEntities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryDb
{
    public class GenreRepository : IDisposable, IGenreRepository
    {
        private LibraryContext Context;
        public GenreRepository(LibraryContext Context)
        {
            this.Context = Context;
        }
        public void Add(Genre entity)
        {
            try
            {
                Context.Genres.Add(entity);
                Save();
            }
            catch 
            {
                return;
            }
        }
        public List<Genre> Fillter(int selectedFilter, string value)
        {
            try
            {
                List<Genre> genres = null;
                switch (selectedFilter)
                {
                    case 0:
                        genres = Find(g => g.GenreId == int.Parse(value));
                        break;
                    case 1:
                        genres = Find(g => g.GenreName == value);
                        break;
                }
                return genres;
            }
            catch
            {
                return null;
            }
        }

        public List<Genre> Find(Expression<Func<Genre, bool>> predicate)
        {
            try
            {
                return Context.Set<Genre>().Where(predicate).ToList<Genre>();
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
        public List<Genre> GetAll()
        {
            try
            {
                return Context.Genres.ToList<Genre>();
            }
            catch
            {
                return null;
            }
        }

        public Genre GetById(int id)
        {
            try
            {
                if (Context.Genres.Find(id) != null)
                {
                    return Context.Genres.Find(id);
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
                var books = FindBooks(b => b.GenreId == id).ToList<Book>();
                List<Borrowing> borrows = null;
                Borrowing openBorrow;
                for (int i = 0; i < books.Count; i++)
                {
                    openBorrow = FindBorrow(b => b.BorrowingBookId == books[i].BookId && b.BorrowingReturnedDate == null);
                    if (openBorrow != null)
                    {
                        return false;
                    }
                    borrows = FindBorrows(b => b.BorrowingBookId == books[i].BookId);
                    if (borrows != null)
                    {
                        for (int j = 0; j < borrows.Count; j++)
                        {
                            RemoveBorrow(borrows[j]);
                        }
                    }
                }
                Genre entity = Context.Genres.Find(id);
                if (books != null && entity != null)
                {
                    for (int i = 0; i < books.Count; i++)
                    {
                        RemoveBook(books[i]);
                    }
                    Context.Genres.Remove(entity);
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

        public void Update(Genre entity)
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
    }
}
