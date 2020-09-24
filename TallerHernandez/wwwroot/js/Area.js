class Area {
    constructor(areaNom,accion)
    {
        this.areaNom = areaNom;
        this.accion = accion;
    }
    nuevoArea()
    {
        if (this.areaNom == "") {
            document.getElementById("areaNom").focus();
        } else {
            var areaNom = this.areaNom;
            var accion = this.accion;
            var mensaje = ""
            $.ajax({
                type: "POST",
                url: accion,
                data: {
                    areaNom
                },
                success: (response) => {
                    $.each(response, (index, val) => {
                        mensaje = val.code;


                    });
                    if (mensaje == "Save") {
                        this.hastalaproxima();

                    } else {
                        document.getElementById("mensaje").innerHTML = "Esta Area ya existe, prueba con otro nombre";
                    }
                }
            });
        }
    }
    hastalaproxima() {

        document.getElementById("areaNom").value = "";
        
        $('#modalAr').modal('hide');
    }
}