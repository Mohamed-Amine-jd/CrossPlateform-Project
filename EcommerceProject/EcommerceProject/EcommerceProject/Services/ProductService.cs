using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Text;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService()
    {
        // Set the BaseAddress to your backend API base URL
        _httpClient = new HttpClient { BaseAddress = new Uri("http://172.20.10.4:8080/api/product/") }; // Ensure the base URL is correctly set
    }

    // Method to add a product
    public async Task<bool> AddProduct(string title, string description, string image, double price, int qty, string category)
    {
        var product = new
        {
            title = title,
            description = description,
            image = image,
            price = price,
            qty = qty,
            category = category
        };

        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            // Use the correct relative URL for the 'create' endpoint
            var response = await _httpClient.PostAsync("create", content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Product added successfully
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync();
                throw new Exception($"Product creation failed: {response.ReasonPhrase} - {responseData}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            throw new Exception("Network error during product addition: " + httpEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Error during product addition: " + ex.Message);
        }
    }

    public async Task<List<Product>> GetAllProducts()
    {
        try
        {
            // Send a GET request to the 'all' endpoint
            var response = await _httpClient.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Deserialize the response into a list of Product objects
                var products = JsonConvert.DeserializeObject<List<Product>>(responseData);
                return products; // Return the list of products
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get products: {response.ReasonPhrase} - {responseData}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            throw new Exception("Network error during fetching products: " + httpEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Error during fetching products: " + ex.Message);
        }
    }

    // Product class to map the response
    public async Task<bool> DeleteProduct(string productId)
    {
        try
        {
            // Make the API call to delete the product using the string ID
            var response = await _httpClient.DeleteAsync($"delete/{productId}");

            if (response.IsSuccessStatusCode)
            {
                return true;  // Successfully deleted
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to delete product: {response.ReasonPhrase} - {responseData}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            throw new Exception("Network error during product deletion: " + httpEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Error during product deletion: " + ex.Message);
        }
    }






    public async Task<bool> UpdateProduct(string id, string title, string description, string image, double price, int qty, string category)
    {
        var product = new
        {
            title = title,
            description = description,
            image = image,
            price = price,
            qty = qty,
            category = category
        };

        var json = JsonConvert.SerializeObject(product);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            // Use the correct relative URL for the 'update' endpoint
            var response = await _httpClient.PutAsync($"update/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Product updated successfully
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync();
                throw new Exception($"Product update failed: {response.ReasonPhrase} - {responseData}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            throw new Exception("Network error during product update: " + httpEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("Error during product update: " + ex.Message);
        }
    }






}
