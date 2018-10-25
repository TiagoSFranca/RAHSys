function ObterSituacao(atividade) {
    $('#infoSituacao').html($("<span class=\"label label-" + atividade.SituacaoAtividade.Classe + "\"> " + atividade.SituacaoAtividade.Nome + "</span>"));
}

function ExibirAtividade(atividade) {
    if (atividade != null) {
        $('#copiarAtividade').data('atividade', atividade);
        $('#transferirAtividade').data('atividade', atividade);
        $('#finalizarAtividade').data('atividade', atividade);
        $('#codigoAtividade').html('#' + atividade.IdAtividade)
        var $showInfoAtividade = $('#showInfoAtividade');
        if ($showInfoAtividade.hasClass('expand'))
            $showInfoAtividade.click();
        ObterSituacao(atividade);
        $('#divInfoAtividade').show();
        $('#tipoAtividade').val(atividade.TipoAtividade.Descricao);
        $('#dataPrevista').val(ConverterData(atividade.DataPrevista));
        $('#dataRealizada').val(ConverterData(atividade.DataRealizacao));
        $('#atribuidoPara').val(atividade.Usuario != null ? atividade.Usuario.EmailEUserName : '')
        if (atividade.Realizada) {
            $('.atividade-realizada').show();
            $('#finalizarAtividade').hide();
        }
        else {
            $('#finalizarAtividade').show();
            $('.atividade-realizada').hide();
        }
        $('#descricao').val(atividade.Descricao);
        $('#observacao').val(atividade.Observacao);
    } else {
        $('#codigoAtividade').html('')
        $('#divInfoAtividade').hide();
        $('#finalizarAtividade').hide();
    }
}

$(function () {
    ExibirAtividade(null);
    var $atividades = JSON.parse($('#atividades').val());
    var $events = [];
    $.each($atividades, function (key, item) {
        $events.push(
            {
                title: item.TipoAtividade.Descricao,
                start: item.DataPrevista,
                atividade: item,
                color: item.SituacaoAtividade.BGCor
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

    $('.calendario-portlet').addClass('portlet-collapsed');
});