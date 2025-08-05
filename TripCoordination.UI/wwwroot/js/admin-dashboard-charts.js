document.addEventListener("DOMContentLoaded", function () {
    console.log('Chart script loaded');

    const canvas = document.getElementById('tripsCreatedChart');
    if (!canvas) {
        console.log('Chart canvas not found');
        return;
    }

    console.log('Chart canvas found, loading chart...');

    fetch('/Admin/GetTripsCreatedChartData')
        .then(res => res.json())
        .then(data => {
            const ctx = canvas.getContext('2d');
            const chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: data.map(d => d.label),
                    datasets: [{
                        label: 'Trips Created',
                        data: data.map(d => d.value),
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        })
        .catch(error => console.error('Error loading chart data:', error));
});