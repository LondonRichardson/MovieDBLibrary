using System;
using System.Collections.Generic;

namespace MoviDBLibrary.DataAccess.EF.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public virtual ICollection<UserList> UserLists { get; set; } = new List<UserList>();
}
