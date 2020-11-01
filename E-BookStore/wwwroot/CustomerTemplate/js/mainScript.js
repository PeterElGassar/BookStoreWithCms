$(document).ready(function () {
    lightbox.option({
        resizeDuration: 400,
        wrapAround: true,
    });

    $(window).scroll(() => {
        let poistion = $(this).scrollTop();
        if (poistion >= 340) {
            $(".gallery").addClass("change");
        } else {
            $(".gallery").removeClass("change");
        }

        // Writers Section 
        $('.writers-accordion').unbind().on("click",function (e) {
            debugger;
            if (e.target.id === 'william-btn') {
                $('#book-1').attr('src', "/CustomerTemplate/img/writers/William_Shakespeare-book1.jpg");
                $('#book-2').attr('src', "/CustomerTemplate/img/writers/William_Shakespeare-book2.jpg");
            } else if (e.target.id === 'jane-btn') {
                $('#book-1').attr('src', "/CustomerTemplate/img/writers/Jane_Austen-book1.jpg");
                $('#book-2').attr('src', "/CustomerTemplate/img/writers/Jane_Austen-book2.jpg");
            } else if (e.target.id === 'victor-btn') {
                $('#book-1').attr('src', "/CustomerTemplate/img/writers/Victor_Hugo-book1.jpg");
                $('#book-2').attr('src', "/CustomerTemplate/img/writers/Victor_Hugo-book2.jpg");
            } else if (e.target.id === 'Charles-btn') {
                $('#book-1').attr('src', "/CustomerTemplate/img/writers/Charles_Dickens-book1.jpg");
                $('#book-2').attr('src', "/CustomerTemplate/img/writers/Charles_Dickens-book2.jpg");
            }
        })

    });


    // hamburger btn

    $(".hamburger-menu").on('click', function () {

        $(".navigation").toggleClass("change");

    });





});


$(document).on("click", ".test", () => {
    swal("Hello world!");
    toastr["success"]("My name is Inigo Montoya. You killed my father. Prepare to die!")
});