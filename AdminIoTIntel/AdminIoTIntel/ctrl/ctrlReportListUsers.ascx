<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlReportListUsers.ascx.cs" Inherits="adminiotintel.ctrl.ctrlReportListUsers" %>
<h2>List User</h2>
<ul>
    <li>
        <label>
            Total User:
        </label>
        <span>
            <asp:Literal ID="ltrAllUser" runat="server"></asp:Literal></span></li>
    <li>
        <label>
            Activated:
        </label>
        <span>
            <asp:Literal ID="ltrActivated" runat="server"></asp:Literal></span></li>
    <li>
        <label>
            Not Activated:
        </label>
        <span>
            <asp:Literal ID="ltrNotActivated" runat="server"></asp:Literal></span></li>

</ul>
