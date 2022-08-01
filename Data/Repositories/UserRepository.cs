using Forum._3.Data.Abstract;
using Forum._3.Models;



namespace Forum._3.Data.Repositories {
    public class UserRepository : EntityBaseRepository<User>, IUserRepository {

        public UserRepository (DBContext context) : base (context) { }

        public bool isEmailUniq (string email) {
            var user = GetSingle(u => u.Email == email);
            return user == null;
        }

        public bool IsUsernameUniq (string username) {
            var user = GetSingle(u => u.Username == username);
            return user == null;
        }

        public bool isAdmin(string id)
        {
            var user = GetSingle(u => u.Id == id, u=>u.Role);
            return user.Role.Name == "admin";
        }

    }
}