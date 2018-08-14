$(document).ready(function () {
    $ddlEstadoCivil = $("#ddlEstadoCivil");
    exibirConjuge($ddlEstadoCivil.val());
});
function exibirConjuge(value) {
    $idCasado = $("#idCasado").val();
    if (value == $idCasado) {
        $(".conjuge").show();
        $(".nao-conjuge").hide();
    } else {
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

$("#repetir-endereco").click(function () {
    repetir("Logradouro");
    repetir("Numero");
    repetir("Bairro");
    repetir("CEP");
    repetir("Cidade_IdEstado", true);
    setTimeout(function () {
        $("#ddlCidadesFiadorConjuge").val($("#ddlCidadesFiador").val());
    }, 200);
});