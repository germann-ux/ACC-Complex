window.ResumenChart = {
    renderChart: function (canvasId, data) {
        const ctx = document.getElementById(canvasId).getContext('2d');

        const gradientFill = ctx.createLinearGradient(0, 0, 0, 300);
        gradientFill.addColorStop(0, 'rgba(114, 9, 183, 0.7)');
        gradientFill.addColorStop(1, 'rgba(67, 97, 238, 0.1)');

        const datasets = data.datasets.map(ds => ({
            ...ds,
            backgroundColor: gradientFill
        }));

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: data.labels,
                datasets: datasets
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        max: 100,
                        grid: {
                            color: 'rgba(255, 255, 255, 0.05)'
                        },
                        ticks: {
                            color: 'rgba(255, 255, 255, 0.7)'
                        }
                    },
                    x: {
                        grid: {
                            color: 'rgba(255, 255, 255, 0.05)'
                        },
                        ticks: {
                            color: 'rgba(255, 255, 255, 0.7)'
                        }
                    }
                },
                plugins: {
                    legend: {
                        display: false
                    },
                    tooltip: {
                        backgroundColor: 'rgba(26, 26, 46, 0.9)',
                        titleColor: '#e0e0e0',
                        bodyColor: '#e0e0e0',
                        padding: 10,
                        borderColor: 'rgba(114, 9, 183, 0.5)',
                        borderWidth: 1
                    }
                }
            }
        });
    }
};
