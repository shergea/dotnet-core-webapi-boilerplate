namespace WebApi.Model
{
    public class RefreshToken : BaseModel

    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime IssuedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}