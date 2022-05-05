
using PublisherData;
using PublisherDomain;

using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

PubContext _context = new PubContext(); //existing database

InsertNewAuthorWithNewBook();

void InsertNewAuthorWithNewBook()
{
    var author = new Author { FirstName = "Lynda", LastName = "Rutledge" };
    author.Books.Add(new Book
    {
        Title = "West With Giraffes",
        PublishDate = new DateTime(2021, 2, 1)
    });
    _context.Authors.Add(author);
    _context.SaveChanges();
}

void AddNewBookToExistingAuthorInMemory()
{
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
    if (author != null)
    {
        author.Books.Add(
          new Book { Title = "Wool", PublishDate = new DateTime(2012, 1, 1) }
          );
    }
    _context.SaveChanges();
}
