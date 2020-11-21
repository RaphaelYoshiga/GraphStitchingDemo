using RYoshiga.CustomerService.Models;

namespace RYoshiga.CustomerService.Services
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