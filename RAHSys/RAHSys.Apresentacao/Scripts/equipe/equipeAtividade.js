function ObterSituacao(atividade) {
    $('#infoSituacaoRecorrencia').html($("<span class=\"label label-" + atividade.SituacaoRecorrencia.Classe + "\"> " + atividade.SituacaoRecorrencia.Nome + "</span>"));
}

function PreencherInfoContrato(atividade) {
    var $contrato = atividade.Contrato;

    var $showInfoContrato = $('#showInfoContrato');
    if ($showInfoContrato.hasClass('expand'))
        $showInfoContrato.click();
    $('#divInfoContrato').show();

    $('#idContrato').val($contrato.IdContrato)
    $('#contatoInicial').val($contrato.ContatoInicial)
    $('#nomeEmpresa').val($contrato.NomeEmpresa)

}

function ExibirAtividade(atividade) {
    if (atividade != null) {
        var urlFinalizarAtividade = $('#inputUrlFinalizarAtividade').val();
        var urlEvidencias = $('#inputUrlEvidencias').val();
        var data = moment(atividade.DataRealizacaoPrevista, "YYYY-MM-DD").format('DD/MM/YYYY');
        urlFinalizarAtividade += '?idAtividade=' + atividade.IdAtividade + '&data=' + data + '&urlRetorno=' + encodeURIComponent($('#inputUrlRetorno').val());
        urlEvidencias += '?idAtividade=' + atividade.IdRecorrencia + '&urlRetorno=' + encodeURIComponent($('#inputUrlRetorno').val());
        $('#finalizarRecorrencia').attr('href', urlFinalizarAtividade)
        $('#evidencias').attr('href', urlEvidencias)
        $('.addDataAtividade').data('atividade', atividade);
        $('#codigoAtividade').html('#' + atividade.IdAtividade)
        $('#codigoRecorrencia').html('nº ' + atividade.NumeroRecorrencia)

        var $showInfoAtividade = $('#showInfoAtividade');
        var $showInfoRecorrencia = $('#showInfoRecorrencia');

        if ($showInfoAtividade.hasClass('expand'))
            $showInfoAtividade.click();

        if ($showInfoRecorrencia.hasClass('expand'))
            $showInfoRecorrencia.click();

        ObterSituacao(atividade);
        PreencherInfoContrato(atividade);

        $('#divInfoAtividade').show();
        $('#divInfoRecorrencia').show();

        $('#tipoAtividade').val(atividade.TipoAtividade);
        $('#dataPrevista').val(ConverterData(atividade.DataRealizacaoPrevista));
        $('#dataRealizada').val(ConverterData(atividade.DataRealizacao));
        var atribuidoPara = "";
        if (atividade.EquipeInteira)
            atribuidoPara = "Equipe Inteira";
        else
            atribuidoPara = atividade.Usuario != null ? atividade.Usuario.EmailEUserName : ''
        $('#atribuidoPara').val(atribuidoPara);
        if (atividade.Realizada) {
            $('.atividade-realizada').show();
            $('#finalizarRecorrencia').hide();
            $('#evidencias').show();
        }
        else {
            $('#evidencias').hide();
            $('#finalizarRecorrencia').show();
            $('#ulRecorrencia').show();
            $('.atividade-realizada').hide();
        }

        if (!atividade.Encerrar) {
            $('#encerrarAtividade').hide();
        } else {
            $('#encerrarAtividade').show();

        }

        $('#descricao').val(atividade.Descricao);
        $('#observacao').val(atividade.Observacao);
    } else {
        $('#codigoAtividade').html('')
        $('#divInfoAtividade').hide();
        $('#divInfoRecorrencia').hide();
        $('#finalizarRecorrencia').hide();
        $('#divInfoContrato').hide();
    }
}

$(function () {
    ExibirAtividade(null);
    var calendar = $('#calendar').fullCalendar('getCalendar');
    calendar.on('eventClick', function (data) {
        ExibirAtividade(data.atividade);
    });
});