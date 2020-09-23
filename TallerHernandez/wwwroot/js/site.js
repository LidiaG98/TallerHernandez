// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#modalC').on('shown.bs.modal', function () {
    $('#clienteID').focus();
});

$('#exampleModalCenter').on('shown.bs.modal', function () {
    $('#nombre').focus();
});
$('#modalProc').on('shown.bs.modal', function () {
    $('#procedimiento').focus();
});
var nuevoCliente = () => {

    var clienteID = document.getElementById("clienteID").value;
    var nombre = document.getElementById("nombre").value;
    var apellido = document.getElementById("apellido").value;
    var correo = document.getElementById("correo").value;
    var telefono = document.getElementById("telefono").value;
    var puntos = document.getElementById("telefono").value;
    var accion = "Clientes/nuevoCliente";
    var cliente = new Cliente(clienteID, nombre, apellido, correo, telefono,puntos, accion);
    cliente.nuevoCliente();
}


//Funciones para roles

var rolNomCrear;

$('#modalCrearRol').on('shown.bs.modal', function () {
    $('#rolNomCrear').focus();
});

function crearRol(action) {
    //obteniendo datos de los inputs
    rolNomCrear = $('input[name=rolNomCrear]')[0].value;

    //verificando que no esten vacíos
    if (rolNomCrear == "") {
        $('#rolNomCrear').focus();
        alert("Ingrese el nombre del rol");
    }
    else {
        console.log(rolNomCrear);
        $.ajax({
            type: "POST",
            url: action,
            data: {
                rolNomCrear
            },
            success: function (response) {
                if (response === "Save") {
                    console.log("Si");
                    window.location.href = "Rols";
                }
                else {
                    console.log("nel");
                    $('mensajecrear').html("Ha ocurrido algún error, no se puede insertar el rol");
                }
            }
        });
    }

}

