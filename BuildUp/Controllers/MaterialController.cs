using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildUp.Data;
using BuildUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaterialController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Material>>> Get()
        {
            return await _context.Materiais
                .Include(p => p.Fornecedor)
                .ToListAsync();
        }
    }
}