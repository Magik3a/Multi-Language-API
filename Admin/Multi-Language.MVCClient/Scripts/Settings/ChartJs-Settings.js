var pieOptions = {
    //Boolean - Whether we should show a stroke on each segment
    segmentShowStroke: true,
    //String - The colour of each segment stroke
    segmentStrokeColor: "#fff",
    //Number - The width of each segment stroke
    segmentStrokeWidth: 1,
    //Number - The percentage of the chart that we cut out of the middle
    percentageInnerCutout: 50, // This is 0 for Pie charts
    //Number - Amount of animation steps
    animationSteps: 100,
    //String - Animation easing effect
    animationEasing: "easeOutBounce",
    //Boolean - Whether we animate the rotation of the Doughnut
    animateRotate: true,
    //Boolean - Whether we animate scaling the Doughnut from the centre
    animateScale: false,
    //Boolean - whether to make the chart responsive to window resizing
    responsive: true,
    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
    maintainAspectRatio: false,
    //String - A legend template
    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
    //String - A tooltip template
    tooltipTemplate: "<%=value %> <%=label%> %"
};

function InitializeDiskSpaceChart(freeSpace, usedSpace) {

    var data = {
        labels: [
            "Free space",
            "Used space"
        ],
        datasets: [
        {
            data: [freeSpace, usedSpace],
            backgroundColor: [
                "#0000FF",
                "#FF0000"
            ],
            hoverBackgroundColor: [
                "#0000FF",
                "#FF0000"
            ]
        }]
    };

    var ctx = document.getElementById("pieChartSystemSpace");
    var diskSpaceChar = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: pieOptions
    });
}