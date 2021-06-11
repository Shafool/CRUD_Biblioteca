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
    public class Libro
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static MySqlConnection conexion = new MySqlConnection(cadena);
        public string[] Actualizar(string codLibro, string titulo, string editorial)
        {
            string[] respuesta = { "", "" };
            string c;
            try
            {
                string consulta = "update tlibro set " +
                    "titulo = @titulo, " +
                    "editorial = @editorial " +
                    "where codLibro like @codLibro";
                

                MySqlCommand comando = new MySqlCommand(consulta, conexion);

                // Envio de parametros
                comando.Parameters.AddWithValue("@codLibro", codLibro);
                comando.Parameters.AddWithValue("@titulo", titulo);
                comando.Parameters.AddWithValue("@editorial", editorial);

                // Ejecutar
                conexion.Open();
                byte opeExitosa = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();

                if (opeExitosa != 1)
                {
                    respuesta[0] = "0";
                    respuesta[1] = titulo + " actualizado";
                }
            }
            catch (Exception ex)
            {

                conexion.Close();
                respuesta[0] = "1";
                respuesta[1] = "Hubo un error al actualizar el libro con cón código: " + codLibro;
            }
            return respuesta;

        }

        public string[] Agregar(string codLibro, string titulo, string editorial)
        {
            string[] respuesta = { "", "" };
            try
            {
                string consulta = "insert into tlibro values(@codLibro,@titulo,@editorial)";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);

                //envio de parametros
                comando.Parameters.AddWithValue("@codLibro", codLibro);
                comando.Parameters.AddWithValue("@titulo", titulo);
                comando.Parameters.AddWithValue("@editorial", editorial);
                
                // Ejecutar
                conexion.Open();
                byte opeExitosa = Convert.ToByte(comando.ExecuteNonQuery());
                conexion.Close();

                if (opeExitosa != 1)
                {
                    respuesta[0] = "0";
                    respuesta[1] = titulo + " registrado con código " + codLibro;
                }
            }
            catch (Exception ex)
            {

                conexion.Close();
                respuesta[0] = "1";
                respuesta[1] = "Hubo un error al registrar el libro con cón código: " + codLibro;
            }
            return respuesta;

        }

        public string[] Eliminar(string codLibro)
        {
            string[] respuesta = {"", ""};
            try
            {
                string consulta = $"delete from tlibro where codLibro = '{codLibro}'";
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
                respuesta[1] = "Hubo un error al eliminar el libro con cón código: " + codLibro;
            }

            return respuesta;
        }

        public DataTable Listar()
        {
            string consulta = "select * from tlibro";
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