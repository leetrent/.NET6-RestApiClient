﻿using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

List<Repository> repositories = await ProcessRepositoriesAsync(client);
foreach (Repository repo in repositories) 
{
    Console.Write($"|| {repo.Name}");
}

 static async Task<List<Repository>>ProcessRepositoriesAsync(HttpClient client)
 {
    await using Stream stream =
        await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories =
        await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

    return repositories ?? new();
 }