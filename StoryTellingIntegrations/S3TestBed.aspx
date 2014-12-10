<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="S3TestBed.aspx.cs" Inherits="About" %>

<%@ Register src="Controls/AmazonS3Control.ascx" tagname="AmazonS3Control" tagprefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>List of Files...
        <uc1:AmazonS3Control ID="AmazonS3Control1" runat="server" />
    <p>
        Tool to Upload Files:
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUploadFile" runat="server" Text="Upload File To S3" 
            onclick="btnUploadFile_Click" />
        <asp:Label ID="Label1" runat="server"
            Text="Label"></asp:Label>


    </p>
</asp:Content>
