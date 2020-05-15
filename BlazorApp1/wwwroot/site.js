$(document).ready(function () {

    fixChartLabels();

});

var fixChartLabels = function () {
    //alert("testing");
    $(".rz-tick-text:odd").css({ '-webkit-transform': 'translate(0,12px)' });
}

var DrawPolyChart = function (div, data, layout) {
    console.log("About to draw Ploty for: " + div);
    console.log(data);
    console.log(layout);

    debugger;

    Plotly.newPlot(div, [data], layout);
    console.log("Done drawing Ploty for " + div);
};