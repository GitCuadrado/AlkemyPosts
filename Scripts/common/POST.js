var POST = POST || {

    init: function () {
        POST.loadPosts();
        POST.searchListener();
        POST.openModalAddPostListener();
        POST.addPostListener();
        POST.EditPostListener();
        //POST.uploadImg();
    },
    openModalAddPostListener: function () {
        $("#addPost").click(function () {
            $("#modalPostAgregar").modal('show');

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
    PostListeners: function () {
        POST.buttonRemoveListener();
        POST.openModalEditPostListener();
        POST.buttonDetailsListener();
    },
    //uploadImg: function () {
    //    $('#upload').submit(function (e) {
    //        e.preventDefault(); // stop the standard form submission
    //        let data = new FormData(this);
    //        console.log("a")
    //        $.ajax({
    //            url: this.action,
    //            type: this.method,
    //            data: data,
    //            async: true,
    //            cache: false,
    //            timeout:10000,
    //            contentType: false,
    //            processData: false,
    //            success: function (data) {
    //                console.log(data)
    //            },
    //            error: function (xhr, error, status) {
    //            }
    //        });
    //    });
    //},
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
        //if (data.lenght == 0) {
            for (item of data) {
                $("")
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
                                  <p class="details">`+ item.TITLE + `</p>
                              </div>
                              <div class="post-content__buttons">
                                 <span>
                                     <i data-id="`+ item.ID + `" class="flaticon-link details"></i>
                                 </span>
                                 <span>
                                     <i data-id="`+ item.ID + `" class="flaticon-edit edit"></i>
                                 </span>
                                 <span>
                                     <i data-id="`+ item.ID + `" class="flaticon-delete delete"></i>
                                 </span>
                              </div>
                          </div>
                      </div>
                   `);
                if (container.html == "") {
                    console.log("nazi")
                }
            }
            POST.PostListeners();
        //} else {
        //    container.append(`
        //              <div class="post-error">
        //                  <p><i class="flaticon-warning"></i> No se han encontrado posts...</p>
        //              </div>
        //           `);
        //}
  
        
    },
    buttonRemoveListener: function () {
        $(".delete").click(function () {
            let data = {
                ID : $(this).attr("data-id")
            }

              
            $.ajax({
                type: "GET",
                data:data,
                url: "/Post/EliminarPost",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    POST.drawPosts(response.Posts);

                }
            });

        });
    },
    openModalEditPostListener: function () {
        $(".edit").click(function () {

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
                    $(".btnModificar").attr("data-id", response.ID)
                    $("#modalPostModify").modal('show');
                }
            });
        });
    },
    addPostListener: function () {
        $(".btnAgregar").click(function () {

            let ob = {
                CONTENT: $("#cont").val(),
                TITLE: $("#title").val(),
                CATEGORIA: $("#selectCat").val()            }
            let data = ob


            $.ajax({
                type: "POST",
                data: data,
                url: "/Post/CrearPost",
                dataType: "json",
                success: function (response) {
                    POST.drawPosts(response.Posts);

                }
            });

        });
    },
    buttonDetailsListener: function () {
        $(".details").click(function () {
            let ob = {
                ID:$(this).attr("data-id")
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
                }
            });
            $("#postDetailsModal").modal('show');

        });
    },
    EditPostListener: function () {
        $(".btnModificar").click(function () {
            let ob = {
                CONTENT: $("#contMod").val(),
                TITLE: $("#titleMod").val(),
                CATEGORIA: $("#selectCatMod").val(),
                ID: $(".btnModificar").attr("data-id")
            }
            let data = ob


            $.ajax({
                type: "POST",
                data: data,
                url: "/Post/ModificarPost",
                dataType: "json",
                success: function (response) {
                    POST.drawPosts(response.Posts);

                }
            });
        });
    }


}