using Assignment.Models.Requests;
using Assignment.Repositories;
using Assignment.Services;
using System.Text.Json;


namespace Assignment.Helpers
{


public static class DemoRunner
    {
        public static void RunApiDemo(
            ILibraryApiService service,
            ILibraryRepository repository)
        {
            Console.WriteLine("===== DEMO START =====");

            // STEP 1
            var member =
                service.RegisterMember(
                new RegisterMemberRequest
                {
                    FirstName = "Nik",
                    LastName = "Chelimela",
                    Email = "nani1@test.com",
                    MembershipType = "Premium"
                });

            Console.WriteLine(
                "Register Member Response");

            Console.WriteLine(
                JsonSerializer.Serialize(
                    member,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }));


            // STEP 2

            var books =
                service.SearchBooks(
                new SearchBooksRequest
                {
                    Genre = "Fiction",
                    Page = 1,
                    PageSize = 10
                });

            Console.WriteLine();
            Console.WriteLine("First 3 Fiction Books");

            foreach (var book in books.Data.Items.Take(3))
            {
                Console.WriteLine(book.Title);
            }


            // STEP 3

            var loan =
                service.CheckoutBook(
                new CheckoutBookRequest
                {
                    MemberId = member.Data.MemberId,
                    ISBN = "978000001",
                    DueDate = DateTime.Now.AddDays(7)
                });

            Console.WriteLine();
            Console.WriteLine("Checkout Response");

            Console.WriteLine(
                JsonSerializer.Serialize(
                    loan,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }));


            // STEP 4

            var duplicateLoan =
                service.CheckoutBook(
                new CheckoutBookRequest
                {
                    MemberId = member.Data.MemberId,
                    ISBN = "978000001",
                    DueDate = DateTime.Now.AddDays(7)
                });

            Console.WriteLine();
            Console.WriteLine("Duplicate Checkout Validation");

            if (!duplicateLoan.Success)
            {
                Console.WriteLine(
                    duplicateLoan.Errors[0].Message);
            }


            // STEP 5

            // STEP 5

            var returnResult =
                service.ReturnBook(
                new ReturnBookRequest
                {
                    LoanId = loan.Data.LoanId,
                    MemberId = member.Data.MemberId,
                    ReturnCondition = "Good"
                });

            Console.WriteLine();
            Console.WriteLine("Return Response");

            Console.WriteLine(
                JsonSerializer.Serialize(
                    returnResult,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }));


            // STEP 6

            var payFineResponse =
                service.PayFine(
                    member.Data.MemberId,
                    50);

            Console.WriteLine();
            Console.WriteLine("Pay Fine Response");

            Console.WriteLine(
                JsonSerializer.Serialize(
                    payFineResponse,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }));

            var updatedMember =
                repository.GetMemberById(
                    member.Data.MemberId);

            Console.WriteLine();
            Console.WriteLine(
                $"Updated Outstanding Balance : ₹{updatedMember.OutstandingFine}");

            Console.WriteLine();
            Console.WriteLine("===== DEMO END =====");
        }
    }
}