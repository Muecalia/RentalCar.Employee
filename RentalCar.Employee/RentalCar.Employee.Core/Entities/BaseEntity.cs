using System.ComponentModel.DataAnnotations;

namespace RentalCar.Employees.Core.Entities
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            IsDeleted = false;
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }

        [MaxLength(50), Required]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
