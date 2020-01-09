using DockerRedisExample.Domains;
using DockerRedisExample.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DockerRedisExample
{
    class Program
    {
        private const string ProductCacheKey = "AllProducts";

        static void Main(string[] args)
        {
            SaveDataToRedis();

            ReadDataFromRedis();
        }

        public static void ReadDataFromRedis()
        {
            var cache = RedisHelper.Connection.GetDatabase();

            var serializedObject = cache.StringGet(ProductCacheKey);

            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(serializedObject);

            foreach (var product in products)
            {
                Console.WriteLine(product.Id + " " + product.Name + " " + product.Price);
            }
        }

        public static void SaveDataToRedis()
        {
            var cache = RedisHelper.Connection.GetDatabase();

            var service = new ProductService();

            var products = service.GetProducts();

            var serializedObject = JsonConvert.SerializeObject(products);

            cache.StringSet(ProductCacheKey, serializedObject);
        }
    }
}
