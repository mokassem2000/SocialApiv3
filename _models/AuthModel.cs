namespace SocialClint._models
{
    public class AuthModel
    {
        public string Id { get; set; }  
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string PhotoUrl{ get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
