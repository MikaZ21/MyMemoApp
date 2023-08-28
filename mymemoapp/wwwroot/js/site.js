function deleteMemo(i)
{
    $.ajax({
        url: 'Home/Delete',
        type: 'POST',
        data: {
            id: i
        },
        success: function() {
            window.location.reload();
        }
    });
}

function populateForm(i) 
{
    $.ajax({
            url: '/Home/PopulateForm',
            type: 'GET',
            data: {
                id: i
        },
        dataType: 'json',
        success: function (response) {
            $("#Memo_Name").val(response.name);
            $("#Memo_Id").val(response.id);
            $("#form-button").val("Update Memo");
            $("#form-action").attr("action", "/Home/Update");
        }
    });
}