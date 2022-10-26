using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Royara_Faq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class FaqController : ControllerBase
    {
        private IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
        // GET: api/<FaqController>
        [HttpGet]
        public IActionResult Get()
        {
            var data =_faqService.GetAll();
            return Ok(data);
        }

        // GET api/<FaqController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var data = _faqService.GetById(id);

            return Ok(data);
        }

        
        
    }
}
