(function () {
    $('.dropdown-trigger').dropdown();

    $('#PictureURL').click(function () {
        $('#PictureUrlInput').show();
        $('#UploadImageInput').hide();
    });
    $('#UploadImage').click(function () {
        $('#PictureUrlInput').hide();
        $('#UploadImageInput').show();
    });
})();