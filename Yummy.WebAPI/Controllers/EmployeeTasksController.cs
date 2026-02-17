using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yummy.WebAPI.Context;
using Yummy.WebAPI.Dtos.EmployeeTaskDto;
using Yummy.WebAPI.Entities;

namespace Yummy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly YummyContext _context;
        private readonly IMapper _mapper;

        public EmployeeTasksController(YummyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeTaskList()
        {
            var values = await _context.EmployeeTasks.Include(x => x.ChefTasks).ThenInclude(y => y.Chef).ToListAsync();
            return Ok(_mapper.Map<List<ResultEmployeeTaskDto>>(values));
        }

        [HttpGet("GetEmployeeTaskById")]
        public async Task<IActionResult> GetEmployeeTaskById(int id)
        {
            var value = await _context.EmployeeTasks.Include(x => x.ChefTasks).FirstOrDefaultAsync(x => x.EmployeeTaskId == id);
            if (value == null) return NotFound();
            return Ok(_mapper.Map<GetEmployeeTaskByIdDto>(value));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeTask(CreateEmployeeTaskDto dto)
        {
            var task = _mapper.Map<EmployeeTask>(dto);
            task.ChefTasks = dto.ChefIds.Select(id => new ChefTask { ChefId = id }).ToList();

            _context.EmployeeTasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok("Görev başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeTask(UpdateEmployeeTaskDto dto)
        {
            var task = await _context.EmployeeTasks
                .Include(x => x.ChefTasks)
                .FirstOrDefaultAsync(x => x.EmployeeTaskId == dto.EmployeeTaskId);

            if (task == null) return NotFound();

            _mapper.Map(dto, task);

            _context.ChefTasks.RemoveRange(task.ChefTasks);
            task.ChefTasks = dto.ChefIds.Select(id => new ChefTask
            {
                ChefId = id,
                EmployeeTaskId = dto.EmployeeTaskId
            }).ToList();

            await _context.SaveChangesAsync();
            return Ok("Güncellendi");
        }


        [HttpPost("CreateBulkTasks")]
        public async Task<IActionResult> CreateBulkTasks(List<CreateEmployeeTaskDto> dtos)
        {
            foreach (var dto in dtos)
            {
                var task = _mapper.Map<EmployeeTask>(dto);
                task.ChefTasks = dto.ChefIds.Select(id => new ChefTask { ChefId = id }).ToList();
                _context.EmployeeTasks.Add(task);
            }
            await _context.SaveChangesAsync();
            return Ok("Kayıtlar başarıyla eklendi!");
        }
    }
}
