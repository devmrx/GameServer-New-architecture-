

var rsp = [12, 19, 3, 5, 2, 3];

function OnSuccessStart(data) {

    $('#serv-work').empty().append(data.IsWork);

    $('#count-gamers').empty().append(data.CountGamers);
    $('#count-games').empty().append(data.CountGames);
    $('#count-sessions').empty().append(data.CountGameSessions);


    rsp = [
        data.raspr[0], data.raspr[1], data.raspr[2], data.raspr[3], data.raspr[4], data.raspr[5]
    ];

    dataChart.data.datasets.data = rsp;

    var myChart = new Chart(ctx, dataChart);



    var lineChart = new Chart(speedCanvas, {
        type: 'line',
        data: speedData,
        options: chartOptions
    });


}


function OnSuccessStop(data) {

    $('#serv-work').empty().append(data.IsWork);

    //$('#count-gamers').empty().append(data.CountGamers);
}



var speedCanvas = document.getElementById("main-line-chart");

//Chart.defaults.global.defaultFontFamily = "Lato";
//Chart.defaults.global.defaultFontSize = 18;


var speedData = {
    labels: ["0s", "10s", "20s", "30s", "40s", "50s", "60s"],
    datasets: [{
        label: "Memory (Mb)",

        data: [0, 59, 75, 20, 20, 55, 40],

        backgroundColor: [

            'rgba(54, 162, 235, 0.3)'

        ]

    }]
};

var chartOptions = {
    legend: {
        display: true,
        //position: 'top',
        labels: {
            boxWidth: 80,
            fontColor: 'white'
        }
    }
};

var lineChart = new Chart(speedCanvas, {
    type: 'line',
    data: speedData,
    options: chartOptions
});


//===========
var ctx = document.getElementById("myChart").getContext('2d');



var dataChart = {
    type: 'pie',
    data: {

        datasets: [{
            label: '# of Votes',
            data: rsp,
            backgroundColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderColor: [
                'rgba(255,99,132,1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1
        }],
        //labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"]
        labels: ["Chess", "CSGO", "Dota 2", "Overwatch", "PUBG", "WOW"]
    }


}

var myChart = new Chart(ctx, dataChart);