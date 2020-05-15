$(document).ready(function () {

    fixChartLabels();

});

var fixChartLabels = function () {
    //alert("testing");
    $(".rz-tick-text:odd").css({ '-webkit-transform': 'translate(0,12px)' });
}

var DrawPolyChart = function (div, data) {
    console.log("About to draw Ploty");
    debugger;
    var layout = { barmode: 'stack' };
    Plotly.newPlot($("#" + div)[0], [data]);
    console.log("Done drawing Ploty");
};