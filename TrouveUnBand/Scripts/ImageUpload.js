var imageCropWidth = 0;
var imageCropHeight = 0;
var cropPointX = 0;
var cropPointY = 0;
var Percentage;

function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();

        reader.onload = function (event) {
            api.destroy();
            $('#PicToCrop').replaceWith('<img id="PicToCrop" class="CropThumbNail" src="' + event.target.result + '"/>');
            initCrop();
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#ImageUploader").change(function () {
    if (FileSize = this.files[0].size > 3145728)
    {
        alert("La taille de l'image ne doit pas dépasser 3 mo.");
        $("#ImageUploader").wrap('<form>').closest('form').get(0).reset();
        $("#ImageUploader").unwrap();
        return;
    }
    readURL(this);
    $("#CropperDialog").modal("show");
});

$(document).ready(function () {
    initCrop();
});

function initCrop() {
    $('#PicToCrop').Jcrop({
        onChange: setCoordsAndImgSize,
        aspectRatio: 250 / 172,
        bgOpacity: 0.5,
        bgColor: 'white',
        keySupport: false
    }, function () {
        api = this;
        api.setSelect([50, 50, 100, 250]);
        api.setOptions({ bgFade: true });
        api.ui.selection.addClass('jcrop-selection');
    });
}

function setCoordsAndImgSize(e) {
    var element;

    element = document.getElementById("X");
    element.value = Math.round(e.x);

    element = document.getElementById("Y");
    element.value = Math.round(e.y);

    element = document.getElementById("Width");
    element.value = Math.round(e.w);

    element = document.getElementById("Height");
    element.value = Math.round(e.h);
}

$("#closeDialog").click(function () {
    $("#ImageUploader").wrap('<form>').closest('form').get(0).reset();
    $("#ImageUploader").unwrap();
    return;
});