namespace Couple.Budget.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> ValidationFailuresMessages { get; private set; }

        public ValidationException(IEnumerable<string> errorMessages)
        {
            if (errorMessages is null)
            {
                throw new ArgumentNullException(nameof(errorMessages));
            }

            ValidationFailuresMessages = errorMessages;
        }

        public ValidationException(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            ValidationFailuresMessages = new List<string> { errorMessage };
        }
    }
}