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
    public class Autor
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static MySqlConnection conexion = new MySqlConnection(cadena);
        public string[] Actualizar(string codAutor, string nombres, string apellidos, string nacionalidad)
        {
            string[] respuesta = { "", "" };
            try
            {
                string consulta = "update tautor set " +
                    "apellidos = @apellidos, " +
                    "nombres = @nombres, " +
                    "nacionalidad = @nacionalidad " +
                    "where CodAutor like @CodAutor";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                // Envio de parametros
                comando.Parameters.AddWithValue("@CodAutor", codAutor);
                comando.Parameters.AddWithValue("@nombres", nombres);
                comando.Parameters.AddWithValue("@apellidos", apellidos);
                comando.Parameters.AddWithValue("@nacionalidad", nacionalidad);

                // Ejecutar
                conexion.Open();
                byte opeExitosa = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();

                if (opeExitosa != 1)
                {
                    respuesta[0] = "0";
                    respuesta[1] = "Autor" + nombres + " actualizado";
                }
            }
            catch (Exception ex)
            {

                conexion.Close();
                respuesta[0] = "1";
                respuesta[1] = "Hubo un error al actualizar el autor con cón código: " + codAutor;

            }
            return respuesta;

        }

        public string[] Agregar(string codAutor, string nombres, string apellidos, string nacionalidad)
        {
            string[] respuesta = { "", "" };
            try
            {
                string consulta = "insert into tautor values(@codAutor, @apellidos, @nombres, @nacionalidad)";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);

                // Envio de parametros
                comando.Parameters.AddWithValue("@codAutor", codAutor);
                comando.Parameters.AddWithValue("@nombres", nombres);
                comando.Parameters.AddWithValue("@apellidos", apellidos);
                comando.Parameters.AddWithValue("@nacionalidad", nacionalidad);

                // Ejecutar
                conexion.Open();
                byte opeExitosa = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();

                if (opeExitosa != 1)
                {
                    respuesta[0] = "0";
                    respuesta[1] = "Autor" + nombres + " registrado con código " + codAutor;
                }
            }
            catch (Exception ex)
            {
                conexion.Close();
                respuesta[0] = "1";
                respuesta[1] = "Hubo un error al registrar el autor con cón código: " + codAutor;
            }
            return respuesta;
        }

        public string[] Eliminar(string codAutor)
        {
            string[] respuesta = { "", "" };
            try
            {
                string consulta = $"delete from tautor where CodAutor = '{codAutor}'";
                Console.WriteLine(consulta);
                MySqlCommand comando = new MySqlCommand(consulta, conexion);

                conexion.Open();
                byte opeExitosa = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();

                if (opeExitosa != 1)
                {
                    respuesta[0] = "0";
                    respuesta[1] = "Eliminado exitosamente";
                }

            }
            catch (Exception ex)
            {
                conexion.Close();
                respuesta[0] = "1";
                respuesta[1] = "Hubo un error al eliminar el autor con cón código: " + codAutor;
            }

            return respuesta;
        }

        public DataTable Listar()
        {
            string consulta = "select * from tautor";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
        public DataTable Buscar(string texto)
        {
            string consulta = $"select * from tautor where codAutor like '%{texto}%' " +
                $"or nombres like '%{texto}%' " +
                $"or nacionalidad like '%{texto}%' " +
                $"or apellidos like '%{texto}%' ";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            return tabla;
        }
    }
}