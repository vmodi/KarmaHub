<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div id="NoAuthDisplay" runat="server" class="yellowbox">
        <p>
            To access all the features of the app, you must first authorize it. This is a sample
            app which does NOT collect your user information in any way, nor engage in spam.</p>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Authorize and test features" OnClick="AuthClick"
                Width="250px" Height="30px" />
        </p>
    </div>
    <div id="UserInfo" runat="server" align="center">
        <asp:Repeater ID="Repeater1" runat="server" Visible="false">
            <HeaderTemplate>
                <p style="line-height: 30px">
            </HeaderTemplate>
            <ItemTemplate>
                <b>
                    <%# DataBinder.Eval(Container.DataItem, "Property") %>: </b>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Value") %>
                    <br />
            </ItemTemplate>
            <FooterTemplate>
                </p>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <asp:Label ID="UserWelcome" runat="server" Font-Size="XX-Large" Style="font-family: Arial"
            Font-Color="Black" ForeColor="#009999">Welcome</asp:Label>
        <br />
        <asp:Label ID="UserNameLbl" runat="server" Font-Size="XX-Large" Style="font-family: Arial"
            Font-Color="Black">Test</asp:Label>
        <br />
        <asp:Label ID="UserLocaltionLbl" runat="server" Font-Size="XX-Large" Style="font-family: Arial"
            Font-Color="Black">Test</asp:Label>
        <br />
        <table>
            <tr>
                <td>
                    <table background="/Images/KarmaBack.png" style="width: 300px; height: 200px">
                        <tr>
                            <td rowspan="2" style="width: 250px" align="center">
                                <asp:Label ID="KarmaPointsLbl" runat="server" Font-Bold="True" Font-Size="80pt" Style="font-family: Arial Black"
                                    ForeColor="#99FF33">32</asp:Label>
                                <asp:Label ID="UnitLbl" runat="server" Font-Bold="True" ForeColor="Black">Pts</asp:Label>
                            </td>
                            <td align="center">
                                <asp:Button ID="DetailsLbl" runat="server" Text="" OnClick="DetailsLbl_Click" Style="background-image: url(/Images/details.png);
                                    background-repeat: no-repeat;" BackColor="Transparent" BorderStyle="None" ForeColor="Transparent"
                                    Height="28px" Width="28px"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="AddKarmaLbl" runat="server" Text="" OnClick="AddKarmaLbl_Click" Style="background-image: url(/Images/add.png);
                                    background-repeat: no-repeat;" BackColor="Transparent" BorderStyle="None" ForeColor="Transparent"
                                    Height="28px" Width="28px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:GridView ID="KarmaDetailsGridView" runat="server" CellPadding="4" GridLines="Horizontal"
                        Visible="False" BackColor="White" BorderColor="#336666" BorderStyle="Double"
                        BorderWidth="3px">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </td>
                <td>
                    <div id="SubmitDiv" runat="server" visible="false" align="left">
                        <table>
                            <tr>
                                <td>
                                    Friends
                                </td>
                                <td>
                                    <asp:DropDownList ID="FriendsDDL" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Category
                                </td>
                                <td>
                                    <asp:DropDownList ID="CategoriesDDL" runat="server" Width="100%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Points
                                </td>
                                <td>
                                    <asp:ListBox ID="PointsLB" runat="server" Rows="1" Width="100%">
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    </asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="GiveKarma" runat="server" Text="Submit Karma" Width="120px" Height="28px"
                                        OnClick="SubmitKarma" Style="background-image: url(/Images/Submit.png);
                                    background-repeat: no-repeat;" BackColor="Transparent" BorderStyle="None" 
                                        ForeColor="Black" />
                                    <asp:Label ID="TestTxt" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%--    <p>
        <asp:Button ID="Button2" runat="server" Text="Test Page Tab feature" OnClick="PageTabClick" Width="250px" Height="30px" />
    </p>--%>
</asp:Content>
