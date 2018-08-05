function ordenar(el) {
    var valorOrdenacao = $('#ordenacao').val();
    var valorCrescente = $('#crescente').val() === 'true';
    console.log(valorOrdenacao);
    if (valorOrdenacao == el)
        valorCrescente = !valorCrescente;

    $('#ordenacao').val(el);
    $('#crescente').val(valorCrescente);
    $('#form-busca').submit();
}