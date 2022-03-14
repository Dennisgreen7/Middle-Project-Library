using LibraryEntities.Models;
using System.Linq.Expressions;

namespace LibraryDb
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        List<Borrowing> FindBorrows(Expression<Func<Borrowing, bool>> predicate);
        Borrowing FindBorrow(Expression<Func<Borrowing, bool>> predicate);
        void RemoveBorrow(Borrowing entity);
        List<Book> ClientFillterBook(int selectedfillter, string value);
        List<Book> GetBooksCmbDB();
    }
}
