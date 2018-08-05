$(".abrirModal").click(function (e) {
    e.preventDefault();
    var href = $(this).attr('href');
    $("#bloco-modal").load(href, function () {
        $("#bloco-modal").modal();
    });
});