<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TryItPagePart3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label2" runat="server" BorderColor="Black" Font-Bold="True" Font-Names="Arial Black" Font-Size="Larger" ForeColor="#3333CC" Text="Try It Page"></asp:Label>
            <br />
            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Text="URL: "></asp:Label>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://localhost:55037/Service1.svc">http://localhost:55037/Service1.svc</asp:HyperLink>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Text="Weather Service"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Font-Names="Arial Narrow" Text="This  REST service returns a JSON object with a simplified 5 day forcast along with the best day to install solar panels for a given latitude and longitude. "></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Text="Latitude, -90 to 90"></asp:Label>
            &nbsp;<asp:TextBox ID="LatitudeBox" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Text="Longitude, from -180 to 180"></asp:Label>
            &nbsp;<asp:TextBox ID="LongitudeBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Submit" runat="server" OnClick="SubmitButton_Click" Text="Get Forcast" />
            <br />
            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Text="JSON object on day to install solar panels: "></asp:Label>
            <br />
            <asp:Label ID="AnswerBox" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Text="Solar Service"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label10" runat="server" Font-Names="Arial Narrow" Text="This REST service returns a JSON object with information about the amount of days per month without any sun and the minimum percentage of sunshine for a given month for a given latitude and longitude. "></asp:Label>
            <br />
            <asp:Label ID="Label11" runat="server" Font-Names="Arial" Text="Latitude, -90 to 90"></asp:Label>
            &nbsp;<asp:TextBox ID="LatitudeBox1" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Text="Longitude, from -180 to 180"></asp:Label>
            &nbsp;<asp:TextBox ID="LongitudeBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Submit1" runat="server" OnClick="SubmitButton_Click" Text="Get Solar Data" />
            <br />
            <asp:Label ID="Label13" runat="server" Font-Names="Arial" Text="JSON object with no sun days, percentage of sunshine, and if solar panels are viable:"></asp:Label>
            <br />
            <asp:Label ID="AnswerBox1" runat="server"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
