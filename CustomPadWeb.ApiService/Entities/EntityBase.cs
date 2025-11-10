using System.ComponentModel.DataAnnotations;

namespace CustomPadWeb.ApiService.Entities
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
