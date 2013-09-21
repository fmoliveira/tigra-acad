/* Auto focus login form when dropdown is shown. */
$('#login-menu').on('shown.bs.dropdown', function () {
	setTimeout(function () { $('#login-menu :input[autofocus="autofocus"]').focus() }, 0);
});

/* Clears login form when dropdown is hidden. */
$('#login-menu').on('hidden.bs.dropdown', function () {
	$('#login-menu form').find('.alert').remove();
	$('#login-menu :input').val('');
});

/* Handles return key to swap input fields and login, and escape key to cancel. */
$('#login-menu input').keyup(function (event) {
	if (event.which == 13) {
	    var $id = $(this).attr('id');

		if ($id == "email") {
			$('#password').focus();
		} else if ($id == "password" || $id == "remember-me") {
			$('#login-menu :button[data-action="Login"]').click();
		}
	}
	else if (event.which == 27)
	{
		$('#login-menu .dropdown-toggle').click();
	}
});

/* Validates an email address. */
function ValidateEmail(email) {
	var re = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
	return re.test(email);
}

/* Shows alert message. */
function Alert(type, msg, elem) {
	$('#login-menu form').find('.alert').remove();
	$('#login-menu form').prepend('<div class="alert alert-' + type + ' alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + msg + '</div>');

	if (elem != null) {
		$('#' + elem).focus();
	}
}

/* Shows errors alert. */
function Error(msg, elem) {
	Alert('danger', msg, elem);
}

/* Shows warning alert. */
function Warning(msg, elem) {
	Alert('warning', msg, elem);
}

/* Shows errors alert. */
function Success(msg, elem) {
	Alert('success', msg, elem);
}

/* Shows info alert. */
function Info(msg, elem) {
	Alert('info', msg, elem);
}

function NewRegisterToken() {
    /* Make post URI and get login data. */
    var $uri = $('body').data('api') + 'NewRegisterToken';
    var $data = JSON.stringify({ "Email": $('#email').val() });

    /* Post login data. */
    $.ajax({
        url: $uri,
        type: 'POST',
        dataType: 'json',
        data: $data,
        contentType: 'application/json; charset=utf-8',
        complete: function (x, y, z) {
            switch (x.status) {
                case 202:
                    Success('Enviamos um novo código de ativação!<br/>Por favor verifique seu email.');
                    break;

                case 304:
                    Warning('A sua conta já está ativada!<br/>Você já pode entrar.');
                    break;

                case 404:
                    Error('Endereço de email inválido.');
                    break;

                case 500:
                    Error('Erro no servidor!');
                    break;

                default:
                    Error('Erro inesperado (código ' + x.status + ')!');
                    break;
            }
        }
    });
}

/* Handles login form button clicks. */
$('#login-menu button').click(function () {
	var $action = $(this).data('action');
	var $email = $('#email');
	var $password = $('#password');
	var $remember = $('#remember-me').is(':checked');
	$('#login-menu form').find('.alert').remove();

	/* Validate login form. */
	if ($email.val().length == 0) {
		Error('Digite seu email!', 'email');
	} else if ($email.val().length > 80) {
		Error('Email deve ter no máximo 80 caracteres!');
	} else if (false == ValidateEmail($email.val())) {
		Error('Endereço de email inválido!');
	} else if ($password.val().length == 0) {
		Error('Digite sua senha!', 'password');
	} else if ($action == "Register" && $password.val().length < 6) {
		Error('Senha deve ter no mínimo 6 caracteres!', 'password');
	} else if ($action == "Register" && $password.val().length > 32) {
		Error('Senha deve ter no máximo 32 caracteres!', 'password');
	} else {
		/* Make post URI and get login data. */
	    var $uri = $('body').data('api') + $action;
		var $data = JSON.stringify({ "Email": $email.val(), "Password": $password.val(), "RememberMe" : $remember });

		/* Post login data. */
		$.ajax({
			url: $uri,
			type: 'POST',
			dataType: 'json',
			data: $data,
			contentType: 'application/json; charset=utf-8',
			complete: function (x, y, z) {
				switch (x.status)
				{
					case 201:
						Success('Registro efetuado com sucesso!<br/>Por favor verifique seu email.');
						break;

					case 202:
						location.reload();
						break;

				    case 304:
				        Warning('A sua conta não está ativada!<br/>Por favor verifique seu email.<br/><a href="./" class="alert-link" onclick="NewRegisterToken(); return false;">O email não chegou?</a>')
				        break;

					case 401:
						Error('Acesso negado!<br/><a href="#" class="alert-link" data-action="LostPassword">Você esqueceu sua senha?</a>');
						break;

					case 409:
						Warning('Email já está registrado!');
						break;

					case 500:
						Error('Erro no servidor!');
						break;

					default:
						Error('Erro inesperado (código ' + x.status + ')!');
						break;
				}
			}
		});
	}
});
