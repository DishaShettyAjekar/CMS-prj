$(document).ready(function(){
var modal= $("#myModal");
    $(".datepicker").datepicker({
        format: 'dd-mm-yyyy'
    });

    $(document).on("change","#upload",function(){
       var File=this.files;
        if(File && File[0]){
            var reader=new FileReader();
            reader.readAsDataURL(File[0]);
            
            reader.onload=function(x){
                var image=new Image;
                image.src=x.target;

                $("#imgNew").attr('src',x.target.result);
                $("#UserImage").value(x.target.result);
            }
        }   
    });

    $("#UserList").dataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
                ajax: {
                            "url":"/Dept/List",
                            "type":"GET",
                            "datatype":"json"
                        },
            columns: [
                            {data:"UserId"},
                            {data:"UserName"},
                            {data:"Email"},
                            {data:"Gender"},
                            {data:"imagePath"},
                            {
                            data: null,
                            render: function(data) {
                                return '<div><img src="' + data.imagePath + '" height="80" width="80"/></div>';
                                },
                            name: 'UserId'
                            },
                            {data:"Role"},
                            {
                                data: null,
                                render: function(data) {
                                    return '<div data_id="' + data.UserId + '">' +
                                        '<a href="javascript:void(0);" class="edit" title="Edit">Edit</a> ' +
                                        ' <a href="javascript:void(0);" class="delete" title="Delete">Delete</a>' +
                                        '</div>';
                            },
                            name: 'id'
                }
                        ],
                });

                $(document).on("click", '.edit', function() {
                        window.data_id = $(this).parent().attr('data_id');
modal.show();
        
                });
                $(document).on("click", '.delete', function() {
                        window.data_id = $(this).parent().attr('data_id');

        modal.show();
                });
            
});