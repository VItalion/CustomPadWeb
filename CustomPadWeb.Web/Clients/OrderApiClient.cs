using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.Web.Clients
{
    public class OrderApiClient
    {
        private readonly HttpClient _http;

        public OrderApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderViewModel vm)
        {
            var response = await _http.PostAsJsonAsync("/api/orders", vm);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<OrderCreatedResponse>();
            return result?.OrderId ?? throw new Exception("OrderId not returned");
        }

        public class OrderCreatedResponse
        {
            public Guid OrderId { get; set; }
        }
    }
}
