$(document).ready(function () {

    if (typeof isImageExists !== 'undefined') {
        //alert('defined');
        $.ajax({
            type: 'POST',
            url: isImageExists,
            dataType: 'json',
            data: { id: userId },
            success: function (result) {
                if (result == true) {
                    $(".deleteImageActionBlock").toggleClass("hidden", false);
                    $(".uploadImageActionBlock").toggleClass("hidden", true);
                    //alert('true');
                }
                if (result == false) {
                    $(".deleteImageActionBlock").toggleClass("hidden", true);
                    $(".uploadImageActionBlock").toggleClass("hidden", false);
                    //alert('false');
                }
            },
            error: function (ex) {
                alert('Failed to info.' + ex);
            }
        });
    }
    //alert('undefined');
    return false;
});
