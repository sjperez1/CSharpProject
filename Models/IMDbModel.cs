#pragma warning disable CS8618

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CSharpProject.Models;
{
    class IMDb
    {
        static string host = "https://imdb-api.com/en/API";
        static string path = "/SearchMovie/";
        static string key = "k_cm9ofxs3";
        static string query = "";

        async static void Search()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("", key);

            string uri = host + path +  key + System.Net.WebUtility.UrlEncode(query);

            HttpResponseMessage response = await client.GetAsync(uri);

            string contentString = await response.Content.ReadAsStringAsync();
            dynamic parsedJson = JsonConvert.DeserializeObject(contentString);
            Console.WriteLine(parsedJson);
        }
    }
}

// var apiLib = new ApiLib("API-Key");
// // Search
// var data = await apiLib.SearchMovieAsync("leon the professional 1994");
// // Title Data
// var data = await apiLib.TitleAsync("tt0110413");
// // Title Data (French Language)
// var data = await apiLib.TitleAsync("tt0110413", Language.fr);
// // Title Data - Get Full Data
// var data = await apiLib.TitleAsync("tt0110413", Language.en, "FullActor,FullCast,Posters,Images,Trailer,Ratings,Wikipedia");
// // Report - As PNG File
// var data = await apiLib.ReportAsync("tt0110413", Language.en);
// // Images (From IMDb)
// var data = await apiLib.ImagesAsync("tt0110413");
// // Posters (From TheMovieDb)
// var data = await apiLib.PostersAsync("tt0110413");
// // Trailer
// var data = await apiLib.TrailerAsync("tt0110413");
// // ExternalSites (Get Movie or Series TV in all external sites with Identifier and URL)
// var data = await apiLib.ExternalSitesAsync("tt0110413");
// // Ratings (Get ratings of Movie or Series TV in: IMDb, Metacritic, RottenTommatoes, TheMovieDb and TV.com)
// var data = await apiLib.RatingsAsync("tt0110413");
// // Wikipedia (PlainText and Html)
// var data = await apiLib.WikipediaAsync("tt0110413");
// // Youtube
// var data = await apiLib.YoutubeAsync("8hP9D6kZseM");
// // Youtube Playlist
// var data = await apiLib.WikipediaAsync("PLReL099Y5nRd28Yv6c-Am9qURCrLMxBmK");
// // AdvancedSearch
// var input = new AdvancedSearchInput();
// input.Genres = AdvancedSearchGenre.Action | AdvancedSearchGenre.Adventure;
// input.Sort = AdvancedSearchSort.User_Rating_Descending;
// input.ReleaseDateFrom = "2010-01-01";
// input.NumberOfVotesFrom = 5000;
// input.Languages = AdvancedSearchLanguage.English | AdvancedSearchLanguage.French;
// // OR - Multiple languages
// //input.LanguagesStr = $"{AdvancedSearchLanguage.English.GetDescription()},{AdvancedSearchLanguage.French.GetDescription()}";
// input.Countries = AdvancedSearchCountry.United_States;
// // OR - Multiple countries
// //input.CountriesStr = $"{AdvancedSearchCountry.United_States},{AdvancedSearchCountry.France},{AdvancedSearchCountry.United_Kingdom}";
// string queryString = input.ToString();
// var advancedSearchdata = await apiLib.AdvancedSearchAsync(input);