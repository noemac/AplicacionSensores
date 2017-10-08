// Global Options:
Chart.defaults.global.defaultFontColor = 'black';
Chart.defaults.global.defaultFontSize = 16;

//  █████╗ ███╗   ███╗ ██████╗ ███╗   ██╗██╗ █████╗  ██████╗ ██████╗ 
// ██╔══██╗████╗ ████║██╔═══██╗████╗  ██║██║██╔══██╗██╔════╝██╔═══██╗
// ███████║██╔████╔██║██║   ██║██╔██╗ ██║██║███████║██║     ██║   ██║
// ██╔══██║██║╚██╔╝██║██║   ██║██║╚██╗██║██║██╔══██║██║     ██║   ██║
// ██║  ██║██║ ╚═╝ ██║╚██████╔╝██║ ╚████║██║██║  ██║╚██████╗╚██████╔╝
// ╚═╝  ╚═╝╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝╚═╝  ╚═╝ ╚═════╝ ╚═════╝ 

var canvas_Amoniaco = document.getElementById("barChartAmoniaco");
var ctx_Amoniaco = canvas_Amoniaco.getContext('2d');


var data_Amoniaco = {
    labels: ["Lun", "Mar", "Mie", "Jue", "Vie", "Sab", "Dom"],
    datasets: [{
        label: "Amoniaco",
        fill: false,
        lineTension: 0.1,
        backgroundColor: "rgba(225,0,0,0.4)",
        borderColor: "red", // The main line color
        borderCapStyle: 'square',
        borderDash: [], // try [5, 15] for instance
        borderDashOffset: 0.0,
        borderJoinStyle: 'miter',
        pointBorderColor: "black",
        pointBackgroundColor: "white",
        pointBorderWidth: 1,
        pointHoverRadius: 8,
        pointHoverBackgroundColor: "yellow",
        pointHoverBorderColor: "brown",
        pointHoverBorderWidth: 2,
        pointRadius: 4,
        pointHitRadius: 10,
        // notice the gap in the data and the spanGaps: true
        data: [1.18, 1.20, 1.90, 1.88, 2.5, 3.1, 0.3],
        spanGaps: true,
    }

    ]
};

// Notice the scaleLabel at the same level as Ticks
var options_Amoniaco = {
    scales: {
        yAxes: [{
            ticks: {
                beginAtZero: true
            },
            scaleLabel: {
                display: true,
                labelString: 'G&L Group',
                fontSize: 20
            }
        }]
    }
};

// Chart declaration:
var myBarChart = new Chart(ctx_Amoniaco, {
    type: 'line',
    data: data_Amoniaco,
    options: options_Amoniaco
});

// ████████╗███████╗███╗   ███╗██████╗ ███████╗██████╗  █████╗ ████████╗██╗   ██╗██████╗  █████╗ 
// ╚══██╔══╝██╔════╝████╗ ████║██╔══██╗██╔════╝██╔══██╗██╔══██╗╚══██╔══╝██║   ██║██╔══██╗██╔══██╗
//    ██║   █████╗  ██╔████╔██║██████╔╝█████╗  ██████╔╝███████║   ██║   ██║   ██║██████╔╝███████║
//    ██║   ██╔══╝  ██║╚██╔╝██║██╔═══╝ ██╔══╝  ██╔══██╗██╔══██║   ██║   ██║   ██║██╔══██╗██╔══██║
//    ██║   ███████╗██║ ╚═╝ ██║██║     ███████╗██║  ██║██║  ██║   ██║   ╚██████╔╝██║  ██║██║  ██║
//    ╚═╝   ╚══════╝╚═╝     ╚═╝╚═╝     ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝
var canvas_Temperatura = document.getElementById("barChartTemperatura");
var ctx_Temperatura = canvas_Temperatura.getContext('2d');


var data_Temperatura = {
    labels: ["Lun", "Mar", "Mie", "Jue", "Vie", "Sab", "Dom"],
    datasets: [{
        label: "Temperatura",
        fill: false,
        lineTension: 0.1,
        backgroundColor: "rgba(225,0,0,0.4)",
        borderColor: "red", // The main line color
        borderCapStyle: 'square',
        borderDash: [], // try [5, 15] for instance
        borderDashOffset: 0.0,
        borderJoinStyle: 'miter',
        pointBorderColor: "black",
        pointBackgroundColor: "white",
        pointBorderWidth: 1,
        pointHoverRadius: 8,
        pointHoverBackgroundColor: "yellow",
        pointHoverBorderColor: "brown",
        pointHoverBorderWidth: 2,
        pointRadius: 4,
        pointHitRadius: 10,
        // notice the gap in the data and the spanGaps: true
        data: [22, 23, 24, 21, 26, 22, 21],
        spanGaps: true,
    }

    ]
};

// Notice the scaleLabel at the same level as Ticks
var options_Temperatura = {
    scales: {
        yAxes: [{
            ticks: {
                beginAtZero: true
            },
            scaleLabel: {
                display: true,
                labelString: 'G&L Group',
                fontSize: 20
            }
        }]
    }
};

// Chart declaration:
var myBarChart = new Chart(ctx_Temperatura, {
    type: 'line',
    data: data_Temperatura,
    options: options_Temperatura
});


// ██╗  ██╗██╗   ██╗███╗   ███╗███████╗██████╗  █████╗ ██████╗ 
// ██║  ██║██║   ██║████╗ ████║██╔════╝██╔══██╗██╔══██╗██╔══██╗
// ███████║██║   ██║██╔████╔██║█████╗  ██║  ██║███████║██║  ██║
// ██╔══██║██║   ██║██║╚██╔╝██║██╔══╝  ██║  ██║██╔══██║██║  ██║
// ██║  ██║╚██████╔╝██║ ╚═╝ ██║███████╗██████╔╝██║  ██║██████╔╝
// ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝╚═════╝ ╚═╝  ╚═╝╚═════╝ 

var canvas_Humedad = document.getElementById("barChartHumedad");
var ctx_Humedad = canvas_Humedad.getContext('2d');


var data_Humedad = {
    labels: ["Lun", "Mar", "Mie", "Jue", "Vie", "Sab", "Dom"],
    datasets: [{
        label: "Humedad",
        fill: false,
        lineTension: 0.1,
        backgroundColor: "rgba(225,0,0,0.4)",
        borderColor: "red", // The main line color
        borderCapStyle: 'square',
        borderDash: [], // try [5, 15] for instance
        borderDashOffset: 0.0,
        borderJoinStyle: 'miter',
        pointBorderColor: "black",
        pointBackgroundColor: "white",
        pointBorderWidth: 1,
        pointHoverRadius: 8,
        pointHoverBackgroundColor: "yellow",
        pointHoverBorderColor: "brown",
        pointHoverBorderWidth: 2,
        pointRadius: 4,
        pointHitRadius: 10,
        // notice the gap in the data and the spanGaps: true
        data: [45, 46, 48, 46, 45, 46, 45],
        spanGaps: true,
    }

    ]
};

// Notice the scaleLabel at the same level as Ticks
var options_Humedad = {
    scales: {
        yAxes: [{
            ticks: {
                beginAtZero: true
            },
            scaleLabel: {
                display: true,
                labelString: 'G&L Group',
                fontSize: 20
            }
        }]
    }
};

// Chart declaration:
var myBarChart = new Chart(ctx_Humedad, {
    type: 'line',
    data: data_Humedad,
    options: options_Humedad
});                                                                                              
