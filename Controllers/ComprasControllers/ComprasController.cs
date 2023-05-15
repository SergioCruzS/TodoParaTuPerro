using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using TodoParaTuPerro.Models;
using TodoParaTuPerro.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace TodoParaTuPerro.Controllers.ComprasControllers
{

    public class ComprasController : Controller
    {

        //Método constructor del Controlador
        public ComprasController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }




        //--------------------------------------- COMPRAS --------------------------------------------------------//
        //Método para mostrar la tabla de compras
        public IActionResult Compras()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Tcompras", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd); //Ejecutar la sentencia
                    DataTable dt = new();  //Crear una tabla
                    da.Fill(dt); //Llenamos la tabla con los datos que trae la consulta
                    da.Dispose(); //.Disposed() lo que hace es liberar los recursos del sistema que esté utilizando el componente al que hace referencia
                    List<ComprasModel> lista = new();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new ComprasModel()
                        {
                            Id_compra = Convert.ToInt32(dt.Rows[i][0]),
                            FechaCompra = ((DateTime)dt.Rows[i][1]),
                            DireccionEnvio = Convert.ToString(dt.Rows[i][2]),
                            FechaEntrega = ((DateTime)dt.Rows[i][3]),
                            CantidadArticulos = Convert.ToInt32(dt.Rows[i][4]),
                            FormaPago = Convert.ToString(dt.Rows[i][5]),
                            PrecioArticulo = (float)Convert.ToDouble(dt.Rows[i][6]),
                            TotalCompra = (float)Convert.ToDouble(dt.Rows[i][7]),
                            NumAutorizacion = Convert.ToInt32(dt.Rows[i][8]),
                            Id_empleado = Convert.ToInt32(dt.Rows[i][9]),
                            Id_producto = Convert.ToInt32(dt.Rows[i][10]),
                            Id_proveedor = Convert.ToInt32(dt.Rows[i][11]),
                        });
                    }
                    ViewBag.Compras = lista;
                    con.Close();
                }
                return View();
            }
        }



        


        //método GET para obtener los datos del registro seleccionado
        public IActionResult ComprarProductos(int id)
        {
            if (!ModelState.IsValid)
            {
                return View("ComprarProductos");
            }
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                ProductosModel registro = new();
                using (SqlCommand cmd = new("sp_Bproductos", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.Id_producto = (int)dt.Rows[0][0];
                    registro.Nombre = dt.Rows[0][1].ToString();
                    registro.Marca = dt.Rows[0][2].ToString();
                    registro.Descripcion = dt.Rows[0][3].ToString();
                    registro.Imagen = dt.Rows[0][4].ToString();
                    registro.Stock = (int)dt.Rows[0][5];
                    registro.Inventario = (int)dt.Rows[0][6];
                    registro.Peso = (float)dt.Rows[0][7];
                    registro.PrecioVenta = (float)dt.Rows[0][8];
                    registro.PrecioCompra = (float)dt.Rows[0][9];
                    registro.Descuento = (float)dt.Rows[0][10];
                    registro.Id_categoria = (int)dt.Rows[0][11];
                }
                return View(registro);
            }
        }

        Random r = new Random();

        [HttpPost]
        public IActionResult ComprarProductos(ProductosModel productos)
        {
            try
            {
                using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
                {
                    using (SqlCommand cmd = new("sp_Icompras", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@FechaCompra ", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@DireccionEnvio ", System.Data.SqlDbType.VarChar).Value = productos.DireccionEnvio;
                        cmd.Parameters.Add("@FechaEntrega ", System.Data.SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@CantidadArticulos ", System.Data.SqlDbType.Int).Value = productos.CantidadArticulos;
                        cmd.Parameters.Add("@FormaPago ", System.Data.SqlDbType.VarChar).Value = productos.FormaPago;
                        cmd.Parameters.Add("@PrecioArticulo ", System.Data.SqlDbType.Float).Value = productos.PrecioCompra;
                        cmd.Parameters.Add("@TotalCompra ", System.Data.SqlDbType.Float).Value = (productos.CantidadArticulos * productos.PrecioCompra);
                        cmd.Parameters.Add("@NumAutorizacion ", System.Data.SqlDbType.Int).Value = r.Next(100000, 999999);
                        cmd.Parameters.Add("@Id_empleado ", System.Data.SqlDbType.Int).Value = productos.Id_empleado;
                        cmd.Parameters.Add("@Id_producto ", System.Data.SqlDbType.Int).Value = productos.Id_producto;
                        cmd.Parameters.Add("@Id_proveedor ", System.Data.SqlDbType.Int).Value = productos.Id_proveedor;
                        con.Open();  //Abrimos la conexión
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (System.Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            return RedirectToAction("Compras");
        }



        //--------------------------------------- PRODUCTOS --------------------------------------------------------//


        public IActionResult Productos()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Tproductos", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd); //Ejecutar la sentencia
                    DataTable dt = new();  //Crear una tabla
                    da.Fill(dt); //Llenamos la tabla con los datos que trae la consulta
                    da.Dispose(); //.Disposed() lo que hace es liberar los recursos del sistema que esté utilizando el componente al que hace referencia
                    List<ProductosModel> lista = new();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new ProductosModel()
                        {
                            Id_producto = Convert.ToInt32(dt.Rows[i][0]),
                            Nombre = Convert.ToString(dt.Rows[i][1]),
                            Marca = Convert.ToString(dt.Rows[i][2]),
                            Descripcion = Convert.ToString(dt.Rows[i][3]),
                            Imagen = Convert.ToString(dt.Rows[i][4]),
                            Stock = Convert.ToInt32(dt.Rows[i][5]),
                            Inventario = Convert.ToInt32(dt.Rows[i][6]),
                            Peso = (float)Convert.ToDouble(dt.Rows[i][7]),
                            PrecioVenta = (float)Convert.ToDouble(dt.Rows[i][8]),
                            PrecioCompra = (float)Convert.ToDouble(dt.Rows[i][9]),
                            Descuento = (float)Convert.ToDouble(dt.Rows[i][10]),
                            Id_categoria = Convert.ToInt32(dt.Rows[i][11]),
                        });
                    }
                    ViewBag.Productos = lista;
                    con.Close();
                }
                return View();
            }
        }

        public ActionResult Buscar(string buscar)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_BNproductos", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@NombreB", SqlDbType.VarChar).Value = buscar;
                    con.Open();
                    SqlDataAdapter da = new(cmd); //Ejecutar la sentencia
                    DataTable dt = new();  //Crear una tabla
                    da.Fill(dt); //Llenamos la tabla con los datos que trae la consulta
                    da.Dispose(); //.Disposed() lo que hace es liberar los recursos del sistema que esté utilizando el componente al que hace referencia
                    List<ProductosModel> lista = new();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new ProductosModel()
                        {
                            Id_producto = Convert.ToInt32(dt.Rows[i][0]),
                            Nombre = Convert.ToString(dt.Rows[i][1]),
                            Marca = Convert.ToString(dt.Rows[i][2]),
                            Descripcion = Convert.ToString(dt.Rows[i][3]),
                            Imagen = Convert.ToString(dt.Rows[i][4]),
                            Stock = Convert.ToInt32(dt.Rows[i][5]),
                            Inventario = Convert.ToInt32(dt.Rows[i][6]),
                            Peso = (float)Convert.ToDouble(dt.Rows[i][7]),
                            PrecioVenta = (float)Convert.ToDouble(dt.Rows[i][8]),
                            PrecioCompra = (float)Convert.ToDouble(dt.Rows[i][9]),
                            Descuento = (float)Convert.ToDouble(dt.Rows[i][10]),
                            Id_categoria = Convert.ToInt32(dt.Rows[i][11]),
                        });
                    }
                    ViewBag.Productos = lista;
                    con.Close();
                }
                return View("Productos");
            }
        }

        /*       public ActionResult Buscar(string buscar)
               {
                   using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
                   {
                       string sentencia = string.Format("SELECT Id_producto, Nombre, Marca, Descripcion, Imagen, Stock, Inventario, Peso, PrecioVenta, PrecioCompra, Descuento, Id_categoria FROM Productos WHERE Nombre like %{0}% order by Nombre asc", buscar);
                       SqlDataAdapter da = new SqlDataAdapter(sentencia, con);
                       DataTable dtProductos = new DataTable();
                       da.Fill(dtProductos);

                       da.Dispose(); //.Disposed() lo que hace es liberar los recursos del sistema que esté utilizando el componente al que hace referencia
                           List<ProductosModel> lista = new();

                           foreach (DataRow r in dtProductos.Rows)
                           {
                               lista.Add(new ProductosModel()
                               {
                                   Id_producto = Convert.ToInt32(r["Id_producto"]),
                                   Nombre = Convert.ToString(r["Nombre"]),
                                   Marca = Convert.ToString(r["Marca"]),
                                   Descripcion = Convert.ToString(r["Descripcion"]),
                                   Imagen = Convert.ToString(r["Imagen"]),
                                   Stock = Convert.ToInt32(r["Stock"]),
                                   Inventario = Convert.ToInt32(r["Inventario"]),
                                   Peso = (float)Convert.ToDouble(r["Peso"]),
                                   PrecioVenta = (float)Convert.ToDouble(r["PrecioVenta"]),
                                   PrecioCompra = (float)Convert.ToDouble(r["PrecioCompra"]),
                                   Descuento = (float)Convert.ToDouble(r["Descuento"]),
                                   Id_categoria = Convert.ToInt32(r["Id_categoria"]),
                               });
                           }
                       return View("Productos");
                   }
               }*/

        /*
        public ActionResult Buscar1(string buscar)
        {
            try
            {

            }
            catch(Exception e)
            {
                List<ProductosModel> list = new List<ProductosModel>();
                TempData["MSG_ERROR"] = "Error.... " + e.Message;
                return View(list);
            }


        }


        public DataTable Buscar1(string datoBuscar)
        {
            string sentencia = string.Format("SELECT Id_producto, Nombre, Marca, Descripcion, Imagen, Stock, Inventario, Peso, PrecioVenta, PrecioCompra, Descuento, Id_categoria FROM Productos WHERE Nombre like '%{0}%' order by Nombre asc", datoBuscar);
            SqlDataAdapter da = new SqlDataAdapter(sentencia, con);
            DataTable dtProductos = new DataTable();
            da.Fill(dtProductos);
            return dtProductos;

        }*/

        public IActionResult AgregarProducto()
        {
            return View();
        }

        //Métodos para registrar nuevo producto
        [HttpPost]
        public IActionResult AgregarProducto(ProductosModel productos)
        {
            if (!ModelState.IsValid)
            {
                return View("AgregarProducto");
            }
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Iproductos", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = productos.Nombre;
                    cmd.Parameters.Add("@Marca", System.Data.SqlDbType.VarChar).Value = productos.Marca;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = productos.Descripcion;
                    cmd.Parameters.Add("@Imagen", System.Data.SqlDbType.VarChar).Value = productos.Imagen;
                    cmd.Parameters.Add("@Stock", System.Data.SqlDbType.Int).Value = productos.Stock;
                    cmd.Parameters.Add("@Inventario", System.Data.SqlDbType.Int).Value = productos.Inventario;
                    cmd.Parameters.Add("@Peso", System.Data.SqlDbType.Float).Value = productos.Peso;
                    cmd.Parameters.Add("@PrecioVenta", System.Data.SqlDbType.Float).Value = productos.PrecioVenta;
                    cmd.Parameters.Add("@PrecioCompra", System.Data.SqlDbType.Float).Value = productos.PrecioCompra;
                    cmd.Parameters.Add("@Descuento", System.Data.SqlDbType.Float).Value = productos.Descuento;
                    cmd.Parameters.Add("@Id_categoria", System.Data.SqlDbType.Int).Value = productos.Id_categoria;
                    con.Open();  //Abrimos la conexión
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Productos");

        }


        //método GET para obtener los datos del registro seleccionado
        public IActionResult EditarProductos(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                ProductosModel registro = new();
                using (SqlCommand cmd = new("sp_Bproductos", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.Id_producto = (int)dt.Rows[0][0];
                    registro.Nombre = dt.Rows[0][1].ToString();
                    registro.Marca = dt.Rows[0][2].ToString();
                    registro.Descripcion = dt.Rows[0][3].ToString();
                    registro.Imagen = dt.Rows[0][4].ToString();
                    registro.Stock = (int)dt.Rows[0][5];
                    registro.Inventario = (int)dt.Rows[0][6];
                    registro.Peso = (float)dt.Rows[0][7];
                    registro.PrecioVenta = (float)dt.Rows[0][8];
                    registro.PrecioCompra = (float)dt.Rows[0][9];
                    registro.Descuento = (float)dt.Rows[0][10];
                    registro.Id_categoria = (int)dt.Rows[0][11];
                }
                return View(registro);
            }
        }

        [HttpPost]
        public IActionResult EditarProductos(ProductosModel productos)
        {
            try
            {
                using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
                {

                    using (SqlCommand cmd = new("sp_Eproductos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = productos.Id_producto;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = productos.Nombre;
                        cmd.Parameters.Add("@Marca", SqlDbType.VarChar).Value = productos.Marca;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = productos.Descripcion;
                        cmd.Parameters.Add("@Imagen", SqlDbType.VarChar).Value = productos.Imagen;
                        cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = productos.Stock;
                        cmd.Parameters.Add("@Inventario", SqlDbType.Int).Value = productos.Inventario;
                        cmd.Parameters.Add("@Peso", SqlDbType.Float).Value = productos.Peso;
                        cmd.Parameters.Add("@PrecioVenta", SqlDbType.Float).Value = productos.PrecioVenta;
                        cmd.Parameters.Add("@PrecioCompra", SqlDbType.Float).Value = productos.PrecioCompra;
                        cmd.Parameters.Add("@Descuento", SqlDbType.Float).Value = productos.Descuento;
                        cmd.Parameters.Add("@Id_categoria", SqlDbType.Int).Value = productos.Id_categoria;
                        con.Open();  //Abrimos la conexión
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (System.Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            return RedirectToAction("Productos");
        }


        public IActionResult EliminarProductos(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                ProductosModel registro = new();
                using (SqlCommand cmd = new("sp_Bproductos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();

                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.Id_producto = (int)dt.Rows[0][0];
                    registro.Nombre = dt.Rows[0][1].ToString();
                    registro.Marca = dt.Rows[0][2].ToString();
                    registro.Descripcion = dt.Rows[0][3].ToString();
                    registro.Imagen = dt.Rows[0][4].ToString();
                    registro.Stock = (int)dt.Rows[0][5];
                    registro.Inventario = (int)dt.Rows[0][6];
                    registro.Peso = (float)dt.Rows[0][7];
                    registro.PrecioVenta = (float)dt.Rows[0][8];
                    registro.PrecioCompra = (float)dt.Rows[0][9];
                    registro.Descuento = (float)dt.Rows[0][10];
                    registro.Id_categoria = (int)dt.Rows[0][11];
                }
                return View(registro);
            }
        }

        [HttpPost]
        public IActionResult EliminarProductos(ProductosModel productos)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Dproductos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = productos.Id_producto;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Productos");
            }
        }


        //--------------------------------------- CATEGORIAS --------------------------------------------------------//

        public IActionResult Categorias()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Tcategorias", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd); //Ejecutar la sentencia
                    DataTable dt = new();  //Crear una tabla
                    da.Fill(dt); //Llenamos la tabla con los datos que trae la consulta
                    da.Dispose(); //.Disposed() lo que hace es liberar los recursos del sistema que esté utilizando el componente al que hace referencia
                    List<CategoriasModel> lista = new();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new CategoriasModel()
                        {
                            Id_categoria = Convert.ToInt32(dt.Rows[i][0]),
                            Nombre = Convert.ToString(dt.Rows[i][1]),
                            Descripcion = Convert.ToString(dt.Rows[i][2]),
                        });
                    }
                    ViewBag.Categorias = lista;
                    con.Close();
                }
                return View();
            }
        }


        public IActionResult AgregarCategorias()
        {
            return View();
        }

        //Métodos para registrar nueva categoría
        [HttpPost]
        public IActionResult AgregarCategorias(CategoriasModel categorias)
        {
            if (!ModelState.IsValid)
            {
                return View("AgregarCategorias");
            }
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Icategorias", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = categorias.Nombre;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = categorias.Descripcion;
                    con.Open();  //Abrimos la conexión
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Categorias");

        }


        //método GET para obtener los datos del registro seleccionado
        public IActionResult EditarCategorias(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                CategoriasModel registro = new();
                using (SqlCommand cmd = new("sp_Bcategorias", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();

                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.Id_categoria = (int)dt.Rows[0][0];
                    registro.Nombre = dt.Rows[0][1].ToString();
                    registro.Descripcion = dt.Rows[0][2].ToString();
                }
                return View(registro);
            }
        }

        [HttpPost]
        public IActionResult EditarCategorias(CategoriasModel categorias)
        {
            try
            {
                using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
                {

                    using (SqlCommand cmd = new("sp_Ecategorias", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = categorias.Id_categoria;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = categorias.Nombre;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = categorias.Descripcion;
                        con.Open();  //Abrimos la conexión
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (System.Exception e)
            {
                ViewBag.error = e.Message;
                return View();
            }
            return RedirectToAction("Categorias");
        }


        public IActionResult EliminarCategorias(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                CategoriasModel registro = new();
                using (SqlCommand cmd = new("sp_Bcategorias", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();

                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.Id_categoria = (int)dt.Rows[0][0];
                    registro.Nombre = dt.Rows[0][1].ToString();
                    registro.Descripcion = dt.Rows[0][2].ToString();
                }
                return View(registro);
            }
        }

        [HttpPost]
        public IActionResult EliminarCategorias(CategoriasModel categorias)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Dcategorias", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = categorias.Id_categoria;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return RedirectToAction("Categorias");
            }
        }

//--------------------------------------- PROVEEDORES --------------------------------------------------------//


        public IActionResult Proveedores()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Tproveedores", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd); //Ejecutar la sentencia
                    DataTable dt = new();  //Crear una tabla
                    da.Fill(dt); //Llenamos la tabla con los datos que trae la consulta
                    da.Dispose(); //.Disposed() lo que hace es liberar los recursos del sistema que esté utilizando el componente al que hace referencia
                    List<ProveedoresModel> lista = new();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lista.Add(new ProveedoresModel()
                        {
                            Id_proveedor = Convert.ToInt32(dt.Rows[i][0]),
                            Nombre = Convert.ToString(dt.Rows[i][1]),
                            Direccion = Convert.ToString(dt.Rows[i][2]),
                        });
                    }
                    ViewBag.Proveedores = lista;
                    con.Close();
                }
                return View();
            }
        }

    }
}





