class Mantenimiento {
    constructor(nombre, precio, areaID, action) {
        this.nombre = nombre;
        this.precio = precio;
        this.areaID = areaID;
        this.action = action;
    }
    agregarMantenimiento() {
        if (this.nombre == "") {
            document.getElementById("nombre").focus();
            document.getElementById("mensaje").innerHTML = "Describa el nuevo mantenimiento";
        }
        else {
            if (this.precio == "") {
                document.getElementById("precio").focus();
                document.getElementById("mensaje").innerHTML = "Ingrese el precio de mano de obra";
            }
            else {                
                var nombre = this.nombre;
                var precio = this.precio;
                var areaID = this.areaID;
                var action = this.action;
                var mensaje = '';
                $.ajax({
                    type: "POST",
                    url: action,
                    data: { nombre, precio, areaID },
                    success: (response) => {
                        $.each(response, (index, val) => {
                            mensaje = val.code;
                        });
                        if (mensaje == "Save") {
                            this.restablecer();
                            this.recargar();
                        }
                        else {
                            document.getElementById("mensaje").innerHTML = "No se pudo guardar el mantenimiento";
                        }

                    }
                });
            }
        }            
    }

    restablecer() {
        document.getElementById("nombre").value = "";
        document.getElementById("precio").value = "";
        document.getElementById("areaID").selectedIndex = 0;
        $('#exampleModalCenter').modal('hide');
    }
    recargar() {
        document.getElementById("regForm").submit();
    }
}
