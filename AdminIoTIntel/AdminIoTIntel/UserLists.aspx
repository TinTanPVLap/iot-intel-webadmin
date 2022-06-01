<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="UserLists.aspx.cs" Inherits="adminiotintel.UserLists" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="group-content">
        <div class="captioncontent">
            <h2>
                List Accounts
            </h2>
        </div>
        <div class="padding" style="height: 40px;">
            <div class="group-button" style="width: 100%;">
              <asp:HyperLink ID="hplCreateNew" runat="server" ToolTip="Create new" NavigateUrl="~/UserDetails.aspx">Create</asp:HyperLink>
              <asp:TextBox ID="txtKey" runat="server" placeholder="UserID, FullName, Email"></asp:TextBox>
                  <label>
                    Status</label>
                <asp:DropDownList ID="ddlTypeUser" runat="server">
                </asp:DropDownList>
             
                <asp:LinkButton ID="lbtFilter" runat="server" ToolTip="Search" OnClick="lbtFilter_Click">Search</asp:LinkButton>
                <asp:LinkButton ID="lbtExportExcel" runat="server" ToolTip="Export Excel" OnClick="lbtExportExcel_Click">Export Excel</asp:LinkButton>
            </div>
        </div>
        <div class="grid-import">
            <telerik:RadGrid ID="radGridUser" runat="server" GridLines="None" AllowPaging="true"
                OnItemDataBound="radGridUser_ItemDataBound" PageSize="30" AllowSorting="true" OnNeedDataSource="radGridUser_NeedDataSource"
                CellSpacing="0" Skin="Hay" Width="100%" AutoGenerateColumns="False">
                <MasterTableView DataKeyNames="UserID" Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="UserID" SortExpression="UserID" UniqueName="UserID">
                            <ItemTemplate>
                                <asp:Literal ID="ltrUUserID" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="FullName" SortExpression="FullName"
                            UniqueName="FullName">
                            <ItemTemplate>
                                <asp:Literal ID="ltrFullName" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                       
                        <telerik:GridTemplateColumn HeaderText="TypeUser" SortExpression="TypeUserID"
                            UniqueName="TypeUserID">
                            <ItemTemplate>
                                <asp:Literal ID="ltrTypeUserID" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Email" FilterControlAltText="Filter Email column"
                            HeaderText="Email" SortExpression="Email" UniqueName="Email">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Status" SortExpression="Status" UniqueName="Status">
                            <ItemTemplate>
                                <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <%--<telerik:GridBoundColumn DataField="UserCreated" FilterControlAltText="Filter UserCreated column"
                            HeaderText="UserCreated" SortExpression="UserCreated" UniqueName="UserCreated">
                            <ItemTemplate>
                                <asp:Literal ID="ltrUserCreated" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn HeaderText="UserCreated" SortExpression="UserCreated" UniqueName="UserCreated">
                            <ItemTemplate>
                                <asp:Literal ID="ltrUserCreated" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="CreatedDate" SortExpression="CreatedDate" UniqueName="CreatedDate">
                            <ItemTemplate>
                                <asp:Literal ID="ltrCreatedDate" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
