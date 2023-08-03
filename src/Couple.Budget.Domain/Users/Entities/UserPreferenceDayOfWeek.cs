using Couple.Budget.Core.DomainObjects;

namespace Couple.Budget.Domain.Users.Entities
{
    public class UserPreferenceDayOfWeek : Entity
    {
        protected UserPreferenceDayOfWeek() { }
        public int DayOfWeek { get; private set; }

        public Guid UserPreferenceId { get; private set; }

        public UserPreference UserPreference { get; private set; }

        public UserPreferenceDayOfWeek(UserPreference userPreference, int dayOfWeek)
        {
            UpdateUserPreference(userPreference);
            UpdateDayOfWeek(dayOfWeek);
        }

        private void UpdateUserPreference(UserPreference userPreference)
        {
            if (userPreference is null)
            {
                throw new ArgumentNullException(nameof(userPreference));
            }

            UserPreference = userPreference;
            UserPreferenceId = userPreference.Id;
        }

        private void UpdateDayOfWeek(int dayOfWeek)
        {
            if (dayOfWeek < 0 || dayOfWeek > 6)
            {
                throw new InvalidOperationException("Dia inválido");
            }

            DayOfWeek = dayOfWeek;
        }
    }
}