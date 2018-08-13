function buscarCidades(id) {
    var $consultaCidades = $("#consultaCidades");
    $.get($consultaCidades.val(), { id: id }, function (data) {
        var $ddlCidades = $('#ddlCidades');
        $ddlCidades.empty();
        $.each(data, function (index, elemento) {
            $ddlCidades.append("<option value=\"" + elemento.IdCidade + "\">" + elemento.Nome + "</option>");
        });
    });
}