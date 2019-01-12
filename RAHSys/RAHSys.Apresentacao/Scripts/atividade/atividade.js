function MoverMesCalendario(data) {
    var mesAno = data.month() + 1 + encodeURIComponent("/") + data.year();
    var rota = $('#inputUrlBase').val()
    var append = 'mesAno=' + mesAno;
    if (rota.indexOf('?') != -1)
        append = '&' + append;
    else
        append = '?' + append;
    rota = rota + append;
    window.location.replace(rota);
}

function EditarAtividade(atividade) {
    var rota = $('#inputEditarAtividade').val();
    var append = 'idAtividade=' + atividade.IdAtividade;
    if (rota.indexOf('?') != -1)
        append = '&' + append;
    else
        append = '?' + append;
    rota = rota + append;
    window.location.href = rota;
}

$(function () {
    IniciarCalendario();
    $('.fc-prev-button').click(function () {
        var data = $('#calendar').fullCalendar('getDate');
        MoverMesCalendario(data);
    });

    $('.fc-today-button').click(function () {
        var data = $('#calendar').fullCalendar('getDate');
        MoverMesCalendario(data);
    });

    $('.fc-next-button').click(function () {
        var data = $('#calendar').fullCalendar('getDate');
        MoverMesCalendario(data);
    });

    ExibirRealizada($('.atividadeChkRealizada').prop('checked'));
    $('.atividadeChkRealizada').change(function () {
        ExibirRealizada(this.checked);
    });

    $('.addDataAtividade').click(function () {
        PopularModais($(this).data('atividade'));
    });

    $('#editarAtividade').click(function () {
        EditarAtividade($(this).data('atividade'));
    });
});

function ExibirRealizada(exibir) {
    if (exibir) {
        $('.atividadeRealizada').show();
        $('#dataRealizacao').prop('required', true);
    }
    else {
        $('.atividadeRealizada').hide();
        $('#dataRealizacao').prop('required', false);
    }
}

function PopularModais(atividade) {
    LimparFinalizarAtividadeForm(atividade);
    //LimparCopiarAtividadeForm(atividade);
    PopularListaUsuariosEquipe(atividade.Equipe);
    $('.urlRetorno').val($('#inputUrlRetorno').val())
    $('.idAtividade').val(atividade.IdAtividade);
    $('.atividadeDataPrevista').val(ConverterData(atividade.DataRealizacaoPrevista));
    $('.atividadeDescricao').val(atividade.Descricao);
    SetRealizada(atividade.Realizada);
    $('.atividadeDataRealizacao').val(ConverterData(atividade.DataRealizacao));
    $('.atividadeObservacao').val(atividade.Observacao);
}

function SetRealizada(check) {
    if (check == true && $('.atividadeChkRealizada').prop('checked') == false) {
        $('.atividadeChkRealizada').click()
    }
}

function LimparFinalizarAtividadeForm(atividade) {
    LimparForm('formFinalizarAtividade')
}

//function LimparCopiarAtividadeForm(atividade) {
//    LimparForm('formCopiarAtividade')
//}

function LimparForm(form) {
    $('#' + form).find('input, textarea').val('')
}

function ConverterData(data) {
    if (data == null)
        return '';
    return moment(new Date(data)).format('DD/MM/YYYY');
}

function PopularListaUsuariosEquipe(equipe) {
    $('.atividadeUsuario').empty().append($('<option>', {
        text: 'Selecione',
        value: ''
    }));
    $.each(equipe.EquipeUsuarios, function (i, item) {
        $('.atividadeUsuario').append($('<option>', {
            text: item.Usuario.EmailEUserName,
            value: item.IdUsuario
        }));
    });
}

function IniciarCalendario() {
    var data = $('#mesAno').val()
    data = "01/" + data;
    var dataConvertida = moment(data, 'DD/MM/YYYY');
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
        events: $events,
        defaultDate: dataConvertida
    });
}