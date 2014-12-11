var imageCropWidth = 0;
var imageCropHeight = 0;
var cropPointX = 0;
var cropPointY = 0;
var modal;
var isClosedByButton = false;
var image = new Image();

if ($("#CropperDialog")[0]) {
    modal = $("#CropperDialog");
}

if ($("#X")[0]) {
    var element;

    element = document.getElementById("X");
    element.value = 0;

    element = document.getElementById("Y");
    element.value = 0;

    element = document.getElementById("Width");
    element.value = 0;

    element = document.getElementById("Height");
    element.value = 0;
}

modal.on("hidden.bs.modal", function (e) {
    if (!isClosedByButton) {
        $("#ImageUploader").wrap('<form>').closest('form').get(0).reset();
        $("#ImageUploader").unwrap();
    }
    api.destroy();
    return;
});

image.onload = function () {
    resizeImage(this);
    $("#PicToCrop").attr("src", this.src);
    initCrop();
};

function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();
        var imageSrc;

        reader.onload = function (event) {
            imageSrc = event.target.result;
            image.src = imageSrc;
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#ImageUploader").change(function () {
    if (FileSize = this.files[0].size > 3145728) {
        alert("La taille de l'image ne doit pas dépasser 3 mo.");
        $("#ImageUploader").wrap('<form>').closest('form').get(0).reset();
        $("#ImageUploader").unwrap();
        return;
    }
    readURL(this);
    $(".photo-upload-filename").val(this.value);
    $(".photo-upload-clear").show()

    setModalButton();
    isClosedByButton = false;
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

function resizeImage(image) {
    var width = image.width;
    var height = image.height;
    var MAX_WIDTH = 800;
    var MAX_HEIGHT = 600;
    var MIN_WIDTH = 250;
    var MIN_HEIGHT = 172;

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

    $("#PicToCrop").attr("width", width);
    $("#PicToCrop").attr("height", height);

    setModalWidth(width);
}

function setModalWidth(modalWidth) {
    modal.find('.modal-dialog').css({ 'width': modalWidth + 100 });
}

function setModalButton() {
    var isToSend = modal.attr("data-send");

    if (isToSend == "false") {
        $("#sendButton").prop("type", "button");
        return;
    }
}

$("#sendButton").click(function () {
    isClosedByButton = true;
    $("#CropperDialog").modal("hide");
});

$("#photo-URL-btn").click(function () {
    var photoSrc = $("#PhotoSrcInput").val();

    if (photoSrc.length === 0) {
        alert("Veuillez inscrire l'URL d'une photo");
        return;
    }

    image.src = photoSrc;
    $("#PhotoSrc").val(photoSrc);

    $("#CropperDialog").modal("show");
});