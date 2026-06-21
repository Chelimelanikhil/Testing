using Assignment.Helpers;
using Assignment.Models.Requests;
using Assignment.Models.Responses;


namespace Assignment.Services
{
    public interface ILibraryApiService
    {
        // POST
        // Register Member
        ApiResponse<MemberResponse>
        RegisterMember(RegisterMemberRequest request);

        // POST
        // Checkout Book
        ApiResponse<LoanResponse>
        CheckoutBook(CheckoutBookRequest request);

        // PUT
        // Return Book
        ApiResponse<LoanResponse>
        ReturnBook(ReturnBookRequest request);

        // GET
        // Search Books
        ApiResponse<PagedResponse<BookResponse>>
        SearchBooks(SearchBooksRequest request);

        // GET
        // Member Loans
        ApiResponse<PagedResponse<LoanResponse>>
 GetMemberLoans(
     int memberId,
     bool activeOnly,
     int page,
     int pageSize);

        // PUT
        // Pay Fine
        ApiResponse<bool>
        PayFine(int memberId, decimal amount);
    }
}