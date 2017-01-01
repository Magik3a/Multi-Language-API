
$.MltApi = $.MltApi || {};

$.MltApi.InitializeDiskSpaceChart = function(freeSpace, usedSpace) {

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
        legendTemplate:
            "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
        //String - A tooltip template
        tooltipTemplate: "<%=value %> <%=label%> %"
    };

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
            }
        ]
    };

    var ctx = document.getElementById("pieChartSystemSpace");
    var diskSpaceChar = new Chart(ctx,
    {
        type: 'pie',
        data: data,
        options: pieOptions
    });
};


$.MltApi.InitializeSystemStabilityChart = function() {

    //-----------------------
    //- MONTHLY SALES CHART -
    //-----------------------

    var systemStabilityChartData = {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [
          {
              label: "Memory Available %",
              fillColor: "rgb(50, 214, 222)",
              strokeColor: "rgb(210, 214, 222)",
              pointColor: "rgb(210, 214, 222)",
              pointStrokeColor: "#c1c7d1",
              pointHighlightFill: "#fff",
              pointHighlightStroke: "rgb(220,220,220)",
              data: [65, 59, 80, 81, 56, 55, 40]
          },
          {
              label: "CPU %",
              fillColor: "rgba(60,141,188,0.9)",
              strokeColor: "rgba(60,141,188,0.8)",
              pointColor: "#3b8bba",
              pointStrokeColor: "rgba(60,141,188,1)",
              pointHighlightFill: "#fff",
              pointHighlightStroke: "rgba(60,141,188,1)",
              data: [28, 48, 40, 19, 86, 27, 90]
          }
        ]
    };

    var systemStabilityChartOptions = {
        //Boolean - If we should show the scale at all
        showScale: true,
        //Boolean - Whether grid lines are shown across the chart
        scaleShowGridLines: false,
        //String - Colour of the grid lines
        scaleGridLineColor: "rgba(0,0,0,.05)",
        //Number - Width of the grid lines
        scaleGridLineWidth: 1,
        //Boolean - Whether to show horizontal lines (except X axis)
        scaleShowHorizontalLines: true,
        //Boolean - Whether to show vertical lines (except Y axis)
        scaleShowVerticalLines: true,
        //Boolean - Whether the line is curved between points
        bezierCurve: true,
        //Number - Tension of the bezier curve between points
        bezierCurveTension: 0.3,
        //Boolean - Whether to show a dot for each point
        pointDot: false,
        //Number - Radius of each point dot in pixels
        pointDotRadius: 4,
        //Number - Pixel width of point dot stroke
        pointDotStrokeWidth: 1,
        //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
        pointHitDetectionRadius: 20,
        //Boolean - Whether to show a stroke for datasets
        datasetStroke: true,
        //Number - Pixel width of dataset stroke
        datasetStrokeWidth: 2,
        //Boolean - Whether to fill the dataset with a color
        datasetFill: true,
        //String - A legend template
        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%=datasets[i].label%></li><%}%></ul>",
        //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
        maintainAspectRatio: true,
        //Boolean - whether to make the chart responsive to window resizing
        responsive: true
    };


    var ctx = document.getElementById('systemStabilityChart');
    //var clientsChart = new Chart(context).Bar(barData);
    var systemStabilityChart = new Chart(ctx, {
        type: "line",
        data: systemStabilityChartData,
        options:  systemStabilityChartOptions 
    });
    //---------------------------
    //- END MONTHLY SALES CHART -
    //---------------------------
};