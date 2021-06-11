using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MySQl_Practica.CapaNegocio
{
    public class Prestamo
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static MySqlConnection conexion = new MySqlConnection(cadena);

        public string[] Prestar(string CodAutor, string CodLibro, string FechaPrestamo)
        {
            string[] respuesta = { "", "" };
            string c = "";
            try
            {
                string consulta = "insert into tprestamo values(@CodAutor, @CodLibro, @FechaPrestamo)";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                c = consulta;
                //envio de parametros
                comando.Parameters.AddWithValue("@CodAutor", CodAutor);
                comando.Parameters.AddWithValue("@CodLibro", CodLibro);
                comando.Parameters.AddWithValue("@FechaPrestamo", FechaPrestamo);
                
                // Ejecutar
                conexion.Open();
                byte opeExitosa = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();

                if (opeExitosa != 1)
                {
                    respuesta[0] = "0";
                    respuesta[1] = $"Libro: {CodLibro} ha sido prestado en {FechaPrestamo}";
                }
            }
            catch (Exception ex)
            {

                conexion.Close();
                respuesta[0] = "1";
                respuesta[1] = $"Hubo un error al prestar el libro: {CodLibro}";

                respuesta[1] = $"{CodAutor} *** {CodLibro} *** {c} *** { ex.ToString() }";
            }
            return respuesta;

        }

        public string[] Devolver(string codLibro)
        {
            string[] respuesta = {"", ""};
            try
            {
                string consulta = $"delete from tprestamo where codLibro = '{codLibro}'";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                
                conexion.Open();
                byte opeExitosa = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();

                if (opeExitosa != 1)
                {
                    respuesta[0] = "0";
                    respuesta[1] = "La devolución ha sido registrada";
                }
                
            }
            catch (Exception ex)
            {
                conexion.Close(); 
                respuesta[0] = "1";
                respuesta[1] = "Hubo un error al registrar la devolucion del libro: " + codLibro;
            }

            return respuesta;
        }

        public DataTable Listar()
        {
            string consulta =   "select " +
                                "tlibro.titulo Titulo, " +
                                "concat(tautor.nombres, ' ', tautor.Apellidos) Autor, " +
                                "tprestamo.fechaPrestamo " +
                                
                                "from tprestamo " +
                                
                                "inner join tlibro on tlibro.CodLibro = tprestamo.CodLibro " +
                                "inner join tautor on tautor.CodAutor = tprestamo.CodAutor";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
        public DataTable Buscar(string texto)
        {
            string consulta = $"select * from tlibro where codLibro like '%{texto}%' " +
                $"or titulo like '%{texto}%' " +
                $"or editorial like '%{texto}%' ";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }
}