function ObterSituacao(atividade) {
    $('#infoSituacaoRecorrencia').html($("<span class=\"label label-" + atividade.SituacaoRecorrencia.Classe + "\"> " + atividade.SituacaoRecorrencia.Nome + "</span>"));
    $('#infoSituacaoAtividade').html($("<span class=\"label label-" + atividade.SituacaoAtividade.Classe + "\"> " + atividade.SituacaoAtividade.Nome + "</span>"));
}

function ExibirAtividade(atividade) {
    if (atividade != null) {
        $('#copiarAtividade').data('atividade', atividade);
        $('#transferirAtividade').data('atividade', atividade);
        $('#finalizarRecorrencia').data('atividade', atividade);
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
        }
        else {
            $('#finalizarRecorrencia').show();
            $('.atividade-realizada').hide();
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
    var $atividades = JSON.parse($('#atividades').val());
    var $events = [];
    $.each($atividades, function (key, item) {
        $events.push(
            {
                title: item.TipoAtividade,
                start: item.DataRealizacaoPrevista,
                atividade: item,
                color: item.SituacaoRecorrencia.BGCor
            })
    });

    var initialLocaleCode = 'pt-br';
    $('#calendar').fullCalendar({
        header: {
            left: '',
            center: 'title',
            right: 'prev,next today'
        },
        lang: initialLocaleCode,
        defaultView: 'month',
        buttonIcons: true,
        eventLimit: true,
        editable: true,
        events: $events
    });

    var calendar = $('#calendar').fullCalendar('getCalendar');
    calendar.on('eventClick', function (data) {
        ExibirAtividade(data.atividade);
    });

});