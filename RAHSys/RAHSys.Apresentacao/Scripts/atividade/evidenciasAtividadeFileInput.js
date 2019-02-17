$(document).on('change.bs.fileinput', '.fileinput', function () {
    var $fileInputDiv = $(this).parent().parent();
    var clone = $fileInputDiv.clone();
    clone.find('.fileinput').fileinput('clear');
    clone.insertAfter($fileInputDiv)
})

$(document).on('clear.bs.fileinput', '.fileinput', function () {
    var $fileInputDiv = $(this).parent().parent();
    $fileInputDiv.remove()
})