
class Cliente {
    constructor(clienteID, nombre, apellido, correo, telefono, puntos, accion) {
        this.clienteID = clienteID;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.telefono = telefono;
        this.puntos = puntos;
        this.accion = accion;
    }
    nuevoCliente() {
        if (this.clienteID == "") {
            document.getElementById("clienteID").focus();
        } else {
            if (this.nombre == "") {
                document.getElementById("nombre").focus();
            } else {
                if (this.Apellido == "") {
                    document.getElementById("apellido").focus();
                } else {
                    if (this.Correo == "") {
                        document.getElementById("correo").focus();
                    } else {
                        if (this.telefono == "") {
                            document.getElementById("telefono").focus();
                        } else {
                            if (this.puntos == "") {
                                document.getElementById("puntos").focus();
                            } else {

                                var clienteID = this.clienteID
                                var nombre = this.nombre;
                                var apellido = this.apellido;
                                var correo = this.correo;
                                var telefono = this.telefono;
                                var puntos = this.puntos;
                                var accion = this.accion;
                                var mensaje = "";
                                $.ajax({
                                    type: "POST",
                                    url: accion,
                                    data: {
                                        clienteID, nombre, apellido, correo, telefono, puntos
                                    },
                                    success: (response) => {
                                        $.each(response, (index, val) => {
                                            mensaje = val.code;
                                            

                                        });
                                        if (mensaje == "Save") {
                                            this.hastalaproxima();

                                        } else {
                                            document.getElementById("mensaje").innerHTML = "Hay un problema con los datos de este Cliente";
                                        }
                                    }
                                });
                            }
                        }
                    }
                }

            }
        }
    }
    hastalaproxima() {
        
        document.getElementById("clienteID").value = "";
        document.getElementById("nombre").value = "";
        document.getElementById("apellido").value = "";
        document.getElementById("correo").value = "";
        document.getElementById("telefono").value = "";
        document.getElementById("puntos").value = "";
        $('#modalC').modal('hide');
    }
}