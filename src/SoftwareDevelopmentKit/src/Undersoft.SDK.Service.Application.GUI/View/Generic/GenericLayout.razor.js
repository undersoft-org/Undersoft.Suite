export function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

export function isDarkMode() {
    let matched = window.matchMedia("(prefers-color-scheme: dark)").matches;

    if (matched)
        return true;
    else
        return false;
}

export async function dialogClosingAnimation() {    
        let dialog = document.getElementById('{instance.Id}')?.dialog;
        if (!dialog) return;
        dialog.style.opacity = '0.0';
        let animation = dialog.animate([
            { opacity: '1', transform: '' },
            { opacity: '0', transform: 'translateX(100%)' }
        ], {
            duration: 2000,
        }
        );
        return animation.finished; // promise that resolves when the animation is complete or interrupted    
}

export async function dialogOpeningAnimation() {
    
        let dialog = document.getElementById('{instance.Id}')?.dialog;
        if (!dialog) return;
        let animation = dialog.animate([
            { opacity: '0', transform: 'translateX(-100%)' },
            { opacity: '1', transform: '' }
        ], {        
                duration: 2000,
            }
        );
        return animation.finished; // promise that resolves when the animation is complete or interrupted    
}

