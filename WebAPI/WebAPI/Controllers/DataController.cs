using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IGetDataInterface _getDataService;
        private readonly IFormSubmitInterface _formSubmitService;

        public DataController(IGetDataInterface getDataService, IFormSubmitInterface formSubmitService)
        {
            _getDataService = getDataService;
            _formSubmitService = formSubmitService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_getDataService.Get());

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var ksiazka = _getDataService.GetByID(id);
            if (ksiazka == null) return NotFound();

            return Ok(ksiazka);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Ksiazka ksiazka)
        {
            _formSubmitService.Add(ksiazka);
            return Ok(true);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Ksiazka ksiazka)
        {
            _formSubmitService.Edit(ksiazka);
            return Ok(true);
        }
    }
}