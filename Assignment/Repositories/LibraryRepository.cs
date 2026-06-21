using Assignment.Data;
using Assignment.Models.Entities;
using Dapper;

namespace Assignment.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly DapperContext _context;

        public LibraryRepository(DapperContext context)
        {
            _context = context;
        }
        public int RegisterMember(Member member)
        {
            string sql = @"
    INSERT INTO Members
    (
        FirstName,
        LastName,
        EmailAddress,
        MembershipType,
        OutstandingFine,
        MembershipExpiryDate,
        IsActive
    )
    VALUES
    (
        @FirstName,
        @LastName,
        @EmailAddress,
        @MembershipType,
        0,
        DATEADD(YEAR,1,GETDATE()),
        1
    );

    SELECT CAST(SCOPE_IDENTITY() as int);
    ";

            using var connection =
                _context.CreateConnection();

            return connection.QuerySingle<int>(
                sql,
                member);
        }
        public Member GetMemberById(int memberId)
        {
            string sql =
                @"SELECT * FROM Members
          WHERE MemberId=@MemberId";

            using var connection =
                _context.CreateConnection();

            return connection.QueryFirstOrDefault<Member>(
                sql,
                new { MemberId = memberId });
        }
        public Book GetBookByISBN(string isbn)
        {
            string sql =
                @"SELECT * FROM Books
          WHERE ISBN=@ISBN";

            using var connection =
                _context.CreateConnection();

            return connection.QueryFirstOrDefault<Book>(
                sql,
                new { ISBN = isbn });
        }
        public IEnumerable<Book> SearchBooks(
    string title,
    string author,
    string genre)
        {
            string sql = @"
    SELECT *
    FROM Books
    WHERE
    (@Title IS NULL OR Title LIKE '%' + @Title + '%')
    AND
    (@Author IS NULL OR Author LIKE '%' + @Author + '%')
    AND
    (@Genre IS NULL OR Genre = @Genre)
    ";

            using var connection =
                _context.CreateConnection();

            return connection.Query<Book>(
                sql,
                new
                {
                    Title = title,
                    Author = author,
                    Genre = genre
                });
        }
        public BookLoan GetActiveLoan(
    int memberId,
    int bookId)
        {
            string sql = @"
    SELECT *
    FROM BookLoans
    WHERE MemberId=@MemberId
    AND BookId=@BookId
    AND LoanStatus='Active'
    ";

            using var connection =
                _context.CreateConnection();

            return connection.QueryFirstOrDefault<BookLoan>(
                sql,
                new
                {
                    MemberId = memberId,
                    BookId = bookId
                });
        }
        public int CreateLoan(BookLoan loan)
        {
            string sql = @"
    INSERT INTO BookLoans
    (
        BookId,
        MemberId,
        CheckoutDate,
        DueDate,
        OverdueFine,
        LoanStatus
    )
    VALUES
    (
        @BookId,
        @MemberId,
        @CheckoutDate,
        @DueDate,
        0,
        'Active'
    );

    SELECT CAST(SCOPE_IDENTITY() as int);
    ";

            using var connection =
                _context.CreateConnection();

            return connection.QuerySingle<int>(
                sql,
                loan);
        }
        public BookLoan GetLoanById(int loanId)
        {
            string sql = @"
    SELECT *
    FROM BookLoans
    WHERE LoanId=@LoanId";

            using var connection =
                _context.CreateConnection();

            return connection.QueryFirstOrDefault<BookLoan>(
                sql,
                new { LoanId = loanId });
        }
        public void UpdateLoan(BookLoan loan)
        {
            string sql = @"
    UPDATE BookLoans
    SET
        ReturnDate=@ReturnDate,
        OverdueFine=@OverdueFine,
        LoanStatus=@LoanStatus
    WHERE LoanId=@LoanId";

            using var connection =
                _context.CreateConnection();

            connection.Execute(sql, loan);
        }
        public void UpdateBook(Book book)
        {
            string sql = @"
    UPDATE Books
    SET AvailableCopies=@AvailableCopies
    WHERE BookId=@BookId";

            using var connection =
                _context.CreateConnection();

            connection.Execute(sql, book);
        }
        public void UpdateMember(Member member)
        {
            string sql = @"
    UPDATE Members
    SET OutstandingFine=@OutstandingFine
    WHERE MemberId=@MemberId";

            using var connection =
                _context.CreateConnection();

            connection.Execute(sql, member);
        }
        public List<BookLoan> GetMemberLoans(
    int memberId,
    bool activeOnly)
        {
            string sql = @"
    SELECT *
    FROM BookLoans
    WHERE MemberId=@MemberId";

            if (activeOnly)
            {
                sql +=
                " AND LoanStatus='Active'";
            }

            using var connection =
                _context.CreateConnection();

            return connection.Query<BookLoan>(
                sql,
                new { MemberId = memberId })
                .ToList();
        }
        public Book GetBookById(int bookId)
        {
            string sql = @"
        SELECT *
        FROM Books
        WHERE BookId = @BookId";

            using var connection = _context.CreateConnection();

            return connection.QueryFirstOrDefault<Book>(
                sql,
                new { BookId = bookId });
        }
    }
}
