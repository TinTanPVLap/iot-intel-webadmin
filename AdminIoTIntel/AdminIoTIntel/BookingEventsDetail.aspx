<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="BookingEventsDetail.aspx.cs" Inherits="adminiotintel.BookingEventsDetail" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID ="ContentPlaceHolder1" runat="server">
    <div class="group-content">
        <div class="captioncontent">
            <h2>Room Detail</h2>
        </div>
        <div class="group-detail" style="width:100%; margin-top:20px;">
           <ul>
                <li>
                    <label for="<%=ddlTRoom.ClientID%>">
                        Cabinet</label>
                    <asp:DropDownList ID="ddlTRoom" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
               </li>
               <li>
                    <label for="<%=txtTitle.ClientID%>">
                        Title</label><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
               </li>
               <li>
                    <label for="<%=ddlTUserHost.ClientID%>">
                        User Book</label>
                    <asp:DropDownList ID="ddlTUserHost" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
               </li>
               <li>
                    <label for="<%=txtTimeStart.ClientID%>">
                        TimeStart</label><asp:TextBox ID="txtTimeStart" runat="server" CssClass="datetimepicker" Width="130" TextMode ="DateTime" ></asp:TextBox>
                    <label for="<%=txtTimeEnd.ClientID%>">
                        TimeEnd</label><asp:TextBox ID="txtTimeEnd" runat="server" CssClass="datetimepicker" Width="130" TextMode ="DateTime" ></asp:TextBox>                               
               </li>
               <li>
                    <label for="<%=txtDescription.ClientID%>">
                        Description</label><asp:TextBox ID="txtDescription" runat="server" Enabled="True" CssClass="Textbox"
                        MaxLength="1073741823" TextMode="MultiLine" Height="50px" Width="381px"></asp:TextBox>
                </li>
               <li>
                   <label>&nbsp;</label>
                   <asp:CheckBox ID="chkBkCabinet" runat="server" CssClass="checkBoxList" OnCheckedChanged="chkBkCabinet_CheckedChanged" Text="Booking Cabinet" AutoPostBack="true" />
               </li>
           </ul>
            <ul style="margin-left:10px; padding-left:10px;padding-right:10px">
               <li>
                   <label for="<%=ddlTCabinet.ClientID%>">Cabinet</label>
                    <asp:DropDownList ID="ddlTCabinet" runat="server" CssClass="Dropdown"></asp:DropDownList>
               </li>
               <li>
                    <label for="<%=txtPincode.ClientID%>">Pincode</label>
                   <asp:TextBox ID="txtPincode" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
               </li>
               <li>
                   <label for="<%=txtTimeStartCabinet.ClientID%>">
                        TimeStart</label><asp:TextBox ID="txtTimeStartCabinet" runat="server" CssClass="datetimepicker" Width="130" TextMode ="DateTime" ></asp:TextBox>
                    <label for="<%=txtTimeEndCabinet.ClientID%>">
                        TimeEnd</label><asp:TextBox ID="txtTimeEndCabinet" runat="server" CssClass="datetimepicker" Width="130" TextMode ="DateTime" ></asp:TextBox>                                   
               </li>
          </ul>
           <ul>
               <li>
                    <label>&nbsp;</label>
                    <asp:LinkButton ID="lbtSave" runat="server" CssClass="updaterequest" CausesValidation="true"
                        ToolTip="Save" OnClick="lbtSave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="lbtDelete" runat="server" CssClass="deleterequest" ToolTip="Delete"
                        CausesValidation="false" OnClick="lbtDelete_Click" Style="margin-left: 5px;">Delete</asp:LinkButton>
                    <a href="UserLists.aspx" title="Close" style="margin-left: 5px;">Close</a>
                </li>
                <li>&nbsp;</li>
          </ul>
            <ul>
                 <li>
                    <asp:HyperLink ID="hplAddUser" runat="server" CssClass="newWindow" rel="0">Add User</asp:HyperLink>
               </li>
            </ul>
            <div class="grid-import" style="width: 90%; margin: 2% 0px 0px 3%;">
                <h4>Booking User</h4>
                <telerik:RadGrid ID="radGridBookingUsers" runat="server" GridLines="None" AllowPaging="true" OnDeleteCommand="radGridBookingUsers_DeleteCommand"
                    OnItemDataBound="radGridBookingUsers_ItemDataBound" PageSize="30" AllowSorting="true" OnNeedDataSource="radGridBookingUsers_NeedDataSource" 
                    
                    CellSpacing="0" Skin="Hay" Width="100%" AutoGenerateColumns="False">
                    <MasterTableView DataKeyNames="UserID" Width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="UserID" SortExpression="UserID" UniqueName="UserID">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrUserID" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Fullname" SortExpression="Fullname" UniqueName="Fullname">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrFullname" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Email" SortExpression="Email" UniqueName="Email">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Phone" SortExpression="Phone" UniqueName="Phone">
                                <ItemTemplate>
                                    <asp:Literal ID="ltrPhone" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" HeaderText ="Delete"
                                    FilterControlAltText="Filter DeleteColumn column"
                                    ImageUrl="~/images/Delete.gif" Text="Delete"
                                    UniqueName="DeleteColumn" Resizable="false" ConfirmText="Are you sure you want to delete the selected row?">
                                    <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                    <ItemStyle CssClass="ButtonColumn" />
                            </telerik:GridButtonColumn>
                       </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
         
        </div>
    </div>

</asp:Content>