using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MySQl_Practica.CapaNegocio
{
    interface ILibro{
		DataTable Listar();
		DataTable Buscar(string texto, string criterio);
		bool Agregar(string codLibro, string titulo, string editorial);
		bool Eliminar(string codLibro);
		bool Actualizar(string codLibro, string titulo, string editorial);
	}
}