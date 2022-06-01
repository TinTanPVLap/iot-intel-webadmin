<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlMenu.ascx.cs" Inherits="adminiotintel.ctrl.ctrlMenu" %>
<div class ="menu">
    <ul>
        <li class ="first">
            <asp:HyperLink ID="hplSystem" runat="server" Class="sub1" ToolTip="System">System</asp:HyperLink>
            <ul>
                <li runat="server" id="liUser">
                    <asp:HyperLink ID="hplUsers" runat="server" ToolTip="List User" NavigateUrl ="~/UserLists.aspx">List Users</asp:HyperLink>
                </li>
                <li>
                    <a href="../ChangePassWord.aspx" title ="Change Password">Change Password</a>
                </li>
            </ul>
        </li>
        <li>
            <asp:HyperLink ID="hplRoom" runat="server" ToolTip="Rooms">Room</asp:HyperLink>
            <ul>
                <li>
                    <a href="../RoomList.aspx" title="List Rooms">List Rooms</a>
                </li>
                <li>
                    <a href ="../BookingEventsList.aspx" title="List Booking Events">List Booking Events</a>
                </li>
            </ul>
        </li>
        <li class ="end">
            <asp:HyperLink ID="hplCabinet" runat="server" ToolTip="Cabinet">Cabinet</asp:HyperLink>
            <ul>
                <li>
                    <a href="../CabinetList.aspx" title ="List Cabinets">List Cabinets</a>
                </li>
                <li>
                    <a href ="../BookingCabinetsList.aspx" title ="List Booking Cabinets">List Booking Cabinets</a>
                </li>
            </ul>
        </li>
    </ul>
</div>
