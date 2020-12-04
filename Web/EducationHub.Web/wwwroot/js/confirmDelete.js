$("a[data-value]").each(function (el) {
    $(this).click(function () {
        let value = $(this).attr("data-value");
        $("#delete").attr('href', '/Courses/DeleteLesson/' + value);
    })
});

$("#delete-course").click(function () {
    let value = $(this).attr("data-value");
    $("#delete").attr('href', '/Courses/Delete/' + value);
});