window.guiaNavigation = {
    back: function () {
        window.history.back();
    },
    backOrFallback: function (fallbackUrl) {
        if (window.history.length > 1) {
            window.history.back();
            return;
        }

        if (fallbackUrl) {
            window.location.assign(fallbackUrl);
        }
    },
    forward: function () {
        window.history.forward();
    }
};
