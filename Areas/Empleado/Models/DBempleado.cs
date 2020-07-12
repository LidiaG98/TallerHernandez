using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Areas.Empleado.Models
{
    public class DBempleado
    {
        string connection = "Data Source=DESKTOP-R5D6DU5\\LIDIA;Initial Catalog=taller;Integrated Security=True";

        //Consultar todos los empleados
        public IEnumerable<Empleado> GetAllEmployee()
        {
            List<Empleado> empList = new List<Empleado>();
            using (SqlConnection con= new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
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
            using (SqlConnection con = new  SqlConnection(connection))
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
    }
}
