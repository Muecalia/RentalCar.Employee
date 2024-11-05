using System.ComponentModel.DataAnnotations;

namespace RentalCar.Employees.Core.Entities
{
    public class Employee : BaseEntity
    {
        [MaxLength(100), Required]
        public required string Name { get; set; }
        [EmailAddress, Required, MaxLength(100)]
        public required string Email { get; set; }
        [MaxLength(25), Required]
        public required string Phone { get; set; }
        public required char Gender { get; set; }
    }
}
