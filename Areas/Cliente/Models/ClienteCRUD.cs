using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Areas.Cliente.Models
{
    public class ClienteCRUD
    {
        string connection = "Data Source=DESKTOP-965NUUB\\TALLERHERNANDEZ;Initial Catalog=TallerHernandez;Integrated Security=True;";

        public IEnumerable<Cliente> ObtenerTodos()
        {
            List<Cliente> listCliente = new List<Cliente>();

            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("Cliente_ObtenerTodos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
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
            using(SqlConnection cn = new SqlConnection(connection))
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
