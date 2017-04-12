var chartHelper = {
    drawLineTimeChart : function(sourceData, chartContainerId, titleId)
    {
        $("#" + titleId).html(sourceData.Title);
        google.charts.load('current', { 'packages': ['line', 'corechart'] });
        google.charts.setOnLoadCallback(drawLTChart);

        function drawLTChart() {
            var chartDiv = document.getElementById(chartContainerId);

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Date');
            data.addColumn('number', sourceData.Line.Title);

            var tmpData = [];
            for (var i = 0; i < sourceData.Line.Points.length; i++)
            {
                var currentPoint = sourceData.Line.Points[i];
                tmpData.push([currentPoint.StringDate, currentPoint.Value]);
            }
            data.addRows(tmpData);

            var materialOptions = {
                height: 500
            };
            var materialChart = new google.charts.Line(chartDiv);
            materialChart.draw(data, materialOptions);
        }
    }
}