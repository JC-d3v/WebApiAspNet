﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiAspNet.Validaciones;

namespace WebApiAspNet.Entidades
{
	public class Autor
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 5, ErrorMessage = "El campo {0} excede la cantidad limite de {1} caracteres")]
		[PrimeraLetraMayuscula]
		public string Nombre { get; set; }
		[Range(18, 120)]
		[NotMapped]
		public int Edad { get; set; }
		[CreditCard]
		[NotMapped]
		public string TarjetasDeCredito { get; set; }
		[Url]
		[NotMapped]
		public string URL { get; set; }
		public List<Libro> Libros { get; set; }
	}
}
