using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {

        private readonly StoreDbContext _dbContext;

        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {

                var HasProducts = await _dbContext.Products.AnyAsync();
                var HasBrands = await _dbContext.ProductBrands.AnyAsync();
                var HasTypes = await _dbContext.ProductTypes.AnyAsync();
                if (HasProducts && HasBrands && HasTypes) return;


                if (!HasBrands)
                    await SeedDataFromJsonAsync<ProductBrand, int>( "brands.json",  _dbContext.ProductBrands);
                if (!HasTypes)
                    await SeedDataFromJsonAsync<ProductType, int>( "types.json",  _dbContext.ProductTypes);
                await _dbContext.SaveChangesAsync();
                if (!HasProducts)
                    await SeedDataFromJsonAsync<Product, int>( "products.json",  _dbContext.Products);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(value: $"Data Seeding Failed : {ex}");
            }
        }

        private async Task SeedDataFromJsonAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var FilePath = @"..\E Commerce.Persistence\Data\DataSeed\JSONFiles\" + fileName;
            if (!File.Exists(path: FilePath)) throw new FileNotFoundException($"File {fileName} is not exists");


            try
            {
                using var dataStream = File.OpenRead( FilePath);
                //var test  = File.re
                var data = await JsonSerializer.DeserializeAsync<List<T>>( dataStream, options: new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data is not null)
                {
                    await dbset.AddRangeAsync(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(value: $"Error While Reading JSON File : {ex}");
                return;
            }
        }
    }
}
