//< !--Responsive -->

$(function () {
    // jQuery functions go here .
    $('#toggle').click(function () {
        $('.toggle_menu').slideToggle('slow');
        $('.btn_open').slideToggle('slow');
        $('.btn_close').slideToggle('slow');
        return false;
    });

    // jQuery functions go here.
    $('#toggle_close').click(function () {
        $('.toggle_menu').slideToggle('slow');

        return false;
    });

    // jQuery functions go here.
    $('#all').click(function () {
        $('.all').show();

        return false;
    });


    // jQuery functions go here.
    $('#ended').click(function () {
        $('.all').hide();
        $('.ended').show();

        return false;
    });

    // jQuery functions go here.
    $('#late').click(function () {
        $('.all').hide();
        $('.late').show();

        return false;
    });

    // jQuery functions go here.
    $('#now').click(function () {
        $('.all').hide();
        $('.now').show();

        return false;
    });

    // jQuery functions go here.
    $('#wait').click(function () {
        $('.all').hide();
        $('.wait').show();

        return false;
    });

    $(".ser_btn").click(function () {
        $("#id01").slideDown("slow"); 
    });

    $("#ser_close").click(function () {
        $("#id01").slideUp("slow");
    });

    $(".ser_btn2").click(function () {
        $("#id02").slideDown("slow");
    });

    $("#ser_close2").click(function () {
        $("#id02").slideUp("slow");
    });


    $(".li").click(function () {
        $("#myModal").slideDown("slow");
    });

    $("#modal_close").click(function () {
        $("#myModal").slideUp("slow");
    });
});
//<!-- / Responsive -->




