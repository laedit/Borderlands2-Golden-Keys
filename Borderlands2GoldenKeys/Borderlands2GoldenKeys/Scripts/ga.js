function GoogleAnalytics(i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r;
    i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date();
    a = s.createElement(o),
    m = s.getElementsByTagName(o)[0];
    a.async = 1;
    a.src = g;
    m.parentNode.insertBefore(a, m);
}

function CallAnalytics() {
    GoogleAnalytics(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
    ga('create', 'UA-6626755-5', 'laedit.net');
    ga('send', 'pageview');
}