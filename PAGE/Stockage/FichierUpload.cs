using Microsoft.AspNetCore.Http;
using PAGE.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PAGE.Stockage
{
    public class FileUpload
    {
        private readonly HttpClient _httpClient;

        public FileUpload(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UploadFile(IFormFile fichier)
        {
            // Convert the file to bytes
            using (var memoryStream = new MemoryStream())
            {
                await fichier.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                // Create a new instance of PieceJointe
                var pieceJointe = new PieceJointe(fichier);
               
                // Create MultipartFormDataContent to send both file and JSON data
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new ByteArrayContent(fileBytes), "file", fichier.FileName);

                    // Send the request to the API endpoint
                    var response = await _httpClient.PostAsync("api/PieceJointe/upload", content);

                    // Handle the response as needed
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Upload successful. Response: {responseContent}");
                    }
                    else
                    {
                        Console.WriteLine($"Upload failed. Status code: {response.StatusCode}");
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error content: {errorContent}");
                    }
                }
            }
        }
    }
}