/* 
 //--------------------------------------- CheckoutTarjetas --------------------------------------------------------//

        public IActionResult CheckoutTarjetas()
        {
            return View();
        }

    //Para insertar compra manualmente
        public IActionResult HacerCompra()
        {
            return View();
        }

        //Métodos para registrar nueva compra
        [HttpPost]
        public IActionResult HacerCompra(ComprasModel compras)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_Icompras", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FechaCompra ", System.Data.SqlDbType.DateTime).Value = compras.FechaCompra;
                    cmd.Parameters.Add("@DireccionEnvio ", System.Data.SqlDbType.VarChar).Value = compras.DireccionEnvio;
                    cmd.Parameters.Add("@FechaEntrega ", System.Data.SqlDbType.DateTime).Value = compras.FechaEntrega;
                    cmd.Parameters.Add("@CantidadArticulos ", System.Data.SqlDbType.Int).Value = compras.CantidadArticulos;
                    cmd.Parameters.Add("@FormaPago ", System.Data.SqlDbType.VarChar).Value = compras.FormaPago;
                    cmd.Parameters.Add("@PrecioArticulo ", System.Data.SqlDbType.Float).Value = compras.PrecioArticulo;
                    cmd.Parameters.Add("@TotalCompra ", System.Data.SqlDbType.Float).Value = compras.TotalCompra;
                    cmd.Parameters.Add("@NumAutorizacion ", System.Data.SqlDbType.Int).Value = compras.NumAutorizacion;
                    cmd.Parameters.Add("@Id_empleado ", System.Data.SqlDbType.Int).Value = compras.Id_empleado;
                    cmd.Parameters.Add("@Id_producto ", System.Data.SqlDbType.Int).Value = compras.Id_producto;
                    cmd.Parameters.Add("@Id_proveedor ", System.Data.SqlDbType.Int).Value = compras.Id_proveedor;
                    con.Open();  //Abrimos la conexión
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Compras");
        }

 * Para Editar compras
         //método GET para obtener los datos del registro seleccionado
        public IActionResult EditarProductos(int id)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                ComprasModel registro = new();
                using (SqlCommand cmd = new("sp_Bproductos", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    con.Open();

                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    registro.Id_compra = (int)dt.Rows[0][0];
                    registro.FechaCompra = (DateTime?)dt.Rows[0][1];
                    registro.DireccionEnvio = dt.Rows[0][2].ToString();
                    registro.FechaEntrega = (DateTime?)dt.Rows[0][3];
                    registro.CantidadArticulos = (int)dt.Rows[0][4];
                    registro.FormaPago = dt.Rows[0][5].ToString();
                    registro.PrecioArticulo = (float)dt.Rows[0][6];
                    registro.TotalCompra = (float)dt.Rows[0][7];
                    registro.NumAutorizacion = (int)dt.Rows[0][8];
                    registro.Id_empleado = (int)dt.Rows[0][9];
                    registro.Id_producto = (int)dt.Rows[0][10];
                    registro.Id_proveedor = (int)dt.Rows[0][11];
                }
                return View(registro);
            }
        }
 */
