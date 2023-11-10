namespace MyTestableApi.Tests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using Xunit;

public class TestsLanguesParlees
{
    /*
    SCÉNARIO 1 : Je récupère les langues parlées d’un pays qui a une langue officielle
    GIVEN le pays que je demande est “France”
    WHEN je demande la liste des langues parlées
    THEN je reçois un json [“Français”]
    */
    [Fact]
    public async Task TestScenario1()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Langues/France");
        string stringResponse = await response.Content.ReadAsStringAsync();

        string[]? listeReponse = JsonConvert.DeserializeObject<string[]>(stringResponse);

        List<string> vraiesReponses = new() {"Français"};
        
        //Assert
        Assert.Equal(vraiesReponses, listeReponse);
    }

    /*
    SCÉNARIO 2 : Je récupère les langues parlées d’un pays qui n’a pas de langues officielles
    GIVEN le pays que je demande est “États-Unis”
    WHEN je demande la liste des langues officielles
    THEN je reçois un json []
    */
    [Fact]
    public async Task TestScenario2()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Langues/États-Unis");
        string stringResponse = await response.Content.ReadAsStringAsync();

        string[]? listeReponse = JsonConvert.DeserializeObject<string[]>(stringResponse);

        List<string> vraiesReponses = new() {};
        
        //Assert
        Assert.Equal(vraiesReponses, listeReponse);
    }

    /*
    SCÉNARIO 3 : Je récupère les langues parlées d’un pays qui ne figure pas dans la base de données
    GIVEN le pays que je demande est “Listenbourg”
    WHEN je demande la liste des langues officielles
    THEN je reçois une erreur 400 : Bad Request
    */
    [Fact]
    public async Task TestScenario3()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Langues/Listenbourg");
        
        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /*
    SCÉNARIO 4 : Je récupère les langues parlées d’un pays qui a plusieurs langues officielles
    GIVEN le pays que je demande est “Suisse”
    WHEN je demande la liste des langues officielles
    THEN je reçois un json [“Français”, “Allemand”, “Italien”, “Romanche”]
    */
    [Fact]
    public async Task TestScenario4()
    {
        await using var _factory = new WebApplicationFactory<Program>();
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Langues/Suisse");
        string stringResponse = await response.Content.ReadAsStringAsync();

        string[]? listeReponse = JsonConvert.DeserializeObject<string[]>(stringResponse);

        List<string> vraiesReponses = new() {"Français", "Allemand", "Italien", "Romanche"};
        
        //Assert
        Assert.Equal(vraiesReponses, listeReponse);
    }
}