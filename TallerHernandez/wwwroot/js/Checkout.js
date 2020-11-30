let preciosPro = document.getElementsByClassName("precio-pro");
let preciosComen = document.getElementsByClassName("precio-comen");
let totalInput = document.getElementById("total");
let impuesto = document.getElementById("impuesto");
let totalNeto = document.getElementById("totalneto");
let pFecha = document.getElementById("fecha");
let f = new Date();
let valoresComen = [];
let valoresPro = [];
//console.log(preciosPro);
//console.log(preciosComen);

const calcularTotal = () => {
    let total = 0;
    valoresComen = [];
    for (const valor of preciosComen) {
        valoresComen.push(+valor.value);
    }
    valoresPro = [];
    for (const valor of preciosPro) {
        valoresPro.push(+valor.value);
    } 
    for (const valorPro of preciosPro) {
        total += +valorPro.value;
    }
    for (const valorComen of preciosComen) {
        total += +valorComen.value;
    }
    totalInput.value = total;
    totalNeto.value = total + (total * (impuesto.value / 100));
}

const fechaActual = () => {
    const month = f.getMonth() + 1;
    const day = f.getDate();
    const year = f.getFullYear();
    const hours = f.getHours();
    const minutes = f.getMinutes();
    const seconds = f.getSeconds();

    pFecha.innerHTML = "Fecha: " + day + "/"
        + month + "/" + year + " "
        + hours + ":" + minutes + ":" + seconds;
    console.log(pFecha);
    console.log(f);
}

const inicio = () => {
    preciosPro = document.getElementsByClassName("precio-pro");
    preciosComen = document.getElementsByClassName("precio-comen");
    totalInput = document.getElementById("total");   
    totalNeto = document.getElementById("totalneto");
    impuesto = document.getElementById("impuesto");
    for (const valorPro of preciosPro) {
        valorPro.addEventListener('input', () => calcularTotal());
    }
    for (const valorComen of preciosComen) {
        valorComen.addEventListener('input', () => calcularTotal());
    }    
    impuesto.addEventListener('input', () => calcularTotal());
}

for (const valorPro of preciosPro) {
    valorPro.addEventListener('input', () => calcularTotal());
}
for (const valorComen of preciosComen) {
    valorComen.addEventListener('input', () => calcularTotal());
}
impuesto.addEventListener('input', () => calcularTotal());

calcularTotal();
fechaActual();

var printThisDiv = (id) => {    
    let printContents = document.getElementById(id).innerHTML;
    let originalContent = document.body.innerHTML;    
    document.body.innerHTML = printContents;

    //Restaurando Valores a los inputs antes de imprimir
    let pC = document.getElementsByClassName("precio-comen");    
    var contador = 0;
    for (let valorComen of pC) {
        valorComen.value = valoresComen[contador];        
        contador++;
    }   
    let pP = document.getElementsByClassName("precio-pro");
    contador = 0;
    for (let valorP of pP) {
        valorP.value = valoresPro[contador];
        contador++;
    }  
    let tI = document.getElementById("total"); 
    tI.value = totalInput.value;    
    let tN = document.getElementById("totalneto");
    tN.value = totalNeto.value;
    let iP = document.getElementById("impuesto");
    iP.value = impuesto.value;

    //Impriir
    window.print();   

    //Restaurando Valores a los inputs al contenido original
    document.body.innerHTML = originalContent;
    tI = document.getElementById("total");
    tI.value = totalInput.value;
    pC = document.getElementsByClassName("precio-comen");
    contador = 0;
    for (let valorComen of pC) {
        valorComen.value = valoresComen[contador];
        contador++;
    }
    pP = document.getElementsByClassName("precio-pro");
    contador = 0;
    for (let valorP of pP) {
        valorP.value = valoresPro[contador];
        contador++;
    } 
    tN = document.getElementById("totalneto");
    tN.value = totalNeto.value;
    iP = document.getElementById("impuesto");
    iP.value = impuesto.value;

    inicio();
}