using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Controllers.ComprasControllers
{
    public class CategoriasController : Controller
    {
        public CategoriasController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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


    }
}
