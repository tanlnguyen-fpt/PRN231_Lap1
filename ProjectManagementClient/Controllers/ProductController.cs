using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectManagementClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7277/api/Products";
            CategoryApiUrl = "https://localhost:7277/api/Category";
        }
        
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProducts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage proRes = await client.GetAsync(ProductApiUrl);
            string proData = await proRes.Content.ReadAsStringAsync();
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(proData, options);

            HttpResponseMessage cateRes = await client.GetAsync(CategoryApiUrl);
            string cateData = await cateRes.Content.ReadAsStringAsync();
            List<Category> listCate = JsonSerializer.Deserialize<List<Category>>(cateData, options);

            Product p = listProducts.Where(p => p.ProductId == id).First();
            p.Category = listCate.Where(c => c.CategoryId == p.CategoryId).First();

            return View(p);
        }

        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Category> listCate = JsonSerializer.Deserialize<List<Category>>(strData, options);

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage proRes = await client.GetAsync(ProductApiUrl);
            string proData = await proRes.Content.ReadAsStringAsync();  
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(proData, options);

            HttpResponseMessage cateRes = await client.GetAsync(CategoryApiUrl);
            string cateData = await cateRes.Content.ReadAsStringAsync();
            List<Category> listCate = JsonSerializer.Deserialize<List<Category>>(cateData, options);

            ViewBag.ListCategory = listCate;

            return View(listProducts.Where(p => p.ProductId == id).First());
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
            HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:7277/api/Products/id?id="+p.ProductId, newProduct);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            HttpResponseMessage proRes = await client.GetAsync(ProductApiUrl);
            string proData = await proRes.Content.ReadAsStringAsync();
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(proData, options);

            HttpResponseMessage cateRes = await client.GetAsync(CategoryApiUrl);
            string cateData = await cateRes.Content.ReadAsStringAsync();
            List<Category> listCate = JsonSerializer.Deserialize<List<Category>>(cateData, options);

            Product p = listProducts.Where(p => p.ProductId == id).First();
            p.Category = listCate.Where(c => c.CategoryId == p.CategoryId).First();

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:7277/api/Products/id?id=" + collection["ProductId"]);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}
