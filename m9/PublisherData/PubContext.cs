﻿using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using Microsoft.Extensions.Logging;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer( 
              "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase9"
            ).LogTo(Console.WriteLine,
                    new[] {DbLoggerCategory.Database.Command.Name},
                    LogLevel.Information)
            .EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Rhoda", LastName = "Lerman" });
            var authorList = new Author[]{
                new Author {AuthorId = 2, FirstName = "Ruth", LastName = "Ozeki" },
                new Author {AuthorId = 3, FirstName = "Sofia", LastName = "Segovia" },
                new Author {AuthorId = 4, FirstName = "Ursula K.", LastName = "LeGuin" },
                new Author {AuthorId = 5, FirstName = "Hugh", LastName = "Howey" },
                new Author {AuthorId = 6, FirstName = "Isabelle", LastName = "Allende" }
            };
            modelBuilder.Entity<Author>().HasData(authorList);

            var someBooks = new Book[]{
                new Book {BookId = 1, AuthorId=1, Title = "In God's Ear",
                    PublishDate= new DateTime(1989,3,1) },
                new Book {BookId = 2, AuthorId=2, Title = "A Tale For the Time Being",
                PublishDate = new DateTime(2013,12,31) },
                new Book {BookId = 3, AuthorId=3, Title = "The Left Hand of Darkness",
                PublishDate=(DateTime)new DateTime(1969,3,1)} };
            modelBuilder.Entity<Book>().HasData(someBooks);

            var someArtists = new Artist[]{
                new Artist {ArtistId = 1, FirstName = "Pablo", LastName="Picasso"},
                new Artist {ArtistId = 2, FirstName = "Dee", LastName="Bell"},
                new Artist {ArtistId = 3, FirstName ="Katharine", LastName="Kuharic"} };
            modelBuilder.Entity<Artist>().HasData(someArtists);
            var someCovers = new Cover[]{
                new Cover {CoverId = 1, DesignIdeas="How about a left hand in the dark?", DigitalOnly=false},
                new Cover {CoverId = 2, DesignIdeas= "Should we put a clock?", DigitalOnly=true},
                new Cover {CoverId = 3, DesignIdeas="A big ear in the clouds?", DigitalOnly = false}};
            modelBuilder.Entity<Cover>().HasData(someCovers);
            //example of mapping an unconventional FK
            //since I have the author prop in books, I am
            //using it in WithOne:
            //modelBuilder.Entity<Author>()
            //   .HasMany(a => a.Books)
            //   .WithOne(b => b.Author)
            //   .HasForeignKey(b=>b.AuthorId).IsRequired(false);


            //example of a more advanced mapping to specify
            //a one to many between author and book when 
            //there are no navigation properties:
            //modelBuilder.Entity<Author>()
            //    .HasMany<Book>()
            //    .WithOne();
        }

    }
}