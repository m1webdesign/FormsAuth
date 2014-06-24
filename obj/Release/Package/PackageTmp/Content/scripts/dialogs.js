function loadDialog(tag, event, target, loading) {
    event.preventDefault();
    var $loading = $('<img src="ajaxLoading.gif" alt="loading" class="ui-loading-icon">');
    var $url = $(tag).attr('href');
    var $title = $(tag).attr('title');
    var $dialog = $('<div></div>');

    $dialog.empty();

    $dialog
        .append($loading)
        .load($url)//URL to fire
        .dialog({
            autoOpen: false
            , title: $title
            , width: 500
            , modal: true
            , minHeight: 200
            , show: 'fade'
            , hide: 'fade',
            position: {
                my: 'center',
                at: 'top',
                of: $('#main')
            }
        });

    $dialog.dialog("option", "buttons", {
        "Cancel": function () {
            $(this).dialog("close");
            $(this).empty();
        },
        "Submit": function () {
            //#target is id of form (given on ajax.beginform) $(target) is target parameter
            $.validator.unobtrusive.parse($("#target"));
            $("#target").validate();
            if ($("#target").validate().form()) {
                var dlg = $(this);
                $.ajax({
                    url: $url,
                    type: 'POST',

                    data: $("#target").serialize(),

                    complete: function () {

                    },

                    success: function (response) {
                        if (response.error)
                            dlg.html(response.message); //if error, show dialog
                        else {

                            $(target).html(response.message); //if not, show #bookinfo

                            dlg.empty();
                            dlg.dialog('close');
                        }


                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        if (xhr.status == 400)
                            dlg.html(xhr.responseText, xhr.status);     /* display validation errors in edit dialog */
                        else
                            displayError(xhr.responseText, xhr.status); /* display other errors in separate dialog */

                    }
                });
            }
        }
    });

    $dialog.dialog('open');

};

function loadHTMLDialog(tag) {
    var $dialog = $('<div></div>');
    var $url = $(tag).attr('href');
    var $title = $(tag).attr('name');
    $dialog.empty();

    $dialog
        .load($url)
        .dialog({
            width: 620,
            modal: true,
            resizable: false,
            position: { 
                    my: 'center',
                    at: 'top',
                    of: $('#main')
                },

            title: $title,
            autoOpen: false,
            buttons:
                {
                    "Cancel": function () {
                        $(this).dialog("close");
                        $(this).empty();
                    },
                    "Submit": function () {
                        $.validator.unobtrusive.parse($("#target"));//validate
                        $("#target").validate();//validate
                        if ($("#target").validate().form()) {//validate
                            $("#target").submit();
                            $(this).dialog("close");
                        }
                    }


                }
        });
    $dialog.dialog('open');
}

function deleteList(url, event, target, loading, deletearray) {
    event.preventDefault();

    var $loading = $('<img src="ajaxLoading.gif" alt="loading" class="ui-loading-icon">');

    $.ajax({
        url: url,
        type: 'POST',
        traditional: true, //to serialize objects into query parameter
        data: { bookings: deletearray },

        complete: function () {

        },

        success: function (response) {
            $(target).html(response);
        },
        error: function (xhr) {
            alert(xhr.responseText);
            if (xhr.status == 400)
                dlg.html(xhr.responseText, xhr.status);     /* display validation errors in edit dialog */
            else
                displayError(xhr.responseText, xhr.status); /* display other errors in separate dialog */

        }
    });
}