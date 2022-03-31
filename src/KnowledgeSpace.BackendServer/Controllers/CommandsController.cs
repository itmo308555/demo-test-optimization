using KnowledgeSpace.BackendServer.Data;
using KnowledgeSpace.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeSpace.BackendServer.Controllers
{
    public class CommandsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CommandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommands()
        {
            // gan them 20 quyen khac nhau cho user (10 quyen cua identity, 10 quyen gan them
            var user = User.Identity.Name;
            // sử dung AsNoTracking -< chỉ đọc mà không ghi --> Tối ưu hóa việc truy cập dữ liệu
            var commands = _context.Commands.AsNoTracking();

            var commandVms = await commands.Select(u => new CommandVm()
            {
                Id = u.Id,
                Name = u.Name,
            }).ToListAsync();

            return Ok(commandVms);
        }
    }
}