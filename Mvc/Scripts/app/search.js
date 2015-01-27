$(document).ready(function () {
    $("#searchForm").submit(submitSearchForm);
    $('#search').focus();
});

function submitSearchForm(event) {
    $("#searchBtn").button("loading");
    event.preventDefault();
    var data = $(this).serialize();
    var url = $(this).attr("action");
    $.post(url, data, function (response) {
        $("#results").empty().append(response);
        $("#searchBtn").button("reset");
        $('#search').focus();
    });
}
