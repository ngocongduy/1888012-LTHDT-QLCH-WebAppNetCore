$(document).ready(function () {
    
    

});


//Function to hide and unhide detail button
function viewDetail(name) {
    id = name.substring(10);
    //console.log(name);
    //console.log(id);

    $("div[name^='detail_']").each(function () {
        if (!$(this).is(':hidden')) {
            $(this).attr('hidden', true);
            //alert(this.hidden);
        }
    });

    $(`div[name='detail_${id}']`).removeAttr('hidden');
}

//Get stock change data from checked products
function getData() {
    var selectedItems = new Array();
    var values = new Array();
    //select where 'name' beginning with '..'
    $("input:checked").each(function () {
        selectedItems.push(this.id);
        //console.log(selectedItems);
        //console.log(this);
    });
    for (var i = 0; i < selectedItems.length; i++) {
        var name = `stockChange_${selectedItems[i]}`;
        //console.log(name);
        var v = $(`input[name=${name}]`).val();
        values.push(v);
    }

    var user = $("#txtUser").val();
    return [user, selectedItems, values];
}

