var imageCropWidth = 0;
var imageCropHeight = 0;
var cropPointX = 0;
var cropPointY = 0;
var modal;

if ($("#CropperDialog")[0]) {
    modal = $("#CropperDialog");
}

modal.on("hidden.bs.modal", function (e) {
    api.destroy();
});

function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();
        var image = new Image();
        var imageSrc;

        reader.onload = function (event) {
            imageSrc = event.target.result;
            image.src = imageSrc;

            image.onload = function () {
                var imgWidth = this.width;
                var imgHeight = this.height;

                if (imgWidth < 250 || imgHeight < 172 || imgWidth > 800 || imgHeight > 600) {
                    imageSrc = resizeImage(image);
                }

                $("#PicToCrop").attr("src", imageSrc);
                initCrop();
            };
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

function resizeImage(image) {
    var width = image.width;
    var height = image.height;
    var MAX_WIDTH = 800;
    var MAX_HEIGHT = 600;
    var MIN_WIDTH = 250;
    var MIN_HEIGHT = 172;
    var canvas = document.createElement("canvas");

    if (width < MIN_WIDTH) {
        height *= MIN_WIDTH / width;
        width = MIN_WIDTH;
    }

    if (height < MIN_HEIGHT) {
        width *= MIN_HEIGHT / height;
        height = MIN_HEIGHT;
    }

    if (width > MAX_WIDTH) {
        height *= MAX_WIDTH / width;
        width = MAX_WIDTH;
    }

    if (height > MAX_HEIGHT) {
        width *= MAX_HEIGHT / height;
        height = MAX_HEIGHT;
     }

    canvas.width = width;
    canvas.height = height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(image, 0, 0, width, height);

    setModalWidth(width);

    return canvas.toDataURL();
}

function setModalWidth(modalWidth) {
    modal.find('.modal-dialog').css({ 'width': modalWidth + 100 });
}
