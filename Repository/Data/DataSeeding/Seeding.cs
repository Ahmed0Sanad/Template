using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.Data.DataSeeding
{
    public static class Seeding
    {
        public static void SeedingHelper(AppDbContext context)
        {

            //string filePath = "../ECommerce.Repository/Data/DataSeeding";

            //seed<ProductBrand>(filePath + "/brands.json", context);
            //seed<ProductCategory>(filePath + "/categories.json", context);
            //seed<Product>(filePath + "/products.json", context);
            //seed<DeliveryMethod>(filePath + "/delivery.json", context);


        }
        public static void seed<T>(string path, AppDbContext context) where T : BaseEntity
        {
            var file = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<List<T>>(file);
            if (context.Set<T>().Count() == 0)
            {
                if (data?.Count > 0)
                {
                    foreach (var item in data)
                    {
                        context.Set<T>().Add(item);
                    }
                    context.SaveChanges();

                }


            }

        }
    }
}
