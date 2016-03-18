function detailsControl() {

    $(".itemWrapper .itemDescription").click(function () {
        displayDetails(this);
    });

    function displayDetails(element) {
        //hide the details of all the other items on small screen
        $(".itemWrapper").each(function () {
            $("[name='itemDetails']").addClass("hide").removeClass("show-for-small-only");
        });

        //send the data to the panel for medium-up screen
        var data = $(element).parent().find("[name='itemDetails']").html();
        $(".panel.details").html(data);

        $(element).parent().find("[name='itemDetails']").addClass("show-for-small-only");

        return data;
    }

}