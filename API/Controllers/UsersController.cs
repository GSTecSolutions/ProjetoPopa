using System.Collections.Generic;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> PegarTodosAsync()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<User>> PegarPeloId(int userId)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user == null)
                NotFound();

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> SalvarUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{UserId}")]
        public async Task<ActionResult> DeletarUserAsync(int userId)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}