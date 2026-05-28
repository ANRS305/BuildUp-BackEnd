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
    public class FornecedorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FornecedorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fornecedor>>> Get()
        {
            return await _context.Fornecedores.ToListAsync();
        }
    }
}