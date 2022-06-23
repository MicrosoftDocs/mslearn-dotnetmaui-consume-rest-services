using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace PartsClient.Data
{
    public static class PartsManager
    {
        // TODO: Add fields for BaseAddress, Url, and authorizationKey
        static readonly string BaseAddress = "URL GOES HERE";
        static readonly string Url = $"{BaseAddress}/api/";

        static HttpClient client;

        private static async Task<HttpClient> GetClient()
        {
            throw new NotImplementedException();
        }

        public static async Task<IEnumerable<Part>> GetAll()
        {
            throw new NotImplementedException();                
        }

        public static async Task<Part> Add(string partName, string supplier, string partType)
        {
            throw new NotImplementedException();
        }

        public static async Task Update(Part part)
        {
            throw new NotImplementedException();
        }

        public static async Task Delete(string partID)
        {
            throw new NotImplementedException();                        
        }
    }
}
