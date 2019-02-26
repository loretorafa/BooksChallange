using Newtonsoft.Json;

namespace BooksChallange.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity() { }

        [JsonProperty(Order = -2)]
        public virtual int Id { get; set; }
    }
}
