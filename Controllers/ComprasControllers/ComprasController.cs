using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Controllers.ComprasControllers
{
    public class ComprasController : Controller
    {
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
                            Precio = (float)Convert.ToDouble(dt.Rows[i][8]),
                            Descuento = (float)Convert.ToDouble(dt.Rows[i][9]),
                            Id_categoria = Convert.ToInt32(dt.Rows[i][10]),

                        });
                    }
                    ViewBag.Productos = lista;
                    con.Close();
                }
                return View();
            }
        }

        public IActionResult AgregarProducto()
        {
            return View();
        }

        //Métodos para registrar nuevo producto
        [HttpPost]
        public IActionResult AgregarProducto(ProductosModel productos)
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






        public IActionResult Categorias()
        {
            return View();
        }



        public IConfiguration Configuration { get; }



        //Método constructor del Controlador
        public ComprasController(IConfiguration configuration)
        {
            Configuration = configuration;
        }



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


    }
}
