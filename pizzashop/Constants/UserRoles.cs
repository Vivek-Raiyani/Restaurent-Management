namespace pizzashop.Constants;

public static class UserRoles
{

    public const string Admin = "Admin";
        public const string Chef = "Chef";

        public const string Manager = "Manager";
        public static readonly List<string> All = new List<string> { "Admin", "Manager", "Chef" };
}
