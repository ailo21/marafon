var $modal, $modalTitle, $modalBody, $modalFooter
var change = 0;
$(document).ready(function () {

    $modal = $('.modal');
    $modalTitle = $('.modal .modal-title');
    $modalBody = $('.modal .modal-body');
    $modalFooter = $('.modal .modal-footer');

    
    if (top !== self) {
        console.log('test');
        top.location.href = location.href;
    }

    if ($('.uc_input').length > 0) ucInput_init();

    if ($('textarea').length > 0) {
        $('textarea').each(function () {
            if ($(this).attr('type') == "editor") {
                InitTinyMCE($(this).attr('id'));
            }
        });
    }

    //События Кнопок
    $('input[type=submit],button[type=submit], .button').bind({
        mousedown: function () {
            //логика всплывающих окон
            var Action = $(this).attr('data-action');
            if (Action !== undefined) {
                switch (Action) {
                    case "delete":
                        $('form input[required]').removeAttr('required');
                        $('form select[required]').removeAttr('required');
                        Confirm('Уведомление', 'Вы хотите удалить эту запись?', $(this));
                        break;
                    case "cancel":
                        $('form input[required]').removeAttr('required');
                        $('form select[required]').removeAttr('required');
                        if (change !== 0) {
                            Confirm('Уведомление', 'Выйти без изменений?', $(this));
                        }
                        else {
                            $('form input[required]').removeAttr('required');
                            $('form select[required]').removeAttr('required');
                            $(this).trigger('click');
                        }

                        break;
                    case "noPreloader-accept":
                        break;
                    //case
                    default: return false;
                }
            }

        },
        click: function () {
            var btn_class = $(this).attr('value');
            var dataAction = $(this).attr('data-action');
            var req_count = $('form input[required]:invalid').length

            if (req_count > 0 && btn_class === 'save-btn') {
            }
            else if (dataAction === 'noPreloader-accept') {
            }
            else {
                // показываем preloader при клике на кнопку
                var $load_bg = $("<div/>", { "class": "load_page" });
                $load_bg.bind({
                    mousedown: function () { return false; },
                    selectstart: function () { return false; }
                });

                $('body').append($load_bg);
            }
        }
    });


});
function InitTinyMCE(id) {
    tinymce.init({
        selector: "textarea#" + id,        
        language: "ru",
        height: 350,
        theme: 'modern',
        plugins: 'print preview fullpage searchreplace autolink directionality visualblocks visualchars fullscreen image link media codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount imagetools contextmenu colorpicker textpattern help',
        toolbar1: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat',
        image_advtab: true,   
        content_css: [
            '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
            '//www.tinymce.com/css/codepen.min.css'
        ]

    });
}
// Инициализация текстовых полей
function ucInput_init() {
    $('.uc_input').each(function () {
        var tagName = $(this).prop("tagName").toLowerCase();
        var ReadOnly = $(this).attr('readonly');
        var Important = $(this).attr('required');
        var Label = $(this).attr('title');
        var Type = $(this).attr('data-type');
        var Mask = $(this).attr('data-mask');
        var Help = $(this).attr('data-help');
        var TypeElem = $(this).attr('type');

        $(this).wrap('<div class="form-group"></div>');
        if (Label) { $(this).before('<label for="' + $(this).attr('id') + '">' + Label + ':</label>'); }

        if (Help && !ReadOnly) {
            $(this).wrap('<div class="input-group"></div>');
            $(this).after('<div class="input-group-addon"><div class="icon-help-circled" data-toggle="popover" data-placement="auto bottom" data-content="' + Help + '"></div></div>');

            $(this).next().find('div').popover();
        }


        if (TypeElem == "checkbox") {
            checkboxInit($(this), ReadOnly);
        }
        else if (TypeElem == "file") {
            fileInit($(this), ReadOnly);
        }
        else if (tagName == 'select') {
            selectInit($(this), ReadOnly);
        }
        else {
            $(this).addClass('form-control');
        }

        if (Type == 'date') {

            $(this).datetimepicker({
                format: 'DD.MM.YYYY'
            });
        }
        if (Type === 'datetime') {
            $(this).datetimepicker();
        }

        if (Mask != undefined) $(this).mask(Mask);
    });
}