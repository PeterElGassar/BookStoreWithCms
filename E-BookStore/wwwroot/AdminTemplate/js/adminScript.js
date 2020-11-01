//Category
CategoryController.InitFunc();
CategoryController.PopupUpsertFormFunc();
CategoryController.SubmitFormFunc();
CategoryController.DeleteCategory();
//CoverType Main Method
CoverTypeController.InitFunc();
CoverTypeController.PopupUpsertFormFunc();
CoverTypeController.SubmitFormFunc();
CoverTypeController.DeleteCategory();
//Book Main Method
BookController.InitFunc();
BookController.DeleteCategory();
//Company
CompanyController.InitFunc();
CompanyController.PopupUpsertFormFunc();
CompanyController.SubmitFormFunc();
CompanyController.DeleteCompany();










//Preview Selected Image
function readUrl(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("img.js-img-book")
                .attr("src", e.target.result)
                .width(226)
                .height(363);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(document).on("change", "#upload-box", function () {
    readUrl(this);
});
