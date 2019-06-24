using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("productName")]
        public string Name { get; set; }

        [JsonProperty("images")]
        public string[] Images { get; set; }

        [JsonProperty("benefits")]
        public string[] Benefits { get; set; }
    }
}
