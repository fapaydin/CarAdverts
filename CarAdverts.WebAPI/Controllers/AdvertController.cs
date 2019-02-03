using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CarAdverts.Core.Entities;
using CarAdverts.Core.Interfaces;
using CarAdverts.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAdverts.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public AdvertController(IAdvertService advertService)
        {
            _advertService = advertService;
        }


        /// <summary>
        /// Adds a new car advert to the database and returns the entity with uri to it.
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Advert), (int)HttpStatusCode.Created)]
        public ActionResult<Advert> Add([FromBody] Advert advert)
        {
            var ad = _advertService.Add(advert);
            var url = Url.Link("FindById", new {id = ad.Id});
            return Created(url, ad);
        }

        /// <summary>
        /// Updates an advert with the given details.
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public IActionResult Update([FromBody]Advert advert)
        {
            _advertService.Update(advert);
            return NoContent();
        }


        /// <summary>
        /// Deletes an advert with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public IActionResult Delete(int id)
        {
            _advertService.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// List all car adverts on the system with ordering by the filter specified
        /// Default orderby value = id
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        [HttpGet("list/{orderBy?}", Name = "ListAdverts")]
        [ProducesResponseType(typeof(IEnumerable<Advert>), (int)HttpStatusCode.OK)]
        public IActionResult List(string orderBy = "id")
        {
            var list = _advertService.GetAll(orderBy);
            return Ok(list);
        }

        /// <summary>
        /// Gets the advert detail with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "FindById")]
        [ProducesResponseType(typeof(Advert), (int)HttpStatusCode.OK)]
        public ActionResult<Advert> FindById(int id)
        {

            var advert = _advertService.FindById(id);
            if (advert == null) return NotFound();
            return Ok(advert);
        }
        


    }
}