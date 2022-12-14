namespace Sat.Recruitment.Api.Entity
{
	public class User
    {
        public User() { }
        public User(string name, string email, string address, string phone, string userType, decimal money) 
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
