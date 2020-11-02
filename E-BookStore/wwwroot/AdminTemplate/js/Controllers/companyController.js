
var CompanyController = function (companyService) {

    var initFunc = function () {
        
        companyService.init();
    };

    var popupUpsertFormFunc = function () {
        //For Insert Button
        $(".js-add-company").on("click", (e) => {
            companyService.popupCreateForm();
            e.preventDefault();
        });
        //For Edit Button
        $(document).on("click", ".js-edit-company", (e) => {
            var button = $(e.target);
            companyService.popupEditForm(button);
            e.preventDefault();
        });
    }

    var submitForm = function () {
        $(document).on("click", ".btn-submit-company", function (e) {

            var formData = $("#upsert-form");

            companyService.submitFormServ(formData);

            console.log(formData)
        })
    }


    var deleteCompany = function () {
        $(document).on("click", ".js-del-company", (e) => {
            
            var companyId = $(e.target).data("id");
            console.log($(e.target))

            console.log(companyId)
            companyService.deleteCompanyServ(companyId);
            e.preventDefault();
        });
    }
    return {
        InitFunc: initFunc,
        PopupUpsertFormFunc: popupUpsertFormFunc,
        SubmitFormFunc: submitForm,
        DeleteCompany:deleteCompany
    }

}(CompanyService)