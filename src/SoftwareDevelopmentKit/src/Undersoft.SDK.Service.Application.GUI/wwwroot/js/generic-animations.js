var GenericAnimations = GenericAnimations || {};

GenericAnimations.closingToRight = function (id) {

    let dialog = document.getElementById(id)?.dialog;

    if (!dialog)
        return;

    dialog.style.opacity = "0.0";

    let animation = dialog.animate([
        { opacity: "1", transform: "" },
        { opacity: "0", transform: "translateX(100%)" }
    ],
        {
            duration: 250
        }

    );
    return animation.finished;
};

GenericAnimations.openingFromLeft = function (id) {
    let dialog = document.getElementById(id)?.dialog;

    if (!dialog)
        return;

    let animation = dialog.animate([
        { opacity: "0", transform: "translateX(-100%)" },
        { opacity: "1", transform: "" }
    ],
        {
            duration: 250
        }
    );
    return animation.finished;
};

GenericAnimations.closingToTop = function (id) {

    let dialog = document.getElementById(id)?.dialog;

    if (!dialog)
        return;

    dialog.style.opacity = "0.0";

    let animation = dialog.animate([
        { opacity: "1", transform: "" },
        { opacity: "0", transform: "translateY(-100%)" }
    ],
        {
            duration: 250
        }
    );
    return animation.finished;
};

GenericAnimations.openningFromBottom = function (id) {

    let dialog = document.getElementById(id)?.dialog;

    if (!dialog)
        return;

    let animation = dialog.animate([
        { opacity: "0", transform: "translateY(100%)" },
        { opacity: "1", transform: "" }
    ],
        {
            duration: 250
        }
    );
    return animation.finished;
};

GenericAnimations.closingCentral = function (id) {

    let dialog = document.getElementById(id)?.dialog;

    if (!dialog)
        return;

    dialog.style.opacity = "0.0";

    let animation = dialog.animate([
        { opacity: "1", transform: "" },
        { opacity: "0", transform: "" }
    ],
        {
            duration: 250
        }
    );
    return animation.finished;
};

GenericAnimations.openingCentral = function (id) {

    let dialog = document.getElementById(id)?.dialog;

    if (!dialog)
        return;

    let animation = dialog.animate([
        { opacity: "0", transform: "" },
        { opacity: "1", transform: "" }
    ],
        {
            duration: 250
        }
    );
    return animation.finished;
};
