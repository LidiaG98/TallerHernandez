function readFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            console.log(e.target.result);
            var previewZone = document.getElementById('file-preview-zone'); //Asignar como id en el html 'file-preview-zone' donde se vea la imagen
            previewZone.src = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
}
var fileUpload = document.getElementById('files'); //Asignar como id en el html 'files' en el input que captura la imagen
fileUpload.onchange = function (e) {
    readFile(e.srcElement);
}