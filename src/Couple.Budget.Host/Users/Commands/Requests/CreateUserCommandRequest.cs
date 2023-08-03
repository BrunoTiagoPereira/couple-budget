namespace Couple.Budget.Host.Users.Commands.Requests
{
    public class CreateUserCommandRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public IEnumerable<int> PreferredDaysOfWeek { get; set; }
    }
}