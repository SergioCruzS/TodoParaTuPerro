using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Data
{
    public class DA_Logica
    {

        public List<UsuarioModel> ListaUsuario()
        {
            return new List<UsuarioModel>()
            {
                new UsuarioModel{Nombre="Jose", Correo="administrador@gmail.com", Clave="123", Roles=new string[]{"Administrador"}},
                new UsuarioModel{Nombre="Maria", Correo="supervisor@gmail.com", Clave="123", Roles=new string[]{"Supervisor"}},
                new UsuarioModel{Nombre="Juan", Correo="empleado@gmail.com", Clave="123", Roles=new string[]{"Empleado"}},
                new UsuarioModel{Nombre="Oscar", Correo="superempleado@gmail.com", Clave="123", Roles=new string[]{"Supervisor","Empleado"}},
                new UsuarioModel{Nombre="Compras", Correo="LuisMiguel56@TPTP.compras.org", Clave="123", Roles=new string[]{"Compras"}}
            };
        }

        //Método que busca dentro de la lista al uaurio que intenta ingresar
        public UsuarioModel ValidarUsuario(string _correo, string _clave)
        {
            return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault(); //Devuelve el primero o Default
        }


    }
}
