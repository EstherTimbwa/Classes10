namespace Classes10.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open System.Diagnostics
open System.IO;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

open Classes10.Models

type HomeController (logger : ILogger<HomeController>) =
    inherit Controller()

    member this.Index () =
        this.View()
       
      [ActionNameAttribute("SendImage")]
        member this.Image () =
        this.View("SendImage");

        [HttpPostAttribute]
        member IActionResult SendImage()
        {
            var file = Request.Form.Files.GetFile("file");
            FileStream stream = new FileStream("wwwroot/file.jpg",
            FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            return View();
        }
        
        member this.Privacy () =
        this.View()

    [<ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)>]
    member this.Error () =
        let reqId = 
            if isNull Activity.Current then
                this.HttpContext.TraceIdentifier
            else
                Activity.Current.Id

        this.View({ RequestId = reqId })
