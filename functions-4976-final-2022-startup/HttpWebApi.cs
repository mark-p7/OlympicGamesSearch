using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using AzureFunc.Data;
using AzureFunc.Models;

namespace Snoopy.Function
{
    public class HttpWebApi
    {
        private readonly OlympicsContext _context;

        public HttpWebApi(OlympicsContext context)
        {
            _context = context;
        }

        // Get games
        [FunctionName("GetGames")]
        public IActionResult GetPlayers(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "games")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP GET/posts trigger function processed a request in GetPlayers().");

            var players = _context.Players.ToArray();

            return new OkObjectResult(players);
        }

        // Get game by id
        [FunctionName("GetGameById")]
        public IActionResult GetGameById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "games/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP GET/posts trigger function processed a request in GetGameById().");

            var game = _context.Players.FirstOrDefault(x => x.Id == id);

            return new OkObjectResult(game);
        }

        // // Get games by continent
        // [FunctionName("GetGamesByContinent")]
        // public IActionResult GetGamesByContinent(
        //     [HttpTrigger(AuthorizationLevel.Function, "get", Route = "games/continent/{continent}")] HttpRequest req,
        //     ILogger log, string continent)
        // {
        //     var games = _context.Players.Where(x => x.Continent == continent).ToArray();

        //     return new OkObjectResult(games);
        // }

        // Creates a game
        [FunctionName("CreateGame")]
        public async Task<IActionResult> CreateGame(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "games")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var game = JsonConvert.DeserializeObject<Game>(requestBody);

            _context.Players.Add(game);
            _context.SaveChanges();

            return new OkObjectResult(game);
        }

        // Updates a game
        [FunctionName("UpdateGame")]
        public async Task<IActionResult> UpdateGame(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "games/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP PUT/posts trigger function processed a request in UpdateGame().");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var game = JsonConvert.DeserializeObject<Game>(requestBody);

            var gameToUpdate = _context.Players.FirstOrDefault(x => x.Id == id);

            if (gameToUpdate != null)
            {
                gameToUpdate.Id = game.Id;
                gameToUpdate.Year = game.Year;
                gameToUpdate.City = game.City;
                gameToUpdate.Country = game.Country;
                gameToUpdate.Continent = game.Continent;
                gameToUpdate.Season = game.Season;
                gameToUpdate.Opening = game.Opening;
                gameToUpdate.Closing = game.Closing;
                _context.SaveChanges();
            }

            return new OkObjectResult(gameToUpdate);
        }

        // Deletes game by id
        [FunctionName("DeleteGame")]
        public IActionResult DeleteGame(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "games/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation("C# HTTP DELETE/posts trigger function processed a request in DeleteGame().");

            var gameToDelete = _context.Players.FirstOrDefault(x => x.Id == id);

            if (gameToDelete != null)
            {
                _context.Players.Remove(gameToDelete);
                _context.SaveChanges();
            }

            return new OkObjectResult(gameToDelete);
        }

    }
}
