$(".ratingStar").hover(function () {
    $(".ratingStar").addClass("far").removeClass("fas");

    $(this).addClass("fas").removeClass("far");
    $(this).prevAll(".ratingStar").addClass("fas").removeClass("far");
});
