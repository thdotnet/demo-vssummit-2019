using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await GetProducts();

            return View(products.First());
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = new List<Product>();


            string databaseId = "db";
            string containerId = "products";
            string partitionKey = "/id";

            // Create new instance of CosmosClient
            using (CosmosClient cosmosClient = new CosmosClient("https://vssummit2019th.documents.azure.com:443/", "zZMBHNJQ5Ai82hvIlIFBvOPM1tdGeVK3MxpIRSnserXKSqbV2Fe4l7KmGBXwkZAyv1VRDCkBY4FdNKxk2lobbA=="))
            {
                // Create new database
                CosmosDatabase database = await cosmosClient.Databases.CreateDatabaseIfNotExistsAsync(databaseId);

                // Create new container
                CosmosContainer container = await database.Containers.CreateContainerIfNotExistsAsync(containerId, partitionKey);

                var product = container.Items.GetItemIterator<Product>(maxItemCount: 1);
                var setIterator = container.Items.GetItemIterator<Product>(maxItemCount: 1);

                while (setIterator.HasMoreResults)
                {
                    foreach (Product item in await setIterator.FetchNextSetAsync())
                    {
                        products.Add(item);
                    }
                }

                return products;
            }
        }

        public async Task<IActionResult> Refresh(int id)
        {
            var products = await GetProducts();
            var product = products.First(x => x.Id == id);

            return Json(product);
        }

    }
}
