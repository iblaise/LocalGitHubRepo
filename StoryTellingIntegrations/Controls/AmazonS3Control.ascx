<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AmazonS3Control.ascx.cs"
    Inherits="Controls_AmazonS3Control" %>
<asp:ListView ID="lvDetail" runat="server" OnItemCommand="LinkButton1_ItemCommand" 
    onselectedindexchanged="lvDetail_SelectedIndexChanged">
    <ItemTemplate>
        <%--<tr>
            <td>
                <%# Eval("Key")%>
            </td>
            <td>
                <%# Eval("Size")%>
            </td>--%>
            <div style="width:55px;border:1px solid green;"><a href="<%# GetAWSFiles(Eval("Key").ToString()) %>">Click</a> 
                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("Key") %>' runat="server">Delete</asp:LinkButton></div>
                
            <%--</td>
        </tr>--%>
    </ItemTemplate>
    <%--<LayoutTemplate>
        <table id="tbl1" runat="server">
            <tr id="tr1" runat="server">
                <td id="td1" runat="server">
                    Key
                </td>
                <td id="td2" runat="server">
                    Size
                </td>
                <td id="td3" runat="server">
                    Link
                </td>
            </tr>
            <tr id="ItemPlaceholder" runat="server">
            </tr>
        </table>
    </LayoutTemplate>--%>
</asp:ListView>
