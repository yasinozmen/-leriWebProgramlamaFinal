<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ÖğrenciGiriş.aspx.cs" Inherits="SınavGirişSistemi.ÖğrenciGiriş" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Öğrenci Giriş</title>
    <style>
        .divider:after,
        .divider:before {
            content: "";
            flex: 1;
            height: 1px;
            background: #eee;
        }

        .h-custom {
            height: calc(100% - 73px);
        }

        @media (max-width: 450px) {
            .h-custom {
                height: 100%;
            }
        }
    </style>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <section class="vh-100">
        <div class="container-fluid h-custom">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-md-9 col-lg-6 col-xl-5">
                    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.webp" class="img-fluid" alt="Sample image">
                </div>
                <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
                    <form runat="server">
                        <div class="divider d-flex align-items-center my-4">
                            <h3 class="text-center fw-bold mx-3 mb-0">GİRİŞ PANELİ</h3>
                        </div> 

                        <!-- Öğrenci Numarası -->
                        <div class="form-outline mb-4">
                            <label class="form-label" for="form3Example3">Öğrenci Numarası</label>
                            <asp:TextBox ID="ÖgrNo" class="form-control form-control-lg" TextMode="Phone" placeholder="Geçerli bir öğrenci numarası girin" runat="server" required="" />
                        </div>

                        <div class="text-center text-lg-start mt-4 pt-2">
                            <asp:Button ID="belge_sorgula" Text="Sınav Giriş Belgesi Sorgula" class="btn btn-primary btn-lg"
                                Style="padding-left: 2.5rem; padding-right: 2.5rem;" runat="server" OnClick="belge_sorgula_Click" />
                        </div>

                    </form>
                </div>
            </div>
        </div>
        <div
            class="d-flex flex-column flex-md-row text-center text-md-start justify-content-between py-4 px-4 px-xl-5 bg-primary">
            <!-- Copyright -->
            <div class="text-white mb-3 mb-md-0">
              KLÜ Sınav Giriş Sistemi
            </div>
            <!-- Copyright -->


        </div>
    </section>

</body>
</html>



