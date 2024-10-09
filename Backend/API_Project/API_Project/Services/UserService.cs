using API_Project.Entities;
using API_Project.IServices;
using API_Project.RequestModel;
using MongoDB.Driver;
using OneOf;


namespace API_Project.Services
{
#pragma warning disable
    public class UserService : IUserService
    {
        #region Declarations
        private readonly IMongoCollection<User>? _users;

        #endregion

        #region Constructor
        public UserService(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("User");
        }
        #endregion

        #region Service Method's

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var users = await _users.Find(_ => true).ToListAsync();
                return users; 
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        
        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OneOf<User, string>> GetUserByIdAsync(string userId)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
                var user = await _users.Find(filter).FirstOrDefaultAsync(); 
                if (user != null)
                    return user; 
                else
                    return "User not found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<OneOf<User, string>> CreateUserAsync(UserRequestModel user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(u => u.Email, user.Email);
                var existingUser = await _users.Find(filter).FirstOrDefaultAsync();
                if (existingUser != null)
                    return "Email already exists!";

                User newUser = new User()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = DateTime.Now,
                };
                await _users.InsertOneAsync(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// upadate user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<OneOf<User, string>> UpdateUserAsync(User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);
                // var existingUser = await _users.Find(filter).FirstOrDefaultAsync();
                var existingUser = await _users.Find(x => x.Id == user.Id || (x.Email == user.Email && x.Id != user.Id)).FirstOrDefaultAsync();
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    return "Email is already in use by another user"; 
                }
                user.CreatedAt = existingUser.CreatedAt;
                await _users.ReplaceOneAsync(filter, user);
                return user; 
            }
            catch (Exception ex)
            {
                return ex.Message;  
            }
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> DeleteUserAsync(string userId)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, userId);
                var deleteResult = await _users.DeleteOneAsync(filter);
                if (deleteResult.DeletedCount > 0)
                    return "User deleted successfully"; 
                else
                {
                    return "User not found"; 
                }
            }
            catch (Exception ex)
            {
                return ex.Message; 
            }
        }
        #endregion

    }
}
