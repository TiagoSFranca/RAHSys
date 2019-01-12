function ObterSituacao(atividade) {
    $('#infoSituacaoRecorrencia').html($("<span class=\"label label-" + atividade.SituacaoRecorrencia.Classe + "\"> " + atividade.SituacaoRecorrencia.Nome + "</span>"));
    //$('#infoSituacaoAtividade').html($("<span class=\"label label-" + atividade.SituacaoAtividade.Classe + "\"> " + atividade.SituacaoAtividade.Nome + "</span>"));
}

function ExibirAtividade(atividade) {
    if (atividade != null) {
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

        $('#divInfoAtividade').show();
        $('#divInfoRecorrencia').show();

        $('#tipoAtividade').val(atividade.TipoAtividade);
        $('#dataPrevista').val(ConverterData(atividade.DataRealizacaoPrevista));
        $('#dataRealizada').val(ConverterData(atividade.DataRealizacao));
        $('#atribuidoPara').val(atividade.Usuario != null ? atividade.Usuario.EmailEUserName : '')
        if (atividade.Realizada) {
            $('.atividade-realizada').show();
            $('#finalizarRecorrencia').hide();
            $('#ulRecorrencia').hide();
        }
        else {
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
    }
}

$(function () {
    ExibirAtividade(null);
    var calendar = $('#calendar').fullCalendar('getCalendar');
    calendar.on('eventClick', function (data) {
        ExibirAtividade(data.atividade);
    });
});