(function ($) {

    function partialContantsLoading() {
        $("#partialContents").each(function (index, item) {
            var url = $(item).data("url");
            if (url && url.length > 0) {
                $.ajax({
                    async: false,
                    url: url
                }).done(function (data) {
                    $(item).after(data);
                    $(item).remove();
                });
            }
        });
    }

    $(document).ready(function () {

        partialContantsLoading();

        $(".fancybox").fancybox({
            nextSpeed: 350,
            prevSpeed: 350
        });
    });

})(jQuery);


