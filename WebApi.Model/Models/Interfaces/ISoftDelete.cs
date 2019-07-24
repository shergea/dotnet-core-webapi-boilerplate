using System;
namespace WebApi.Model.Interfaces
{
    public interface ISoftDelete
    {
        Nullable<DateTimeOffset> DeletedAt { get; set; }
    }
}