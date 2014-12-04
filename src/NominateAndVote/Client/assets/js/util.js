function log(variable) { if (console) { console.log(variable); } }
function log2(name, variable) { if (console) { console.log(name, variable); } }

function restServiceCall(type, controller, action, data, callback) {
    log('Request to ' + controller + '/' + action);
    log2('Data:', data);

    $.ajax({
        url: RestService.baseUrl + '/' + controller + '/' + action,
        data: data,
        type: type,
        contentType: 'application/json',
        dataType: 'jsonp',
        success: function (responseData) {
            log2('Response from ' + controller + '/' + action + ':', responseData);
            callback(responseData);
        },
        error: function (xhr, status, error) {
            log("An error happened, see log");
            log2(status, error);
        },
    });
}