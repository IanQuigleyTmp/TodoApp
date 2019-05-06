
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



    // Date display information
    monthNameShort = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];
    monthName = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    dayNameShort = ["SUN", "MON", "TUE", "WED", "THR", "FRI", "SAT"];
    dayName = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    
    todoApp.addDateFields = function (obj, ticks) {

        var date = new Date(ticks);
        obj.date = date.getDate();
        obj.month = date.getMonth()

        obj.monthNameShort = monthNameShort[obj.month];
        obj.monthName = monthName[obj.month];
        obj.dayNameShort = dayNameShort[date.getUTCDay()];
        obj.dayName = dayName[date.getUTCDay()];

        obj.year = date.getFullYear();
        obj.hour = date.getUTCHours();
        obj.minute = zeroPad(2, date.getUTCMinutes());
        obj.seconds = zeroPad(2, date.getSeconds());

        obj.dateTH = 'th';
        if (obj.date == 01 || obj.date == 21 || obj.date == 31)
            obj.dateTH = 'st';
        if (obj.date == 02 || obj.date == 22)
            obj.dateTH = 'nd';
        if (obj.date == 03 || obj.date == 23)
            obj.dateTH = 'rd';
    };

    zeroPad = function (digits, number) {
        var pad = "0000000000" + number;
        return pad.substring(pad.length - digits);
    };

    todoApp.logout = function () {
        todoApi.logout(function () {
            window.location.reload();
        });
    }

}(window.todoApp = window.todoApp || {}, jQuery));
