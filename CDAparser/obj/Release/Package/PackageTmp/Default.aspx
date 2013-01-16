<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CDAparser._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <asp:TextBox ID="TextBoxOutput" runat="server" Width="345px"></asp:TextBox>
        <br />
        <br />
        <asp:ListBox ID="ListBox1" runat="server" Height="159px" Width="565px">
        </asp:ListBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        <br />
    </div>
    </form>
</body>
</html>
