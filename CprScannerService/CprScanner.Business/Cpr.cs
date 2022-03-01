namespace CprScanner.Business
{
    using System;

    public struct Cpr
    {
        private readonly string cpr;

        public Cpr(string cpr)
        {
            this.cpr = cpr ?? throw new ArgumentNullException(nameof(cpr));
        }

        public bool ErValid()
        {
            return new CprValidator(this.cpr).Validate();
        }

        public override string ToString()
        {
            return this.cpr;
        }

        public override bool Equals(object obj)
        {
            if (obj is Cpr other)
            {
                return object.Equals(other.cpr, this.cpr);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.cpr.GetHashCode();
        }

        public static implicit operator Cpr(string cpr)
        {
            return new Cpr(cpr);
        }
    }
}
