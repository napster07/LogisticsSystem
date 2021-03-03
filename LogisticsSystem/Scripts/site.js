$(document).ready(function ($) {
    $(".table-row").click(function () {
        var _row = $(this).parents("tr");
        var cols = _row.children("td");
        $("#txtItemID").val($(cols[1]).text());
        $("#txtItemName").val($(cols[2]).text());
        $("#txtWeight").val($(cols[3]).text());
    });    
});

function DeleteItems() {

    var selectedIDs = GetSelectedItems();

    if (selectedIDs == "") {
        $('#NoSelectedItemModal').modal('show'); 
    }
    else {
        $.ajax({
            type: "POST",
            url: "Home/Delete", //remove the first slash from the url
            data: { input: selectedIDs },
            success: function (data) {
                location.reload(true);
            },
            error: function (data) {
                alert(data); //error handler
            }
        });
    }    
}

function GetSelectedItems() {

    var selected = [];
    $('#itemTable tbody tr td input:checked').each(function () {
        selected.push($(this).attr('value'));
    });
    return selected.toString();
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

