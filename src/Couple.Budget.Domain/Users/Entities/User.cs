using Couple.Budget.Core.DomainObjects;
using Couple.Budget.Core.ValueObjects;

namespace Couple.Budget.Domain.Users.Entities
{
    public class User : AggregateRoot
    {
        public const int MAX_USER_NAME_LENGTH = 128;
        public string UserName { get; private set; }
        public Password Password { get; private set; }
        public UserPreference UserPreference { get; private set; }

        protected User() { }
        public User(string userName, string password, IEnumerable<int> daysOfWeek)
        {
            UpdateUserName(userName);
            UpdatePassword(password);
            UpdateUserPreference(daysOfWeek);
        }

        public void UpdateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            UserName = userName;
        }

        public void UpdatePassword(string password)
        {
            Password = new Password(password);
        }

        public void UpdateUserPreference(IEnumerable<int> daysOfWeek)
        {
            UserPreference = new UserPreference(this, daysOfWeek);
        }
    }
}