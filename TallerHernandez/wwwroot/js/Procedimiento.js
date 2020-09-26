class Procedimiento {
    constructor(procedimiento, precio, areaID, action) {
        this.procedimiento = procedimiento;
        this.precio = precio;
        this.areaID = areaID;
        this.action = action;
    }
    agregarProcedimiento() {
        if (this.procedimiento == "") {
            document.getElementById("procedimiento").focus();
            document.getElementById("mensajeProc").innerHTML = "Describa el nuevo procedimiento";
        }
        else {
            if (this.precio == "") {
                document.getElementById("precioProc").focus();
                document.getElementById("mensajeProc").innerHTML = "Ingrese el precio de mano de obra";
            }
            else {                
                var procedimiento = this.procedimiento;
                var precio = this.precio;
                var areaID = this.areaID;
                var action = this.action;
                var mensaje = '';                
                $.ajax({
                    type: "POST",
                    url: action,
                    data: { procedimiento, precio, areaID },
                    success: (response) => {
                        console.log(response);
                        $.each(response, (index, val) => {
                            mensaje = val.code;
                        });
                        if (mensaje == "SaveProc") {
                            this.restablecer();
                            this.recargar();
                        }
                        else {
                            document.getElementById("mensajeProc").innerHTML = "No se pudo guardar el procedimiento";
                        }

                    }
                });
            }
        }
    }

    restablecer() {
        document.getElementById("procedimiento").value = "";
        document.getElementById("precioProc").value = "";
        document.getElementById("areaIDProc").selectedIndex = 0;
        $('#modalProc').modal('hide');
    }
    recargar() {
        document.getElementById("regForm").submit();
    }
}