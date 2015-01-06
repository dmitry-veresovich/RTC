$(function () {
    var chat = $.connection.chatHub;
    window.chat = chat;

    chat.client.sendWordToClient = function (word) {
        appendNewWord(word, "friendMessage");
    }

    $.connection.hub.start();
});

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

function changeChattingTo(id) {
    var data = "Id=" + id;
    var url = "/Chat/ChattingTo";
    $.post(url, data, function (response) {
        $('#chattingTo').empty().append(response);
        setUpChatting();
    });



    //var chat = window.chat;
    //chat.server.getChattingTo(id);    
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
            chat.server.sendWord(word, identity);
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
