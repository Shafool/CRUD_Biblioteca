using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
using System.Data;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySQl_Practica.CapaNegocio;

namespace MySQl_Practica
{
    public partial class MySQL_Test : System.Web.UI.Page
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;
        private static MySqlConnection conexion = new MySqlConnection(cadena);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Listar();
                ListarAutores();
                LlenarDropdowns();
                ListarPrestamos();
            }
        }
        protected void GridView_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        /* Libro */
        private void Listar()
        {
            Libro l = new Libro();
            DataTable tabla = l.Listar();
            gvLibros.DataSource = tabla;
            gvLibros.DataBind();
        }
        protected void btBuscar_Click(object sender, EventArgs e)
        {
            Libro l = new Libro();
            string busqueda = tbBusqueda.Text.Trim();

            // Buscar
            DataTable tabla = l.Buscar(busqueda);
            gvLibros.DataSource = tabla;
            gvLibros.DataBind();
        }
        protected void btListar_Click(object sender, EventArgs e) { Listar(); }
        protected void btEliminar_Click(object sender, EventArgs e)
        {
            Libro l = new Libro();
            string codLibro = tbCodigo.Text.Trim();

            // Eliminar
            l.Eliminar(codLibro);
            Listar();
        }
        protected void btActualizar_Click(object sender, EventArgs e)
        {
            Libro l = new Libro();
            string codLibro = tbCodigo.Text.Trim();
            string titulo = tbTitulo.Text.Trim();
            string editorial = tbEditorial.Text.Trim();

            // Actualizar
            string[] res = l.Actualizar(codLibro, titulo, editorial);
            Listar();
        }
        protected void btAgregar_Click(object sender, EventArgs e)
        {
            Libro l = new Libro();
            string codLibro = tbCodigo.Text.Trim();
            string titulo = tbTitulo.Text.Trim();
            string editorial = tbEditorial.Text.Trim();

            // Agregar
            string[] res = l.Agregar(codLibro, titulo, editorial);
            Listar();
        }
        

        /* Autor */
        private void ListarAutores()
        {
            Autor a = new Autor();
            DataTable tabla = a.Listar();
            gvAutores.DataSource = tabla;
            gvAutores.DataBind();
        }
        protected void btActualizarListaAutor_Click(object sender, EventArgs e) { ListarAutores(); }
        protected void btBuscarAutor_Click(object sender, EventArgs e)
        {
            Autor a = new Autor();
            string busqueda = tbBusqueda.Text.Trim();

            // Buscar
            DataTable tabla = a.Buscar(busqueda);
            gvAutores.DataSource = tabla;
            gvAutores.DataBind();
        }
        protected void btListarAutor_Click(object sender, EventArgs e) { ListarAutores(); }
        protected void btEliminarAutor_Click(object sender, EventArgs e)
        {
            Autor a = new Autor();
            string codAutor = tbCodAutor.Text.Trim();

            // Eliminar
            a.Eliminar(codAutor);
            ListarAutores();
        }
        protected void btActualizarAutor_Click(object sender, EventArgs e)
        {
            Autor a = new Autor();
            string codAutor = tbCodAutor.Text.Trim();
            string nombres = tbNomAutor.Text.Trim();
            string apellidos = tbApeAutor.Text.Trim();
            string nacionalidad = tbNacAutor.Text.Trim();

            // Actualizar
            string[] res = a.Actualizar(codAutor, nombres, apellidos, nacionalidad);
            Response.Write(res[1]);
            ListarAutores();
        }
        protected void btAgregarAutor_Click(object sender, EventArgs e)
        {
            Autor a = new Autor();
            string codAutor = tbCodAutor.Text.Trim();
            string nombres = tbNomAutor.Text.Trim();
            string apellidos = tbApeAutor.Text.Trim();
            string nacionalidad = tbNacAutor.Text.Trim();

            // Agregar
            string[] res = a.Agregar(codAutor, nombres, apellidos, nacionalidad);
            ListarAutores();
        }
        protected void btBuscarAutor_Click1(object sender, EventArgs e)
        {
            Autor a = new Autor();
            string busqueda = tbBusquedaAutor.Text.Trim();

            // Buscar
            DataTable tabla = a.Buscar(busqueda);
            gvAutores.DataSource = tabla;
            gvAutores.DataBind();
        }

        /* Prestamos */
        private void LlenarDropdowns()
        {
            // Limpiar dropdowns
            ddlAutores.Items.Clear();
            ddlLibros.Items.Clear();

            // Llenar datos
            Libro l = new Libro();
            DataTable tabla = l.Listar();
            
            foreach (DataRow fila in tabla.Rows)
                ddlLibros.Items.Add(fila["CodLibro"].ToString() + " | " + fila["Titulo"].ToString());
            
            Autor a = new Autor();
            DataTable tablaAut = a.Listar();

            foreach (DataRow fila in tablaAut.Rows)
                ddlAutores.Items.Add($"{fila["CodAutor"].ToString()} | {fila["Nombres"].ToString()} {fila["Apellidos"].ToString()}");
        }
        private string keyFromText(string str) {
            if (str == null) return "";
            return str.Split(new string[] { " | " }, StringSplitOptions.None)[0];
        }
        private void ListarPrestamos()
        {
            Prestamo p = new Prestamo();
            DataTable tabla = p.Listar();
            gvPrestamos.DataSource = tabla;
            gvPrestamos.DataBind();
        }
        protected void btEliminarPres_Click(object sender, EventArgs e)
        {
            Prestamo p = new Prestamo();
            string codLibro = keyFromText(ddlLibros.Text);

            // Eliminar
            p.Devolver(codLibro);
            ListarPrestamos();
        }
        protected void btAgregarPres_Click(object sender, EventArgs e)
        {
            Prestamo p = new Prestamo();
            string codLibro = keyFromText(keyFromText(ddlLibros.SelectedItem.ToString()));
            string codAutor = keyFromText(keyFromText(ddlAutores.SelectedItem.ToString()));
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");

            // Agregar
            string[] res = p.Prestar(codAutor, codLibro, fecha);

            Response.Write(res[1]);
            ListarPrestamos();

        }
        protected void btBuscarPres_Click1(object sender, EventArgs e)
        {
            Prestamo p = new Prestamo();
            string busqueda = tbBusquedaPres.Text.Trim();

            // Buscar
            DataTable tabla = p.Buscar(busqueda);
            gvPrestamos.DataSource = tabla;
            gvPrestamos.DataBind();
        }

        protected void btListarPres_Click(object sender, EventArgs e)
        {
            ListarPrestamos();
        }
    }
}