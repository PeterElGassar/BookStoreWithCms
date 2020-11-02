var UserService = function (notificationsService) {
    let coverTypeTable;
    var init = function () {
        coverTypeTable = $("#users-table").DataTable({
            ajax: {
                url: '/admin/User/GetAllUsers',
                type: 'GET',
                dataType: 'json'
            },
            columns: [
                { data: 'name', width: '15%' },
                { data: 'email', width: '15%' },
                { data: 'phoneNumber', width: '15%' },
                { data: 'company.name', width: '20%' },
                { data: 'role', width: '15%' },
                {
                    data: { id: 'id', lockoutEnd: 'lockoutEnd' },
                    render: function (data) {
                        let todayDate = new Date().getTime();
                        let leckout = new Date(data.lockoutEnd).getTime();

                        if (leckout > todayDate) {
                            //meaning User locked
                            return `
                                      <div class="custom-control custom-switch">
                                          <input type="checkbox" class="custom-control-input js-lock" id="${data.id}" "checked >
                                          <label class="custom-control-label d-inline-block" for="${data.id}" ">Locked</label>
                                       </div>                                                            
                                   `;
                        } else {
                            return `
                                       <div class="custom-control custom-switch">
                                          <input type="checkbox" class="custom-control-input js-lock" id="${data.id} ">
                                          <label class="custom-control-label d-inline-block" for="${data.id} ">Unlocked</label>
                                       </div>
                                    `;
                        }
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

    var deleteCoverType = function (bookId) {
        swal({
            title: "Are you sure you want delete ?",
            text: "You will't be able to restore the  data",
            icon: "warning",
            buttons: true,
            dangerMode: true

        }).then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: `/admin/book/DeleteUser/${bookId}`,
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



    var lockoutUser = function (userId) {
        if (userId !== null || userId !== undefined) {
            console.log(typeof userId);
            $.ajax({
                type: "POST",
                url: "/Admin/User/LockoutUser",
                data: JSON.stringify({ "id": userId }),
                contentType: "application/json",
                success: function (data) {
                    debugger
                    if (data.status) {
                        notificationsService.successNotify(data.message)
                    } else {
                        notificationsService.errorNotify(data.message)
                    }
                }
            });


            //$.post("/Admin/User/LockoutUser", { id: JSON.stringify(userId) }, function (data) {
            //    debugger
            //    if (data.status) {
            //        notificationsService.successNotify(data.message)
            //    } else {
            //        notificationsService.errorNotify(data.message)
            //    }
            //})
        }
    }

    var unLockoutUser = function (userId) {
        if (userId !== null || userId !== undefined) {
            $.ajax({
                type:"POST",
                url: "/Admin/User/UnLockoutUser",
                data: JSON.stringify(userId),
                dataType: "application/json",
                success: function (data) {
                    debugger
                    if (data.status) {
                        notificationsService.successNotify(data.message)
                    } else {
                        notificationsService.errorNotify(data.message)
                    }
                }
            });

            //$.post("/Admin/User/UnLockoutUser", { id: JSON.stringify(userId) }, function (data) {
            //    debugger
            //    if (data.status) {
            //        notificationsService.successNotify(data.message)
            //    } else {
            //        notificationsService.errorNotify(data.message)
            //    }
            //})
        }
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
        deleteCoverTypeServ: deleteCoverType,
        lockoutUserServ: lockoutUser,
        unLockoutUserServ: unLockoutUser
    }

}(NotificationsService)
