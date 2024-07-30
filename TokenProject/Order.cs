using System.ComponentModel.DataAnnotations.Schema;
using TokenProject.Entities;

namespace TokenProject
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [InverseProperty("Order")]
        public ICollection<LoginUser> LoginUsers { get; set; }
    }
}
