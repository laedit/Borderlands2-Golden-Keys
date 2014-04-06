function DocumentReadyInitializeTooltip() {
    $(document).ready(InitializeTooltip());
}

function InitializeTooltip() {
    $('.validKey').tooltip();
}

function ShowAllSuccess() {
    InitializeTooltip();
    InitializezClip();
}
function ShowAllFailure() {
    $('#showAllButton').val("Retry");
    $('#showAllButton').show();
    $('#showAllError').show();
}
function ShowAllBegin() {
    $('#showAllButton').hide();
    $('#showAllError').hide();
    window.history.replaceState(null, "Borderlands 2: Golden Keys", "/ShowAll");
}

function SettingsSuccess(data, messageName) {
    $("input").prop("disabled", false);
    if (data.Success) {
        $(messageName).addClass('alert-success');
    } else {
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
    $('#updateStatus').attr("class", "text-success");
    $('#updateStatus').text("Update running");
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
    $('#updateStatus').attr("class", "text-danger");
    $('#updateStatus').text("Update not running");
}

function DeleteAllFailure() {
    SettingsFailure('#DeleteAll', '#messageInfo3');
    $("#BaseTweetsButton").prop("disabled", true);
}

function DeleteAllBegin() {
    SettingsBegin('#DeleteAll', '#messageInfo3');
}

function SendMailSuccess(data) {
    if (data.Success) {
        //clear all fields
        $('#contactForm').trigger("reset");
        $('#mailForm').fadeOut();

        $('#mailResult').html("<div class='alert alert-success'>");
        $('#mailResult > .alert-success').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;").append("</button>");
        $('#mailResult > .alert-success').append("<strong><span class=\"glyphicon glyphicon-send\"></span> &nbsp; Your message has been sent. </strong>");
        $('#mailResult > .alert-success').append('</div>');
    } else {
        if (data.ErrorSource === null) {
            SendMailFailure();
        } else {
            if (data.ErrorSource == "PoliteCaptcha") {

                $('#youAreAClaptrap').html("It appear that you might be a ClapTrap. If not, thanks to complete this little test.");
                Recaptcha.create("6LcAG_ASAAAAANM52qRMQ8XX_9yz7cxbx0rIRfMB",
                                    "ReCaptcha",
                                    {
                                        theme: "blackglass",
                                        callback: Recaptcha.focus_response_field
                                    }
                                  );
            } else {
                $('#mailResult').html(data.Message);
            }
            $('#sendMailButton').prop("disabled", false);
        }
    }
}

function SendMailFailure() {
    $('#mailResult').html("<div class='alert alert-danger'>");
    $('#mailResult > .alert-danger').html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;").append("</button>");
    $('#mailResult > .alert-danger').append("<strong>AHHHHHHHHHHHHHHHHH! Dang.</strong> My bad. You could retry in a few minutes. Or never.");
    $('#mailResult > .alert-danger').append('</div>');

    $('#sendMailButton').prop("disabled", false);
}

function SendMailBegin() {
    $('#sendMailButton').prop("disabled", true);
}

function DocumentReadyInitializezClip() {
    $(document).ready(InitializezClip());
}

function InitializezClip() {
    if (HasFlashSupport()) {
        $('.copyContainer').removeClass('hidden');

        $('.codeCopy').zclip({
            path: 'Scripts/ZeroClipboard.swf',
            copy: function () {
                return $($(this).data('copy')).text();
            },
            afterCopy: function () {
                $('.imgCopy').each(function () {
                    $(this).attr("src", "/Content/images/clipboard.png");
                });
                $($(this).data('img')).attr("src", "/Content/images/clipboard-ok.png");
            }
        });
        $('.copyContainer').tooltip();
    }
}

function HasFlashSupport() {
    var flashMimeType = 'application/x-shockwave-flash';
    var flashActiveX = ['ShockwaveFlash.ShockwaveFlash', 'ShockwaveFlash.ShockwaveFlash.3', 'ShockwaveFlash.ShockwaveFlash.4', 'ShockwaveFlash.ShockwaveFlash.5', 'ShockwaveFlash.ShockwaveFlash.6', 'ShockwaveFlash.ShockwaveFlash.7'];

    //for standard compliant browsers
    if (navigator.mimeTypes) {
        var version;
        if (navigator.mimeTypes[flashMimeType] && navigator.mimeTypes[flashMimeType].enabledPlugin) {
                return true;
        }
    }
    //for IE
    if (typeof ActiveXObject != 'undefined') {
        for (var i = 0; i < flashActiveX.length; i++) {
            try {
                new ActiveXObject(flashActiveX[i]);
                return true;
            }
            catch (err) {
            }
        }
    }
    return false;
}

!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');
