<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="Demo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Repeater id="Repeater1" runat="server">
        <HeaderTemplate>
            <p style="line-height:30px">
        </HeaderTemplate>
        <ItemTemplate>
                <b><%# DataBinder.Eval(Container.DataItem, "Property") %>: </b> 
                <td> <%# DataBinder.Eval(Container.DataItem, "Value") %> <br />
        </ItemTemplate>
        <FooterTemplate>
            </p>
        </FooterTemplate>
    </asp:Repeater>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Publish Sample Message" OnClick="PublishClick" Width="200px" Height="30px" />
    </p>
    <p><asp:Label ID="Label1" runat="server" /></p>
    <p><asp:Button runat="server" Text="Go to authorize dialog" OnClick="AuthDialogClick" /></p>
</asp:Content>

