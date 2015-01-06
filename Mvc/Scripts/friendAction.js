$('#searchResults').ready(function () {
    $('.btnFriendAction').click(onSubmitForm);
});


function onSubmitForm() {
    var form = $(this).closest('form');
    var serialized = form.serialize();
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var data = JSON.parse(xmlhttp.responseText);
            $('#btnFriendAction' + data.id.toString()).text(data.text);
            $('#search').focus();
        }
    };
    xmlhttp.open("POST", "/Users/FriendAction", true);
    xmlhttp.setRequestHeader("X-Requested-With", "XMLHttpRequest");
    xmlhttp.setRequestHeader("Accept", "json");
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.send(serialized);
    return false;
};

