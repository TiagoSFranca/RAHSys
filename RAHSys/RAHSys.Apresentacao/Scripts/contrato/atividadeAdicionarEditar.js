function MontarSelect2(create) {
    var placeholder = "Selecione";
    $(".select2-multiple").select2({
        placeholder: placeholder,
        allowClear: true
    });
}

function MontarData() {
    $(".dataAtividade").datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR'
    });
}

function ExcluirSelect2() {
    if ($(".select2-multiple").data('select2'))
        $(".select2-multiple").select2("destroy");
}

$(document).ready(function () {
    MontarSelect2(true);
    MontarData();
    removerConfigAtividadeBody();
    $ddlRecorrencia = $("#ddlRecorrencia");
    exibirConfigAtividadeBody($ddlRecorrencia.val());
});

function removerConfigAtividadeBody() {
    ExcluirSelect2();
    if ($("#configAtividadeBody").val() == "") {
        $("#configAtividadeBody").val($(".config-atividade-body").html());
    }
    $(".config-atividade-body").html("");
}

function exibirConfigBody(idTipo) {
    $(".config-atividade-body").html($("#configAtividadeBody").val());
    MontarData();
    MontarSelect2(false);
    if (idTipo == $("#idTipoMes").val()) {
        ExcluirSelect2();
        $(".selectMes").show();
        $(".selectSemana").hide();
    } else if (idTipo == $("#idTipoSemana").val()) {
        $(".selectMes").hide();
        $(".selectSemana").show();
    } else {
        ExcluirSelect2();
        $(".selectMes").hide();
        $(".selectSemana").hide();
    }
}

function exibirConfigAtividadeBody(value) {
    if (value > 0) {
        exibirConfigBody(value);
    } else {
        removerConfigAtividadeBody();
    }
}