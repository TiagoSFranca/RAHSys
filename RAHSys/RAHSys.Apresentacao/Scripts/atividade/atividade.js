function MoverDatasCalendario() {
    var view = $('#calendar').fullCalendar('getView');
    var datas = ObterIntervaloDatas();
    var dataInicial = datas.inicio;
    var dataFinal = datas.fim;
    var modoVisualizacao = ObterDefaultView();
    var rota = $('#inputUrlBase').val()
    var append = 'dataInicial=' + dataInicial + '&dataFinal=' + dataFinal + '&modoVisualizacao=' + modoVisualizacao;

    if (rota.indexOf('?') != -1)
        append = '&' + append;
    else
        append = '?' + append;

    rota = rota + append;

    window.location.replace(rota);
}

function ObterIntervaloDatas() {
    var calendar = $('#calendar').fullCalendar('getCalendar');
    var view = calendar.view;
    var start = moment(view.start).format('l');
    var end = moment(view.end).format('l');
    var dates = { inicio: start, fim: end };

    return dates;
}

function ObterDefaultView() {
    var view = $('#calendar').fullCalendar('getView');
    return view.name;
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
        MoverDatasCalendario();
    });

    $('.fc-next-button').click(function () {
        MoverDatasCalendario();
    });

    $('.fc-month-button').click(function () {
        MoverDatasCalendario();
    });

    $('.fc-basicWeek-button').click(function () {
        MoverDatasCalendario();
    });

    $('.fc-basicDay-button').click(function () {
        MoverDatasCalendario();
    });

    $('.fc-today-button').click(function () {
        MoverDatasCalendario();
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

function LimparForm(form) {
    $('#' + form).find('input, textarea').val('')
}

function ConverterData(data) {
    if (data == null)
        return '';
    return moment(new Date(data)).format('DD/MM/YYYY');
}

function PopularListaUsuariosEquipe(equipe) {
    var equipeHelper = $("#equipeInteiraHelper").val();
    var $eq = JSON.parse(equipeHelper)
    $('.atividadeUsuario').empty().append($('<option>', {
        text: 'Selecione',
        value: ''
    }));
    $('.atividadeUsuario').append($('<option>', {
        text: $eq.Descricao,
        value: $eq.Codigo
    }));
    $.each(equipe.Usuarios, function (i, item) {
        $('.atividadeUsuario').append($('<option>', {
            text: item.EmailEUserName,
            value: item.IdUsuario
        }));
    });
}

function IniciarCalendario() {
    var dataInicial = $('#dataInicial').val()
    var dataFinal = $('#dataFinal').val()
    var dataInicialConvertida = moment(dataInicial, 'DD/MM/YYYY');
    var dataFinalConvertida = moment(dataFinal, 'DD/MM/YYYY');

    var diff = dataFinalConvertida.diff(dataInicialConvertida, 'days');

    var dias = diff <= 1 ? 0 : (diff / 2 + (diff % 2 != 0 ? 1 : 0));
    var dataMedia = dataInicialConvertida.add(dias.toFixed(0), 'days');

    var defaultView = $('#modoVisualizacao').val();
    var $atividades = JSON.parse($('#atividades').val());

    var $events = [];
    $.each($atividades, function (key, item) {
        $events.push(
            {
                title: item.TipoAtividade,
                start: item.DataRealizacaoPrevista,
                atividade: item,
                allDay: true,
                color: item.SituacaoRecorrencia.BGCor
            })
    });

    var initialLocaleCode = 'pt-br';
    $('#calendar').fullCalendar({
        header: {
            left: 'prev,next',
            center: 'title',
            right: 'month,basicWeek,basicDay, today'
        },
        lang: initialLocaleCode,
        defaultView: defaultView,
        buttonIcons: true,
        eventLimit: true,
        editable: true,
        defaultDate: dataMedia,
        events: $events,
        timeFormat: '',
        visibleRange: {
            start: dataInicialConvertida,
            end: dataFinalConvertida
        },
        dayClick: function (date, jsEvent, view) {
            if (ObterDefaultView() !== 'basicDay') {
                $('#calendar').fullCalendar('changeView', 'basicDay');
                $('#calendar').fullCalendar('gotoDate', date);

                MoverDatasCalendario()
            }
        }
    });

    $('#calendar').fullCalendar('option', 'timezone', false);
}