var POST = POST || {

    init: function () {
        POST.loadPosts();
        POST.searchListener();
        POST.openModalAddPostListener();
        POST.closeModalAddPostListener();
        POST.closeModalDetailsPostListener();
        POST.closeModalEditPostListener();
        POST.addPostListener();
        POST.EditPostListener();
        POST.uploadImgListener();
        
    },
    openModalAddPostListener: function () {
        $("#addPost").click(function () {
            $("#modalPostAgregar").modal('show');

        });
    },
    openModalEditPostListener: function () {
        $(".modalEdit").click(function () {

            let ob = {
                ID: $(this).attr("data-id")
            }

            let data = ob

            $.ajax({
                type: "GET",
                data: data,
                url: "/Post/ObtenerPost",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#contMod").val(response.CONTENT)
                    $("#titleMod").val(response.TITLE)
                    $("#selectCatMod").val(response.CATEGORIA)
                    if (response.IMAGEN != null) {
                        $(".postImgContainer.Mod").html('<img src="resources/imagesPosts/' + response.IMAGEN + '" alt="' + response.IMAGEN + '" />');
                    } else {
                        $(".postImgContainer.Mod").html('');
                    }
                    $(".btnModificar").attr("data-id", response.ID)

                    $("#modalPostModify").modal('show');
                }
            });
        });
    },
    openModalDetailsPostListener: function () {
        $(".modalDetails").click(function () {
            let ob = {
                ID: $(this).attr("data-id")
            }
            let data = ob;
            $.ajax({
                type: "GET",
                data: data,
                url: "/Post/ObtenerPost",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#postTitle").text(response.TITLE)
                    $("#postCat").text(response.CATEGORIA)
                    $("#postCont").text(response.CONTENT)
                    $("#postFecha").text(response.FECHA_CREACION)
                    if (response.IMAGEN != null) {
                        $(".postImgContainer").html('<img src="resources/imagesPosts/' + response.IMAGEN + '" alt="' + response.IMAGEN + '" />');
                    } else {
                        $(".postImgContainer").html('');
                    }
                }
            });
            $("#postDetailsModal").modal('show');

        });
    },

    closeModalAddPostListener: function () {
        $(".closeModalAgregar").click(function () {
            $("#modalPostAgregar").modal('hide');
        });
    },
    closeModalEditPostListener: function () {
        $(".closeModalEdit").click(function () {
            $("#modalPostModify").modal('hide');
        });
    },
    closeModalDetailsPostListener: function () {
        $(".closeModalDetails").click(function () {
            $("#postDetailsModal").modal('hide');
        });
    },



    removePostListener: function () {
        $(".delete").click(function () {
            Swal.fire({
                title: 'Estas seguro?',
                text: "No podras revertir los cambios!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#52c0f7',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Si, Eliminar!'
            }).then((result) => {
                if (result.isConfirmed) {

                    let data = {
                        ID: $(this).attr("data-id")
                    }

                    $.ajax({
                        type: "GET",
                        data: data,
                        url: "/Post/EliminarPost",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.status == true) {
                                Swal.fire(
                                    'Eliminado!',
                                    'El post fue eliminado con exito.',
                                    'success'
                                )
                                POST.drawPosts(response.Posts);

                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: response.name,
                                    text: response.desc
                                })
                            }
                        }
                    });
                }
            })
        });
    },
    addPostListener: function () {
        $(".btnAgregar").click(function () {

            if (POST.validarDato($("#title")) && POST.validarDato($("#cont"))) {
                let ob = {
                    CONTENT: $("#cont").val(),
                    TITLE: $("#title").val(),
                    CATEGORIA: $("#selectCat").val(),
                    IMAGEN: $("#fileUpload")[0].files[0] != undefined ? $("#fileUpload")[0].files[0].name : null
                }
                let data = ob
                console.log("a")
                $.ajax({
                    type: "POST",
                    data: data,
                    url: "/Post/CrearPost",
                    dataType: "json",
                    success: function (response) {


                        if (response.status == true) {
                            Swal.fire(
                                'Creado!',
                                'El post fue creado con exito.',
                                'success'
                            )
                            POST.drawPosts(response.Posts);

                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: response.name,
                                text: response.desc
                            })
                        }

                    }
                });
            }
        });
    },
    EditPostListener: function () {
        $(".btnModificar").click(function () {
            if (POST.validarDato($("#titleMod")) && POST.validarDato($("#contMod"))) {
                Swal.fire({
                    title: 'Estas seguro?',
                    text: "No podras revertir los cambios!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#52c0f7',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si, Modificar!'
                }).then((result) => {
                    if (result.isConfirmed) {

                        let data = {
                            CONTENT: $("#contMod").val(),
                            TITLE: $("#titleMod").val(),
                            CATEGORIA: $("#selectCatMod").val(),
                            IMAGEN: $("#fileUploadMod")[0].files[0] != undefined ? $("#fileUploadMod")[0].files[0].name : null,
                            ID: $(".btnModificar").attr("data-id")
                        }
                        $.ajax({
                            type: "POST",
                            data: data,
                            url: "/Post/ModificarPost",
                            dataType: "json",
                            success: function (response) {

                                if (response.status == true) {
                                    Swal.fire(
                                        'Modificado!',
                                        'El post fue modificado exitosamente.',
                                        'success'
                                    )
                                    POST.drawPosts(response.Posts);
                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: response.name,
                                        text: response.desc
                                    })
                                }
                            }
                        });

                    }
                })
            }
        });
    },

    searchListener: function () {
        $('#searchInput').keypress(function (e) {
            var key = e.which;
            if (key == 13)  // the enter key code
            {
                let data = {
                    palabra : $('#searchInput').val()
                }
                $.ajax({
                    type: "GET",
                    url: "/Post/FiltrarPosts",
                    data: data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        POST.drawPosts(response.Posts);
                    }
                });
            }
        }); 
    },
    uploadImgListener: function () {
        $("#fileUpload, #fileUploadMod").change(function () {
            if ($(this)[0].files[0] != undefined) {
                var formData = new FormData();
                var file = $(this)[0].files[0];
                formData.append("fileUpload", file);

                console.log(file.name)

                console.log()
                $.ajax({
                    type: "POST",
                    data: formData,
                    timeout: 1000,
                    contentType: false,
                    processData: false,
                    url: "/Post/UploadImg",
                    success: function (response) {
                        //imagen subida con exito
                    }
                });
            }
        });
    },
    PostListeners: function () {
        POST.removePostListener();
        POST.openModalEditPostListener();
        POST.openModalDetailsPostListener();
    },

    loadPosts: function () {
        $.ajax({
            type: "GET",
            url: "/Post/ListarPosts",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                POST.drawPosts(response.Posts);
            }
        });
    },
    drawPosts: function (data) {
        let container = $(".content__posts");
        container.html("");
        if (data.length != 0) {
            for (item of data) {
                container.append(`
                    <div class="post-container">
                        <div class="post-content">
                            <div class="post-content__date">
                                <div>
                                    <p>`+ item.CATEGORIA + `</p>
                                </div>
                                <p>`+ item.FECHA_CREACION + `</p>
                            </div>
                            <div class="post-content__title">
                                <p data-id="`+ item.ID + `" class="details">` + item.TITLE + `</p>
                            </div>
                            <div class="post-content__footer">
                                <p></p>
                                <div class="post-content__buttons">
                                    <span>
                                        <i data-id="`+ item.ID + `" class="flaticon-link modalDetails"></i>
                                    </span>
                                    <span>
                                        <i data-id="`+ item.ID + `" class="flaticon-edit modalEdit"></i>
                                    </span>
                                    <span>
                                        <i data-id="`+ item.ID + `" class="flaticon-delete delete"></i>
                                    </span>
                                </div>
                            </div>
                    
                        </div>
                    </div>
                   `);
            }
            POST.PostListeners();
        } else {
            container.append(`
                      <div class="post-error">
                          <p><i class="flaticon-warning"></i> No se han encontrado posts...</p>
                      </div>
                   `);
        }
    },
    validarDato: function (data) {
        if (data.val() != "") {
            data.next().next().css('opacity', '0')
            return true;
        } else {
            data.next().next().css('opacity', '1')
            return false
        }
    },

}