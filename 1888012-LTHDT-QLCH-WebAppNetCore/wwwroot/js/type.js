$(document).ready(function () {
    //Function to auto fill data into UpdateForm
    $('input[name="trackRadio"]').change(function () {
        if ($(this).is(':checked')) {
            //alert($(this).val());

            var id = $(this).attr("id").substring(6);

            $("#txtId").val(id);
            $("#txtIdDelete").val(id);

            $("#txtName").val($(`#name_${id}`).text());
            $("#txtDateAdded").val($(`#addedDate_${id}`).text());
            $("#txtStatus").val($(`#status_${id}`).text());

        }
    });

});
//Function to submit UpdateForm
function submitForm() {
    $("#updateForm").submit();
}
//Function to submit Deleteform
function submitFormWithAlert() {
    var result = confirm("Want to delete?");
    if (result) {
        $("#deleteForm").submit();

    }
    else {
        $("#txtIdDelete").val(0);
        $('input[name="trackRadio"]').each(function () {
            $(this).prop('checked', false);
        });
    }
}