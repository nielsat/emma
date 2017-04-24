var chartHelper = {
    drawLineTimeChart : function(sourceData, chartContainerId, titleId)
    {
        $("#" + titleId).html(sourceData.Title);
        google.charts.load('current', { 'packages': ['line', 'corechart'] });
        google.charts.setOnLoadCallback(drawLTChart);

        function drawLTChart() {
            if (!sourceData.Line.Points.length || sourceData.Line.Points.length == 0)
            {
                $("#" + chartContainerId).html("No data");
            }
            var chartDiv = document.getElementById(chartContainerId);

            var data = new google.visualization.DataTable();
            data.addColumn('string', '');
            data.addColumn('number', sourceData.Line.Title);
            var tmpData = [];
            for (var i = 0; i < sourceData.Line.Points.length; i++)
            {
                var currentPoint = sourceData.Line.Points[i];
                tmpData.push([currentPoint.StringDate, currentPoint.Value]);
            }
            data.addRows(tmpData);

            var options = {
                height: 500,
                pointSize: 10,
                dataOpacity: 1,
                vAxis: { minValue: 0, maxValue: 20 },
            };
            var materialChart = new google.visualization.LineChart(chartDiv);
            materialChart.draw(data, options);
        }
    }
}