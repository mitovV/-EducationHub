(function () {
    $('.dropdown-trigger').dropdown();

    $('#PictureURL').click(function () {
        $('.picture-url').show();
        $('.upload-image').hide();
    });
    $('#UploadImage').click(function () {
        $('.picture-url').hide();
        $('.upload-image').show();
    });
})();