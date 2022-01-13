namespace AnimalAdoptionCenter.Services.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}