# Defining and using many-many relationship

-   Three ways to define Many-to-Many
    -   Skip navigation : most common, direct refs from both ends
    -   Skip with Payload: allow database-generated data in extra columns
    -   Explicit Join Class: Addition properties accessible via code

Add missing index in link table in generated files
    note serizable issue

    //example of mapping skip navigation with payload -- no need for dataset
    `charp
    modelBuilder.Entity<Artist>()
        .HasMany(a => a.Covers)
        .WithMany(c => c.Artists)
        .UsingEntity<CoverAssignment>(
           join => join
            .HasOne<Cover>()
            .WithMany()
            .HasForeignKey(ca => ca.CoverId),
           join => join
            .HasOne<Artist>()
            .WithMany()
            .HasForeignKey(ca => ca.ArtistId));
    modelBuilder.Entity<CoverAssignment>()
                .Property(ca => ca.DateCreated).HasDefaultValueSql("GetDate()");
    modelBuilder.Entity<CoverAssignment>()
                 .Property(ca => ca.CoverId).HasColumnName("CoversCoverId");
    modelBuilder.Entity<CoverAssignment>()
                 .Property(ca => ca.ArtistId).HasColumnName("ArtistsArtistId");

# m10 one to one

Dbcontext must be able to identify a principal ('parent') and a dependent ('child')

-   Navigations on both ends with FK independent
-   Navigation on one end, FK on the other
-   Navigation on both ends, requires a mapping to define principal/dependent

Principals are required by default

remove dependent _context.remove(obj);

# m11 sp & view

Retrieved entities without relying on Linq or ef core's generated sql

-   FromSqlRaw
-   FromSqlInterpolated --avoid sql injection

EF core will track the query and expect the result to be in the shape of db entity

    var authors = _context.Authors
        .FromSqlRaw("AuthorsPublishedinYearRange {0}, {1}", 2010, 2015)
        .include(a=>a.Books)
        .ToList();

    .FromSqlInterpolated($"AuthorsPublishedinYearRange {start}, {end}")


    var authors = _context.Authors.FromSqlRaw(sql)
        .OrderBy(a => a.LastName).TagWith("Formatted_Unsafe").ToList(); 
        // tagwith will add comment attched to the  command 

**FromSqlInterpolated** expect one formatted string as parameter, will not accept string

-   safe
        .FromSqlRaw("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
        .FromSqlInterpolated($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")
        .FromSqlInterpolated
            ("SELECT * FROM authors WHERE lastname LIKE '{0}%'", lastnameStart)
-   unsafe
        string sql = $"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'";
        var authors = _context.Authors.FromSqlRaw(sql)

        .FromSqlRaw($"SELECT * FROM authors WHERE lastname LIKE '{lastnameStart}%'")

-   Raw Sql method Rules apply to Sprocs, too
    -   Sql cannot return shaped data (use include() to do that)
    -   Schema of result must match the entity of the Dbset
    -   column names of result must match property name of entity

 -  Ef core cannot capture random data, anonymous types, from raw sql

  ## Use keyless entites to map views

  Keyless entities != Non-Tracking Queries

-   No-Tracking Query
    - Entity has a key prop
    - No-Tracking is optional
    - Maps to tables with PK

-   Mixed Use
    - Entity has a key prop
    - Maps to view and table
    - Query from the view

        Update to the table

Keyless entities are always **read-only**

        modelBuilder.Entity<AuthorByArtist>().HasNoKey()
        .ToView(nameof(AuthorsByArtist));
migrations will not attempt to create a database view that is mapped in a dbcontext
Keyless entities will never be tracked, and find method won't work

    var rowCount = _context.Database.ExecuteSqlRaw("DeleteCover {0}", coverId);

    //ExecuteSql will  return number of rows been affected

## Commands to execute Raw Sql and Storedprocedure

    DbContext.Database.ExecuteSqlRaw
    DbContext.Database.ExecuteSqlRawAsync
    DbContext.Database.ExecuteSqlInterpolated
    DbContext.Database.ExecuteSqlInterpolatedAsync


# m14. AspnetCore

builder.Services.AddControllers()
.AddJsonOptions(opt =>
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



