namespace TokenProject
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfficialEmail { get; set; }
        public string OfficialLandline { get; set; }
        public string OfficialMobile { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<LoginUser> LoginUsers { get; set; }
    }
}
