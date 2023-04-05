namespace clinic.Controllers.Auth
{
    public class Validators
    {
        public static bool IsValidPassword(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            return password.Length >= 8;

        }
    }
}
