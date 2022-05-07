$(document).on('click', '.EmployeeList li.EmployeeChatInfo', function () {
    var EmployeeId = $(this).attr("data-Employeeid");
    var Name = $(this).attr("data-EmployeeName");
    var EmployeeCode = $(this).attr("data-EmployeeCode");
    $(".SelectedEmployeeName").text(Name);
    $(".EmployeeCode").text("KT-"+EmployeeCode);
    $(".AddChat").attr("data-Employeeid", EmployeeId);
    $(".chat-content").html('');
    if (typeof (PreviousChatListIds) != "undefined" && PreviousChatListIds != null) {
        PreviousChatListIds = [];
        clearInterval(interval);
    }
    _employeeId = EmployeeId;
    //LoadChatData();
    interval = setInterval(LoadChatData, 500);
});
var interval;

var _employeeId = "";

function LoadChatData() {
    
    var data = '{ToId:"' + _employeeId + '"}';
    handleAjaxRequest(null, true, "/Method/GetChatById", data, "CallBackGetChatById");
}

function areEqual(arr1, arr2) {
    let n = arr1.length;
    let m = arr2.length;

    // If lengths of array are not equal means
    // array are not equal
    if (n != m)
        return false;

    // Sort both arrays
    arr1.sort();
    arr2.sort();

    // Linearly compare elements
    for (let i = 0; i < n; i++)
        if (arr1[i] != arr2[i])
            return false;

    // If all elements were same.
    return true;
}

var PreviousChatListIds;

function CallBackGetChatById(responseData) {
    if (responseData.message.status == "success") {
        var ChatsList = responseData.message.ChatsInfo;
        var ChatListIds = ChatsList.map(item => item.Id);
        var PreviousDivHeight = $(".chat-content")[0].scrollHeight;
        if (typeof (PreviousChatListIds) != "undefined" && PreviousChatListIds != null) {
            var st = areEqual(PreviousChatListIds, ChatListIds);
        }
        var result = (typeof (PreviousChatListIds) != "undefined" && PreviousChatListIds != null) ? !areEqual(PreviousChatListIds, ChatListIds) : true;
        if (result) {
            if (typeof (ChatsList) != "undefined" && ChatsList != null) {
                $.each(ChatsList, function (key, value) {
                    if ((typeof (PreviousChatListIds) == "undefined" || PreviousChatListIds == null) || !PreviousChatListIds.includes(value.Id)) {
                        var Position = "left";
                        var profileImage = value.EmployeeProfileImage;
                        if (LogginEmployeeId == value.FromId) {
                            Position = "right";
                            profileImage = LogginEmployeeProfileImage;
                        }
                        var ChatTextHTML = '<div class="chat-item chat-' + Position + '" style="">';
                        ChatTextHTML += '<img src="' + value.EmployeeProfileImage + '">';
                        ChatTextHTML += '<div class="chat-details">';
                        ChatTextHTML += '<div class="chat-text">' + value.Message + '</div>';
                        ChatTextHTML += '<div class="chat-time">' + value.ChatDateString + '</div>';
                        ChatTextHTML += '</div></div>';

                        $(".chat-content").append(ChatTextHTML);
                    }
                });
                PreviousChatListIds = ChatsList.map(item => item.Id);
            }
            else {

            }
            $("#mychatbox").show();
            var myDiv = $(".chat-content")[0];
            if (PreviousDivHeight != myDiv.scrollHeight) {
                myDiv.scrollTop = myDiv.scrollHeight;
            }
        }
    }
    
}

$(document).on('click', '.AddChat', function () {
    var ToId = $(this).attr("data-Employeeid");
    var Message = $(".MessageContent").val().trim();

    if (Message != "") {
        var data = '{Message:"' + Message + '", ToId:"' + ToId + '"}';
        handleAjaxRequest(null, true, "/Method/AddChat", data, "CallBackAddChat", Message);
    }
});

function CallBackAddChat(responseData, Message) {
    if (responseData.message.status == "success") {
        $("input[type='text']").val("");
        var ChatTextHTML = '<div class="chat-item chat-right" style="">';
        ChatTextHTML += '<img src="../assets/img/avatar/avatar-1.png">';
        ChatTextHTML += '<div class="chat-details">';
        ChatTextHTML += '<div class="chat-text">' + Message+'</div>';
        ChatTextHTML += '<div class="chat-time">11:15</div>';
        ChatTextHTML += '</div></div>';
        //$(".chat-content").append(ChatTextHTML);
    }
    else {
        $(".customErrorMessageUpdateEmployee").text("Error on Adding employee, Try agin in few min");
    }
}