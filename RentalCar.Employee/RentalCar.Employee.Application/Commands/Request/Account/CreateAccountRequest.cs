namespace RentalCar.Employees.Application.Commands.Request.Account
{
    public class CreateAccountRequest(string name, string phone, string role, string email, string password, string idUser)
    {
        public string Name { get; set; } = name;
        public string Phone { get; set; } = phone;
        public string Role { get; set; } = role;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string IdUser { get; set; } = idUser;
        public bool IsClient { get; set; } = false;
    }
}
