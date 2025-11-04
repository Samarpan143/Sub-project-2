using DataServiceLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataServiceLayer;

public class ImdbContext : DbContext
{
    private readonly string _connectionString;

    public ImdbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Title> Titles { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<TitlePerson> TitlePersons { get; set; }
    public DbSet<TitleGenre> TitleGenres { get; set; }
    public DbSet<TitleRating> TitleRatings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }
    public DbSet<UserBookmark> UserBookmarks { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<PersonProfession> PersonProfessions { get; set; }
    public DbSet<TitleAka> TitleAkas { get; set; }
    public DbSet<TitleEpisode> TitleEpisodes { get; set; }
    public DbSet<UserNote> UserNotes { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Title>().ToTable("titles");
        modelBuilder.Entity<Title>().HasKey(x => x.TConst);
        modelBuilder.Entity<Title>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<Title>().Property(x => x.TitleType).HasColumnName("titletype");
        modelBuilder.Entity<Title>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
        modelBuilder.Entity<Title>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
        modelBuilder.Entity<Title>().Property(x => x.IsAdult).HasColumnName("isadult");
        modelBuilder.Entity<Title>().Property(x => x.StartYear).HasColumnName("startyear");
        modelBuilder.Entity<Title>().Property(x => x.EndYear).HasColumnName("endyear");
        modelBuilder.Entity<Title>().Property(x => x.RuntimeMinutes).HasColumnName("runtimeminutes");

        modelBuilder.Entity<Person>().ToTable("persons");
        modelBuilder.Entity<Person>().HasKey(x => x.NConst);
        modelBuilder.Entity<Person>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<Person>().Property(x => x.PrimaryName).HasColumnName("primaryname");
        modelBuilder.Entity<Person>().Property(x => x.BirthYear).HasColumnName("birthyear");
        modelBuilder.Entity<Person>().Property(x => x.DeathYear).HasColumnName("deathyear");

        modelBuilder.Entity<TitlePerson>().ToTable("title_persons");
        modelBuilder.Entity<TitlePerson>().HasKey(x => new { x.TConst, x.NConst, x.Ordering });
        modelBuilder.Entity<TitlePerson>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePerson>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePerson>().Property(x => x.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitlePerson>().Property(x => x.Category).HasColumnName("category");
        modelBuilder.Entity<TitlePerson>().Property(x => x.Job).HasColumnName("job");
        modelBuilder.Entity<TitlePerson>().Property(x => x.CharacterName).HasColumnName("charactername");

        modelBuilder.Entity<TitleGenre>().ToTable("title_genres");
        modelBuilder.Entity<TitleGenre>().HasKey(x => new { x.TConst, x.GenreName });
        modelBuilder.Entity<TitleGenre>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleGenre>().Property(x => x.GenreName).HasColumnName("genre_name");

        modelBuilder.Entity<TitleRating>().ToTable("title_ratings");
        modelBuilder.Entity<TitleRating>().HasKey(x => x.TConst);
        modelBuilder.Entity<TitleRating>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleRating>().Property(x => x.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<TitleRating>().Property(x => x.NumVotes).HasColumnName("numvotes");

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasKey(x => x.UserId);
        modelBuilder.Entity<User>().Property(x => x.UserId).HasColumnName("user_id");
        modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
        modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<User>().Property(x => x.PasswordHash).HasColumnName("password_hash");
        modelBuilder.Entity<User>().Property(x => x.CreatedAt).HasColumnName("created_at");

        modelBuilder.Entity<UserRating>().ToTable("user_ratings");
        modelBuilder.Entity<UserRating>().HasKey(x => x.RatingId);
        modelBuilder.Entity<UserRating>().Property(x => x.RatingId).HasColumnName("rating_id");
        modelBuilder.Entity<UserRating>().Property(x => x.UserId).HasColumnName("user_id");
        modelBuilder.Entity<UserRating>().Property(x => x.TitleTConst).HasColumnName("title_tconst");
        modelBuilder.Entity<UserRating>().Property(x => x.Rating).HasColumnName("rating");
        modelBuilder.Entity<UserRating>().Property(x => x.RatedAt).HasColumnName("rated_at");

        modelBuilder.Entity<UserBookmark>().ToTable("user_bookmarks");
        modelBuilder.Entity<UserBookmark>().HasKey(x => x.BookmarkId);
        modelBuilder.Entity<UserBookmark>().Property(x => x.BookmarkId).HasColumnName("bookmark_id");
        modelBuilder.Entity<UserBookmark>().Property(x => x.UserId).HasColumnName("user_id");
        modelBuilder.Entity<UserBookmark>().Property(x => x.TitleTConst).HasColumnName("title_tconst");
        modelBuilder.Entity<UserBookmark>().Property(x => x.BookmarkType).HasColumnName("bookmark_type");
        modelBuilder.Entity<UserBookmark>().Property(x => x.Folder).HasColumnName("folder");
        modelBuilder.Entity<UserBookmark>().Property(x => x.Notes).HasColumnName("notes");
        modelBuilder.Entity<UserBookmark>().Property(x => x.CreatedAt).HasColumnName("created_at");

        modelBuilder.Entity<SearchHistory>().ToTable("search_history");
        modelBuilder.Entity<SearchHistory>().HasKey(x => x.SearchId);
        modelBuilder.Entity<SearchHistory>().Property(x => x.SearchId).HasColumnName("search_id");
        modelBuilder.Entity<SearchHistory>().Property(x => x.UserId).HasColumnName("user_id");
        modelBuilder.Entity<SearchHistory>().Property(x => x.SearchQuery).HasColumnName("search_query");
        modelBuilder.Entity<SearchHistory>().Property(x => x.SearchType).HasColumnName("search_type");
        modelBuilder.Entity<SearchHistory>().Property(x => x.SearchedAt).HasColumnName("searched_at");

        modelBuilder.Entity<Profession>().ToTable("professions");
        modelBuilder.Entity<Profession>().HasKey(x => x.ProfessionName);
        modelBuilder.Entity<Profession>().Property(x => x.ProfessionName).HasColumnName("profession_name");

        modelBuilder.Entity<PersonProfession>().ToTable("person_professions");
        modelBuilder.Entity<PersonProfession>().HasKey(x => new { x.NConst, x.ProfessionName });
        modelBuilder.Entity<PersonProfession>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<PersonProfession>().Property(x => x.ProfessionName).HasColumnName("profession_name");

        modelBuilder.Entity<TitleAka>().ToTable("title_akas");
        modelBuilder.Entity<TitleAka>().HasKey(x => new { x.TitleId, x.Ordering });
        modelBuilder.Entity<TitleAka>().Property(x => x.TitleId).HasColumnName("titleid");
        modelBuilder.Entity<TitleAka>().Property(x => x.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitleAka>().Property(x => x.Title).HasColumnName("title");
        modelBuilder.Entity<TitleAka>().Property(x => x.Region).HasColumnName("region");
        modelBuilder.Entity<TitleAka>().Property(x => x.Language).HasColumnName("language");
        modelBuilder.Entity<TitleAka>().Property(x => x.Types).HasColumnName("types");
        modelBuilder.Entity<TitleAka>().Property(x => x.Attributes).HasColumnName("attributes");
        modelBuilder.Entity<TitleAka>().Property(x => x.IsOriginalTitle).HasColumnName("isoriginaltitle");

        modelBuilder.Entity<TitleEpisode>().ToTable("title_episodes");
        modelBuilder.Entity<TitleEpisode>().HasKey(x => x.TConst);
        modelBuilder.Entity<TitleEpisode>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleEpisode>().Property(x => x.ParentTConst).HasColumnName("parenttconst");
        modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
        modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episodenumber");

        modelBuilder.Entity<UserNote>().ToTable("user_notes");
        modelBuilder.Entity<UserNote>().HasKey(x => x.NoteId);
        modelBuilder.Entity<UserNote>().Property(x => x.NoteId).HasColumnName("note_id");
        modelBuilder.Entity<UserNote>().Property(x => x.UserId).HasColumnName("user_id");
        modelBuilder.Entity<UserNote>().Property(x => x.TitleTConst).HasColumnName("title_tconst");
        modelBuilder.Entity<UserNote>().Property(x => x.NoteText).HasColumnName("note_text");
        modelBuilder.Entity<UserNote>().Property(x => x.CreatedAt).HasColumnName("created_at");
        modelBuilder.Entity<UserNote>().Property(x => x.UpdatedAt).HasColumnName("updated_at");

        modelBuilder.Entity<Genre>().ToTable("genres");
        modelBuilder.Entity<Genre>().HasKey(x => x.GenreName);
        modelBuilder.Entity<Genre>().Property(x => x.GenreName).HasColumnName("genre_name");
    }
}



