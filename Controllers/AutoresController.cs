﻿using System;
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

		[HttpGet] // api/autores
		[HttpGet("listado")] // api/autores/listado
		[HttpGet("/listado")] // listado
		public async Task<ActionResult<List<Autor>>> Get()
		{
			return await context.Autores.Include(x => x.Libros).ToListAsync();
		}

		[HttpGet("primero")]  //api/autores/primero?nombre=jorge&otroparam=otroValor
		public async Task<ActionResult<Autor>> PrimerAutor([FromHeader] int miValor, [FromQuery] string nombre)
		{
			return await context.Autores.FirstOrDefaultAsync();
		}

		//TODO: variables de ruta
		// [HttpGet("{id:int}/{param2}")]  segundo parametro
		// [HttpGet("{id:int}/{param2?}")] segundo paramentro opcional
		// [HttpGet("{id:int}/{param2=persona}")] segundo paramentro por defecto
		[HttpGet("{id:int}")]
		public async Task<ActionResult<Autor>> Get(int id)
		{
			var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
			if (autor == null)
			{
				return NotFound();
			}
			return autor;
		}

		[HttpGet("{nombre}")]
		public async Task<ActionResult<Autor>> Get([FromRoute] string nombre) // Parametros desde ruta
		{
			var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
			if (autor == null)
			{
				return NotFound();
			}
			return autor;
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Autor autor) // Parametros desde el body
		{
			var existeAutorConElMismoNombre = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

			if (existeAutorConElMismoNombre)
			{
				return BadRequest($"Ya existe un autor con el nombre {autor.Nombre} ");
			}

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
