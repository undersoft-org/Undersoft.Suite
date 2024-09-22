var GenericUtilities = GenericUtilities || {};

GenericUtilities.setFocusByElement = function (element) {
    if (!!element) {
        element.focus();
    }
};

GenericUtilities.setFocusById = function (id) {
    GenericUtilities.setFocusByElement(document.getElementById(id));    
};