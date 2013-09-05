$().ready(function () {
    $('.word-clamp').each(function (key, val) {
        var $words = $(this).html().split(' ', $(this).data('word-clamp'));
        var $text = $words.join(' ');
        var $dot = $text.lastIndexOf('.');

        if ($words.length >= $(this).data('word-clamp')) {
            if ($dot == -1 || ($text.length - $dot) > 3) {
                $text += "...";
            }
        }

        $(this).html($text);
    });
});
