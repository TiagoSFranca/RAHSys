$(function () {
    if ($("#ddlLider").val())
        $(".integrantes").show();
    else
        $(".integrantes").hide();

    $("#ddlLider").change(function () {
        var id = $(this).val();
        var $consultaCidades = $("#consultaIntegrantes");
        var $ddlIntegrantes = $("#selectIntegrantes");
        $ddlIntegrantes.empty();
        if (id) {
            $(".integrantes").show();
            $.get($consultaCidades.val(), { id: id }, function (data) {
                $.each(data, function (index, elemento) {
                    $ddlIntegrantes.append(AdicionarOptionGroup(elemento));
                });
                $('#selectIntegrantes').multiSelect("refresh");
                $(".ms-container").width("100%");
            });
        } else {
            $(".integrantes").hide();
            $('#selectIntegrantes').multiSelect("refresh");
            $(".ms-container").width("100%");
        }
    });

    $('#selectIntegrantes').multiSelect({
        selectableOptgroup: true,
    });

    $(".ms-container").width("100%");
});

function AdicionarOption(listaUsuarios) {
    var options = "";
    $.each(listaUsuarios, function (index, elemento) {
        options += "<option value=\"" + elemento.IdUsuario + "\">" + elemento.EmailEUserName + "</option>"
    });
    return options;
}

function AdicionarOptionGroup(grupo) {
    var optionGroup = "<optgroup label=\"" + grupo.Perfil + "\">" +
        AdicionarOption(grupo.Usuarios) +
        "</optgroup>";
    return optionGroup;
}