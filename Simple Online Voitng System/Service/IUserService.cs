namespace Simple_Online_Voitng_System.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool isAuthenticated();
        public string FullName();
        string Email();
        string AdminEmail();
    }
}

