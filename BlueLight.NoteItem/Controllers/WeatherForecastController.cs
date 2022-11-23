using BlueLight.NoteItem.Models;
using BlueLight.NoteItem.Models.Relationship;
using BlueLight.NoteItem.Models.Relationship.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tools;

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
            //保证数据库处于最原始状态
            //noteContext.Database.EnsureDeleted();
            //noteContext.Database.EnsureCreated();
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

        //关联添加方式一
        [HttpPost]
        [Route("AddBlogPost")]
        public async Task<bool> AddBlogAndPost(BlogDto blog)
        {
           var blo1g=  await _noteContext.Blogs.AddAsync(new Blog() { Url = blog.Url });
            await _noteContext.Posts.AddAsync(new Post() { Title = "title1", Content = "conetne1", Blog = blo1g.Entity });           
            
            var result = _noteContext.SaveChanges() > 0;
            return result;
        }
      

        //关联添加方式二
        [HttpGet]
        [Route("AddBlogAndPost1111")]
        public async Task<bool> AddBlogAndPost()
        {
            var blog = new Blog() {
                Url = "333333" ,
                Posts = new List<Post>() {
                    new Post() {
                        Title = "333333string",
                        Content = "333333string"
                    },
                         new Post() {
                        Title = "4444",
                        Content = "444444"
                    }
                }

            };
            _noteContext.Blogs.Add(blog);
            var result = _noteContext.SaveChanges() > 0;
            return result;
        }

        /// <summary>
        /// 添加POST 同时保存博客
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AddPostandBlog")]
        public async Task<bool> AddPostandBlog()
        {
            var post = new Post() { Title = "title1", Content = "conetne1" };
            var blog = new Blog() { Url = "url1" };
            post.Blog = blog;
            _noteContext.Posts.Add(post);
            var result = _noteContext.SaveChanges() > 0;
            return result;
        }

        [HttpGet]
        [Route("DeletePost")]
        public async Task<bool> DeletePost()
        {
            var post = _noteContext.Posts.FirstOrDefault(c => c.Title == "title1");
                _noteContext.Posts.Remove(post);
            var result = _noteContext.SaveChanges()>0;
            return result;

        }

        [HttpPost]
        [Route("DeleteBlog")]
        public async Task<bool> DeleteBlog(string urls)
        {
            var bloe = _noteContext.Blogs.First(c => c.Url == urls);
            _noteContext.Blogs.Remove(bloe);
            var result = _noteContext.SaveChanges() > 0;
            return result;
        }



        [HttpGet(Name = "GetAllNote")]
        public async Task<List<Note>> GetNotes()
        {
            return await _noteContext.Notes.ToListAsync();
        }


        [HttpGet]
        [Route("SM4ECBeEncrypt")]
        public async Task<string> SM4ECBeEncrypt(string str)
        {
            return SM4Handler.SM4ECBeEncrypt(str);
            
        }
        
        [HttpGet]
        [Route("SM4ECBeDecrypt")]
        public async Task<string> SM4ECBeDecrypt(string str)
        {
            return SM4Handler.SM4ECBeDecrypt(str);
        }


        [HttpGet]
        [Route("SM4CBCEncrypt")]
        public async Task<string> SM4CBCEncrypt(string str)
        {
            return SM4Handler.SM4CBCEncrypt(str);

        }

        [HttpGet]
        [Route("SM4CBCDecrypt")]
        public async Task<string> SM4CBCDecrypt(string str)
        {
            return SM4Handler.SM4CBCDecrypt(str);
        }


    }
}