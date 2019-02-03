using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CarAdverts.Core.Entities;
using CarAdverts.WebAPI;
using Newtonsoft.Json;
using Xunit;

namespace FunctionalTests.WebAPI.Controllers
{
    public class AdvertControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public AdvertControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }


        [Fact]
        public async Task ShouldListAdvertWithOrderByParameter()
        {
            var response = await Client.GetAsync("/api/advert/list/price");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ICollection<Advert>>(stringResponse);

            var minPrice = model.Min(t => t.Price);
            Assert.Equal(minPrice, model.FirstOrDefault().Price );

        }
    }
}
