function sortTable(column) {
    var form = $("#filterForm");
    var formData = form.serialize() + '&sortColumn=' + column;
    $.ajax({
        url: '@Url.Page("Kullanicis")',
        type: 'get',
        data: formData,
        success: function (data) {
            $("#userTableBody").html($(data).find("#userTableBody").html());
        }
    });
}