namespace WebApi.Model
{
    public class BaseModel
    {
        public DateTimeOffset CreatedAt { get; set; }
        public Nullable<DateTimeOffset> UpdatedAt { get; set; }
    }
}