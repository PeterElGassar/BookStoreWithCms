
var BookController = function (bookService) {

    var initFunc = function () {
        bookService.init();
    };


    var popupUpsertFormFunc = function () {
        //For Insert Button
        $(".js-add-coverType").on("click", (e) => {
            bookService.popupCreateForm();
            e.preventDefault();
        });
        //For Edit Button
        $(document).on("click", ".js-edit-coverType", (e) => {
            var button = $(e.target);
            bookService.popupEditForm(button);
            e.preventDefault();
        });
    }

    var submitForm = function () {
        $(document).on("click", ".btn-submit", function (e) {

            var formData = $("#upsert-form");

            bookService.submitFormServ(formData);

            console.log(formData)
        })
    }


    var deleteCategory = function () {
        $(document).on("click", ".js-del-book", (e) => {
            
            var bookId = $(e.target).data("id");
            bookService.deleteCoverTypeServ(bookId);
            e.preventDefault();

        });
    }
    return {
        InitFunc: initFunc,
        PopupUpsertFormFunc: popupUpsertFormFunc,
        SubmitFormFunc: submitForm,
        DeleteCategory:deleteCategory
    }

}(BookService)