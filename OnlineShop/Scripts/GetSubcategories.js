// set default value to dropdown list
document.onreadystatechange=function(){
    a = document.getElementById("PrRootCatID");
    //a.selectedIndex = 2;
    //getSubCategories(a, "PrSubCatID");
};
function getSubCategories(option, destination) {
    var selectedItem = $(option).val();
    var subCats = $(destination);
    var statesProgress = $("#subcat-loading-progress");
    statesProgress.show();
    $.ajax({
        cache: false,
        type: "GET",
        url: "@(Url.RouteUrl("GetSubCatsId"))",
        data: { option: selectedItem },
    success: function (data) {
        subCats.html('');
        $.each(data, function (id, option) {
            subCats.append($('<option></option>').val(option.id).html(option.name));
        });
        //statesProgress.hide();
    },
    error: function (xhr, ajaxOptions, thrownError) {
        alert('Failed to retrieve sub categories.');
        //statesProgress.hide();
    }
});
};