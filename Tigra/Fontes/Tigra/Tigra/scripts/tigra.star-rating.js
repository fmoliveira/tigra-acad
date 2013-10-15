function setRating(ctrl, rating) {
    var stars = $(ctrl).parent().find('[data-stars!=""]');

    for (i = 0; i < stars.length; i++) {
        if ($(stars[i]).data('star') <= rating) {
            $(stars[i]).removeClass('glyphicon-star-empty').addClass('glyphicon-star');
        } else {
            $(stars[i]).removeClass('glyphicon-star').addClass('glyphicon-star-empty');
        }
    }
}

$().ready(function () {

    $('[data-role="star-rating"] > [data-star!=""]').mouseout(function () {
        setRating($(this), $(this).parent().find('input').val());
    });

    $('[data-role="star-rating"] > [data-star!=""]').mouseover(function () {
        setRating($(this), $(this).data('star'));
    });

    $('[data-role="star-rating"] > [data-star!=""]').click(function () {
        $(this).parent().find('input').val($(this).data('star'));
        setRating($(this), $(this).parent().find('input').val());
    });

});
