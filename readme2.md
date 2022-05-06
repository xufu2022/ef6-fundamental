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



