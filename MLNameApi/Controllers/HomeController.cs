using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MLNameClass;

namespace MLNameApi.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class HomeController: Controller
    {
        private readonly Name _name;
        public HomeController(Name name)
        {
            _name = name;
        }
        [HttpPost]
        public JsonResult Post([FromBody] InputName Input)
        {
            return new JsonResult(PredictName(Input.firstname));
        }
        public predictName PredictName(string firstname)
        {
            var Clasification= _name.PredictName(firstname);
            var predictName = new predictName
            {
                
                Male = Clasification.Prediction
            };
            return predictName;
            
        }
        public class predictName
        {
            public bool Male { get; set; }
        }
        public class InputName
        {
            public String firstname { get; set; }
        }
    }
}
