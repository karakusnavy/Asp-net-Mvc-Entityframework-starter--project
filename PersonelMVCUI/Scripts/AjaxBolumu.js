//DEPARTMAN SİLME / DEPARTMAN SİLME / DEPARTMAN SİLME / DEPARTMAN SİLME / DEPARTMAN SİLME / DEPARTMAN SİLME
$(function () {
    $("#tblDepartmanlar").DataTable();
    $("#tblDepartmanlar").on("click", ".btnDepartmanSil", function () {
        var btn = $(this);
        bootbox.confirm("Silmek İstediğinize Emin misiniz ? ", function (result) {
            if (result) {
                var id = btn.data("id");
                $.ajax({
                    type: "GET",
                    url: "Departman/Sil/" + id,
                    success: function () {
                        btn.parent().parent().remove();
                        console.log("Departman Silinmiştir.");
                    }
                });
            }           
        });
    });
});
//-------------------------------------------------------------------------------------------------------------
$(function () {
    $("#tblPersonel").DataTable();
    $("#tblPersonel").on("click", ".btnPersonelSil", function () {
        var btn = $(this);
        bootbox.confirm("Silmek İstediğinize Emin misiniz ? ", function (result) {
            if (result) {
                var id = btn.data("id");
                $.ajax({
                    type: "GET",
                    url: "Personel/Sil/" + id,
                    success: function () {
                        btn.parent().parent().remove();
                        console.log("Personel Silinmiştir.");
                    }
                });
            }
        });
    });
});
//-----------------------------------------------------------------------------------------------------------------
