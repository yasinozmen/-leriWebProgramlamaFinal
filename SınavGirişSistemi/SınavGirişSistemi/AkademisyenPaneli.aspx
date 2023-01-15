<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AkademisyenPaneli.aspx.cs" Inherits="SınavGirişSistemi.AkademisyenPaneli" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Akademisyen Paneli</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />

            <asp:Button ID="liste_yukle" Style="margin-left: 30px;" class="btn btn-success" runat="server" Text="Öğrenci Listesi Yükle" OnClick="liste_yukle_Click" OnClientClick="return CheckFileExistence()" />
            <asp:Button ID="rastgele_dagit" Style="margin-left: 30px;" class="btn btn-info" runat="server" Text="Rastgele Dağıt ve Kaydet" OnClick="rastgele_dagit_Click" />
            <br />
            <br />
            <asp:FileUpload ID="FileUpload1" Style="margin-left: 30px;" runat="server" Width="250px" EnableTheming="True" /><br />
            <asp:Label Style="margin-left: 30px;" Text="Sınıf Mevcutu:" runat="server" />
            <asp:Label ID="sınıfmevcudu" Text="0" runat="server" />

            <br />
            <div>
                <asp:GridView ID="GridView1" Style="margin-left: 30px" Width="500px" runat="server" Height="300px" HorizontalAlign="Left"></asp:GridView>
                <asp:CheckBoxList ID="liste" Style="margin-left: 30px;" runat="server" Width="797px" AutoPostBack="True" BorderStyle="Solid" Height="514px" RepeatLayout="Flow" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="13pt" CssClass="overflow-scroll" OnSelectedIndexChanged="liste_SelectedIndexChanged">
                </asp:CheckBoxList>
                <br />
                <asp:Label Style="margin-left: 30px; color: red;" Text="Seçilen Kontenjan:" runat="server" />
                <asp:Label ID="kontenjan" Style="color: red;" Text="0" runat="server" />
            </div>
            <br />
        </div>
    </form>
</body>
</html>
<script type="text/javascript">

    function CheckFileExistence() {

        var filePath = document.getElementById('<%= this.FileUpload1.ClientID %>').value;

        if (filePath.length < 1) {

            alert("Dosya seçiniz..."); return false;

        }

        var validExtensions = new Array();

        var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

        validExtensions[0] = 'xls';

        for (var i = 0; i < validExtensions.length; i++) {

            if (ext == validExtensions[i]) return true;

        }

        alert(ext.toUpperCase() + ' uzantılı dosya yükleyemezsiniz!');

        return false;

    }

</script>
