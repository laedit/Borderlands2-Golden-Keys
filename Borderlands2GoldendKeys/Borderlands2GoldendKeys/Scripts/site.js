function DocumentReadyInitializeTooltip() {
    $(document).ready(function () {
        InitializeTooltip();
    });
}

function InitializeTooltip() {
    $('.validKey').tooltip();
}

function ShowAllSuccess() {
    InitializeTooltip();
}
function ShowAllFailure() {
    $('#showAllButton').val("Retry");
    $('#showAllButton').show();
    $('#showAllError').show();
}
function ShowAllBegin() {
    $('#showAllButton').hide();
    $('#showAllError').hide();
}

function SettingsSuccess(data, messageName) {
    $("input").prop("disabled", false);
    if (data.Success)
    {
        $(messageName).addClass('alert-success');
    }
    else
    {
        $(messageName).addClass('alert-danger');
    }
    $(messageName).text(data.Message);
    $(messageName).show();
}
function SettingsFailure(buttonName, messageName) {
    $("input").prop("disabled", false);
    $(messageName).addClass('alert-danger');
    $(messageName).show();
}
function SettingsBegin(buttonName, messageName) {
    $("input").prop("disabled", true);
    $(".alert").hide();
}


function GetBaseRawTweetsSuccess(data) {
    SettingsSuccess(data, '#messageInfo');
    $("#BaseTweetsButton").prop("disabled", true);
}

function GetBaseRawTweetsFailure() {
    SettingsFailure('#BaseTweetsButton', '#messageInfo');
    $("#LaunchUpdateProcess").prop("disabled", true);
    $("#DeleteAll").prop("disabled", true);
}

function GetBaseRawTweetsBegin() {
    SettingsBegin('#BaseTweetsButton', '#messageInfo');
}


function LaunchUpdateProcessSuccess(data) {
    SettingsSuccess(data, '#messageInfo2');
    $("#LaunchUpdateProcess").prop("disabled", true);
}

function LaunchUpdateProcessFailure() {
    SettingsFailure('#LaunchUpdateProcess', '#messageInfo2');
    $("#BaseTweetsButton").prop("disabled", true);
    $("#DeleteAll").prop("disabled", true);
}

function LaunchUpdateProcessBegin() {
    SettingsBegin('#LaunchUpdateProcess', '#messageInfo2');
}


function DeleteAllSuccess(data) {
    SettingsSuccess(data, '#messageInfo3');
    $("#DeleteAll").prop("disabled", true);
    $("#LaunchUpdateProcess").prop("disabled", true);
}

function DeleteAllFailure() {
    SettingsFailure('#DeleteAll', '#messageInfo3');
    $("#BaseTweetsButton").prop("disabled", true);
}

function DeleteAllBegin() {
    SettingsBegin('#DeleteAll', '#messageInfo3');
}

!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');
