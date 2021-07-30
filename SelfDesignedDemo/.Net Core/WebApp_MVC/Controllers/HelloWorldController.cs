using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebApp_MVC.Controllers
{
    //控制器中的每个 public 方法均可作为 HTTP 终结点调用
    //HTTP 终结点是 Web 应用程序中可定向的 URL
    //（例如 https://localhost:5001/HelloWorld），
    //其中结合了所用的协议 HTTPS、TCP 端口等 Web 服务器的网络位置 localhost:5001，以及目标 URI HelloWorld。
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        public string Index1()
        {
            return "This is my default action...";
        }
        public IActionResult Index()
        {
            return View();
        }
        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome1(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
