using System;

namespace AzureFunc.Models;

public class Game
{
    public int Id { get; set; }
    public int Year { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Continent { get; set; }
    public string Season { get; set; }
    public DateTime Opening { get; set; }
    public DateTime Closing { get; set; }

}
