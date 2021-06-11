<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySQL_Test.aspx.cs" Inherits="MySQl_Practica.MySQL_Test" EnableViewState="true"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	<!-- CSS only -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-+0n0xVW2eSR5OomGNYDnhzAbDsOXxcvSN1TPprVMTNDbiYZCxYbOOl7+AMvyTG2x" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
	<h2 class="text-center mb-3">Sistema de Biblioteca</h2>
	<hr class="mb-4">




	<div class="container py-4">
	<div class="row">
	<div class="col-md-12"> 

	<div class="row justify-content-center">

		<div class="col-md-6">
		
				

				<div class="card card-outline-secondary">
					<div class="card-header">
						<h3 class="mb-0">Libros</h3>
					</div>
					<div class="card-body">

						<!-- Formulario -->
						<div autocomplete="off" class="form" id="formLogin" name="formLogin" role="form">
							<div class="form-group">
								<label for="uname1">Codigo</label> 
								<asp:TextBox class="form-control" ID="tbCodigo" runat="server"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Titulo</label> 
								<asp:TextBox class="form-control" ID="tbTitulo" runat="server"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Editorial</label> 
								<asp:TextBox class="form-control" ID="tbEditorial" runat="server"></asp:TextBox>
							</div>
					
							<div class="form-group py-1 mt-2">
								<asp:Button ID="btAgregar"		class="btn btn-success btn-lg float-right"	runat="server" Text="Agregar Libro" OnClick="btAgregar_Click" />
								<asp:Button ID="btActualizar"	class="btn btn-outline-warning btn-lg float-right"	runat="server" Text="Actualizar Libro" OnClick="btActualizar_Click" />
							</div>
							<div class="form-group py-1 mb-2">
								<asp:Button ID="btListar"		class="btn btn-outline-primary btn-lg float-right"	runat="server" Text="Actualizar Lista" OnClick="btListar_Click" />
								<asp:Button ID="btEliminar"		class="btn btn-outline-danger btn-lg float-right"	runat="server" Text="Eliminar" OnClick="btEliminar_Click" />
							</div>
						</div>

						<!-- Listado -->
						<h4>Busqueda</h4>
						<hr />
						<div>
							<div class="input-group mb-3">
								<asp:TextBox ID="tbBusqueda" class="form-control" runat="server"></asp:TextBox>
					  
							  <div class="input-group-append">
								  <asp:Button ID="btBuscar" class="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btBuscar_Click" />
							  </div>
							</div>
							<div>
								<asp:GridView ID="gvLibros" runat="server" OnPreRender="GridView_PreRender" CssClass="table table-hover table-striped"></asp:GridView>
							</div>
						</div>

					</div>
				</div>

		</div>

		<div class="col-md-6">

				<div class="card card-outline-secondary">
					<div class="card-header">
						<h3 class="mb-0">Autores</h3>
					</div>
					<div class="card-body">

						<!-- Formulario -->
						<div autocomplete="off" class="form" id="formLogin" name="formLogin" role="form">
							<div class="form-group">
								<label for="uname1">Codigo</label> 
								<asp:TextBox class="form-control" ID="tbCodAutor" runat="server"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Nombres</label> 
								<asp:TextBox class="form-control" ID="tbNomAutor" runat="server"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Apellidos</label> 
								<asp:TextBox class="form-control" ID="tbApeAutor" runat="server"></asp:TextBox>
							</div>
							<div class="form-group">
								<label>Nacionalidad</label>
								<asp:TextBox class="form-control" ID="tbNacAutor" runat="server"></asp:TextBox>
							</div>
					
							<div class="form-group py-1 mt-2">
								<asp:Button ID="btAgregarAutor"		class="btn btn-success btn-lg float-right"	runat="server" Text="Agregar Autor" OnClick="btAgregarAutor_Click"/>
								<asp:Button ID="btActualizarAutor"	class="btn btn-outline-warning btn-lg float-right"	runat="server" Text="Actualizar Autor" OnClick="btActualizarAutor_Click"/>
							</div>
							<div class="form-group py-1 mb-2">
								<asp:Button ID="btActualizarListaAutor"		class="btn btn-outline-primary btn-lg float-right"	runat="server" Text="Actualizar Lista" OnClick="btActualizarListaAutor_Click" />
								<asp:Button ID="btEliminarAutor"		class="btn btn-outline-danger btn-lg float-right"	runat="server" Text="Eliminar" OnClick="btEliminarAutor_Click"  />
							</div>
						</div>

						<!-- Listado -->
						<h4>Busqueda</h4>
						<hr />
						<div>
							<div class="input-group mb-3">
								<asp:TextBox ID="tbBusquedaAutor" class="form-control" runat="server"></asp:TextBox>
					  
							  <div class="input-group-append">
								  <asp:Button ID="btBuscarAutor" class="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btBuscarAutor_Click1"/>
							  </div>
							</div>
							<div>
								<asp:GridView ID="gvAutores" runat="server" OnPreRender="GridView_PreRender" CssClass="table table-hover table-striped"></asp:GridView>
							</div>
						</div>

					</div>
				</div>

		</div>

	</div>

		<div class="row justify-content-center">

		<div class="col-md-6">
				<div class="card card-outline-secondary">
					<div class="card-header">
						<h3 class="mb-0">Prestamos</h3>
					</div>
					<div class="card-body">

						<!-- Formulario -->
						<div autocomplete="off" class="form" id="formLogin" name="formLogin" role="form">
							<div class="form-group">
								<label for="uname1">Libros</label>
								<asp:DropDownList ID="ddlLibros" class="form-control" runat="server"></asp:DropDownList>
							</div>
							<div class="form-group">
								<label>Autores</label> 
								<asp:DropDownList ID="ddlAutores" class="form-control" runat="server"></asp:DropDownList>
							</div>
					
							<div class="form-group py-1 mt-2">
								<asp:Button ID="btAgregarPres"		class="btn btn-primary btn-lg float-right"	runat="server" Text="Prestar Libro" OnClick="btAgregarPres_Click"/>
								<asp:Button ID="btEliminarPres"		class="btn btn-success btn-lg float-right"	runat="server" Text="Registrar como devuelto" OnClick="btEliminarPres_Click"  />
							</div>
							<div class="form-group py-1 mb-2">
								<asp:Button ID="btListarPres"		class="btn btn-outline-primary btn-lg float-right"	runat="server" Text="Actualizar Lista" OnClick="btListarPres_Click" />
							</div>
						</div>

						<!-- Listado -->
						<h4>Busqueda</h4>
						<hr />
						<div>
							<div class="input-group mb-3">
								<asp:TextBox ID="tbBusquedaPres" class="form-control" runat="server"></asp:TextBox>
					  
							  <div class="input-group-append">
								  <asp:Button ID="btBusquedaPres" class="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btBuscar_Click" />
							  </div>
							</div>
							<div>
								<asp:GridView ID="gvPrestamos" runat="server" OnPreRender="GridView_PreRender" CssClass="table table-hover table-striped"></asp:GridView>
							</div>
						</div>

					</div>
				</div>

		</div>

	</div>
	</div>
	</div>
	</div>





    </form>
</body>
</html>
