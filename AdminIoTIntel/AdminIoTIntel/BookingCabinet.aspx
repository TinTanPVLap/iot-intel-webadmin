<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterNewWindows.Master"   AutoEventWireup="true" CodeBehind="BookingCabinet.aspx.cs" Inherits="adminiotintel.BookingCabinet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var urlCurrent = '../BookingEventsDetail.aspx?id=<%= _eventID %>';
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="group-detail" style="width: 100%; margin-top: 20px;">
        <h4 style="width:100%;text-align:center;text-transform:uppercase;font-size:18px;">Addnew Production</h4>
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
                    <a href="BookingEventsDetail.aspx" title="Close" style="margin-left: 5px;">Close</a>
                </li>
                <li>&nbsp;</li>
        </ul>
    </div>
</asp:Content>
