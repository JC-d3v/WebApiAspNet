using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAspNet.Entidades;

namespace WebApiAspNet.Controllers
{
	[ApiController]
	[Route("api/autores")]
	public class AutoresController : ControllerBase
	{
		[HttpGet]
		public ActionResult<List<Autor>> Get()
		{
			return new List<Autor>() {
				new Autor () {Id = 1, Nombre = "Jorge"},
				new Autor () {Id = 2, Nombre = "Fernando"}
			};
		}
	}
}
