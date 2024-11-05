namespace RentalCar.Employees.Application.Commands.Request.Account
{
    public class UpdateAccountRequest(string idUser, string name, string email, string phone)
    {
        public string IdUser { get; set; } = idUser;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Phone { get; set; } = phone;
    }
}
