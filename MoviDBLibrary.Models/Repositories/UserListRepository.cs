using MoviDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.DataAccess.EF;

namespace MoviDBLibrary.DataAccess.EF.Repositories
{
    public class UserListListRepository
    {
        private MovieDbContext _dbContext;


        public UserListListRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int create(UserList userLists)
        {
            _dbContext.Add(userLists);
            _dbContext.SaveChanges();

            return userLists.UserListId;
        }
        public int Update(UserList userLists)
        {
            UserList existingUserList = _dbContext.UserLists.Find(userLists.UserListId)!;

            existingUserList.UserId = userLists.UserId;
            existingUserList.MovieId = userLists.MovieId;
            existingUserList.Notes = userLists.Notes;
            existingUserList.Movie = userLists.Movie;
            existingUserList.User = userLists.User;
            


            _dbContext.SaveChanges();
            return existingUserList.UserListId;
        }
        public bool Delete(int ulID)
        {
            UserList userLists = _dbContext.UserLists.Find(ulID);
            if (userLists != null)
            {
                _dbContext.Remove(userLists);
            }
            else
            {
                _dbContext.SaveChanges();
            }

            return true;
        }
        public List<UserList> GetAllUserList()
        {
            List<UserList> userLists = _dbContext.UserLists.ToList();

            return userLists;
        }
        public UserList GetUserListByID(int userListId)
        {
            UserList userLists = _dbContext.UserLists.Find(userListId);

            return userLists;
        }

    }
}
