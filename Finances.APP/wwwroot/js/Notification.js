$(document).ready(function () {
    var successMessage = $('#success').val();
    var infoMessage = $('#info').val();
    var warningMessage = $('#warning').val();
    var errorMessage = $('#error').val();

    if (successMessage) {
        toastr.success(successMessage);
    }
    if (infoMessage) {
        toastr.info(infoMessage);
    }
    if (warningMessage) {
        toastr.warning(warningMessage);
    }
    if (errorMessage) {
        toastr.error(errorMessage);
    }
});