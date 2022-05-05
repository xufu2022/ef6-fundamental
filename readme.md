# Ef6 fundamental

## tips m3

-   no track queries and dbcontext

    var author=context.authors.AsNoTracking() // return query, not dbset

    optionsBuilder.useSqlServer(myconn).UseQueryTrackingBehavior.QueryTrackingBehavior.NoTracking);  // all queries for this dbcontext will default to no tracking
        use Dbset.AsTracking() for special queries that need to be tracked

-   tracking view

        -context.ChangeTracker.DebugView.ShortView


## tip m4 skip

## migration

    Get-Help entityframework
    ef core 6 migration is orders of maguitude easier to use in source control

    Script-Migration : script very migration

    HasData seed data
    
    -   provide all parameters including keys and foreign keys
    -   hasdata will get interpreted into migrations
    -   insert will get interpreted into sql
    -   data will get inserted when migrations are executed

    Script-Migration -idempotent : scripts all migrations but checks for each object first eg, table already exists
    Script-Migration From To
        From : specifies last migration run, so start at the next one 
        To args: final one to apply

scaffold-dbcontext (connectionstring with quote) Microsoft.EntityFrameworkCore.SqlServer

## logging and sql

    optionbuilder.useSqlServer(conn).LogTo(target);
    LogTo(Console.WriteLine,new[] {DbLoggerCategory.Database.Command.Name},Microsoft.Extensions.Logging.LogLevel.Information)
    EnableSensitiveDataLogging

    override dispose method to close streamwriter

    override encoding on console (github.com/julielerman/efcoreencodingdemo)

# Eager loading Related Data in queries

    var authors = _context.Authors
        .Include(a => a.Books
                       .Where(b => b.PublishDate >= pubDateStart)
                       .OrderBy(b => b.Title))
        .ToList();
    
    var authors = _context.Authors
        .Include(a => a.Books.BookJackets).ToList() //get the jackets for each authors' books <b>but don't get the books</b>

    include defaults to a single Sql command. use AsSplitQuery() to send multiple sql commands instead

## loading related data for objects

-   load collection
    _context.Entry(author).Collection(a=>a.Books).load()
-   load reference
    _context.Entry(book).Reference(a=>a.Author).load()
-   Filter on loading
    _context.Entry(author).Collection(a=>a.Books).Query().Where(b=>b.Title.Contains("newF")).ToList()

## Lazy loading

    off by default
-   Enable Lazy loading
    -   every navigation properety in every entity must be virtual
    -   reference Microsoft.EntityFramework.Proxy package
    -   Use the proxy logic provided by the package
        optionsBuilder.UseLazyLoadingProxies()

-   Pro and Cons
    -   pros:
        -   one command sent to db to get the authors' books
    -   cons:
        -   retrieve all books objects from db and materialize them to give the count
            var bookcount=author.Books.Count()
        -   send N+1 comamnds to db as each author's book is loaded into gridview
        -   No data is retrieved (disposed context return no data )
    
## using related data to filter objects

    var authors = _context.Authors.Where(a=>a.Books.Any(b=>b.pYear.Year>2015)).ToList()

## modify related data
    _context.ChangeTracker.DetectChanges()
    var state=_context.ChangeTracker.DebugView.ShortView

-   Update method updates all the objects in a graph,
-   unless they have no key values.. those are "added"

-   attach start tracking with state set to unchanged
-   _context.Entry(object).State has more control

## Delete object from graph

-   Delete Rule: Cascade (no orphan) which enforce by EF core
    Any related data that is also tracked will be marked as Deleted along with the principal object

-   QA Cascade Deletet:
    -   remove() remove will only remove the specific object
    -   delete dependent not in graph -- Just Call DbSet.Remove method
    -   move child from one parent to another (without tracked)
        -   book.AuthorId=3
        -   new Authors.Books.Add (book)
        -   book.Author=newAuthor

    -   Option relationships
        -   book.AuthorId=null
        -   author.Books.Remove(book)








