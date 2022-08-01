namespace Forum._3.Models.ViewModels
{
    public class AuthData
    {
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}