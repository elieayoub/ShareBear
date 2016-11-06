<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="ShareBear._Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>share A Bear</title>
    <style type="text/css">
        #BearContainer {
            margin-top: 10px;
        }

        .Validator {
            color: #FF0000;
        }

        img {
            border: 0;
        }

        #ShareSection {
            margin-top: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input id="hfGeneratedImagePath" type="hidden" runat="server" />
        <div id="Controls">
            <b><u>Choose Your Bear:</u></b>
            <asp:RadioButtonList ID="ddlBears" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="<img src='Images/Bears/Bear_Doctor_s.jpg' />" Value="Bear_Doctor.jpg"
                    data-Color="#000000" Selected="True" />
                <asp:ListItem Text="<img src='Images/Bears/Bear_BusinessWoman_s.jpg' />" Value="Bear_BusinessWoman.jpg"
                    data-Color="#000000" />
                <asp:ListItem Text="<img src='Images/Bears/Bear_Marines_s.jpg' />" Value="Bear_Marines.jpg"
                    data-Color="#000000" />
                <asp:ListItem Text="<img src='Images/Bears/Bear_Firefighter_s.jpg' />" Value="Bear_Firefighter.jpg"
                    data-Color="#000000" />
            </asp:RadioButtonList>
            <br />
            <br />
            <b><u>Write Name:</u></b>
            <asp:TextBox ID="txtName" runat="server" MaxLength="15" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required"
                ControlToValidate="txtName" runat="server" CssClass="Validator" SetFocusOnError="true" />
            <asp:Button Text="Generate Bear" runat="server" ID="btnGenerateBear" />
        </div>

        <div id="BearContainer">
            <asp:Image ID="img" ImageUrl="Images/Bears/Bear_Green.jpg" runat="server" Visible="false" />
            <asp:Button Text="Download Generated Bear" runat="server" ID="btnDownloadGeneratedImage" Visible="false" />
        </div>
    </form>
</body>
</html>
