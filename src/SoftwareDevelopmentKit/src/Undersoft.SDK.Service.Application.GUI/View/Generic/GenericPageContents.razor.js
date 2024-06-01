export function queryDomForTocEntries() {
    const article = document.getElementById('article');
    const headings = article.querySelectorAll("h2, h3, h4");

    let tocArray = new Array()
    let chapter = null;
    let subchapter = null;

    for (let element of headings) {
        if (!element.id) {
            let anchorText = element.innerText;
            let elementId = anchorText.replaceAll(" ", "-", "/", "\\", "#", "$", "@", ":", ",").toLowerCase();
            element.id = elementId;
        }
        if (element.innerText) {
            let anchor = {
                "level": element.nodeName,
                "text": element.innerText,
                "href": "#" + element.id,
                "anchors": new Array()
            };

            if ("H3" === element.nodeName) {
                if (chapter) {
                    subchapter = anchor;
                    chapter.anchors.push(subchapter);

                }
            } else if ("H4" === element.nodeName) {
                if (subchapter) {
                    subchapter.anchors.push(anchor);
                }
            }
            else {
                chapter = anchor;
                tocArray.push(chapter);

            }
        }
    }
    return tocArray;
}