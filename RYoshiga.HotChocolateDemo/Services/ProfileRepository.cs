using RYoshiga.HotChocolateDemo.Models;

namespace RYoshiga.HotChocolateDemo.Services
{
    public interface IProfileRepository
    {
        Customer GetProfile();
    }

    public class ProfileRepository : IProfileRepository
    {
        public Customer GetProfile()
        {
            return new Customer
            {
                Id = 1,
                Title = "Mr",
                LastName = "Wick",
                FirstName = "John"
            };
        }
    }
}