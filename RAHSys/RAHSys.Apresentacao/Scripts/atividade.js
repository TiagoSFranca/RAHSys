$(function () {
    ExibirRealizada($(".atividadeChkRealizada").prop('checked'));
    $(".atividadeChkRealizada").change(function () {
        ExibirRealizada(this.checked);
    });

    $('.inputCopiarAtividade').click(function () {
        PopularModais($(this).data('atividade'));
    });

    $('.inputTransferirAtividade').click(function () {
        PopularModais($(this).data('atividade'));
    });

    $('.inputFinalizarAtividade').click(function () {
        PopularModais($(this).data('atividade'));
    });
});

function ExibirRealizada(exibir) {
    if (exibir) {
        $(".atividadeRealizada").show();
        $("#dataRealizacao").prop('required', true);
    }
    else {
        $(".atividadeRealizada").hide();
        $("#dataRealizacao").prop('required', false);
    }
}

function PopularModais(atividade) {
    LimparFinalizarAtividadeForm(atividade);
    LimparCopiarAtividadeForm(atividade);
    PopularListaUsuariosEquipe(atividade.Equipe);
    $('.urlRetorno').val($('#inputUrlRetorno').val())
    $('.idAtividade').val(atividade.IdAtividade)
    $('.atividadeDataPrevista').val(ConverterData(atividade.DataPrevista));
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

function LimparCopiarAtividadeForm(atividade) {
    LimparForm('formCopiarAtividade')
}

function LimparForm(form) {
    $('#' + form).find("input, textarea").val("")
}

function ConverterData(data) {
    if (data == null)
        return "";
    return moment(new Date(data)).format("DD/MM/YYYY");
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