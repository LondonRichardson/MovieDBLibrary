using MoviDBLibrary.DataAccess.EF.Models;
using System;
using System.Collections.Generic;

namespace MovieDBLibrary.DataAccess.EF.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; }

    public string Title { get; set; }

    public int YearReleased { get; set; }

    public string Director { get; set; } 

    public string LeadActorActress { get; set; } 

    public string Cast { get; set; } = null!;

    public string GrossRevenue { get; set; } = null!;

    public string MaturityRating { get; set; } = null!;

    public virtual ICollection<UserList> UserLists { get; set; } = new List<UserList>();

    public Movie(int movieId, int genreId, string title, int yearReleased, string director, string leadActorActress, string cast, string grossRevnue, string maturityRating, ICollection<UserList> userLists)
    {
        MovieId = movieId;
        GenreId = genreId;
        Title = title;
        YearReleased = yearReleased;
        Director = director;
        LeadActorActress = leadActorActress;
        Cast = cast;
        GrossRevenue = grossRevnue;
        MaturityRating = maturityRating;
        UserLists = userLists;
    }
    public Movie()
    {
        
    }
}
