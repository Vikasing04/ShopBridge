using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopBridge.Modal;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.BAL
{
    public class Services : IServices
    {
        private  IConfiguration _config;
        private readonly HttpClient _client;
        public Services(IConfiguration configuration, HttpClient httpClient)
        {
            _config = configuration;
            _client = httpClient;
        }
        public async  Task<StatusML> Delete(int id)
        {
            try
            {
                StatusML statusML = new StatusML();
                var baseUrl = _config.GetSection("BaseURL").Value;
                var serialize = JsonConvert.SerializeObject(id);
                var httpResponse = await _client.DeleteAsync(baseUrl + "Inventory/" + id);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve tasks");
                }

                statusML.Code = (int)HttpStatusCode.OK;

                return statusML;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StatusML> Edit(InventoryML inventoryML)
            {
                try
                {
                    StatusML statusML = new StatusML();
                    var baseUrl = _config.GetSection("BaseURL").Value;
                    var serialize = JsonConvert.SerializeObject(inventoryML);
                    var httpResponse = await _client.PutAsync(baseUrl + "Inventory/"+ inventoryML.Id, new StringContent(serialize, Encoding.UTF8, "application/json"));

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("Cannot retrieve tasks");
                    }

                    statusML.Code = (int)HttpStatusCode.OK;

                    return statusML;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        public async Task<List<InventoryML>> GetALL()
        {
            try
            {
                var baseUrl = _config.GetSection("BaseURL").Value;
                var httpResponse = await _client.GetAsync(baseUrl+ "Inventory");

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve tasks");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<List<InventoryML>>(content);

                return tasks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<InventoryML> GetById(int id)
        {
            try
            {
                var baseUrl = _config.GetSection("BaseURL").Value;
                var httpResponse = await _client.GetAsync(baseUrl + "Inventory/"+id);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve tasks");
                }

                var content = await httpResponse.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<InventoryML>(content);

                return tasks;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<StatusML> Create(InventoryML inventoryML)
        {
            try
            {
                StatusML statusML = new StatusML();
                var baseUrl = _config.GetSection("BaseURL").Value;
                var serialize = JsonConvert.SerializeObject(inventoryML);
                var httpResponse = await _client.PostAsync(baseUrl + "Inventory", new StringContent(serialize, Encoding.UTF8, "application/json"));

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve tasks");
                }

                statusML.Code = (int)HttpStatusCode.OK;

                return statusML;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
