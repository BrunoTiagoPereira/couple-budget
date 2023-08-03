using Couple.Budget.Core.DomainObjects;

namespace Couple.Budget.Domain.Users.Entities
{
    public class UserPreference : Entity
    {
        protected UserPreference()
        {
            _userPreferenceDaysOfWeeks = new List<UserPreferenceDayOfWeek>();
        }

        public UserPreference(User user, IEnumerable<int> preferredDaysOfWeek) : this()
        {
            UpdatePreferredDaysOfWeek(preferredDaysOfWeek);
            UpdateUser(user);
        }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        private List<UserPreferenceDayOfWeek> _userPreferenceDaysOfWeeks;
        public IReadOnlyCollection<UserPreferenceDayOfWeek> UserPreferenceDaysOfWeeks => _userPreferenceDaysOfWeeks.AsReadOnly();

        private void UpdatePreferredDaysOfWeek(IEnumerable<int> preferredDaysOfWeek)
        {
            if (preferredDaysOfWeek is null)
            {
                throw new ArgumentNullException(nameof(preferredDaysOfWeek));
            }

            if (!preferredDaysOfWeek.Any())
            {
                throw new InvalidOperationException("É necessário selecionar ao menos um dia na semana");
            }

            foreach (var preferredDayOfWeek in preferredDaysOfWeek)
            {
                _userPreferenceDaysOfWeeks.Add(new UserPreferenceDayOfWeek(this, preferredDayOfWeek));
            }
        }

        private void UpdateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User = user;
            UserId = user.Id;
        }
    }
}