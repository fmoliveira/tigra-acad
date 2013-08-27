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
		var $prox = $(this).next();
		if ($prox.prop('tagName') == "INPUT") {
			$prox.focus();
		}
		else
		{
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

/* Shows popver message below an invalid input. */
function Error(elem, msg) {
	$('#login-menu form').prepend('<div class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + msg + '</div>');

	if (elem != null) {
		$('#' + elem).focus();
	}
}

/* Handles login form button clicks. */
$('#login-menu button').click(function () {
	var $action = $(this).data('action');
	var $email = $('#email');
	var $password = $('#password');
	$('#login-menu form').find('.alert').remove();

	/* Validate login form. */
	if ($email.val().length == 0) {
		Error('email', 'Digite seu email!');
	} else if ($email.val().length > 80) {
		Error('email', 'Email deve ter no máximo 80 caracteres!');
	} else if (false == ValidateEmail($email.val())) {
		Error('email', 'Endereço de email inválido!');
	} else if ($password.val().length == 0) {
		Error('password', 'Digite sua senha!');
	} else if ($action == "Register" && $password.val().length < 6) {
		Error('password', 'Senha deve ter no mínimo 6 caracteres!');
	} else if ($action == "Register" && $password.val().length > 32) {
		Error('password', 'Senha deve ter no máximo 32 caracteres!');
	} else {
	    /* Post login data. */
	    // { email: $email.val(), password: $password.val() }

		var $uri = $(this).parent().data('api') + $action;
	    $.post($uri, { '': JSON.stringify({ "Email": $email.val(), "Password": $password.val() }) }, function (data) {
		    Error(null, 'Sucesso! ' + data);
		})
		.done(function () {
		    //Error(null, 'Sucesso denovo!');
		})
		.fail(function () {
		    Error(null, 'Login falhou!');
		});
	}
});
