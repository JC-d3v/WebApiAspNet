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

		//TODO: //api/autores/1
		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(Autor autor, int id)
		{
			if (autor.Id != id)
			{
				return BadRequest("El id del autor no coincide en con el Id de la URL");
			}
			var existe = await context.Autores.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound();
			}

			context.Update(autor);
			await context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id:int}")] //api/autores/2
		public async Task<ActionResult> Delete(int id)
		{
			var existe = await context.Autores.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound();
			}

			context.Remove(new Autor() { Id = id });
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
