$(function () {
    $(".menuReportText").eq(0).bind("click", function () {
        var topBody = window.parent.document.body;
        var rightBody = window.parent.frames["rightFrame"].document.body;
        var loadingDiv = $("#loading", rightBody);
        if (loadingDiv.length == 0) {
            loadingDiv = $("<div id='loading'><img id='loadingImg' src='Images/loading.gif'/></div>");
            loadingDiv.css("position", "absolute");
            loadingDiv.css("z-index", "999");
            loadingDiv.css("background-color", "#E0E8FF");

            loadingBankDiv = $("<div id='bankDiv'></div>");
            loadingBankDiv.width($(rightBody).width());
            loadingBankDiv.height($(topBody).height() - 30);
            loadingBankDiv.css("position", "absolute");
            loadingBankDiv.css("z-index", "998");
            loadingBankDiv.css("background-color", "#ffffff");
            loadingBankDiv.css("filter", "alpha(opacity=90)");

            loadingBankDiv.append(loadingDiv);
            $(rightBody).append(loadingBankDiv);

            loadingBankDiv.css("left", 0);
            loadingBankDiv.css("top", 0);
            loadingDiv.css("left", ($(rightBody).width() - loadingDiv.width()) / 2);
            loadingDiv.css("top", ($(topBody).height() - 86 - 30) / 2);
        }
    });
});