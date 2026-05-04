(function () {
    "use strict";

    var config = window.accChatbase || {};
    if (!config.enabled || window.__accChatbaseLoaded) {
        return;
    }

    var chatbotId = typeof config.chatbotId === "string" ? config.chatbotId.trim() : "";
    var domain = typeof config.domain === "string" ? config.domain.trim() : "";
    var delay = Number(config.loadDelayMs);

    if (!chatbotId || !domain) {
        console.warn("Chatbase is enabled but ChatbotId or Domain is missing.");
        return;
    }

    if (!/^[a-z0-9.-]+$/i.test(domain)) {
        console.warn("Chatbase domain must be a hostname without protocol or path.");
        return;
    }

    if (!Number.isFinite(delay) || delay < 0) {
        delay = 0;
    }

    function loadChatbase() {
        if (window.__accChatbaseLoaded) {
            return;
        }

        window.__accChatbaseLoaded = true;

        var script = document.createElement("script");
        script.src = "https://" + domain + "/embed.min.js";
        script.defer = true;
        script.async = true;
        script.setAttribute("chatbotId", chatbotId);
        script.setAttribute("domain", domain);

        (document.body || document.head).appendChild(script);
    }

    function scheduleLoad() {
        var startTimer = function () {
            window.setTimeout(loadChatbase, delay);
        };

        if ("requestIdleCallback" in window) {
            window.requestIdleCallback(startTimer, { timeout: delay + 2000 });
            return;
        }

        startTimer();
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", scheduleLoad, { once: true });
        return;
    }

    scheduleLoad();
})();
