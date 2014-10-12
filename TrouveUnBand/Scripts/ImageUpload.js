var imageCropWidth = 0;
var imageCropHeight = 0;
var cropPointX = 0;
var cropPointY = 0;

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#PicToCrop').attr('src', e.target.result);
            $('.jcrop-holder img').attr('src', e.target.result);
            api.destroy();
            initCrop();
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#ImageUploader").change(function () {
    readURL(this);
    $('#CropperDialog').modal('show');
});


$(document).ready(function () {
    initCrop();
});

$("#hl-crop-image").on("click", function (e) {
    e.preventDefault();
    cropImage();
});


function initCrop() {
    $('#PicToCrop').Jcrop({
        onChange: setCoordsAndImgSize,
        aspectRatio: 250 / 172,
        addClass: 'jcrop-light',
        bgOpacity: 0.4,
        bgColor: 'black',
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
        url: '/Image/CropImage',
        type: 'POST',
        data: {
            imagePath: $("#my-origin-image").attr("src"),
            cropPointX: cropPointX,
            cropPointY: cropPointY,
            imageCropWidth: imageCropWidth,
            imageCropHeight: imageCropHeight
        },
        success: function (data) {
            $("#my-cropped-image")
                .attr("src", data.photoPath + "?t=" + new Date().getTime())
                .show();
        },
        error: function (data) { }
    });
}