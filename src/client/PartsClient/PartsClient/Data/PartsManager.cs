using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsClient.Data
{
    public class PartsManager
    {
        // TODO: Add fields for BaseAddress, Url, and authorizationKey

        private async Task<HttpClient> GetClient()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Part>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Part> Add(string partName, string supplier, string partType)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Part part)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string partID)
        {
            throw new NotImplementedException();
        }
    }
}
