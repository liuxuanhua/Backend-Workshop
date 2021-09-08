
using Microsoft.AspNetCore.Mvc;

using MyMvc.Models;

namespace MyMvc.Controllers
{
    //[AllowAnonymous]
    public class ActionResultController : Controller
    {
        // GET: ActionResult
        public ActionResult Index()
        {
            //basic points 1 change "null" to correct value.
            ContentResult returnData = new ContentResult();
            returnData.Content = "1";
            return returnData;
        }

        public ActionResult Baidu()
        {
            //basic points 2 change "" to Redirect to www.baidu.com
            RedirectResult returnData = new RedirectResult("http://www.baidu.com");
            return returnData;
        }

        public ActionResult Page()
        {
            //basic points 3 change "" to correct value.
            string content = "this is content";
            ContentResult returnData = new ContentResult();
            returnData.Content = content;
            return returnData;
        }

        public ActionResult EmptyAction()
        {
            EmptyResult returnData = new EmptyResult();
            //basic points 4 change "null" to correct value.
            return returnData;
        }

        public ActionResult Redirect2Action()
        {
            //basic points 5 change null : Redirect to Baidu Action
            RedirectToActionResult returnData = new RedirectToActionResult("Baidu", "ActionResult", "");
            return returnData;
        }

        public ActionResult Redirect2Route()
        {
            //basic points 6 change null : Redirect to Page Route
            RedirectToRouteResult returnData = new RedirectToRouteResult("ActionResult");
            returnData.RouteName = "";
            return returnData;
        }

        public ActionResult JsonResult()
        {

            JsonObject json = new JsonObject("ActionResultController", "JsonResult");
            JsonResult returnData = new JsonResult(json);
            //basic points 7  change null to return a json obj
            return returnData;
        }

        public ActionResult ScriptResult()
        {
            ContentResult returnData = new ContentResult();
            returnData.Content = "<script><alert>hi,welcome to .net</alert></script>";
            returnData.ContentType = "application/javascript";
            //basic points 8 change null to return a script code
            return returnData;
        }

        public ActionResult HttpUnauthorizedResult()
        {
            //basic points 9 change "null" to correct value.
            UnauthorizedObjectResult result = new UnauthorizedObjectResult("Unauthorized");
            return result;
        }

        public ActionResult HttpNotFoundResult()
        {
            //basic points 10 change "null" to correct value.
            NotFoundResult returnData = new NotFoundResult();
            return returnData;
        }

        public ActionResult HttpStatusCodeResult()
        {
            //basic points 11 change "null" to correct value.
            StatusCodeResult returnData = new StatusCodeResult(405);
            return returnData;
        }
    }
}