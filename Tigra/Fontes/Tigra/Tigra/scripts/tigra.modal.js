function TigraModalClick() {
	var $action = $(this).data('action');
	console.log($action);

	if ($action == "SaveProfile") {
		/* Make post URI and get login data. */
		var $fullname = $('#fullname');
		var $uri = $('#MyProfile').data('api') + $action;
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
						location.reload();
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
}

$('a[data-tigra="modal"]').click(function () {
	$.get($(this).attr('href'), function (data) {
		$('#TigraModal').remove();
		var $div = $("<div>", { id: "foo", class: "a" });
		var $modal = '<div class="modal fade" id="TigraModal" data-api="' + location.href + 'Api/' + '" tabindex="-1" role="dialog" aria-label="Meu Perfil" aria-hidden="true"><div class="modal-dialog"><div class="modal-content">';
		$modal += data;
		$modal += '</div></div></div>';
		$('body').append($modal);
		Holder.run({ images: '.img-thumbnail' });
		$('#TigraModal button').click(TigraModalClick);
		$('#TigraModal').modal();
	});
	return false;
});