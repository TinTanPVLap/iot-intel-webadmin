<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CabinetDetail.aspx.cs" Inherits="adminiotintel.CabinetDetail" %>


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
                   <label for="<%=txtName.ClientID%>">Name(*)</label>
                   <asp:TextBox ID="txtName" runat="server" MaxLength ="10000"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rwName" runat="server" ErrorMessage="*" ControlToValidate ="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
               </li>
               <li>
                    <label for="<%=chkStatus.ClientID%>">
                        Status</label><asp:CheckBox ID="chkStatus" runat="server" CssClass="checkbox" />
                </li>
               <li>
                    <label>&nbsp;</label>
                    <asp:LinkButton ID="lbtSave" runat="server" CssClass="updaterequest" CausesValidation="true"
                        ToolTip="Save" OnClick="lbtSave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="lbtDelete" runat="server" CssClass="deleterequest" ToolTip="Delete"
                        CausesValidation="false" OnClick="lbtDelete_Click" Style="margin-left: 5px;">Delete</asp:LinkButton>
                    <a href="CabinetList.aspx" title="Close" style="margin-left: 5px;">Close</a>
                </li>
                <li>&nbsp;</li>
           </ul>
        </div>
    </div>

</asp:Content>
