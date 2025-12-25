namespace CustomPadWeb.AuthService.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; } = default!;
    }
}
