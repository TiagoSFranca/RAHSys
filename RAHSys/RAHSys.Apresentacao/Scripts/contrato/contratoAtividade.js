function ObterSituacao(atividade) {
    $('#infoSituacaoRecorrencia').html($("<span class=\"label label-" + atividade.SituacaoRecorrencia.Classe + "\"> " + atividade.SituacaoRecorrencia.Nome + "</span>"));
}

function PreencherInfoEquipe(atividade) {
    var $equipe = atividade.Equipe;

    var $showInfoEquipe = $('#showInfoEquipe');
    if ($showInfoEquipe.hasClass('expand'))
        $showInfoEquipe.click();
    $('#divInfoEquipe').show();

    $('#idEquipe').val($equipe.IdEquipe)
    $('#infoLider').val($equipe.Lider.EmailEUserName)

    if ($equipe.EquipeUsuarios.length > 0) {
        $('.membros-equipe').show();
        var $divIntegrantes = $('#divIntegrante');
        $.each($equipe.EquipeUsuarios, function (index, element) {
            if (index == 0) {
                $divIntegrantes.find('#infoIntegrante').val(element.Usuario.EmailEUserName)
            } else {
                var clone = $divIntegrantes.clone();
                clone.find('#infoIntegrante').val(element.Usuario.EmailEUserName)
                clone.insertBefore($divIntegrantes)
            }
        });
    } else {
        $('.membros-equipe').hide();
    }
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
        PreencherInfoEquipe(atividade);

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
        $('#divInfoEquipe').hide();
    }
}

$(function () {
    ExibirAtividade(null);
    var calendar = $('#calendar').fullCalendar('getCalendar');
    calendar.on('eventClick', function (data) {
        ExibirAtividade(data.atividade);
    });
});