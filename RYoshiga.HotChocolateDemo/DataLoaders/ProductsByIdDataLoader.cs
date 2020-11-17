using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.DataLoader;
using RYoshiga.HotChocolateDemo.Models;
using RYoshiga.HotChocolateDemo.QueryTypes;

namespace RYoshiga.HotChocolateDemo.DataLoaders
{
    public class ProductsByIdDataLoader : BatchDataLoader<Guid, Product>
    {
        protected override async Task<IReadOnlyDictionary<Guid, Product>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            var products = new Dictionary<Guid, Product>
            {
                {Demo.ProductId, new Product {Name = "PS5"}},
                {Demo.ProductId2, new Product {Name = "Headset"}},
                {Demo.ProductId3, new Product {Name = "Controller"}},
                {Demo.ProductId4, new Product {Name = "Assorted Game"}}
            };

            var result = keys.ToDictionary(key => key, key => products[key]);
            return await Task.FromResult(result);
        }
    }
}