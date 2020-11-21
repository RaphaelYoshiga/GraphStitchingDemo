using HotChocolate;
using RYoshiga.CustomerService.Models;
using RYoshiga.CustomerService.Services;

namespace RYoshiga.CustomerService.QueryTypes
{
    public class Query
    {
        public Customer GetProfile([Service]IProfileRepository repository) => repository.GetProfile();

    }
}