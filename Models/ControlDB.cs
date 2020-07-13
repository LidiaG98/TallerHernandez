using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Areas.Cliente.Models;
using TallerHernandez.Areas.Empleado.Models;

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
                }
                con.Close();
            }
            return emp;
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
    }
}
