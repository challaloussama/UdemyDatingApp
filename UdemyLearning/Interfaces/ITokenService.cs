using UdemyLearning.Entities;

namespace UdemyLearning.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
