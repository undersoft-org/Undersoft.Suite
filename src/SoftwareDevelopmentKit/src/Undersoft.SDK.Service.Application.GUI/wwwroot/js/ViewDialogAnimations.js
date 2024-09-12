export async function dialogClosingToRight(id) {    

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
}

export async function dialogOpeningFromLeft(id) {
    

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
}


export async function dialogClosingToTop(id) {

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
}

export async function dialogOpeningFromBottom(id) {


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
}


export async function dialogClosing(id) {

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
}

export async function dialogOpening(id) {


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
}
