window.accMermaid = (() => {
    let currentTheme = null;

    function waitForMermaid() {
        return new Promise((resolve, reject) => {
            let attempts = 0;

            const check = () => {
                if (window.mermaid) {
                    resolve(window.mermaid);
                    return;
                }

                attempts += 1;
                if (attempts > 250) {
                    reject(new Error("Mermaid no esta disponible en la pagina."));
                    return;
                }

                setTimeout(check, 20);
            };

            check();
        });
    }

    function prefersDark() {
        return window.matchMedia && window.matchMedia("(prefers-color-scheme: dark)").matches;
    }

    function ensureInitialized(mermaidApi) {
        const nextTheme = prefersDark() ? "dark" : "base";
        if (currentTheme === nextTheme) {
            return;
        }

        mermaidApi.initialize({
            startOnLoad: false,
            securityLevel: "strict",
            suppressErrorRendering: true,
            theme: nextTheme,
            flowchart: {
                useMaxWidth: true,
                htmlLabels: false
            },
            maxTextSize: 50000,
            maxEdges: 200
        });

        currentTheme = nextTheme;
    }

    function escapeHtml(text) {
        return String(text)
            .replaceAll("&", "&amp;")
            .replaceAll("<", "&lt;")
            .replaceAll(">", "&gt;")
            .replaceAll("\"", "&quot;")
            .replaceAll("'", "&#39;");
    }

    function showError(host, message) {
        host.innerHTML = `<div class="mermaid-error">No se pudo renderizar el diagrama.<br>${escapeHtml(message)}</div>`;
    }

    async function render(containerId, code) {
        const host = document.getElementById(containerId);
        if (!host) {
            return;
        }

        try {
            const mermaidApi = await waitForMermaid();
            ensureInitialized(mermaidApi);

            host.innerHTML = "";

            const normalizedCode = (code ?? "").trim();
            if (!normalizedCode) {
                showError(host, "El codigo Mermaid esta vacio.");
                return;
            }

            await mermaidApi.parse(normalizedCode, { suppressErrors: true });

            const renderId = `acc-mermaid-${containerId}-${Date.now()}`;
            const { svg, bindFunctions } = await mermaidApi.render(renderId, normalizedCode);
            host.innerHTML = svg;
            bindFunctions?.(host);
        }
        catch (error) {
            const message = error instanceof Error ? error.message : "Error desconocido.";
            showError(host, message);
            console.error("Mermaid render error:", error);
        }
    }

    return { render };
})();
