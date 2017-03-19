using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DbConnection;
using TodoList.Models;
namespace TodoList.Controllers
{
    public class ListController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        [Route("/list")]
        public JsonResult List()
        {  
            //Normally obviously I would use a orm and/or seperate these into the models but this is much quicker and I don't have as much experience with entity (I know this is vaulnarable to SQL injection).
            List<Dictionary<string, object>> AllItems = DbConnector.Query("SELECT * FROM items");
            return Json(AllItems);
        }

        
        [HttpPost]
        [Route("/list/{content}")]
        public void Create(string content)
        {
            System.Console.WriteLine("*************************");            
            System.Console.WriteLine(content);
            System.Console.WriteLine("*************************");
            DbConnector.Query($"INSERT INTO items (content, status, created_at, updated_at) VALUES ('{content}','unchecked', NOW(), NOW())");
            return;
        }
        [HttpPost]
        [Route("/delete/{Id}")]
        public void Remove(string Id)
        {
            DbConnector.Query($"DELETE FROM items WHERE id={Id}");
            return;
        }
        [HttpPost]
        [Route("/update/{Id}")]
        public void Update(string Id)
        {
            DbConnector.Query($"UPDATE items SET updated_at = NOW(),status = CASE WHEN status='unchecked' THEN 'checked' ELSE 'unchecked' END WHERE id = {Id}");
            return;
        }
    }
}