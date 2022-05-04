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
