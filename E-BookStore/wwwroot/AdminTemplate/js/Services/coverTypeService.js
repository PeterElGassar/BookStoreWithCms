var CoverTypeService = function (notificationsService) {
    let coverTypeTable;
    var init = function () {
        coverTypeTable = $("#coverType-table").DataTable({
            ajax: {
                url: '/admin/CoverType/GetAllCategories',
                type: 'GET',
                dataType: 'json'
            },
            columns: [
                { data: 'name', width: '40%' },
                { data: 'slug', width: '40%' },
                {
                    data: 'id',
                    render: function (data) {
                        return `
                                 <a href="#"  class="btn btn-info js-edit-coverType" data-id="${data}">
                                     <i data-id="${data}" class="fas fa-edit fa-lg"></i>
                                 </a>
                                 <a href="#" class="btn btn-danger js-del-coverType" data-id="${data}">
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
        url = `/Admin/CoverType/Upsert/`
        //Call By Ajax
        $("#popup .modal-body").load(url, () => {
            $(".modal-title").empty().text("Add New Cover Type");
            $("#popup").modal("show");
        });
    }

    var popupEditForm = function (button) {

        if (button.data("id")) {

            var url = `/Admin/CoverType/Upsert/${button.attr("data-id")}`
            //Call By Ajax
            $("#popup .modal-body").load(url, () => {
                $(".modal-title").empty().text("Edit CoverType");
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
                    coverTypeTable.ajax.reload();
                } else {
                    if ("isExist" in data) {
                        popupWarningMessage(data.message);
                    }
                    else
                        notificationsService.errorNotify(data.message);

                }

            });
    }

    var deleteCoverType = function (coverTypeId) {
        swal({
            title: "Are you sure you want delete ?",
            text: "You will't be able to restore the  data",
            icon: "warning",
            buttons: true,
            dangerMode: true

        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: `/admin/CoverType/DeleteCoverType/${coverTypeId}`,
                    type: "DELETE",
                    success: (data) => {
                        if (data.status) {

                            notificationsService.successNotify(data.message);

                        } else {
                            notificationsService.errorNotify(data.message);
                        }
                        coverTypeTable.ajax.reload();
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
        deleteCoverTypeServ: deleteCoverType
    }

}(NotificationsService)
