using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ScavEditor.Api;
using ScavEditor.Api.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

//Thread.Sleep(5000);

Console.WriteLine("Populating Database with Dummy-Data.");
Console.ReadKey();

string baseUrl = "http://localhost:5282";

using (var httpClient = new HttpClient())
{
    try
    {
        AddUsers(httpClient, baseUrl);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

static void AddUsers(HttpClient httpClient, string baseUrl)
{
    List<Participant> participants = [];

    for(int i = 1; i <= 10; i++)
    {
        participants.Add(new()
        {
            Id = i,
            Username = $"DummyMan{i}"
        });
    }

    foreach(var participant in participants)
    {
        string jsonContent = JsonConvert.SerializeObject(participant);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            var response = httpClient.PostAsync(baseUrl + "/api/Participant/", httpContent).Result;
            ValidateResponse(response, nameof(AddUsers));

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}

static void ValidateResponse(HttpResponseMessage response, string methodName)
{
    if (response.IsSuccessStatusCode)
        Console.WriteLine($"{methodName}: Request was successful!");
    else
        Console.WriteLine($"{methodName}: Request failed! Response: {response.StatusCode}");
}
