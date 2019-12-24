$(document).ready(function () {
    //Function to auto fill data into UpdateForm
    $('input[name="trackRadio"]').change(function () {
        if ($(this).is(':checked')) {
            //alert($(this).val());

            var id = $(this).attr("id").substring(6);

            $("#txtId").val(id);
            $("#txtIdDelete").val(id);

            $("#txtName").val($(`#name_${id}`).text());
            $("#txtType").val($(`#type_${id}`).text());
            $("#txtDateAdded").val($(`#addedDate_${id}`).text());
            $("#txtMfgName").val($(`#mfgName_${id}`).text());
            $("#txtMfgDate").val($(`#mfgDate_${id}`).text());
            $("#txtDateExpired").val($(`#expiredDate_${id}`).text());
            $("#txtStock").val($(`#stock_${id}`).text());
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




            //get the focus element
            //var $focused = $(':focus');
            //var id = $focused.attr("id").substring(10);
            //Cannot get value attribut then use text