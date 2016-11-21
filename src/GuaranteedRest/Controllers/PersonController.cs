using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Cheatham.GuaranteedConsole.TextFileParser;
using CoreTextFileParser;
using CoreTextFileParser.Entities;

namespace GuaranteedRest.Controllers
{
    [Route("api/person")]
    public class PersonController : Controller
    {
        // GET api/person
        [HttpGet]
        public List<Person> Get()
        {
            return ParseFile.Parse("PipeDelimited", "gender", "asc", '|');
            //return null;
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public List<Person> Get(string id)
        {
            if (id == "gender")
            {
                return ParseFile.Parse("PipeDelimited", "gender", "asc", '|');
            }
            else if (id == "birth")
            {
                return ParseFile.Parse("CommaDelimited", "birth", "asc", ',');
            }
            else if (id == "lastname")
            {
                return ParseFile.Parse("SpaceDelimited", "lastname", "desc", ' ');
            }
            return null;
        }

        //PostMan Sample Request
        //http://localhost:50507/api/person
        //Body Type Set To application/json which will add a header to the request for Content-Type: application/json

        //{
        //"FirstName": "George",
        //"LastName": "Costanza",
        //"Gender": "Male",
        //"FavoriteColor": "Orange",
        //"DateOfBirth": "9/23/1959"
        //}

        // POST api/person
        [HttpPost]
        public IActionResult Post([FromBody]Person newPerson)
        {
            if (newPerson == null)
            {
                return BadRequest();
            }

            //Do Not Use Static Method For Posting Data
            //TODO : Accept Another Parameter For The File Type To Save To
            CreateNewInTextFile async = new CreateNewInTextFile();
            if (async.CreateNewRecord("PipeDelimited", newPerson))
            {
                return CreatedAtRoute("Get", new { id = "PipeDelimited" });
            }
            else
            {
                return BadRequest();
            }
        }

        //// PUT api/person/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/person/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
