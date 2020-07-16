function readFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            console.log(e.target.result);
            var previewZone = document.getElementById('file-preview-zone');
            previewZone.src = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
}
var fileUpload = document.getElementById('files');
fileUpload.onchange = function (e) {
    readFile(e.srcElement);
}