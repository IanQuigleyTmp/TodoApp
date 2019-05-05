
(function (todoApi, $, undefined) {

    
    auth_token = '';

    todoApi.currentUser = function (onComplete) {
        call('GET', '/api/user/current', {}, onComplete);
    };

    todoApi.createUser = function (credentials, onComplete) {
            call('POST', '/api/user/create', credentials, onComplete);
        };

    todoApi.authenticate = function (credentials, onComplete) {
        call('POST', '/api/user/authenticate', credentials, onComplete);
    };

    todoApi.logout= function (onComplete) {
        call('POST', '/api/user/logout', {}, onComplete);
    };

    todoApi.getAll = function (onComplete) {
        call('GET', '/api/todo/all', {}, onComplete);
    };

    todoApi.createTodo = function (data, onComplete) {
        call('POST', '/api/todo/create', data, onComplete);
    };

    todoApi.updateTodo = function (data, onComplete) {
        call('PATCH', '/api/todo/update', data, onComplete);
    };

    todoApi.deleteTodo= function (id, onComplete) {
        call('DELETE', '/api/todo/delete', { id: id }, onComplete);
    };

    


    function call(method, nameUrl, dataObject, successCallback) {

        nameUrl = 'http://localhost:64289' + nameUrl;
        
        if (dataObject === undefined)
            dataObject = {};

        if (successCallback === undefined)
            successCallback = function (d) { };

        $.ajax({
            type: method,
            url: nameUrl,
            data: JSON.stringify(dataObject),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function (request) {
                request.setRequestHeader("auth_token", getCookie('auth_token'));
            },
            success: function (data) {
                if (typeof data.auth_token == "string") {
                    document.cookie = "auth_token=" + data.auth_token + ";";
                }
                if (typeof data.error == "string" && data.error.length > 0) {
                    alert(data.error);
                }

                successCallback(data);                
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log('AJAX ERROR: status: ' + xhr.status + ' text: ' + xhr.statusText);
                console.log('AJAX ERROR: ' + thrownError);
            }
        });
    };

    // copied from stack overflow
    function getCookie(name) {
        var value = "; " + document.cookie;
        var parts = value.split("; " + name + "=");
        if (parts.length == 2) return parts.pop().split(";").shift();
    }

}(window.todoApi = window.todoApi || {}, jQuery));
