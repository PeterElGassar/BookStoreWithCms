var UserController = function (userService) {

    var initFunc = function () {
        userService.init();
    };

    var popupUpsertFormFunc = function () {
        //For Insert Button
        $(".js-add-user").on("click", (e) => {
            userService.popupCreateForm();
            e.preventDefault();
        });
        //For Edit Button
        $(document).on("click", ".js-edit-user", (e) => {
            var button = $(e.target);
            userService.popupEditForm(button);
            e.preventDefault();
        });
    }

    var submitForm = function () {
        $(document).on("click", ".btn-submit", function (e) {

            var formData = $("#upsert-form");

            userService.submitFormServ(formData);

            console.log(formData)
        })
    }

    var deleteCategory = function () {
        $(document).on("click", ".js-del-user", (e) => {
            
            var userId = $(e.target).data("id");
            userService.deleteUserServ(userId);
            e.preventDefault();

        });
    }

    var lockAndUnockUser = function () {
        $(document).on("change", ".js-lock", (e) => {

            var check = $(e.target);
            if (check.is(":checked")) {
                userService.lockoutUserServ(check.attr("id"));
                check.siblings("label").empty().text("Locked");
            } else {
                userService.lockoutUserServ(check.attr("id"));
                check.siblings("label").empty().text("Unlocked");
            }

        });
    }

    return {
        InitFunc: initFunc,
        PopupUpsertFormFunc: popupUpsertFormFunc,
        SubmitFormFunc: submitForm,
        DeleteCategory: deleteCategory,
        lockAndUnockUser: lockAndUnockUser
    }

}(UserService)