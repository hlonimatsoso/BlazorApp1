$(document).ready(function () {

    fixChartLabels();

});

var fixChartLabels = function () {
    //alert("testing");
    $(".rz-tick-text:odd").css({ '-webkit-transform': 'translate(0,12px)' });
}