function buscarCidades(id, dropdown) {
    var $consultaCidades = $("#consultaCidades");
    var $ddlCidades = $("#" + dropdown);    
    $ddlCidades.empty();
    if (id > 0) {
        $.get($consultaCidades.val(), { id: id }, function (data) {
            $.each(data, function (index, elemento) {
                $ddlCidades.append("<option value=\"" + elemento.IdCidade + "\">" + elemento.Nome + "</option>");
            });
        });
    }
}
