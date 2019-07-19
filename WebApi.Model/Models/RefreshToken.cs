using System;
namespace WebApi.Model
{
    public class RefreshToken

    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime IssuedTime { get; set; }
        public DateTime ExpiredTime {get;set;}
    }
}