function handleAjaxRequest(timeOut, async, requestMethod, dataContent, callBackMethodName, option) {
    var responseData;
    urlValue = requestMethod;
    if (typeof (timeOut) == 'undefined' || timeOut == null) {
        timeOut = 0;
    }
    try {
        $.ajax({
            async: async,
            timeout: timeOut,
            cache: false,
            type: "POST",
            url: urlValue,
            data: dataContent,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                responseData = data;
                if (typeof callBackMethodName != "undefined") {
                    if (typeof option != "undefined") { window[callBackMethodName](data, option); } else { window[callBackMethodName](data); }
                }
            },
            failure: function (failureData) {
                //alert("Application Error : " + failureData.d);
                responseData = failureData;
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                } else if (jqXHR.status == 401) {//unauthorized or session expired
                    window.location.reload();
                } else if (jqXHR.status == 403) {
                    //window.location.reload();
                } else if (jqXHR.status == 404) {
                    //window.location.reload();
                } else if (jqXHR.status == 500) {
                    //window.location.reload();
                } else if (exception === 'parsererror') {
                    //window.location.reload();
                } else if (exception === 'timeout') {
                    //window.location.reload();
                } else if (exception === 'abort') {
                    //window.location.reload();
                } else {
                }
            }
        });

        if (!async && typeof callBackMethodName == "undefined") {
            return responseData;
        }
    } catch (error) {
        alert(error);
    }

}