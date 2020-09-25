// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#modalC').on('shown.bs.modal', function () {
    $('#clienteID').focus();
});

$('#modalEditarAsignacion').on('shown.bs.modal', function () {
    $('#myInput').focus()
})



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

/**
 CODIGO DE ASIGNACION DE TAREAS
 */


function GetAsignacionTarea(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            console.log(response);
            mostrarAsignacionTarea(response);
        }
    });
}

function GetListadoEncargado(id, action) {
    alert(id);
    parseInt(id,10);
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            console.log(response);
            mostrarAsignacionTarea(response);
        }
    });
}

var items;
function mostrarAsignacionTarea(response) {
    items = response;

    $.each(items, function (index, val) {
        //Mostrar los datos de la asignacion que deseo eliminar
        $("#automovilID").text(val.recepcion.automovilID);
        $("#mantenimiento").text(val.recepcion.mantenimiento.nombre);
        $("#procedimiento").text(val.recepcion.procedimiento.procedimiento);
        $("#empleadoID").text(val.empleado.nombre);
        $("#fechaSalida").text(val.recepcion.fechaSalida);
        if (val.estadoTarea == true) {
            $("#estadoTarea").text("Finalizada");
        }
        else {
            $("#estadoTarea").text("No Finalizada");
        }
        $('input[name=asignacionTareaID]').val(val.asignacionTareaID);

        //Mostrar los datos de la asignacion de tarea que se está consultando
        $("#dAutomovilID").text(val.recepcion.automovilID);
        $("#dMantenimiento").text(val.recepcion.mantenimiento.nombre);
        $("#dProcedimiento").text(val.recepcion.procedimiento.procedimiento);
        $("#dEmpleadoID").text(val.empleado.nombre);
        $("#dFechaSalida").text(val.recepcion.fechaSalida);
        if (val.estadoTarea == true) {
            $("#dEstadoTarea").text("Finalizada");
        }
        else {
            $("#dEstadoTarea").text("No Finalizada");
        }

        //Mostrar los datos de la asignacion de tarea que quiero editar
        $('input[name=Id]').val(val.asignacionTareaID);
        $('input[name=eAutomovil]').val(val.recepcion.automovilID);
        $('input[name=eMantenimiento]').val(val.recepcion.mantenimiento.nombre);
        $('input[name=eProcedimiento]').val(val.recepcion.procedimiento.procedimiento);
        $('input[name=eEmpleado]').val(val.empleado.nombre);
        $('input[name=eFechaSalida]').val(val.recepcion.fechaSalida);
        if (val.estadoTarea == true) {
            $('select[name=eEstadoTarea]').val(val.estadoTarea);
        }
        else {
            $('select[name=eEstadoTarea]').val(val.estadoTarea);
        }

        $('input[name=nombreEmpleado]').val(val.recepcionID);
    });
}

function EditarAsignacionTarea(action) {

    var id;
    var encargado;
    var recepcion;
    var estado;
    //Se obtiene el estado seleccionado de la asignacion
    id = $('input[name=Id]')[0].value;
    estado = $('select[name=eEstadoTarea]')[0].value;

    $.each(items, function (index, val) {
        recepcion = val.recepcionID;
        encargado = val.empleadoID;
    });

    $.ajax({
        type: "POST",
        url: action,
        data: { id, recepcion, encargado, estado },
        success: function (response) {
            if (response == "Save") {
                window.location.href = "AsignacionTareas";
            }
            else {
                alert("No se pueden editar el estado de la tarea")
            }
        }
    })
}

function EliminarAsignacionTarea(action) {
    var id = $('input[name=asignacionTareaID]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "AsignacionTareas";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}

function OcultarDetalleAsignacion() {
    $("#modalDetalleAsignacion").hide();
}

//ASIGNACION TAREAS FINALIZADAS
function EditarAsignacionTareaFinalizada(action) {

    var id;
    var encargado;
    var recepcion;
    var estado;
    //Se obtiene el estado seleccionado de la asignacion
    id = $('input[name=Id]')[0].value;
    estado = $('select[name=eEstadoTarea]')[0].value;

    $.each(items, function (index, val) {
        recepcion = val.recepcionID;
        encargado = val.empleadoID;
    });

    $.ajax({
        type: "POST",
        url: action,
        data: { id, recepcion, encargado, estado },
        success: function (response) {
            if (response == "Save") {
                window.location.href = "TareasFinalizadas";
            }
            else {
                alert("No se pueden editar el estado de la tarea")
            }
        }
    })
}

function EliminarAsignacionTareaFinalizada(action) {
    var id = $('input[name=asignacionTareaID]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "TareasFinalizadas";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}

//ASIGNACION TAREAS EMPLEADO
function EditarAsignacionTareaE(action) {

    var id;
    var encargado;
    var recepcion;
    var estado;
    //Se obtiene el estado seleccionado de la asignacion
    id = $('input[name=Id]')[0].value;
    estado = $('select[name=eEstadoTarea]')[0].value;

    $.each(items, function (index, val) {
        recepcion = val.recepcionID;
        encargado = val.empleadoID;
    });

    $.ajax({
        type: "POST",
        url: action,
        data: { id, recepcion, encargado, estado },
        success: function (response) {
            if (response == "Save") {
                window.location.href = "TareasEnProcesoEmpleado";
            }
            else {
                alert("No se pueden editar el estado de la tarea")
            }
        }
    })
}
function EditarTareaFinalizadaEmpleado(action) {

    var id;
    var encargado;
    var recepcion;
    var estado;
    //Se obtiene el estado seleccionado de la asignacion
    id = $('input[name=Id]')[0].value;
    estado = $('select[name=eEstadoTarea]')[0].value;

    $.each(items, function (index, val) {
        recepcion = val.recepcionID;
        encargado = val.empleadoID;
    });

    $.ajax({
        type: "POST",
        url: action,
        data: { id, recepcion, encargado, estado },
        success: function (response) {
            if (response == "Save") {
                window.location.href = "TareasFinalizadasEmpleado";
            }
            else {
                alert("No se pueden editar el estado de la tarea")
            }
        }
    })
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


