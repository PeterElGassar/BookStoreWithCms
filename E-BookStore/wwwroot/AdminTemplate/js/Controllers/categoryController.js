
var CoverTypeController = function (categoryService) {

    var initFunc = function () {
        categoryService.init();
    };

    var popupUpsertFormFunc = function () {
        //For Insert Button
        $(".js-add-category").on("click", (e) => {
            categoryService.popupCreateForm();
            e.preventDefault();
        });
        //For Edit Button
        $(document).on("click", ".js-edit-category", (e) => {
            var button = $(e.target);
            categoryService.popupEditForm(button);
            e.preventDefault();
        });
    }

    var submitForm = function () {
        $(document).on("click", ".btn-submit-category", function (e) {

            var formData = $("#upsert-form");

            categoryService.submitFormServ(formData);

            console.log(formData)
        })
    }


    var deleteCategory = function () {
        $(document).on("click", ".js-del-category", (e) => {
            debugger
            var categoryId = $(e.target).data("id");
            console.log($(e.target))

            console.log(categoryId)
            categoryService.deleteCategoryServ(categoryId);
            e.preventDefault();
        });
    }
    return {
        InitFunc: initFunc,
        PopupUpsertFormFunc: popupUpsertFormFunc,
        SubmitFormFunc: submitForm,
        DeleteCategory:deleteCategory
    }

}(CategoryService)