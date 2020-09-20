// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#modalC').on('shown.bs.modal', function () {
    $('#clienteID').focus();
});
$('#modalAr').on('shown.bs.modal', function () {
    $('#areaNom').focus();
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
var nuevoArea = () => {
    var areaNom = document.getElementById("areaNom").value;
    var accion = "Areas/nuevoArea";
    var area = new Area(areaNom,accion);
    area.nuevoArea();
}
$(document).ready(function () {
    $('#switchAuto').on('click',function () {
        if ($('#switchAuto').is(':checked'))
        {
            $('#tAuto').show();
            $('#cAuto').hide();
        } else {
            $('#tAuto').hide();
            $('#cAuto').show();

        }
        
       
    });
});

