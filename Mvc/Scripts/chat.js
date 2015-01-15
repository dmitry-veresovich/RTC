$(function () {
    window.chat = $.connection.chatHub;
    window.chat.client.sendWordToClient = function (word) {
        appendNewWord(word, "friendMessage");
    }
    $.connection.hub.start();
});

function changeChattingTo(id) {
    var data = "Id=" + id;
    var url = "/Chat/ChattingTo";
    $.post(url, data, function (response) {
        $('#chattingTo').empty().append(response);
        setUpChatting();
    });
    window.chat.server.connectToUser(id);
}

function setUpChatting() {
    var message = document.getElementById('word');
    message.disabled = false;
    $('#word').focus();

    $('#word').keyup(function (e) {
        var lastSymbol = String.fromCharCode(e.which);
        if (isStringEmpty(lastSymbol)) {
            var word = message.value.trim() + " ";
            var identity = document.getElementById('identity').value;
            $.connection.chatHub.server.sendWord(word, identity);
            appendNewWord(word, "message");
            $('#word').val('').focus();
        }
    });

    //$('#word').keyup(function (e) {
    //    var word = message.value;
    //    chat.server.sendWord(word, 10);
    //    appendNewWord(word, "message");
    //    $('#word').val('').focus();
    //});
}



function appendNewWord(word, id) {
    $('#' + id).append(htmlEncode(word));
};

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function isStringEmpty(s) {
    return !s.trim();
};
