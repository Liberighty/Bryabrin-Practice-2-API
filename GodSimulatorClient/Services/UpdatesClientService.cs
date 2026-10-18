using GodSimulatorClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using WorldUpdate = GodSimulatorClient.Data.Update;
using GodSimulatorClient.Data;

namespace GodSimulatorClient.Services;

public class UpdatesClientService
{
    public readonly HttpClient _httpClient;
    public readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

        public UpdatesClientService(HttpClient httpClient)
        {
        _httpClient = httpClient;
        }
    #region Сервисы для контроллера обновлений
    #region GET ALL
    public async Task<List<WorldUpdate>> GetUpdatesAll()
        {

        
        var response = await _httpClient.GetAsync("/ServerUpdate");
        response.EnsureSuccessStatusCode();
        var responseText = await response.Content.ReadAsStringAsync();

        var responseData = JsonSerializer.Deserialize<List<WorldUpdate>>(responseText, _jsonOptions);

        return responseData;
        }
#endregion

    #region ADD
    public async Task AddUpdates(UpdateClientAdd request)
        {


        var response = await _httpClient.PutAsJsonAsync("/ServerUpdate", request);
        response.EnsureSuccessStatusCode();

        return;
        }
    #endregion

    #region DELETE
    public async Task RemoveUpdates(int Id)
    {

        var response = await _httpClient.DeleteAsync($"/ServerUpdate?updateId={Id}");
        response.EnsureSuccessStatusCode();

        return;
    }
#endregion

    #region SEARCH
    public async Task<List<WorldUpdate>> SearchUpdates(string? version, string? description)
        {

            var baseUrl = "/ServerUpdate/search";

            var queryParams = new Dictionary<string, string?>
        {
            { "descriptionFinder", description },
            { "versionFinder", version }
        };

            var finalUri = QueryHelpers.AddQueryString(baseUrl, queryParams);

            var response = await _httpClient.GetAsync(finalUri);
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<List<WorldUpdate>>(responseText, _jsonOptions);

            return responseData;
        }
#endregion

    #region Edit                            
    public async Task<WorldUpdate> GetEditUpdates(int Id)
    {


        var response = await _httpClient.GetAsync($"/ServerUpdate/{Id}");
        response.EnsureSuccessStatusCode();
        var responseText = await response.Content.ReadAsStringAsync();

        var responseData = JsonSerializer.Deserialize<WorldUpdate>(responseText, _jsonOptions);
        return responseData;
    }


    public async Task PostEditUpdates(UpdateClientEdit request)
    {


        var response = await _httpClient.PatchAsJsonAsync("/ServerUpdate", request);
        response.EnsureSuccessStatusCode();


        return;
    }
    #endregion
    #endregion



    #region Сервисы для контроллера отделов

    #region GET ALL
    public async Task<List<Division>> GetDivisionsAll()
    {

        var response = await _httpClient.GetAsync("/Division");
        response.EnsureSuccessStatusCode();
        var responseText = await response.Content.ReadAsStringAsync();

        var responseData = JsonSerializer.Deserialize<List<Division>>(responseText, _jsonOptions);
        return responseData;
    }
    #endregion

    #region ADD
    public async Task AddDivisions(Division request)
    {

        var response = await _httpClient.PutAsJsonAsync("/Division", request);
        response.EnsureSuccessStatusCode();
    }
    #endregion

    #region DELETE
    public async Task RemoveDivision(int Id)
    {

        var response = await _httpClient.DeleteAsync($"/Division?divisionId={Id}");
        response.EnsureSuccessStatusCode();
    }
    #endregion

    #endregion
}