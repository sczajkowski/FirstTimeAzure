

(function () {
    var txtMessage = $('#txtMessage')
    var btnSend = $('#btnSend')
    var listMessages = $('#listMessages')
    var connection = new signalR.HubConnectionBuilder().withUrl('/chathub').build();
    var userName = $('#userName').val();



    $(btnSend).click(function () {
        var userMessage = $(txtMessage).val();
        connection.invoke('SendMessage', {
            userName: userName,
            message: userMessage
        }).catch(function (error) {
            alert("Nie można wysłać");
            console(error);
        })
        $(txtMessage).val('');
    });



    connection.start().then(function () {
        console.log('Connection to the signalhub');
        $(btnSend).removeAttr('disabled');
    })
    connection.on('ReceiveMessage', function (obj) {
        $(listMessages).prepend('<li>['
            + obj.timeStampString + ']'
            + '</span class="font-weight-bold">user:' + obj.username
            + '</span> | message' + obj.message
            + '</li>'
        )
    })
})();