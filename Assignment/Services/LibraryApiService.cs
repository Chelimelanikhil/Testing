using Assignment.Helpers;
using Assignment.Models.Entities;
using Assignment.Models.Requests;
using Assignment.Models.Responses;
using Assignment.Repositories;

namespace Assignment.Services
{
    public class LibraryApiService : ILibraryApiService
    {
        private readonly ILibraryRepository _repository;

        public LibraryApiService(
            ILibraryRepository repository)
        {
            _repository = repository;
        }
        public ApiResponse<MemberResponse>
RegisterMember(RegisterMemberRequest request)
        {
            var member = new Member
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.Email,
                MembershipType = request.MembershipType
            };

            int memberId =
                _repository.RegisterMember(member);

            return new ApiResponse<MemberResponse>
            {
                Success = true,
                Data = new MemberResponse
                {
                    MemberId = memberId,
                    FullName =
                        $"{request.FirstName} {request.LastName}",
                    Email = request.Email,
                    MembershipType =
                        request.MembershipType,
                    OutstandingFine = 0,
                    ActiveLoans = 0
                }
            };
        }
        public ApiResponse<LoanResponse>
CheckoutBook(
CheckoutBookRequest request)
        {
            var member =
                _repository.GetMemberById(
                    request.MemberId);

            if (member == null)
            {
                return ValidationError(
                    "Member not found");
            }

            if (member.OutstandingFine > 500)
            {
                return ValidationError(
                    "Outstanding fine exceeds Rs.500");
            }

            var book =
                _repository.GetBookByISBN(
                    request.ISBN);

            if (book == null)
            {
                return ValidationError(
                    "Book not found");
            }

            if (book.AvailableCopies == 0)
            {
                return ValidationError(
                    "No copies available");
            }

            var existingLoan =
                _repository.GetActiveLoan(
                    request.MemberId,
                    book.BookId);

            if (existingLoan != null)
            {
                return ValidationError(
                    "Book already checked out");
            }

            var loan = new BookLoan
            {
                BookId = book.BookId,
                MemberId = request.MemberId,
                CheckoutDate = DateTime.Now,
                DueDate = request.DueDate
            };

            int loanId =
                _repository.CreateLoan(loan);

            book.AvailableCopies--;

            _repository.UpdateBook(book);

            return new ApiResponse<LoanResponse>
            {
                Success = true,
                Data = new LoanResponse
                {
                    LoanId = loanId,
                    BookTitle = book.Title,
                    MemberName =
                        member.FirstName +
                        " " +
                        member.LastName,
                    CheckoutDate = loan.CheckoutDate,
                    DueDate = loan.DueDate,
                    IsOverdue = false,
                    OverdueFine = 0
                }
            };
        }
        private ApiResponse<LoanResponse>
ValidationError(string message)
        {
            return new ApiResponse<LoanResponse>
            {
                Success = false,
                Errors =
        {
            new ApiError
            {
                Code = "VALIDATION",
                Message = message
            }
        }
            };
        }
        public ApiResponse<LoanResponse>
ReturnBook(ReturnBookRequest request)
        {
            var loan =
                _repository.GetLoanById(
                    request.LoanId);

            if (loan == null)
            {
                return ValidationError(
                    "Loan not found");
            }

            var member =
                _repository.GetMemberById(
                    request.MemberId);

            var book =
                _repository.GetBookById(
                    loan.BookId);

            decimal fine = 0;

            if (DateTime.Now.Date >
                loan.DueDate.Date)
            {
                int daysLate =
                    (DateTime.Now.Date
                    - loan.DueDate.Date).Days;

                fine = daysLate * 5;
            }

            loan.ReturnDate = DateTime.Now;
            loan.OverdueFine = fine;
            loan.LoanStatus = "Returned";

            _repository.UpdateLoan(loan);

            book.AvailableCopies++;

            _repository.UpdateBook(book);

            member.OutstandingFine += fine;

            _repository.UpdateMember(member);

            return new ApiResponse<LoanResponse>
            {
                Success = true,
                Data = new LoanResponse
                {
                    LoanId = loan.LoanId,
                    BookTitle = book.Title,
                    MemberName =
                        member.FirstName + " " +
                        member.LastName,
                    CheckoutDate = loan.CheckoutDate,
                    DueDate = loan.DueDate,
                    IsOverdue = fine > 0,
                    OverdueFine = fine
                }
            };
        }
        public ApiResponse<PagedResponse<BookResponse>>
SearchBooks(SearchBooksRequest request)
        {
            if (request.PageSize > 50)
            {
                request.PageSize = 50;
            }

            var books =
                _repository.SearchBooks(
                    request.Title,
                    request.Author,
                    request.Genre)
                    .ToList();

            int totalCount = books.Count;

            var pagedBooks =
                books
                .Skip((request.Page - 1)
                * request.PageSize)
                .Take(request.PageSize)
                .Select(book =>
                    new BookResponse
                    {
                        BookId = book.BookId,
                        ISBN = book.ISBN,
                        Title = book.Title,
                        Author = book.Author,
                        AvailableCopies =
                            book.AvailableCopies,
                        IsAvailable =
                            book.AvailableCopies > 0
                    })
                .ToList();

            return new ApiResponse
                <PagedResponse<BookResponse>>
            {
                Success = true,
                Data =
                    new PagedResponse<BookResponse>
                    {
                        Items = pagedBooks,
                        TotalCount = totalCount,
                        Page = request.Page,
                        PageSize = request.PageSize,
                        TotalPages =
                            (int)Math.Ceiling(
                                totalCount /
                                (double)request.PageSize)
                    }
            };
        }
        public ApiResponse<PagedResponse<LoanResponse>>
 GetMemberLoans(
     int memberId,
     bool activeOnly,
     int page,
     int pageSize)
        {
            var loans =
                _repository.GetMemberLoans(
                    memberId,
                    activeOnly);

            var totalCount = loans.Count;

            var pagedLoans =
                loans
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new LoanResponse
                {
                    LoanId = x.LoanId,
                    CheckoutDate = x.CheckoutDate,
                    DueDate = x.DueDate,
                    IsOverdue = x.OverdueFine > 0,
                    OverdueFine = x.OverdueFine
                })
                .ToList();

            return new ApiResponse<PagedResponse<LoanResponse>>
            {
                Success = true,
                Data = new PagedResponse<LoanResponse>
                {
                    Items = pagedLoans,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages =
                        (int)Math.Ceiling(
                            totalCount / (double)pageSize)
                }
            };
        }
        public ApiResponse<bool>
PayFine(
int memberId,
decimal amount)
        {
            var member =
                _repository.GetMemberById(
                    memberId);

            if (member == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Errors =
            {
                new ApiError
                {
                    Code="VALIDATION",
                    Message="Member not found"
                }
            }
                };
            }

            member.OutstandingFine -= amount;

            if (member.OutstandingFine < 0)
            {
                member.OutstandingFine = 0;
            }

            _repository.UpdateMember(member);

            return new ApiResponse<bool>
            {
                Success = true,
                Data = true
            };
        }
    }
}
