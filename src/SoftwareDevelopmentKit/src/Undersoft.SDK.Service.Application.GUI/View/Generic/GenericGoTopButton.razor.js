let backToTopButton = document.getElementById("backtotop");

// When the user scrolls down 20px from the top of the document, show the button
let bodycontent = document.getElementById('body-content');
if (!bodycontent) {
    bodycontent = document.body;
}

bodycontent.onscroll = function ()
{ 
    scrollFunction()
};

function scrollFunction() {
    if (document.body.scrollTop > 20 ||document.getElementById('body-content').scrollTop > 20 || document.documentElement.scrollTop > 20) {
        backToTopButton.style.display = "flex";
    } else {
        backToTopButton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
export function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
    document.getElementById('body-content').scrollTop = 0;
}