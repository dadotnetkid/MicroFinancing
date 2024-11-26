function renderChart(opt) {
    const ctx = document.getElementById(opt.chartId);

    let chartStatus = Chart.getChart(opt.chartId); // <canvas> id
    if (chartStatus != undefined) {
        chartStatus.destroy();
    }

    opt.data.options = {
        onClick: (e, element) => {
            opt.helper.invokeMethodAsync(opt.callback, opt.data.data.labels[element[0].index], JSON.stringify(opt.parameters));
        },
        scales: {
            y: {
                ticks: {
                    display: false,
                }
            }
        },
        maintainAspectRatio: false,
    }
    new Chart(ctx, opt.data);
}

function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}