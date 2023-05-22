using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Controllers.ComprasControllers
{
    public class ProveedoresController : Controller
    {
        public ProveedoresController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
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
