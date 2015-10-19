
$(document).ready(function () {

    $("#organization").change(function () {
        if ($("#organization option:selected").text() == "Select a Organization") {
            $(".ddl").toggleClass("hidden", true);
        } else {
            $(".ddl").toggleClass("hidden", false);
        }
        $("#department").empty();
        $.ajax({
            type: 'POST',
            url: getDepartmentsAction,
            dataType: 'json',
            data: { id: $("#organization").val() },
            success: function (states) {
                $.each(states, function (i, state) {
                    $("#department").append('<option value="' + state.Value + '">' + state.Text + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });
        return false;
    });
});