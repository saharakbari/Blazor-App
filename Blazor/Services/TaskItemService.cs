using Shared.DTO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Blazor.Services
{
    public class TaskItemService
    {
        private readonly HttpClient _httpClient;

        public TaskItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

     
        public async Task<List<TaskItemDTO>> GetTasksAsync()
        {
           
       
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<TaskItemDTO>>>("api/Task/GetTask");

            return response?.Data ?? new List<TaskItemDTO>();
        }


        public async Task<TaskItemDTO> GetTaskByIdAsync(int id)
        {

            var response = await _httpClient.GetFromJsonAsync<ApiResponse<TaskItemDTO>>($"api/Task/GetTaskById/{id}");

            return response?.Data ?? new TaskItemDTO();
        }

        public async Task<bool> UpdateTaskAsync(TaskItemDTO task)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Task/UpdateTask", task);

                if (response.IsSuccessStatusCode)
                {
                    
                    return true;
                }
                else
                {
                    
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ UpdateTask failed: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception in UpdateTaskAsync: {ex.Message}");
                return false;
            }
        }


        //public async Task<bool> AddTaskAsync(TaskItemDTO task)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("api/Task/AddTask", task);

         
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        var error = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine("❌ AddTask Error: " + error);
        //        return false;
        //    }
        //}

        //*********just for adding item ,I use the mediatR*******
        public async Task<bool> AddTaskAsync(TaskItemDTO task)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TaskMediatR/AddTask", task);


            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ AddTask Error: " + error);
                return false;
            }
        }

    }
}

