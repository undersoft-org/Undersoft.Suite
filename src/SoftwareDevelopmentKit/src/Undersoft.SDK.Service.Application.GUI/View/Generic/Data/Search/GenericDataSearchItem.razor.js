// Add Left click event to open the PowerMenu
export function focusElement(id) {
    let item = document.getElementById(id);
    if (!!item) {
        item.focus();
    }
}