using DataAccess.Interfaces;
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Royara_Faq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ManageFaq",Roles = "SuperAdmin")]
    public class ManageFaqController : ControllerBase
    {
        private ILogger<ManageFaqController> _logger;

        private IFaqService _faqService;

        public ManageFaqController(ILogger<ManageFaqController> logger, IFaqService faqService)
        {
            _logger = logger;
            _faqService = faqService;
        }
        // GET: api/<FaqController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get All Data");
            _logger.LogInformation("***************************");
            _logger.LogWarning("Log Warning");
            var data = _faqService.GetAll();
            
            return Ok(data);
        }

        // GET api/<FaqController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var data = _faqService.GetById(id);

            return Ok(data);
        }

        // POST api/<FaqController>
        [HttpPost]
        public IActionResult Post([FromBody] AddFaqDto addFaqDto)
        {
            _faqService.Add(addFaqDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        //Delete 

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _faqService.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateFaqDto model)
        {
            _faqService.Update(id, model);
            return StatusCode(StatusCodes.Status202Accepted);
        }

    }
}
