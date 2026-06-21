using Assignment.Models.Entities;

namespace Assignment.Repositories
{
    public interface ILibraryRepository
    {
        Member GetMemberById(int memberId);

        //Member GetMemberByEmail(string email);

        int RegisterMember(Member member);

        Book GetBookByISBN(string isbn);

        Book GetBookById(int bookId);

        IEnumerable<Book> SearchBooks(
            string title,
            string author,
            string genre);

        int CreateLoan(BookLoan loan);

        BookLoan GetLoanById(int loanId);

        BookLoan GetActiveLoan(
            int memberId,
            int bookId);

        List<BookLoan> GetMemberLoans(
            int memberId,
            bool activeOnly);

        void UpdateBook(Book book);

        void UpdateLoan(BookLoan loan);

        void UpdateMember(Member member);
    }
}