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
$('#cAuto').hide();
$('#tAuto').show();
$(document).ready(function () {
    $('#switchAuto').on('click',function () {
        if ($('#switchAuto').is(':checked'))
        {
            
            $('#tAuto').hide();
            $('#cAuto').show();
        } else {
            $('#cAuto').hide();
            $('#tAuto').show();

        }
        
       
    });
});
$(document).ready(function () {
    $('.chosen-s').chosen();
});

var items;
function AhiTeVoy(response) {
    items = response;

    $.each(items, function (index, val) {
        //Mostrar los datos de la asignacion que deseo eliminar
        $("#ahMeVengo").text(val.automovilID);

        $('input[name=automovilID]').val(val.automovilID);

    });
}

function VeniteAuto(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            AhiTeVoy(response);
        }
    });
}


function MuerteALasMaquinas(action) {
    var id = $('input[name=automovilID]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "Automovils";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}

function VeniteAuto(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            AhiTeVoy(response);
        }
    });
}


function MuerteALasMaquinas(action) {
    var id = $('input[name=automovilID]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "Automovils";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}

function VeniteCliente(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            AhiTeVoyC(response);
        }
    });
}


function EliminarCliente(action) {
    var id = $('input[name=clienteID]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "Clientes";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}
function AhiTeVoyC(response) {
    items = response;

    $.each(items, function (index, val) {
        //Mostrar los datos de la asignacion que deseo eliminar
        $("#ahMeVengo").text(val.clienteID);

        $('input[name=clienteID]').val(val.clienteID);

    });
}
function AhiTeVoyE(response) {
    items = response;

    $.each(items, function (index, val) {
        //Mostrar los datos de la asignacion que deseo eliminar
        $("#ahMeVengo").text(val.empleadoID);

        $('input[name=empleadoID]').val(val.empleadoID);

    });
}

function VeniteEmpleado(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            AhiTeVoyE(response);
        }
    });
}


function EliminarEmpleado(action) {
    var id = $('input[name=empleadoID]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "Empleadoes";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}


