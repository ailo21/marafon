$(document).ready(function () {
    if ($('#birthday_years').length > 0) {
        $('#birthday_years').datetimepicker({
            //format: 'Do MMMM, YYYY',
            format: 'DD.MM.YYYY',
            //defaultDate: new Date(1990, 0, 1, 00, 01),
            viewMode: 'years',
        });

        var localeString = 'ru';
        $('#birthday_years').data("DateTimePicker").locale(localeString);
    }


    $('.modal[data-show="true"]').modal('toggle');
    
});

// Всплывающие окна
function Confirm(Title, Body, Object) {
    clear_modal();

    var $BtnOk = $('<button/>', { 'id': 'modal-btn-ok', 'class': 'btn btn-danger' }).append('Да');
    $BtnOk.click(function () {
        $('form input[required]').removeAttr('required');
        Object.trigger('click');
    });

    var $BtnNo = $('<button/>', { 'id': 'modal-btn-no', 'class': 'btn btn-default' }).append('Нет');
    $BtnNo.click(function () {
        $('.modal').modal('toggle');
    });

    $modalTitle.append(Title);
    $modalBody.append('<p>' + Body + '</p>');
    $modalFooter.append($BtnOk).append($BtnNo);

    $modal.modal('toggle');
};
// Очищаем модальное окно
function clear_modal() {
    $modal.find('.modal-dialog').removeClass().addClass('modal-dialog'),
        $modalTitle.empty();
    $modalBody.empty();
    $modalFooter.empty();
};