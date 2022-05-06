using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext(); //existing database

//ConnectExistingArtistAndCoverObjects();
void ConnectExistingArtistAndCoverObjects()
{
    var artistA = _context.Artists.Find(1);
    var artistB = _context.Artists.Find(2);
    var coverA = _context.Covers.Find(1);
    coverA.Artists.Add(artistA);
    coverA.Artists.Add(artistB);
    _context.SaveChanges();
}

//CreateNewCoverWithExistingArtist();
void CreateNewCoverWithExistingArtist()
{
    var artistA = _context.Artists.Find(1);
    var cover = new Cover { DesignIdeas = "Author has provided a photo" };
    cover.Artists.Add(artistA);
    _context.Covers.Add(cover);
    _context.SaveChanges();
}

//CreateNewCoverAndArtistTogether();
void CreateNewCoverAndArtistTogether()
{
    var newArtist = new Artist { FirstName = "Kir", LastName = "Talmage" };
    var newCover = new Cover { DesignIdeas = "We like birds!" };
    newArtist.Covers.Add(newCover);
    _context.Artists.Add(newArtist);
    _context.SaveChanges();
}

//RetrieveAnArtistWithTheirCovers();
void RetrieveAnArtistWithTheirCovers()
{
    var artistWithCovers = _context.Artists.Include(a => a.Covers)
                            .FirstOrDefault(a => a.ArtistId == 1);
}

//RetrieveAllArtistsWhoHaveCovers();

void RetrieveAllArtistsWhoHaveCovers()
{
    var artistsWithCovers = _context.Artists.Where(a => a.Covers.Any()).ToList();
}

UnAssignAnArtistFromACover();
void UnAssignAnArtistFromACover()
{
    var coverwithartist = _context.Covers
        .Include(c => c.Artists.Where(a => a.ArtistId == 2))
        .FirstOrDefault(c => c.CoverId == 1);
    //coverwithartist.Artists.RemoveAt(0);
    _context.Artists.Remove(coverwithartist.Artists[0]);
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
    //_context.SaveChanges();
}