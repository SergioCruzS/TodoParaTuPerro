using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using TodoParaTuPerro.Models;
using TodoParaTuPerro.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Administrador,Supervisor,Compras")] //Autorizada sólo para estos roles de DA_Logica
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
                            //Id_producto = Convert.ToInt32(dt.Rows[i][10]),
                            //Id_proveedor = Convert.ToInt32(dt.Rows[i][11]),
                            NombreProducto = Convert.ToString(dt.Rows[i][10]),
                            NombreProveedor = Convert.ToString(dt.Rows[i][11]),
                        });
                    }

                        ViewBag.Compras = lista;
                    con.Close();
                }
                return View();
            }
        }


        [Authorize(Roles = "Administrador,Supervisor,Compras")] //Autorizada sólo para estos roles de DA_Logica
        public IActionResult ComprasFecha() //Método para mostrar la tabla ordenada de acuerdo a la fecha más antigua
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_TcomprasFecha", con))  //Ejecuta el procedimiento almacenado
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
                            //Id_producto = Convert.ToInt32(dt.Rows[i][10]),
                            //Id_proveedor = Convert.ToInt32(dt.Rows[i][11]),
                            NombreProducto = Convert.ToString(dt.Rows[i][10]),
                            NombreProveedor = Convert.ToString(dt.Rows[i][11]),
                        });
                    }
                    ViewBag.Compras = lista;
                    con.Close();
                }
                return View("Compras");
            }
        }

        [Authorize(Roles = "Administrador,Supervisor,Compras")] //Autorizada sólo para estos roles de DA_Logica
        public IActionResult ComprasTotal() //Método para mostrar la tabla ordenada de acuerdo al Precio Total mas alto
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
            {
                using (SqlCommand cmd = new("sp_TcomprasTotal", con))  //Ejecuta el procedimiento almacenado
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
                            //Id_producto = Convert.ToInt32(dt.Rows[i][10]),
                            //Id_proveedor = Convert.ToInt32(dt.Rows[i][11]),
                            NombreProducto = Convert.ToString(dt.Rows[i][10]),
                            NombreProveedor = Convert.ToString(dt.Rows[i][11]),
                        });
                    }
                    ViewBag.Compras = lista;
                    con.Close();
                }
                return View("Compras");
            }
        }



        //--------------------------------------- Login --------------------------------------------------------//
        [AllowAnonymous]
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }


        //--------------------------------------- PRODUCTOS --------------------------------------------------------//
        [Authorize(Roles = "Administrador,Supervisor,Compras")] //Autorizada sólo para estos roles de DA_Logica
        public IActionResult Productos()
        {
            return RedirectToAction("Productos", "Productos");
        }


        //--------------------------------------- CATEGORIAS --------------------------------------------------------//
        [Authorize(Roles = "Administrador,Supervisor,Compras")] //Autorizada sólo para estos roles de DA_Logica
        public IActionResult Categorias()
        {
            return RedirectToAction("Categorias", "Categorias");
        }


        //--------------------------------------- PROVEEDORES --------------------------------------------------------//
        [Authorize(Roles = "Administrador,Supervisor,Compras")] //Autorizada sólo para estos roles de DA_Logica
        public IActionResult Proveedores()
        {
            return RedirectToAction("Proveedores", "Proveedores");
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



using (SqlCommand cmd2 = new("sp_Tproductos", con))  //Ejecuta el procedimiento almacenado
{
    cmd.CommandType = System.Data.CommandType.StoredProcedure;
    SqlDataAdapter da2 = new(cmd); //Ejecutar la sentencia
    DataTable dt2 = new();  //Crear una tabla
    da2.Fill(dt); //Llenamos la tabla con los datos que trae la consulta
    da2.Dispose(); //.Disposed() lo que hace es liberar los recursos del sistema que esté utilizando el componente al que hace referencia
    List<ComprasModel> lista2 = new();

    for (int i = 0; i < dt2.Rows.Count; i++)
    {
        lista2.Add(new ComprasModel()
        {
            Nombre = Convert.ToString(dt.Rows[i][12])
,
        });
    }
}

 */