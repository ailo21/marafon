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
    
});

