$(document).ready(function () {
    InitializeTooltip();
});

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

!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');
