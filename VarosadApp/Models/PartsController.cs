using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Varuosad.Models;


namespace Varuosad.Models
{
    [Route("[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Part> Get([FromQuery] QueryParams queryParams)
        {
            Console.WriteLine(queryParams.Page);
            List<Part> parts = new List<Part>();
            var path = @"./LE.txt"; 

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.SetDelimiters(new string[] { "\t" });
                csvParser.HasFieldsEnclosedInQuotes = true;

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    var part = new Part();
                    part.SerialNumber = fields[0];
                    part.Product_Decription = fields[1];
                    part.Price = Double.Parse(fields[8]);
                    part.Car = fields[9];
                    part.Vat_Included = Double.Parse(fields[10]);
                    parts.Add(part);

                }

            }

            var query = parts.AsQueryable();
            if (queryParams.Sort.ToLower() == "-price")
            {
                query = query.OrderBy(part => part.Price);
                query = query.Skip(60).Take(30);
                return query.ToList();
            }
            if (queryParams.Sort.ToLower() == "price")
            {
                query = query.OrderByDescending(part => part.Price);
                query = query.Skip(60).Take(30);
                return query.ToList();
            }
            if (queryParams.Sort.ToLower() == "-Price")
            {
                query = query.OrderByDescending(part => part.Product_Decription);
                query = query.Skip(60).Take(30);
                return query.ToList();
            }
            else
            {
                var CurrentPageContents = parts
                    .Skip(queryParams.PageSize * queryParams.Page - 1)
                    .Take(queryParams.PageSize);
                return CurrentPageContents;
            }
        }
    }
}
