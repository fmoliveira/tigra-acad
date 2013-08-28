$('#MyProfile button').click(function () {
    var $action = $(this).data('action');
    var $fullname = $('#fullname');
    console.log($action);

    if ($action == "SaveProfile") {
        /* Make post URI and get login data. */
        var $uri = $(this).parent().data('api') + $action;
        var $data = JSON.stringify({ "FullName": $fullname.val() });

        /* Post login data. */
        $.ajax({
            url: $uri,
            type: 'POST',
            dataType: 'json',
            data: $data,
            contentType: 'application/json; charset=utf-8',
            complete: function (x, y, z) {
                switch (x.status) {
                    case 200:
                        location.relo();
                        break;

                    case 500:
                        alert('Erro no servidor!');
                        break;

                    default:
                        alert('Erro inesperado (código ' + x.status + ')!');
                        break;
                }
            }
        });
    }
});