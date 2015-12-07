$(document).ready(function () {

    if (typeof isImageExists !== 'undefined') {
        $.ajax({
            type: 'POST',
            url: isImageExists,
            dataType: 'json',
            data: { id: userId },
            success: function (result) {
                if (result == true) {
                    $(".deleteImageActionBlock").toggleClass("hidden", false);
                    $(".uploadImageActionBlock").toggleClass("hidden", true);
                }
                if (result == false) {
                    $(".deleteImageActionBlock").toggleClass("hidden", true);
                    $(".uploadImageActionBlock").toggleClass("hidden", false);
                }
            },
            error: function (ex) {
                alert('Failed to info.' + ex);
            }
        });
    }
    return false;
});
