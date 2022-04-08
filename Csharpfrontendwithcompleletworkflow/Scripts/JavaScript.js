var cacheMemory;
$(document).ready(function () {
    $('#but_upload').click(function () {
        $("#upl").empty();
        $("#upl").append('<img src="https://upload.wikimedia.org/wikipedia/commons/b/b1/Loading_icon.gif"style="width: 50px; height: 20px; color:green" />', ' File is Uploading...');
        var fd = new FormData();
        var option = $("input[type='radio']:checked").val();
        var files = $('#file')[0].files[0];
        fd.append('file', files)
        fd.append('options', option);
        


        $.ajax({

            url: 'http://localhost:49054/api/file/savefile',
            type: 'post',
            data: fd,
            async: true,
            contentType: false,
            processData: false,
            success: function (respose) {
                alert(respose);
                $("#upl").empty();
                $("#upl").append('<img src="https://static9.depositphotos.com/1431107/1143/i/950/depositphotos_11437164-stock-photo-green-tick.jpg"alt=""style="width: 30px; height: 20px;" />', ' File uploaded Successfully');
                $("#but_upload").attr("disabled", true);
                dipResponse();
            },
            error: function (respose) {
                $("#upl").empty();
                $("#upl").append('<img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQWFntcWBEUs8GXR2NGvYddqKxoT-H5n4PBd_Qm_iaIPsrOEWIKaWYL7F3ft92M6ZTWd6Q&usqp=CAU" alt=""style="width: 25px; height: 20px;" />', ' File uploaded failed');

            }



        });




    });
});
function dipResponse() {

    $.ajax({

        url: 'http://localhost:49054/api/file/getdipresponse',
        type: 'get',
        async: true,
        success: function (response) {


            cacheMemory = response;
            if (cacheMemory == true) {

                alert(cacheMemory);
                $("#but_upload").attr("disabled", false);

            }

            if (cacheMemory == false) {
                dipResponse();
            }




        }

    });
}



