<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageTab.aspx.cs" Inherits="PageTab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div_Authorized" runat="server">
        <p>Your name is: <asp:Label ID="Label_name" runat="server" /></p>
        <p>The sum of the ASCII values of your name is: <asp:Label ID="Label_Sum" runat="server" /></p>
    </div>
    <div id="div_NotAuthorized" runat="server">
        <p>You have not authorized the app.</p>
        <asp:Button ID="Button1" runat="server" Text="Authorize" OnClick="AuthClick" />
    </div>
    </form>
</body>
</html>
