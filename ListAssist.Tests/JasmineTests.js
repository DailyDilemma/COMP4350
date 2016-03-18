/// <reference path="../ListAssist/Scripts/jquery-1.10.2.js" />
/// <reference path="../ListAssist/Scripts/app.js" />

describe("Details", function () {

    var element;

    beforeEach(function () {
        var element = document.createElement("div");
        element.setAttribute("name", "itemDetails");
        element.appendChild(document.createTextNode("DETAILS"));
    });

    it("should be equal to itself", function () {
        expect(detailsControl(element)).toEqual(element);
    });
});