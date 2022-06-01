<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ChangePassWord.aspx.cs" Inherits="adminiotintel.ChangePassWord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="group-content">
        <div class="captioncontent">
            <h2>
                Change Your's Password
            </h2>
        </div>
        <div class="group-detail" style="width: 100%;">
            <ul>
                <li>
                    <label for="<%=txtUserName.ClientID%>">
                        User name</label><asp:TextBox ID="txtUserName" runat="server" Enabled="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        CssClass="validation" Display="Dynamic" ControlToValidate="txtUserName"></asp:RequiredFieldValidator></li>
                <li>
                    <label for="<%=txtPassOld.ClientID%>">
                        Old password</label><asp:TextBox ID="txtPassOld" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation" Display="Dynamic" ControlToValidate="txtPassOld"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <label for="<%=txtPassNew.ClientID%>">
                        New password</label><asp:TextBox ID="txtPassNew" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqPassNew" runat="server" ErrorMessage="*"
                        CssClass="validation" Display="Dynamic" ControlToValidate="txtPassNew"></asp:RequiredFieldValidator>
                     <a href="#" title="Show" id="showPasswordNew" class="showpass">Show</a>
                </li>
                <li>
                    <label for="<%=txtPassNewAgain.ClientID%>">
                        Confirm password</label><asp:TextBox ID="txtPassNewAgain" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqPassNewAgain" runat="server" ErrorMessage="*"
                        CssClass="validation" Display="Dynamic" ControlToValidate="txtPassNewAgain"></asp:RequiredFieldValidator>
                     <a href="#" title="Show" id="showPassNewAgain" class="showpass">Show</a>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password and confirm password does not match" Display="Dynamic" 
                        CssClass="validation" ControlToValidate="txtPassNewAgain" ControlToCompare="txtPassNew"></asp:CompareValidator>
                   
                </li>
                <li>
                    <label>
                        &nbsp;</label>
                    <asp:LinkButton ID="lbtUpdate" runat="server" CssClass="updaterequest" ToolTip="Update password"
                        CausesValidation="true" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                    <a href="../UserLists.aspx" title="Cancel" style="margin-left: 5px;">Cancel</a>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
