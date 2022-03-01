namespace CprScanner.Business
{
    using System.Linq;

    public class CprValidator
    {
        private const int LengthOfCprWithoutSeperator = 10;
        private const int LengthOfCprWithSeperator = 11;
        private readonly string[] acceptedSeperators = { "-", "/" };
        private readonly string cpr;

        public CprValidator(string cpr)
        {
            this.cpr = cpr ?? throw new System.ArgumentNullException(nameof(cpr));
        }

        public bool Validate()
        {
            if (this.cpr.Length == LengthOfCprWithSeperator && !this.ValidateCprSeperator())
            {
                return false;
            }

            string trimmedCpr = this.RemoveSeperators(this.cpr);
            if (trimmedCpr.Length != LengthOfCprWithoutSeperator)
            {
                return false;
            }

            return this.ValidateDigits(trimmedCpr);
        }

        private string RemoveSeperators(string cpr)
        {
            string returnValue = cpr;
            foreach (var seperator in this.acceptedSeperators)
            {
                returnValue = returnValue.Replace(seperator, string.Empty);
            }
            return returnValue;
        }

        private bool ValidateDigits(string cprNumber)
        {
            if (!int.TryParse(cprNumber.Substring(0, 2), out var dayOfMonth))
            {
                return false;
            }

            if (dayOfMonth < 1 || dayOfMonth > 31 && dayOfMonth < 61 || dayOfMonth > 91)
            {
                return false;
            }

            if (!int.TryParse(cprNumber.Substring(2, 2), out var month))
            {
                return false;
            }

            if (month < 1 || month > 12)
            {
                return false;
            }

            return int.TryParse(cprNumber.Substring(4, 6), out _);
        }

        private bool ValidateCprSeperator()
        {
            var matches = this.acceptedSeperators.Count(seperator => this.cpr.Contains(seperator));
            return matches == 1;
        }

    }
}