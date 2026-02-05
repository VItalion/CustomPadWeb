window.popoverManager = (function () {
    let current = null;

    function isInside(element, target) {
        if (!element || !target) return false;
        return element.contains(target) || (document.querySelector('.popover') && document.querySelector('.popover').contains(target));
    }

    function toggle(el, title, content) {
        try {
            if (!el) return;
            // If same element, dispose
            if (current && current.el === el) {
                current.popover.dispose();
                current = null;
                return;
            }

            // Dispose previous
            if (current) {
                current.popover.dispose();
                current = null;
            }

            const pop = new bootstrap.Popover(el, {
                title: title || '',
                content: content || '',
                html: true,
                placement: 'right',
                trigger: 'manual'
            });

            pop.show();
            current = { el: el, popover: pop };

            // click outside to close
            const docHandler = function (e) {
                if (!isInside(el, e.target)) {
                    if (current) {
                        current.popover.dispose();
                        current = null;
                    }
                    document.removeEventListener('click', docHandler);
                }
            };

            setTimeout(() => document.addEventListener('click', docHandler));
        } catch (e) {
            console.error('popoverManager.toggle', e);
        }
    }

    function hideAll() {
        if (current) {
            current.popover.dispose();
            current = null;
        }
    }

    return { toggle, hideAll };
})();
