using MoviDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.DataAccess.EF;

namespace MoviDBLibrary.DataAccess.EF.Repositories
{
    public class UserRepository
    {
        private MovieDbContext _dbContext;


        public UserRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int create(User users)
        {
            _dbContext.Add(users);
            _dbContext.SaveChanges();

            return users.UserId;
        }
        public int Update(User users)
        {
            User existingUser = _dbContext.Users.Find(users.UserId)!;

            existingUser.FirstName = existingUser.FirstName;
            existingUser.LastName = users.LastName;
            existingUser.Username = users.Username;
            existingUser.Password = users.Password;
            existingUser.EmailAddress = users.EmailAddress;
            existingUser.UserLists = existingUser.UserLists;
            

            _dbContext.SaveChanges();
            return existingUser.UserId;
        }
        public bool Delete(int uID)
        {
            User users = _dbContext.Users.Find(uID);
            _dbContext.Remove(users);
            _dbContext.SaveChanges();

            return true;
        }
        public List<User> GetAllUser()
        {
            List<User> usersList = _dbContext.Users.ToList();

            return usersList;
        }
        public User GetUserByID(int userId)
        {
            User users = _dbContext.Users.Find(userId);

            return users;
        }

    }
}
