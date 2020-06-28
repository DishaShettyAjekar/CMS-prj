$(document).ready(function(){
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
});