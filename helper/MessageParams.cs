﻿namespace SocialClint.Dto
{
    public class MessageParams
    {
        public string UserID { get; set; }
        public string Container { get; set; } = "Unread";
        public int  PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
