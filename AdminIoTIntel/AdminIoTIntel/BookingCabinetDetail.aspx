<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="BookingCabinetDetail.aspx.cs" Inherits="adminiotintel.BookingCabinetDetail" %>

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
                    <label for="<%=ddlTCabinet.ClientID%>">
                        Cabinet</label>
                    <asp:DropDownList ID="ddlTCabinet" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
               </li>
               <li>
                    <label for="<%=txtPincode.ClientID%>">
                        Pincode</label><asp:TextBox ID="txtPincode" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rqCode" runat="server" ErrorMessage="*" ControlToValidate="txtPincode"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <span id="errorCode" runat="server" visible="false">Code is exits. Please input other Product.</span>--%>
               </li>
               <li>
                    <label for="<%=ddlTUserHost.ClientID%>">
                        User Book</label>
                    <asp:DropDownList ID="ddlTUserHost" runat="server" CssClass="Dropdown">
                    </asp:DropDownList>
               </li>
               <li>
                    <label for="<%=txtTimeStart.ClientID%>">
                        TimeStart</label><asp:TextBox ID="txtTimeStart" runat="server" CssClass="datetimepicker" Width="122" TextMode ="DateTime" ></asp:TextBox>
                    <label for="<%=txtTimeEnd.ClientID%>">
                        TimeEnd</label><asp:TextBox ID="txtTimeEnd" runat="server" CssClass="datetimepicker" Width="121" TextMode ="DateTime" ></asp:TextBox>                                  
               </li>

               <li>
                    <label>&nbsp;</label>
                    <asp:LinkButton ID="lbtSave" runat="server" CssClass="updaterequest" CausesValidation="true"
                        ToolTip="Save" OnClick="lbtSave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="lbtDelete" runat="server" CssClass="deleterequest" ToolTip="Delete"
                        CausesValidation="false" OnClick="lbtDelete_Click" Style="margin-left: 5px;">Delete</asp:LinkButton>
                    <a href="BookingCabinetsList.aspx" title="Close" style="margin-left: 5px;">Close</a>
                </li>
                <li>&nbsp;</li>
           </ul>
        </div>
    </div>

</asp:Content>



