<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HW4Part1.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            XML URL<br />
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" Width="566px">https://www.public.asu.edu/~adelac10/Restaurants.xml</asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
            <br />
            <div id="BigDiv" runat="server">
            </div>
        </div>
        <div ID="TheDiv" runat="server">

        </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
