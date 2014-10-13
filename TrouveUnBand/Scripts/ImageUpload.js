var imageCropWidth = 0;
var imageCropHeight = 0;
var cropPointX = 0;
var cropPointY = 0;
var ImgUploaderControl = $("#ImageUploader");

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            
            api.destroy();
            $('#PicToCrop').replaceWith('<img id="PicToCrop" class="image-to-crop" src="' + e.target.result + '"/>');
            initCrop();
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#ImageUploader").change(function () {
    if (FileSize = this.files[0].size > 1048576)
    {
        alert("La taille de l'image ne doit pas dépasser 1mo.");
        $("#ImageUploader").wrap('<form>').closest('form').get(0).reset();
        $("#ImageUploader").unwrap();
        return;
    }
    readURL(this);
    $('#CropperDialog').modal('show');
});

$(document).ready(function () {
    initCrop();
});

$("#CropImage").on("click", function (e) {
    e.preventDefault();
    cropImage();
});


function initCrop() {
    $('#PicToCrop').Jcrop({
        onChange: setCoordsAndImgSize,
        aspectRatio: 250 / 172,
        bgOpacity: 0.5,
        bgColor: 'white',
    }, function () {
        api = this;
        api.setSelect([50, 50, 100, 250]);
        api.setOptions({ bgFade: true });
        api.ui.selection.addClass('jcrop-selection');
    });
}

function setCoordsAndImgSize(e) {

    imageCropWidth = e.w;
    imageCropHeight = e.h;
    
    cropPointX = e.x;
    cropPointY = e.y;
}

function cropImage() {

    if (imageCropWidth == 0 && imageCropHeight == 0) {
        alert("Please select crop area.");
        return;
    }

    $.ajax({
        url: '/Users/CropImage',
        type: 'POST',
        data: {
            imagePath: $("#PicToCrop").attr("src"),
            cropPointX: cropPointX,
            cropPointY: cropPointY,
            imageCropWidth: imageCropWidth,
            imageCropHeight: imageCropHeight,
            stringf:"vs"
        },
        success: function (data) {
            //$("#PicToCrop")
            //    .attr("src", data.photoPath + "?t=" + new Date().getTime())
            //    .show();
            alert("Success");
        },
        error: function (data) {
            alert("Error");
        }
    });
}