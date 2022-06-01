<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterNewWindows.Master"   AutoEventWireup="true" CodeBehind="UserChoose.aspx.cs" Inherits="adminiotintel.UserChoose" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var urlCurrent = '../BookingEventsDetail.aspx?id=<%=_eventID%>&tab2=2';
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="group-content">
        <div class="captioncontent">
            <h2>List Users
            </h2>
        </div>
        <div class="padding" style="height: 40px;">
            <div class="group-button" style="width: 100%;">
                <asp:TextBox ID="txtKey" runat="server" placeholder="FullName, Email"></asp:TextBox>
                <asp:LinkButton ID="lbtFilter" runat="server" ToolTip="Search" OnClick="lbtFilter_Click">Search</asp:LinkButton>
                <asp:LinkButton ID="lbtAddUser" runat="server" ToolTip="Add User" OnClick="lbtAddUser_Click">Add User</asp:LinkButton>

            </div>
        </div>
        <div class="grid-import">
            <telerik:RadGrid ID="radGridUser" runat="server" GridLines="None" AllowPaging="true"
                OnItemDataBound="radGridUser_ItemDataBound" PageSize="30" AllowSorting="true" OnNeedDataSource="radGridUser_NeedDataSource"
                CellSpacing="0" Skin="Hay" Width="100%" AutoGenerateColumns="False">
                <MasterTableView DataKeyNames="UserID" Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Chose" SortExpression="Chose" UniqueName="Chose">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkChose" runat="server" />
                                <asp:HiddenField ID="hdUserID" runat="server" Value="0" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="UserID" SortExpression="UserID" UniqueName="UserID">
                            <ItemTemplate>
                                <asp:Literal ID="ltrUserID" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="FullName" SortExpression="FullName" UniqueName="UseFullNamerID">
                            <ItemTemplate>
                                <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Email" SortExpression="Email"
                            UniqueName="Email">
                            <ItemTemplate>
                                <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                            <ItemTemplate>
                                <asp:Literal ID="ltrPhone" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                       
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>