﻿var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab

function showTab(n) {
    // This function will display the specified tab of the form ...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    // ... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        document.getElementById("nextBtn").innerHTML = "Guardar";
        document.getElementById("nextBtn").value = "Create";        
        document.getElementById("nextBtn").className = "btn btn-success";
    } else {
        document.getElementById("nextBtn").innerHTML = "Siguiente";
        document.getElementById("nextBtn").className = "btn btn-outline-dark";
        document.getElementById("nextBtn").value = ""; 
    }
    // ... and run a function that displays the correct step indicator:
    fixStepIndicator(n)
}

function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    if (n == 1 && !validateForm()) return false;
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form... :
    if (currentTab >= x.length) {
        //...the form gets submitted:
        document.getElementById("regForm").submit();
        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}
sv = document.getElementById('proc-invalid');

function ocultar() {
    sv.className = "text-danger d-none";
    document.getElementById("sproc").className.replace(" invalid", "");
}

function validateForm() {
    // This function deals with validation of the form fields
    var x, y, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByTagName("input");
    z = x[currentTab].getElementsByTagName("textarea");
    s = x[currentTab].getElementsByClassName("search form-control activo");
    
    // A loop that checks every input field in the current tab:
    for (i = 0; i < y.length; i++) {
        // If a field is empty...
        if (y[i].value == "") {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            // and set the current valid status to false:
            valid = false;
        }
    }

    for (i = 0; i < z.length; i++) {
        // If a textarea is empty...
        if (z[i].value == "") {            
            // add an "invalid" class to the field:
            z[i].className += " invalid";
            // and set the current valid status to false:
            valid = false;
        }
    }

    for (i = 0; i < s.length; i++) {
        // If a select is empty...        
        if (s[i].value == "") {
            // add an "invalid" class to the field:
            s[i].className += " invalid";
            sv.className = "text-danger d-block";
            // and set the current valid status to false:
            valid = false;
        }
    }

    // If the valid status is true, mark the step as finished and valid:
    if (valid) {
        document.getElementsByClassName("step")[currentTab].className += " finish";
    }
    return valid; // return the valid status
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class to the current step:
    x[n].className += " active";
}


//************************************************* Crear mantenimientos y procedimientos********************************************
var agregarMantenimiento = () => {
    var nombre = document.getElementById("nombre").value;
    var precio = document.getElementById("precio").value;
    var areaIDs = document.getElementById('areaID');
    var areaID = areaIDs.options[areaIDs.selectedIndex].value;
    var action = "agregarMantenimiento";
    var mantenimiento = new Mantenimiento(nombre, precio, areaID, action);
    mantenimiento.agregarMantenimiento();
}

var agregarProcedimiento = () => {
    document.getElementById("mensajeProc").innerHTML = "";
    var nombre = document.getElementById("procedimiento").value;
    var precio = document.getElementById("precioProc").value;
    var areaIDs = document.getElementById('areaIDProc');
    var areaID = areaIDs.options[areaIDs.selectedIndex].value;
    var action = "agregarProcedimiento";
    var procedi = new Procedimiento(nombre, precio, areaID, action);
    procedi.agregarProcedimiento();
}
