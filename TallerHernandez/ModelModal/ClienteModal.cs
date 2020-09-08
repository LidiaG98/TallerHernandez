using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.ModelModal
{
    public class ClienteModal
    {
        private TallerHernandezContext context;
        public ClienteModal(TallerHernandezContext context)
        {
            this.context = context;
        }
            public  List<IdentityError> nuevoCliente(string clienteID, string nombre, string apellido, string correo, string telefono, string puntos)
        {
            var erroList = new List<IdentityError>();
            var cliente = new Cliente
            {
                clienteID = clienteID,
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                telefono = telefono,
                puntos = Convert.ToInt32(puntos)
            };
            context.Add(cliente);
             context.SaveChangesAsync();
            erroList.Add(new IdentityError
            {
                Code ="Save",
                Description="Se guardó, que chivo"
            });
            return erroList;
        }
        
    }
}
