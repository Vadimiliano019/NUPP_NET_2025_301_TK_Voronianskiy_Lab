namespace Library.Common;

public class Borrowing
{
    public Guid Id { get; set; }
    public Member Borrower { get; set; }
    public Book Book { get; set; }
    public DateTime BorrowedAt { get; set; }
}
