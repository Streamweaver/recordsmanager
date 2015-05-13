$(function () { 
    $("#nameSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Staff/AsyncLookUp",
                type: "POST",
                data: { "term": request.term },
                dataType: "Json",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            value: item.Key,
                            label: item.Value
                        }
                    }));
                },
                error: function (jqXHR, data, errorThrown) {
                    console.log(data);
                }
            })
        },
        minLength: 3,
        select: function (event, ui) {
            $('#Staff_ID').val(ui.item.value);
            $('#nameSearch').val(ui.item.label);
            return false;
        }
    });
});
        

    