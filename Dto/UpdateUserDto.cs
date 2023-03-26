using SocialClint.entity;

namespace SocialClint.Dto
{
    public class UpdateUserDto
    {
        public string id { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Intersts { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
