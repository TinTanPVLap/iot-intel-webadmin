<%@ Page Language="C#" MasterPageFile="~/masterpage/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="adminiotintel.UserDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID ="ContentPlaceHolder1" runat="server">
    <div class="group-content">
        <div class="captioncontent">
            <h2>User Detail</h2>
        </div>
        <div class="group-detail" style="width:100%; margin-top:20px;">
           <ul>
               <li>
                   <label for="<%=ddlTypeUser.ClientID%>">TypeUser</label>
                   <asp:DropDownList ID="ddlTypeUser" runat="server"></asp:DropDownList>
               </li>
               <li>
                   <label for="<%=txtFullName.ClientID%>">FullName(*)</label>
                   <asp:TextBox ID="txtFullName" runat="server" MaxLength ="250"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rwFullName" runat="server" ErrorMessage="*" ControlToValidate ="txtFullName" Display="Dynamic"></asp:RequiredFieldValidator>
               </li>
               <li>
                   <label for="<%=txtUserName.ClientID%>">UserName(*)</label>
                   <asp:TextBox ID="txtUserName" runat="server" MaxLength="150"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rwUserName" runat="server" ErrorMessage="*" ControlToValidate ="txtUserName" Display="Dynamic"></asp:RequiredFieldValidator>
               </li>
               <li>
                   <label for="<%=txtPassWord.ClientID%>">
                        Password</label><asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqPass" runat="server" ErrorMessage="*" ControlToValidate="txtPassWord"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:LinkButton ID="lbtReset" runat="server" CausesValidation="false" ToolTip="Reset Password"
                        Visible="false" Style="background: none; border: none; margin: 0px; padding-left: 0px; color: Blue;"
                        OnClick="lbtReset_Click">Reset Password</asp:LinkButton>
                    <a href="#" title="Show" id="showPassword" runat="server" class="showpass">Show</a>
                    <%--<asp:RegularExpressionValidator ID="rwRexPass" runat="server" ErrorMessage="Password have least 8 characters and contain 1 special character, 1 number"
                        ControlToValidate="txtPassWord" Display="Dynamic" ValidationExpression="^.*(?=.{8,})(?=.*[\d])(?=.*[\W]).*$"></asp:RegularExpressionValidator>--%>

                   <%--<label for="<%=txtPassword.ClientID%>">Password(*)</label>
                   <asp:TextBox ID ="txtPassword" runat="server" TextMode ="Password"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rwPassword" runat="server" ErrorMessage ="*" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                   <asp:LinkButton ID ="lbtReset" runat="server" CausesValidation="false" ToolTip ="Reset Password" Visible ="false"
                       Style="background:none; border:none;margin:0px; padding-left:0px;color:blue;"
                       OnClick="lbtReset_Click">
                       Reset Password
                   </asp:LinkButton>
                    <a href="#" title="Show" id="showPassword" runat="server" class="showpass">Show</a>
                   <asp:RegularExpressionValidator ID="rwRexPass" runat="server" ErrorMessage="Password have least 8 characters and contain 1 special character, 1 number"
                        ControlToValidate="txtPassWord" Display="Dynamic" ValidationExpression="^.*(?=.{8,})(?=.*[\d])(?=.*[\W]).*$"></asp:RegularExpressionValidator>--%>
               </li>
               <li>
                   <label for="<%=txtEmail.ClientID%>">Email(*)</label>
                   <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqInEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rqEmail" runat="server" ErrorMessage="is valid Email"
                        ControlToValidate="txtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
               </li>
                <li>
                   <label for="<%=txtPhone.ClientID%>">Phone</label>
                   <asp:TextBox ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox>
                  <%-- <asp:RequiredFieldValidator ID="rwPhone" runat="server" ErrorMessage="*" ControlToValidate ="txtPhone" Display="Dynamic"></asp:RequiredFieldValidator>--%>
               </li>
               <li>
                    <label for="<%=imgThumbs.ClientID%>">Images</label>
                    <asp:FileUpload ID="FileUploadControl" runat="server"  /> 
                    <asp:Image ID="imgThumbs" runat="server" Width="150" Height="60" Style="float: left;" />
                    <asp:LinkButton ID="lbtChangeImages" runat="server" ToolTip="Change Picture" OnClick="lbtChangeImages_Click">Change Picture</asp:LinkButton>
               </li>
               <li>
                    <label for="<%=chkStatus.ClientID%>">Active</label>
                    <asp:CheckBox ID="chkStatus" runat="server" CssClass="checkbox" />
               </li>
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
        </div>
    </div>

</asp:Content>