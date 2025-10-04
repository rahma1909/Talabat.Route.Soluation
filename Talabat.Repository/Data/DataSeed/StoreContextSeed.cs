using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.DataSeed
{
    public  static  class StoreContextSeed
    {


        public  async static Task  SeedAsync(StoreContext _DbContext)
        {
            if (_DbContext.Brands.Count()==0) //then seed
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");


                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        await _DbContext.Set<ProductBrand>().AddAsync(brand);
                    }


                    await _DbContext.SaveChangesAsync();
                }  
            }

            if (_DbContext.Categories.Count()==0)//then seed
            {
                var CategoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);

                if (categories?.Count() > 0)
                {

                    foreach (var category in categories)
                    {
                        await _DbContext.Set<ProductCategory>().AddAsync(category);
                    }

                    await _DbContext.SaveChangesAsync();
                }
            }


            if (_DbContext.products.Count() == 0)//then seed
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (products?.Count() > 0)
                {

                    foreach (var product in products)
                    {
                        await _DbContext.Set<Product>().AddAsync(product);
                    }

                    await _DbContext.SaveChangesAsync();
                }
            }

        }
    }
}
