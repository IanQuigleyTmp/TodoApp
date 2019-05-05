
(function (todoApp, $, undefined) {


    todoApp.buttonRequiresField = function ($button, $container) {
        


        // Quick and dirty required fields validation
        $container.find('.xx-required-field').on('keyup', function () {
            $button.removeAttr('disabled');
            $container.find('.xx-required-field').each(function (idx, elem) {
                if ($(elem).val().trim().length == 0) {
                    $button.attr('disabled', 'disabled');
                }
            });
        });

    };

    todoApp.dataFrom = function ($elem) {

        // take data from controls and add to object
        var data = {};
        $elem.find('*[data-name]').each(function (idx, elem) {

            var $elem = $(elem);
            if (elem.type == 'checkbox')
                data[$elem.data('name')] = elem.checked ? "true" : "false";
            else
                data[$elem.data('name')] = $elem.val();

        });

        return data;
    }

    todoApp.logout = function () {
        todoApi.logout(function () {
            window.location.reload();
        });
    }

}(window.todoApp = window.todoApp || {}, jQuery));
