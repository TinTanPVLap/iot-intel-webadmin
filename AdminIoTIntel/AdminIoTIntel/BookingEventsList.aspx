<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="BookingEventsList.aspx.cs" Inherits="adminiotintel.ListBookingEvents" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="group-content">
        <div class="captioncontent">
            <h2>
                List Booking Event
            </h2>
        </div>
        <div class="padding" style="height: 40px;">
            <div class="group-button" style="width: 100%;">
              <asp:HyperLink ID="hplCreateNew" runat="server" ToolTip="Create new" NavigateUrl="~/BookingEventsDetail.aspx">Create</asp:HyperLink>
              <asp:TextBox ID="txtKey" runat="server" placeholder="Title, User"></asp:TextBox>
                  <label> Room</label>
                <asp:DropDownList ID="ddlRoom" runat="server"></asp:DropDownList>
             
                <asp:LinkButton ID="lbtFilter" runat="server" ToolTip="Search" OnClick="lbtFilter_Click">Search</asp:LinkButton>
                <asp:LinkButton ID="lbtExportExcel" runat="server" ToolTip="Export Excel" OnClick="lbtExportExcel_Click">Export Excel</asp:LinkButton>
            </div>
        </div>
        <div class="grid-import">
            <telerik:RadGrid ID="radGridBooking" runat="server" GridLines="None" AllowPaging="true"
                OnItemDataBound="radGridBooking_ItemDataBound" PageSize="30" AllowSorting="true" OnNeedDataSource="radGridBooking_NeedDataSource"
                CellSpacing="0" Skin="Hay" Width="100%" AutoGenerateColumns="False">
                <MasterTableView DataKeyNames="EventID" Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="EventID" SortExpression="EventID" UniqueName="EventID">
                            <ItemTemplate>
                                <asp:Literal ID="ltrEventID" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Title" SortExpression="Name"
                            UniqueName="Name">
                            <ItemTemplate>
                                <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Room Name" SortExpression="RoomName"
                            UniqueName="RoomName">
                            <ItemTemplate>
                                <asp:Literal ID="ltrRoomName" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="TimeStart" SortExpression="TimeStart" UniqueName="TimeStart">
                            <ItemTemplate>
                                <asp:Literal ID="ltrTimeStart" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="TimeEnd" SortExpression="TimeEnd" UniqueName="TimeEnd">
                            <ItemTemplate>
                                <asp:Literal ID="ltrTimeEnd" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="User Book" SortExpression="UserBook"
                            UniqueName="UserBook">
                            <ItemTemplate>
                                <asp:Literal ID="ltrUserbook" runat="server"></asp:Literal>
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
