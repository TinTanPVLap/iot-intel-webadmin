<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="RoomList.aspx.cs" Inherits="adminiotintel.ListRooms" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="group-content">
        <div class="captioncontent">
            <h2>
                List Rooms
            </h2>
        </div>
        <div class="padding" style="height: 40px;">
            <div class="group-button" style="width: 100%;">
              <asp:HyperLink ID="hplCreateNew" runat="server" ToolTip="Create new" NavigateUrl="~/UserDetails.aspx">Create</asp:HyperLink>
              <asp:TextBox ID="txtKey" runat="server" placeholder="Name"></asp:TextBox>
                 <%-- <label> Status</label>
                <asp:DropDownList ID="ddlTypeUser" runat="server"></asp:DropDownList>--%>
             
                <asp:LinkButton ID="lbtFilter" runat="server" ToolTip="Search" OnClick="lbtFilter_Click">Search</asp:LinkButton>
                <asp:LinkButton ID="lbtExportExcel" runat="server" ToolTip="Export Excel" OnClick="lbtExportExcel_Click">Export Excel</asp:LinkButton>
            </div>
        </div>
        <div class="grid-import">
            <telerik:RadGrid ID="radGridRooms" runat="server" GridLines="None" AllowPaging="true"
                OnItemDataBound="radGridRooms_ItemDataBound" PageSize="30" AllowSorting="true" OnNeedDataSource="radGridRooms_NeedDataSource"
                CellSpacing="0" Skin="Hay" Width="100%" AutoGenerateColumns="False">
                <MasterTableView DataKeyNames="RoomID" Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="RoomID" SortExpression="RoomID" UniqueName="RoomID">
                            <ItemTemplate>
                                <asp:Literal ID="ltrRoomID" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Name" SortExpression="Name"
                            UniqueName="Name">
                            <ItemTemplate>
                                <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Description" SortExpression="Description"
                            UniqueName="Description">
                            <ItemTemplate>
                                <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                         
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
