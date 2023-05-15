using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using TodoParaTuPerro.Models;
using TodoParaTuPerro.Data;

namespace TodoParaTuPerro.Controllers.ComprasControllers
{
    public class ProductosController : Controller
    {

        public IConfiguration Configuration { get; }


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
                using (SqlCommand cmd = new("sp_Iproductos", con))  //Ejecuta el procedimiento almacenado
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Nombre ", System.Data.SqlDbType.DateTime).Value = productos.Nombre;
                    cmd.Parameters.Add("@Marca ", System.Data.SqlDbType.VarChar).Value = productos.Marca;
                    cmd.Parameters.Add("@Descripcion ", System.Data.SqlDbType.DateTime).Value = productos.Descripcion;
                    cmd.Parameters.Add("@Imagen ", System.Data.SqlDbType.Int).Value = productos.Imagen;
                    cmd.Parameters.Add("@Stock ", System.Data.SqlDbType.VarChar).Value = productos.Stock;
                    cmd.Parameters.Add("@Inventario ", System.Data.SqlDbType.Float).Value = productos.Inventario;
                    cmd.Parameters.Add("@Peso ", System.Data.SqlDbType.Float).Value = productos.Peso;
                    cmd.Parameters.Add("@PrecioVenta ", System.Data.SqlDbType.Int).Value = productos.PrecioVenta;
                    cmd.Parameters.Add("@Descuento ", System.Data.SqlDbType.Int).Value = productos.Descuento;
                    cmd.Parameters.Add("@Id_categoria ", System.Data.SqlDbType.Int).Value = productos.Id_categoria;
                    con.Open();  //Abrimos la conexión
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Productos");

        }


        //método GET para obtener los datos del registro seleccionado
        public IActionResult Editar(int id)
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
                    registro.Descuento = (float)dt.Rows[0][9];
                    registro.Id_categoria = (int)dt.Rows[0][10];

                }
                return View(registro);
            }
        }

        [HttpPost]
        public IActionResult Editar(ProductosModel productos)
        {
            try
            {
                using (SqlConnection con = new(Configuration["ConnectionStrings:DefaultConnection"]))
                {

                    using (SqlCommand cmd = new("sp_Eproductos", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Nombre ", System.Data.SqlDbType.DateTime).Value = productos.Nombre;
                        cmd.Parameters.Add("@Marca ", System.Data.SqlDbType.VarChar).Value = productos.Marca;
                        cmd.Parameters.Add("@Descripcion ", System.Data.SqlDbType.DateTime).Value = productos.Descripcion;
                        cmd.Parameters.Add("@Imagen ", System.Data.SqlDbType.Int).Value = productos.Imagen;
                        cmd.Parameters.Add("@Stock ", System.Data.SqlDbType.VarChar).Value = productos.Stock;
                        cmd.Parameters.Add("@Inventario ", System.Data.SqlDbType.Float).Value = productos.Inventario;
                        cmd.Parameters.Add("@Peso ", System.Data.SqlDbType.Float).Value = productos.Peso;
                        cmd.Parameters.Add("@PrecioVenta ", System.Data.SqlDbType.Int).Value = productos.PrecioVenta;
                        cmd.Parameters.Add("@Descuento ", System.Data.SqlDbType.Int).Value = productos.Descuento;
                        cmd.Parameters.Add("@Id_categoria ", System.Data.SqlDbType.Int).Value = productos.Id_categoria;
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
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
