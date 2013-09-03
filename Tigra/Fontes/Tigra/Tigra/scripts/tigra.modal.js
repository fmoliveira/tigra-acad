$(document).ajaxSend(function (event, request, settings) {
	$('#loading').show();
});

$(document).ajaxComplete(function (event, request, settings) {
	$('#loading').fadeOut();
});

$('a[data-tigra="modal"]').click(function () {
	$.get($(this).attr('href'), function (data) {
		$('#TigraModal').remove();
		var $div = $("<div>", { id: "foo", class: "a" });
		var $modal = '<div class="modal fade" id="TigraModal" tabindex="-1" role="dialog" aria-label="Meu Perfil" aria-hidden="true"><div class="modal-dialog"><div class="modal-content">';
		$modal += data;
		$modal += '</div></div></div>';
		$('body').append($modal);
		Holder.run({ images: '.img-thumbnail' });
		$('#TigraModal').modal();

		var $foc = $('.modal-body :input[autofocus="autofocus"]');
		if ($foc.length != 0) {
			setTimeout(function () { $foc[0].focus() }, 500);
		}
	});
	return false;
});