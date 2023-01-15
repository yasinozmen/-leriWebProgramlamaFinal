<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GirişSayfası.aspx.cs" Inherits="SınavGirişSistemi.GirişSayfası" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body style="background-color:white;">
    <form id="form1" runat="server">
        <div>
            <div class="text-center text-lg-start mt-4 pt-2">
                <h1 style="margin-left: 34%; margin-top: 15%;">Sınav Giriş Sistemine Hoşgeldiniz</h1>
                <asp:Button ID="ÖğrenciGirişYap" Text="Öğrenci Giriş Yap" class="btn btn-primary btn-lg"
                    Style="margin-left: 36%; margin-top: 3%; height: 100px; width: 250px; float: left;" runat="server" OnClick="ÖğrenciGirişYap_Click" />

                <asp:Button ID="AkademisyenButonu" Text="Akademisyen Girişi" class="btn btn-danger btn-lg"
                    Style="margin-left: 36%; margin-top: 3%; height: 100px; width: 250px; margin-left: 10px; float: left;" runat="server" OnClick="AkademisyenButonu_Click" />
            </div>
        </div>
    </form>

</body>
</html>
