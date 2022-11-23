using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlueLight.NoteItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("testGenerics")]
        public IActionResult Get()
        {
            var type = typeof(Test);
            //通过反射 创建type对象
            var instance = Activator.CreateInstance(type);

            var fullName = type.FullName;
            //通过反射获取type中的Name属性并赋值
            var property = type.GetProperty("Name");
            property.SetValue(instance, "123123");
            var propertyValue =(string) property.GetValue(instance);

            //创建计时器





            var record = new Result(fullName, propertyValue);
            return Ok(record);

        }

        
    }

    public record Result(string fullname, string value);


    public class Test
    {
        public string Name { get; set; }
    }
}
