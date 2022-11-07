using BlueLight.NoteItem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueLight.NoteItem.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly NoteContext _noteContext;
        
        
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, NoteContext noteContext)
        {
            _logger = logger;
            _noteContext = noteContext;
        }

        [HttpGet]
        [Route("getWeather")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost(Name = "AddNoteItem")]
        public async Task<bool> AddNoteItem(Note note)
        {
           await   _noteContext.AddAsync(note);
            var result = _noteContext.SaveChanges() > 0;
            return result;

        }
        [HttpGet(Name = "GetAllNote")]
        public async Task<List<Note>> GetNotes()
        {
            return await _noteContext.Notes.ToListAsync();
        }


    }
}