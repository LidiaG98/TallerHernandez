using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Areas.Cliente.Models;
using TallerHernandez.Areas.Empleado.Models;
using TallerHernandez.Areas.Vehiculo.Models;

namespace TallerHernandez.Models
{
    public class ControlDB
    {
        string connection = "Data Source=DESKTOP-R5D6DU5\\LIDIA;Initial Catalog=taller;Integrated Security=True";
        //string connection = "Data Source=DESKTOP-965NUUB\\TALLERHERNANDEZ;Initial Catalog=TallerHernandez;Integrated Security=True;";

        /*CRUD EMPLEADO*/

        //Consultar todos los empleados
        public IEnumerable<Empleado> GetAllEmployee()
        {
            List<Empleado> empList = new List<Empleado>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Empleado emp = new Empleado();
                    emp.idEmpleado = Convert.ToInt32(dr["IDEMPLEADO"].ToString());
                    emp.idModo = Convert.ToInt32(dr["IDMODO"].ToString());
                    emp.idArea = Convert.ToInt32(dr["IDAREA"].ToString());
                    emp.nombre = dr["Nombre"].ToString();
                    emp.apellido = dr["Apellido"].ToString();
                    emp.dui = dr["Dui"].ToString();
                    emp.salario = (float)Convert.ToDecimal(dr["Salario"].ToString());
                    emp.telefono = dr["Telefono"].ToString();
                    emp.correo = dr["Correo"].ToString();

                    empList.Add(emp);
                }
                con.Close();
            }
            return empList;
        }

        //Insertar empleado
        public void InsertarEmpleado(Empleado emp)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarEmpleado", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdModo", emp.idModo);
                cmd.Parameters.AddWithValue("@IdArea", emp.idArea);
                cmd.Parameters.AddWithValue("@Nombre", emp.nombre);
                cmd.Parameters.AddWithValue("@Apellido", emp.apellido);
                cmd.Parameters.AddWithValue("@Dui", emp.dui);
                cmd.Parameters.AddWithValue("@Salario", emp.salario);
                cmd.Parameters.AddWithValue("@Telefono", emp.telefono);
                cmd.Parameters.AddWithValue("@Correo", emp.correo);
                cmd.Parameters.AddWithValue("@IdUser", emp.idUsuario);
                try
                {
                    cmd.Parameters.AddWithValue("@NombreImagen", emp.imagen.nombreImagen);
                }
                catch (NullReferenceException e)
                {
                    cmd.Parameters.AddWithValue("@NombreImagen", "");
                }
                

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Actualizar empleado
        public void ActualizarEmpleado(Empleado emp)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarEmpleado", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEmpleado", emp.idEmpleado);
                cmd.Parameters.AddWithValue("@IdModo", emp.idModo);
                cmd.Parameters.AddWithValue("@IdArea", emp.idArea);
                cmd.Parameters.AddWithValue("@Nombre", emp.nombre);
                cmd.Parameters.AddWithValue("@Apellido", emp.apellido);
                cmd.Parameters.AddWithValue("@Dui", emp.dui);
                cmd.Parameters.AddWithValue("@Salario", emp.salario);
                cmd.Parameters.AddWithValue("@Telefono", emp.telefono);
                cmd.Parameters.AddWithValue("@Correo", emp.correo);
                cmd.Parameters.AddWithValue("@nombreImagen", emp.imagen.nombreImagen);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Eliminar empleado
        public void EliminarEmpleado(int? idemp)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SP_EliminarEmpleado", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdEmpleado", idemp);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Obtener empleado por id
        public Empleado BuscarEmpleado(int? empId)
        {
            Empleado emp = new Empleado();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SP_GetEmpleado", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdEmpleado", empId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emp.idEmpleado = Convert.ToInt32(dr["IDEMPLEADO"].ToString());
                    emp.idModo = Convert.ToInt32(dr["IDMODO"].ToString());
                    emp.idArea = Convert.ToInt32(dr["IDAREA"].ToString());
                    emp.nombre = dr["Nombre"].ToString();
                    emp.apellido = dr["Apellido"].ToString();
                    emp.dui = dr["Dui"].ToString();
                    emp.salario = (float)Convert.ToDecimal(dr["Salario"].ToString());
                    emp.telefono = dr["Telefono"].ToString();
                    emp.correo = dr["Correo"].ToString();
                    emp.idUsuario = dr["idUser"].ToString();
                    emp.imagen = new Imagen();
                    emp.imagen.nombreImagen = dr["nombreImagen"].ToString();
                }
                con.Close();
            }
            return emp;
        }

        //Buscar empleado por nombre parcial
        public IEnumerable<Empleado> BusquedaEmpleado(Empleado e)
        {
            List<Empleado> listEmpleado = new List<Empleado>();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Empleado_BusquedaEmpleado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NOMBRE", e.nombre);
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Empleado emp = new Empleado();
                    emp.idEmpleado = Convert.ToInt32(dataReader["IDEMPLEADO"].ToString());
                    emp.idModo = Convert.ToInt32(dataReader["IDMODO"].ToString());
                    emp.idArea = Convert.ToInt32(dataReader["IDAREA"].ToString());
                    emp.nombre = dataReader["Nombre"].ToString();
                    emp.apellido = dataReader["Apellido"].ToString();
                    emp.dui = dataReader["Dui"].ToString();
                    emp.salario = (float)Convert.ToDecimal(dataReader["Salario"].ToString());
                    emp.telefono = dataReader["Telefono"].ToString();
                    emp.correo = dataReader["Correo"].ToString();
                    listEmpleado.Add(emp);
                }
                cn.Close();
            }
            return listEmpleado;
        }

        //Obtener areas

        public IEnumerable<AreaViewModel> ObtenerArea()
        {
            List<AreaViewModel> areas = new List<AreaViewModel>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Area_ObtenerAreas", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AreaViewModel a = new AreaViewModel();
                    a.idArea = Convert.ToInt32(dr["IDAREA"].ToString());                   
                    a.nombreArea = dr["NOMBREAREA"].ToString();                  

                    areas.Add(a);
                }
                con.Close();
            }
            return areas;
        }

        //Obtener modos de pago

        public IEnumerable<MPagoViewModel> ObtenerModoPago()
        {
            List<MPagoViewModel> modos = new List<MPagoViewModel>();
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("ModoPago_ObtenerModos", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MPagoViewModel mp = new MPagoViewModel();
                    mp.idModo = Convert.ToInt32(dr["IDMODO"].ToString());
                    mp.tipoPago= dr["TIPOPAGO"].ToString();

                    modos.Add(mp);
                }
                con.Close();
            }
            return modos;
        }

        //Crear contraseña por defecto para la cuenta del nuevo empleado
        public String crearContra(String nombre)
        {            
            String contra="@"+nombre.ToUpper()+"123a";
            contra = contra.Replace(" ","");
            return contra;
        }

        //Insertar datos de imagen 
        public void InsertarImagen(Imagen imagen)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Imagen_InsertarImagen", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreImagen", imagen.nombreImagen);
                cmd.Parameters.AddWithValue("@ImagePath", imagen.imagePath);
                cmd.Parameters.AddWithValue("@PerteneceA", imagen.perteneceA);
                cmd.Parameters.AddWithValue("@IdDueño", imagen.idDuenio);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        //CRUD CLIENTE

        public IEnumerable<Cliente> ObtenerTodos()
        {
            List<Cliente> listCliente = new List<Cliente>();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Cliente_ObtenerTodos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.ID = Convert.ToInt32(dataReader["IDCLIENTE"].ToString());
                    cliente.dui = dataReader["DUI"].ToString();
                    cliente.nombre = dataReader["NOMBRE"].ToString();
                    cliente.apellido = dataReader["APELLIDO"].ToString();
                    cliente.telefono = dataReader["TELEFONO"].ToString();
                    cliente.correo = dataReader["CORREO"].ToString();
                    listCliente.Add(cliente);
                }
                cn.Close();
            }
            return listCliente;
        }

        public Cliente ObtenerCliente(int? idcliente)
        {
            Cliente cliente = new Cliente();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Cliente_ObtenerCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDCLIENTE", idcliente);
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    cliente.ID = Convert.ToInt32(dataReader["IDCLIENTE"].ToString());
                    cliente.dui = dataReader["DUI"].ToString();
                    cliente.nombre = dataReader["NOMBRE"].ToString();
                    cliente.apellido = dataReader["APELLIDO"].ToString();
                    cliente.telefono = dataReader["TELEFONO"].ToString();
                    cliente.correo = dataReader["CORREO"].ToString();

                }
                cn.Close();
            }
            return cliente;
        }

        public IEnumerable<Cliente> BusquedaCliente(Cliente c)
        {
            List<Cliente> listCliente = new List<Cliente>();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Cliente_BusquedaCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NOMBRE", c.nombre);
                //cmd.Parameters.AddWithValue("@DUI", c.dui);
                //cmd.Parameters.AddWithValue("@APELLIDO", c.apellido);
                //cmd.Parameters.AddWithValue("@CORREO", c.correo);
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.ID = Convert.ToInt32(dataReader["IDCLIENTE"].ToString());
                    cliente.dui = dataReader["DUI"].ToString();
                    cliente.nombre = dataReader["NOMBRE"].ToString();
                    cliente.apellido = dataReader["APELLIDO"].ToString();
                    cliente.telefono = dataReader["TELEFONO"].ToString();
                    cliente.correo = dataReader["CORREO"].ToString();
                    listCliente.Add(cliente);
                }
                cn.Close();
            }
            return listCliente;
        }

        public void InsertarCliente(Cliente cliente)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Cliente_InsertarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DUI", cliente.dui);
                cmd.Parameters.AddWithValue("@NOMBRE", cliente.nombre);
                cmd.Parameters.AddWithValue("@APELLIDO", cliente.apellido);
                cmd.Parameters.AddWithValue("@TELEFONO", cliente.telefono);
                cmd.Parameters.AddWithValue("@CORREO", cliente.correo);
                cmd.Parameters.AddWithValue("@PUNTAJE", 0);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void ActualizarCliente(Cliente cliente)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Cliente_ActualizarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDCLIENTE", cliente.ID);
                cmd.Parameters.AddWithValue("@DUI", cliente.dui);
                cmd.Parameters.AddWithValue("@NOMBRE", cliente.nombre);
                cmd.Parameters.AddWithValue("@APELLIDO", cliente.apellido);
                cmd.Parameters.AddWithValue("@TELEFONO", cliente.telefono);
                cmd.Parameters.AddWithValue("@CORREO", cliente.correo);
                cmd.Parameters.AddWithValue("@PUNTAJE", cliente.puntaje);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void EliminarCliente(int? idcliente)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Cliente_EliminarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDCLIENTE", idcliente);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        //CRUD VEHICULO

        public IEnumerable<Vehiculo> ObtenerTodosVehiculos()
        {
            List<Vehiculo> listVehiculo = new List<Vehiculo>();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Vehiculo_ObtenerTodos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.placa = dataReader["PLACA"].ToString();
                    vehiculo.idcliente = Convert.ToInt32(dataReader["IDCLIENTE"].ToString());
                    vehiculo.marca = dataReader["MARCA"].ToString();
                    vehiculo.modelo = dataReader["MODELO"].ToString();
                    vehiculo.ano = dataReader["ANO"].ToString();
                    vehiculo.estado = dataReader["ESTADO"].ToString();
                    vehiculo.procedimiento = dataReader["PROCEDIMIENTO"].ToString();
                    vehiculo.comentario = dataReader["COMENTARIO"].ToString();
                    listVehiculo.Add(vehiculo);
                }
                cn.Close();
            }
            return listVehiculo;
        }
       

        public IEnumerable<Vehiculo> ObtenerVehiculo(Vehiculo v)
        {
            List<Vehiculo> listVehiculo = new List<Vehiculo>();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Vehiculo_BusquedaVehiculo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PLACA", v.placa);              
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Vehiculo vehiculo = new Vehiculo();
                    vehiculo.placa = dataReader["PLACA"].ToString();
                    vehiculo.idcliente = Convert.ToInt32(dataReader["IDCLIENTE"].ToString());
                    vehiculo.marca = dataReader["MARCA"].ToString();
                    vehiculo.modelo = dataReader["MODELO"].ToString();
                    vehiculo.ano = dataReader["ANO"].ToString();
                    vehiculo.estado = dataReader["ESTADO"].ToString();
                    vehiculo.procedimiento = dataReader["PROCEDIMIENTO"].ToString();
                    vehiculo.comentario = dataReader["COMENTARIO"].ToString();
                    listVehiculo.Add(vehiculo);
                }
                cn.Close();
            }
            return listVehiculo;
        }

        public Vehiculo BusquedaVehiculo(string? placa)
        {
            Vehiculo vehiculo = new Vehiculo();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Vehiculo_ObtenerVehiculo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PLACA", placa);
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    vehiculo.placa = dataReader["PLACA"].ToString();
                    vehiculo.idcliente = Convert.ToInt32(dataReader["IDCLIENTE"].ToString());
                    vehiculo.marca = dataReader["MARCA"].ToString();
                    vehiculo.modelo = dataReader["MODELO"].ToString();
                    vehiculo.ano = dataReader["ANO"].ToString();
                    vehiculo.estado = dataReader["ESTADO"].ToString();
                    vehiculo.procedimiento = dataReader["PROCEDIMIENTO"].ToString();
                    vehiculo.comentario = dataReader["COMENTARIO"].ToString();
                }
                cn.Close();
            }
            return vehiculo;
        }

        public void InsertarVehiculo(Vehiculo vehiculo)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Vehiculo_InsertarVehiculo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PLACA", vehiculo.placa);
                cmd.Parameters.AddWithValue("@IDCLIENTE", vehiculo.idcliente);
                cmd.Parameters.AddWithValue("@MARCA", vehiculo.marca);
                cmd.Parameters.AddWithValue("@MODELO", vehiculo.modelo);
                cmd.Parameters.AddWithValue("@ANO", vehiculo.ano);
                cmd.Parameters.AddWithValue("@ESTADO", vehiculo.estado);
                cmd.Parameters.AddWithValue("@PROCEDIMIENTO", vehiculo.procedimiento);
                cmd.Parameters.AddWithValue("@COMENTARIO", vehiculo.comentario);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void ActualizarVehiculo(Vehiculo vehiculo)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Vehiculo_ActualizarVehiculo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PLACA", vehiculo.placa);
                cmd.Parameters.AddWithValue("@IDCLIENTE", vehiculo.idcliente);
                cmd.Parameters.AddWithValue("@MARCA", vehiculo.marca);
                cmd.Parameters.AddWithValue("@MODELO", vehiculo.modelo);
                cmd.Parameters.AddWithValue("@ANO", vehiculo.ano);
                cmd.Parameters.AddWithValue("@ESTADO", vehiculo.estado);
                cmd.Parameters.AddWithValue("@PROCEDIMIENTO", vehiculo.procedimiento);
                cmd.Parameters.AddWithValue("@COMENTARIO", vehiculo.comentario);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void EliminarVehiculo(string? placa)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Vehiculo_EliminarVehiculo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PLACA", placa);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}
