window.onload = function () {
    setTimeout(function () {
        var alertElement = document.getElementById("errorAlert");
        if (alertElement) {
            alertElement.style.display = "none";
        }
    }, 3000);
};