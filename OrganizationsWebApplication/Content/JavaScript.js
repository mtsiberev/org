function sortSwitch() {
    var x = getCookie("sort");

    if (x == "ascending") {
        deleteCookie("sort");
        setCookie("sort", "descending");
        location.reload();
        return false;
    }

    if (x == "descending") {
        deleteCookie("sort");
        setCookie("sort", "ascending");
        location.reload();
        return false;
    }
    else {
        setCookie("sort", "ascending");
        location.reload();
        return false;
    }
}
/////////////////////////////////////////
function nextPage() {
    var x = getCookie("pageNumber");
    if (x == undefined) {
        x = 2;
        setCookie("pageNumber", x);
        location.reload();
        return false;
    } else {
        x = parseInt(x) + 1;

        var maxPage = getCookie("maxPageNumber");
        if (x > maxPage) return false;

        deleteCookie("pageNumber");
        setCookie("pageNumber", x);
        location.reload();
        return false;
    }
}

function prevPage() {
    var x = getCookie("pageNumber");
    if (x == 1) return false;
    if (x == undefined) {
        x = 1;
        setCookie("pageNumber", x);
        location.reload();
        return false;
    } else {
        x = parseInt(x) - 1;
        deleteCookie("pageNumber");
        setCookie("pageNumber", x);
        location.reload();
        return false;
    }
}
/////////////////////////////////////////
function setList() {
    deleteCookie("view");
    setCookie("view", "list");
    location.reload();
}

function setGrid() {
    deleteCookie("view");
    setCookie("view", "grid");
    location.reload();
}

function setView() {
    var x = getCookie("view");
    if (x == "grid") {
        $(".content").toggleClass("grid", true);
    }
    if (x == "list") {
        $(".content").toggleClass("grid", false);
    }
}
///////////////////////////////////////
function setCookie(name, value, options) {
    options = options || {};

    var expires = options.expires;

    if (typeof expires == "number" && expires) {
        var d = new Date();
        d.setTime(d.getTime() + expires * 1000);
        expires = options.expires = d;
    }
    if (expires && expires.toUTCString) {
        options.expires = expires.toUTCString();
    }

    value = encodeURIComponent(value);

    var updatedCookie = name + "=" + value;

    for (var propName in options) {
        updatedCookie += "; " + propName;
        var propValue = options[propName];
        if (propValue !== true) {
            updatedCookie += "=" + propValue;
        }
    }

    document.cookie = updatedCookie;
}

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function deleteCookie(name) {
    setCookie(name, "", {
        expires: -1
    });
}




