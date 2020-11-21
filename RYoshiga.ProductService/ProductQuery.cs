using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;

namespace RYoshiga.ProductService
{
    public class ProductQuery
    {
        public async Task<Product> Product([DataLoader] ProductsByIdDataLoader dataLoader, Guid id, CancellationToken token)
        {
            return await dataLoader.LoadAsync(id, token);
        }
    }
}