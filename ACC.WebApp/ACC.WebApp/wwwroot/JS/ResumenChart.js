// JS/ResumenChart.js
window.ResumenChart = (() => {
    const _charts = new Map();
    let _readyPromise = null;

    // Espera a que window.Chart exista y el DOM tenga layout
    function waitForReady() {
        if (_readyPromise) return _readyPromise;
        _readyPromise = new Promise((resolve) => {
            const check = () => {
                if (window.Chart && document.readyState !== "loading") {
                    // rAF garantiza que el layout y tamaños ya estén calculados
                    requestAnimationFrame(() => resolve());
                    return;
                }
                setTimeout(check, 20);
            };
            check();
        });
        return _readyPromise;
    }

    function makeGradient(ctx2d, canvas, bg) {
        if (!bg || bg.type !== "linear" || !Array.isArray(bg.colors) || bg.colors.length < 2) return bg;
        const g = ctx2d.createLinearGradient(0, 0, 0, canvas.height);
        g.addColorStop(0, bg.colors[0]);
        g.addColorStop(1, bg.colors[1]);
        return g;
    }

    function destroyIfAny(id) {
        if (_charts.has(id)) {
            try { _charts.get(id).destroy(); } catch (_) { }
            _charts.delete(id);
        }
    }

    function drawNoData(canvas, msg = "Aún no hay datos de progreso") {
        // Dibuja después de un frame para tener tamaños correctos
        requestAnimationFrame(() => {
            const rect = canvas.getBoundingClientRect();
            const width = Math.max(1, rect.width || canvas.clientWidth || 600);
            const height = Math.max(1, rect.height || canvas.clientHeight || 300);

            canvas.width = width;
            canvas.height = height;

            const ctx = canvas.getContext("2d");
            ctx.clearRect(0, 0, width, height);
            ctx.font = "16px system-ui, -apple-system, Segoe UI, Roboto, sans-serif";
            ctx.fillStyle = "#8a8f98";
            ctx.textAlign = "center";
            ctx.textBaseline = "middle";
            ctx.fillText(msg, width / 2, height / 2);
        });
    }

    function isEmpty(chartData) {
        const labels = chartData?.labels ?? [];
        const dsets = chartData?.datasets ?? [];
        if (!labels.length || !dsets.length) return true;
        return dsets.every(ds => !Array.isArray(ds?.data) || ds.data.length === 0);
    }

    async function renderChart(canvasId, chartData) {
        await waitForReady();

        const canvas = document.getElementById(canvasId);
        if (!canvas) return;

        destroyIfAny(canvasId);

        if (isEmpty(chartData)) {
            drawNoData(canvas);
            return;
        }

        const data = {
            labels: chartData.labels ?? [],
            datasets: (chartData.datasets ?? []).map(ds => ({ ...ds }))
        };

        const ctx2d = canvas.getContext("2d");
        for (const ds of data.datasets) {
            if (ds.backgroundColor && typeof ds.backgroundColor === "object" && ds.backgroundColor.type === "linear") {
                ds.backgroundColor = makeGradient(ctx2d, canvas, ds.backgroundColor);
            }
        }

        const options = {
            responsive: true,
            maintainAspectRatio: false,
            scales: { y: { beginAtZero: true, min: 0, max: 100, ticks: { stepSize: 10 } } },
            interaction: { mode: "index", intersect: false },
            plugins: { legend: { display: false }, tooltip: {} },
            elements: { line: { tension: 0.35, borderWidth: 2 }, point: { radius: 5, hoverRadius: 7, borderWidth: 2 } }
        };

        const chart = new window.Chart(ctx2d, { type: "line", data, options });
        _charts.set(canvasId, chart);
    }

    return { waitForReady, renderChart };
})();
