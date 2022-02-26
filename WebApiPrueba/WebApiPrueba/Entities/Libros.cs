using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPrueba.Entities
{
    public class Libros
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantPaginas { get; set; }
        public int IdAutor { get; set; }
    }
}
