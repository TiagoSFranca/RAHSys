$(function () {
    ExibirRealizada($(".atividadeChkRealizada").prop('checked'));
    $(".atividadeChkRealizada").change(function () {
        ExibirRealizada(this.checked);
    });
});

function ExibirRealizada(exibir) {
    if (exibir) {
        $(".atividadeRealizada").show();
        $("#dataRealizacao").prop('required', true);
    }
    else {
        $(".atividadeRealizada").hide();
        $("#dataRealizacao").prop('required', false);
    }
}