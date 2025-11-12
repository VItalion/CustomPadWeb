namespace CustomPadWeb.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }


        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty", nameof(value));


            // Minimal validation. Replace with more rigorous validation if desired.
            if (!value.Contains("@"))
                throw new ArgumentException("Invalid email format", nameof(value));


            Value = value.Trim();
        }


        public override string ToString() => Value;


        public static implicit operator string(Email e) => e.Value;
        public static explicit operator Email(string s) => new Email(s);
    }
}
