using Api.Data;
using Api.DTOs.Account;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraduateController : ControllerBase
    {
        private readonly Context _context;

        public GraduateController(Context context)
        {
            _context = context;
        }

        

        [HttpPost("Graduate")]
        public async Task<ActionResult> CreateGraduate(GraduateAdd model)
        {
            var graduate = new Graduate
            {
                Id = model.Id,
                LevelStudy = model.LevelStudy,
                Program = model.Program,
                FaculityDivision = model.FaculityDivision,

            };
           if (ModelState.IsValid)
            {
                _context.Graduate.Add(graduate);
                _context.SaveChanges();
                return Ok(graduate);
            }
           return BadRequest(ModelState);
        }
    }
}
