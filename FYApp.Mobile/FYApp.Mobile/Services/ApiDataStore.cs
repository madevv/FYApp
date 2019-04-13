using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using FYApp.Core.Models;

namespace FYApp.Mobile.Services
{
    public class ApiDataStore : IDataStore<Household>
    {
        HttpClient client;
        IEnumerable<Household> items;

        public ApiDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.ApiBackendUrl}/");

            items = new List<Household>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Household>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/households");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Household>>(json));
            }

            return items;
        }

        public async Task<Household> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Household>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Household item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/households", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Household item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/household/{item.HouseholdId}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/household/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
