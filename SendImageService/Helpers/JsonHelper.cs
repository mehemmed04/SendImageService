using Newtonsoft.Json;
using SendImageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendImageService.Helpers
{
    public class JsonHelper
    {
        public static string GetString(Item data)
        {
            var myDetails = JsonConvert.SerializeObject(data);
            return myDetails;
        }
        public static Item GetData(string data)
        {
            var Item = JsonConvert.DeserializeObject<Item>(data);
            return Item;
        }
    }
}
