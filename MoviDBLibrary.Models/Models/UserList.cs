using MovieDBLibrary.DataAccess.EF.Models;
using System;
using System.Collections.Generic;

namespace MoviDBLibrary.DataAccess.EF.Models;

public partial class UserList
{
    public int UserListId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public string? Notes { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}
