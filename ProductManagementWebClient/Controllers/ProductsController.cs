using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementWebClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;
        private readonly HttpClient client = new();
        private readonly string ProductApiUrl = "https://localhost:7104/api/products";
        private readonly string CategoryApiUrl = "https://localhost:7104/api/categories";
        private readonly JsonSerializerOptions jsonOption = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ProductsController()
        {

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, jsonOption);
            return View(listProducts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Product product = await GetProductById(id);

            return View(product);
        }


        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            List<Category> listCate = JsonSerializer.Deserialize<List<Category>>(strData, jsonOption);

            ViewBag.ListCategory = listCate;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product p)
        {
            Product newProduct = new Product
            {
                ProductName = p.ProductName,
                UnitInStock = p.UnitInStock,
                UnitPrice = p.UnitPrice,
                CategoryId = p.CategoryId
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(ProductApiUrl, newProduct);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Product product = await GetProductById(id);
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            List<Category> listCate = JsonSerializer.Deserialize<List<Category>>(strData, jsonOption);

            ViewBag.ListCategory = listCate;

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product p)
        {
            Product newProduct = new Product
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitInStock = p.UnitInStock,
                UnitPrice = p.UnitPrice,
                CategoryId = p.CategoryId
            };

            HttpResponseMessage response = await client.PutAsJsonAsync(ProductApiUrl, newProduct);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Product product = await GetProductById(id);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/{collection["ProductId"]}");
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        private async Task<Product> GetProductById(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string apiUrl = $"{ProductApiUrl}/{id}";
            HttpResponseMessage proRes = await client.GetAsync(apiUrl);
            string proData = await proRes.Content.ReadAsStringAsync();
            Product product = JsonSerializer.Deserialize<Product>(proData, options);

            return product;
        }
    }
}
