using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAspNet.Entidades;

namespace WebApiAspNet.Controllers
{
	[ApiController]
	[Route("api/autores")]
	public class AutoresController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public AutoresController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Autor>>> Get()
		{
			return await context.Autores.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult> Post(Autor autor)
		{
			context.Add(autor);
			await context.SaveChangesAsync();
			return Ok();
		}

	}
}
