var CompanyService = function (notificationsService) {
    let companyTable;

    var init = function () {
        companyTable = $("#company-table").DataTable({
            ajax: {
                url: '/admin/Company/GetAllCompanies',
                type: 'GET',
                dataType: 'json'
            },
            columns: [
                { data: 'name', width: '20%' },
                { data: 'streetAddress', width: '15%' },

                { data: 'city', width: '15%' },
                { data: 'postalCode', width: '15%' },
                { data: 'state', width: '15%' },
                { data: 'phoneNumber', width: '20%' },
                {
                    data: 'isVerifiedCompany',
                    render: function (data) {
                        if (data) {
                            return `<input type="checkbox" disabled checked/>`;
                        } else {
                            return `<input type="checkbox" disabled />`;

                        }
                    }
                    , width: '15%'
                },

                {
                    data: 'id',
                    render: function (data) {
                        return `
                                 <a href="#"  class="btn btn-info js-edit-company" data-id="${data}">
                                     <i data-id="${data}" class="fas fa-edit fa-lg"></i>
                                 </a>
                                 <a href="#" class="btn btn-danger js-del-company" data-id="${data}">
                                     <i data-id="${data}" class="fas fa-trash fa-lg"></i>
                                 </a>
                                `

                    },
                    width: "20%"

                }

            ]
        });
    }

    var popupCreateForm = function () {
        url = `/Admin/Company/Upsert/`
        //Call By Ajax
        $("#popup .modal-body").load(url, () => {
            $(".modal-title").empty().text("Add New Company");
            $("#popup").modal("show");
        });
    }

    var popupEditForm = function (button) {

        if (button.data("id")) {

            var url = `/Admin/Company/Upsert/${button.attr("data-id")}`
            //Call By Ajax
            $("#popup .modal-body").load(url, () => {
                $(".modal-title").empty().text("Edit Company");
                $("#popup").modal("show");
            });

        } else {
            alert("Not has id Data attribute");
        }
    }

    var submitFormServ = function (form) {

        $.post(form.attr("action"), form.serialize())
            .done(function (data) {

                if (data.status) {
                    $("#popup").modal("hide");
                    $("#popup .modal-body").empty();
                    notificationsService.successNotify(data.message);
                    companyTable.ajax.reload();

                } else {
                    if ("isExist" in data) {
                        popupWarningMessage(data.message);
                    }
                    else
                        notificationsService.errorNotify(data.message);
                }

            });
    }

    var deleteCompany = function (companyId) {
        swal({
            title: "Are you sure you want delete ?",
            text: "You will't be able to restore the  data",
            icon: "warning",
            buttons: true,
            dangerMode: true

        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: `/admin/company/DeleteCompany/${companyId}`,
                    type: "DELETE",
                    success: (data) => {
                        if (data.status) {

                            notificationsService.successNotify(data.message);

                        } else {
                            notificationsService.errorNotify(data.message);
                        }
                        companyTable.ajax.reload();
                    }
                })
            }

        });
    }

    //===============Private Methods===================//
    function popupWarningMessage(message) {
        
        $(".ajax-ms").toggleClass("show-ajax-ms");
        $(".ajax-ms span").text(message);

        setTimeout(() => {
            $(".ajax-ms").toggleClass("show-ajax-ms");
            $(".ajax-ms span");
        }, 2500);
    }

    return {
        init: init,
        popupEditForm: popupEditForm,
        popupCreateForm: popupCreateForm,
        submitFormServ: submitFormServ,
        deleteCompanyServ: deleteCompany
    }

}(NotificationsService)
