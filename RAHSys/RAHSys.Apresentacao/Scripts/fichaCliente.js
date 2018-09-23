$(document).ready(function () {
    removerConjugeBody();
    $ddlEstadoCivil = $("#ddlEstadoCivil");
    exibirConjuge($ddlEstadoCivil.val());
    exibirBotaoAdicionarContrato(false);
});

$("#ddlEquipe").change(function () {
    var id = $(this).val();
    var equipe = "";
    if (id) {
        equipe = $("#equipe_" + id).val();
    }
    $("#nomeEquipe")
        .attr('data-original-title', equipe);
});

function removerConjugeBody() {
    if ($("#conjugeBody").val() == "") {
        $("#conjugeBody").val($(".conjuge-body").html());
    }
    $(".conjuge-body").html("");
}

function exibirConjugeBody() {
    $(".conjuge-body").html($("#conjugeBody").val());
    $(".cep").inputmask("99.999-999");
    $('.telefone').inputmask('(99) 9999[9]-9999');
}

function exibirConjuge(value) {
    $idCasado = $("#idCasado").val();
    if (value == $idCasado) {
        $(".conjuge").show();
        exibirConjugeBody();
        $(".nao-conjuge").hide();
    } else {
        removerConjugeBody();
        $(".conjuge").hide();
        $(".nao-conjuge").show();
    }
}
function repetir(texto, dropdown) {
    $("#Cliente_Fiadores_1__FiadorEndereco_Endereco_" + texto).val(
        $("#Cliente_Fiadores_0__FiadorEndereco_Endereco_" + texto).val()
    )
    if (dropdown)
        $("#Cliente_Fiadores_1__FiadorEndereco_Endereco_" + texto).change();
}

$(document).on("click", "#repetir-endereco", function () {
    repetir("Logradouro");
    repetir("Numero");
    repetir("Bairro");
    repetir("CEP");
    repetir("Cidade_IdEstado", true);
    setTimeout(function () {
        $("#ddlCidadesFiadorConjuge").val($("#ddlCidadesFiador").val());
    }, 200);
});

function exibirBotaoAdicionarContrato(exibir) {
    if (exibir == true)
        $(".adicionar-contrato").show();
    else
        $(".adicionar-contrato").hide();
}

$(".fileinput").on("change.bs.fileinput", function (e) {
    exibirBotaoAdicionarContrato(true);
});

$(".fileinput").on("clear.bs.fileinput", function (e) {
    exibirBotaoAdicionarContrato(false);
});

$(".fileinput").on("reset.bs.fileinput", function (e) {
    exibirBotaoAdicionarContrato(false);
});

