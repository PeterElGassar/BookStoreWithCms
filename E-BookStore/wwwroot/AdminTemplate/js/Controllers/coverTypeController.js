
var CategoryController = function (coverTypeService) {

    var initFunc = function () {
        coverTypeService.init();
    };


    var popupUpsertFormFunc = function () {
        //For Insert Button
        $(".js-add-coverType").on("click", (e) => {
            coverTypeService.popupCreateForm();
            e.preventDefault();
        });
        //For Edit Button
        $(document).on("click", ".js-edit-coverType", (e) => {
            var button = $(e.target);
            coverTypeService.popupEditForm(button);
            e.preventDefault();
        });
    }

    var submitForm = function () {
        $(document).on("click", ".btn-submit", function (e) {

            var formData = $("#upsert-form");

            coverTypeService.submitFormServ(formData);

            console.log(formData)
        })
    }


    var deleteCategory = function () {
        $(document).on("click", ".js-del-coverType", (e) => {
            debugger
            var categoryId = $(e.target).data("id");
            console.log($(e.target))

            console.log(categoryId)
            coverTypeService.deleteCoverTypeServ(categoryId);
            e.preventDefault();
        });
    }
    return {
        InitFunc: initFunc,
        PopupUpsertFormFunc: popupUpsertFormFunc,
        SubmitFormFunc: submitForm,
        DeleteCategory:deleteCategory
    }

}(CoverTypeService)