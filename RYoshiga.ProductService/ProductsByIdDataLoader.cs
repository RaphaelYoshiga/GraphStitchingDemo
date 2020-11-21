using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate.DataLoader;

namespace RYoshiga.ProductService
{
    public class ProductsByIdDataLoader : DataLoaderBase<Guid, Product>
    {
        //protected override async Task<IReadOnlyDictionary<Guid, Product>> LoadBatchAsync(
        //    IReadOnlyList<Guid> keys,
        //    CancellationToken cancellationToken)
        //{
        //    var result = keys.ToDictionary(key => key, key => new Product{ Id = key, Name = "Random product"});
        //    return await Task.FromResult(result);
        //}

        protected override async Task<IReadOnlyList<Result<Product>>> FetchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var result = keys.Select(key => Result<Product>.Resolve(new Product{ Id = key, Name = "Random product"})).ToList();
            return await Task.FromResult(result);
        }
    }
}