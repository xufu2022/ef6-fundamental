# Ef6 fundamental

## tips m3

-   no track queries and dbcontext

    var author=context.authors.AsNoTracking() // return query, not dbset

    optionsBuilder.useSqlServer(myconn).UseQueryTrackingBehavior.QueryTrackingBehavior.NoTracking);  // all queries for this dbcontext will default to no tracking
        use Dbset.AsTracking() for special queries that need to be tracked

        