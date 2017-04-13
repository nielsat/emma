var chartHelper = {
    drawLineTimeChart : function(sourceData, chartContainerId, titleId)
    {
        console.log(1);
        $("#" + titleId).html(sourceData.Title);
        console.log(2);
        google.charts.load('current', { 'packages': ['line', 'corechart'] });
        console.log(3);
        google.charts.setOnLoadCallback(drawLTChart);
        console.log(4);

        function drawLTChart() {
            console.log(5);
            var chartDiv = document.getElementById(chartContainerId);
            console.log(6);

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Date');
            data.addColumn('number', sourceData.Line.Title);
            console.log(7);
            var tmpData = [];
            for (var i = 0; i < sourceData.Line.Points.length; i++)
            {
                var currentPoint = sourceData.Line.Points[i];
                tmpData.push([currentPoint.StringDate, currentPoint.Value]);
            }
            console.log(8);
            data.addRows(tmpData);

            var materialOptions = {
                height: 500
            };
            console.log(9);
            var materialChart = new google.charts.Line(chartDiv);
            materialChart.draw(data, materialOptions);
            console.log(10);
        }
    }
}