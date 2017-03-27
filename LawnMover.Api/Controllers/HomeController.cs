using System.Threading.Tasks;
using System.Web.Mvc;

namespace LawnMower.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //var uri = "api/rotation"; //Substitute "hello/random" for the ServiceStack self-hosted example and "api/hello/random" for the ServiceStack ASP.Net hosted example
            ////return await client.GetAsync(uri, requestOptions);
            //var values = new Dictionary<string, string> { { "direction", "North" } };
            //var content = new FormUrlEncodedContent(values);
            //return await client.PutAsync(uri, content);
            //using (HttpClient client = new HttpClient())
            //{
            //    var uri = "http://localhost:25522/api/rotation";
            //    var values = new Dictionary<string, string> { { "direction", "North" } };
            //    var content = new FormUrlEncodedContent(values);
            //    await client.PutAsync(uri, content);
            //}
            return View();
        }
    }
}
