// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#modalC').on('shown.bs.modal', function () {
    $('#clienteID').focus();
});

$('#exampleModalCenter').on('shown.bs.modal', function () {
    $('#nombre').focus();
    document.getElementById('mensaje').innerHTML = "";
});
$('#modalProc').on('shown.bs.modal', function () {
    $('#procedimiento').focus();
    document.getElementById('mensajeProc').innerHTML = "";
});
$('#modalEditarAsignacion').on('shown.bs.modal', function () {
    $('#myInput').focus();
    document.getElementById('mensaje').innerHTML = "";
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

//Funciones para roles
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
        if (val.recepcion.mantenimientoID != null && val.recepcion.procedimientoID == null) {
            $("#automovilID").text(val.recepcion.automovilID);
            $("#mantenimiento").text(val.recepcion.mantenimiento.nombre);
            $("#empleadoID").text(val.empleado.nombre);
            $("#fechaSalida").text(val.recepcion.fechaSalida);
            if (val.estadoTarea == true) {
                $("#estadoTarea").text("Finalizada");
            }
            else {
                $("#estadoTarea").text("No Finalizada");
            }
            $('input[name=asignacionTareaID]').val(val.asignacionTareaID);
        } else if (val.recepcion.mantenimientoID == null && val.recepcion.procedimientoID != null) {
            $("#automovilID").text(val.recepcion.automovilID);
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
        } else if (val.recepcion.mantenimientoID != null && val.recepcion.procedimientoID != null) {
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
        }

        //Mostrar los datos de la asignacion de tarea que se está consultando
        if (val.recepcion.mantenimientoID != null && val.recepcion.procedimientoID == null) {
            $("#dAutomovilID").text(val.recepcion.automovilID);
            $("#dMantenimiento").text(val.recepcion.mantenimiento.nombre);
            $("#dEmpleadoID").text(val.empleado.nombre);
            $("#dFechaSalida").text(val.recepcion.fechaSalida);
            if (val.estadoTarea == true) {
                $("#dEstadoTarea").text("Finalizada");
            }
            else {
                $("#dEstadoTarea").text("No Finalizada");
            }
        }
        else if (val.recepcion.mantenimientoID == null && val.recepcion.procedimientoID != null) {
            $("#dAutomovilID").text(val.recepcion.automovilID);
            $("#dProcedimiento").text(val.recepcion.procedimiento.procedimiento);
            $("#dEmpleadoID").text(val.empleado.nombre);
            $("#dFechaSalida").text(val.recepcion.fechaSalida);
            if (val.estadoTarea == true) {
                $("#dEstadoTarea").text("Finalizada");
            }
            else {
                $("#dEstadoTarea").text("No Finalizada");
            }
        } else if (val.recepcion.mantenimientoID != null && val.recepcion.procedimientoID != null) {
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
        }

        //Mostrar los datos de la asignacion de tarea que quiero editar
        if (val.recepcion.mantenimientoID != null && val.recepcion.procedimientoID == null) {
            $('input[name=Id]').val(val.asignacionTareaID);
            $('input[name=eAutomovil]').val(val.recepcion.automovilID);
            $('input[name=eMantenimiento]').val(val.recepcion.mantenimiento.nombre);
            $('input[name=eEmpleado]').val(val.empleado.nombre);
            $('input[name=eFechaSalida]').val(val.recepcion.fechaSalida);
            if (val.estadoTarea == true) {
                $('select[name=eEstadoTarea]').val(val.estadoTarea);
            }
            else {
                $('select[name=eEstadoTarea]').val(val.estadoTarea);
            }

            $('input[name=nombreEmpleado]').val(val.recepcionID);
        } else if (val.recepcion.mantenimientoID == null && val.recepcion.procedimientoID != null) {
            $('input[name=Id]').val(val.asignacionTareaID);
            $('input[name=eAutomovil]').val(val.recepcion.automovilID);
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
        } else if (val.recepcion.mantenimientoID != null && val.recepcion.procedimientoID != null) {
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
        }
    });
}

function EditarAsignacionTarea(action) {

    var id;
    var encargado;
    var recepcion;
    var estado;
    //Se obtiene el estado seleccionado de la asignacion
    id = $('input[name=Id]')[0].value;
    var x = document.getElementById('eEstadoTarea');
    estado = x.options[x.selectedIndex].value;

    $.each(items, function (index, val) {
        recepcion = val.recepcionID;
        encargado = val.empleadoID;
    });

    if (!(estado == "true" || estado== "false")) {
        document.getElementById("mensaje").innerHTML = "**Seleccione un estado";
    } else {
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
        });
    }   
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
    var x = document.getElementById('eEstadoTarea');
    estado = x.options[x.selectedIndex].value;

    $.each(items, function (index, val) {
        recepcion = val.recepcionID;
        encargado = val.empleadoID;
    });

    if (!(estado == "true" || estado == "false")) {
        document.getElementById("mensaje").innerHTML = "**Seleccione un estado";
    }
    else {
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
        $("#ahMeVengo").text(val.nombre);

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


function AhiTeVoyA(response) {
    items = response;

    $.each(items, function (index, val) {
        //Mostrar los datos de la asignacion que deseo eliminar
        $("#ahMeVengo").text(val.areaNom);

        $('input[name=AreaID]').val(val.areaID);

    });
}

function VeniteArea(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            AhiTeVoyA(response);
            console.log(response);
        }
    });
}


function EliminarArea(action) {
    var id = document.getElementsByName("AreaID")[0].value;
    alert(id);
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "Areas";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}


