<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmError.aspx.cs" Inherits="AdminTool.frmError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div style=" padding:20px;">  
        <h2 style="color:Blue">  
            Application Error:</h2> 
             
        <h3> 
        <br/> 
            Sorry, an error occurred while processing your request.  
        </h3>  
        <br/>
       <asp:HyperLink 
            ID="HyperLink1" 
            runat="server"
            Text="Go Back To Home Page"
            NavigateUrl="~/default.aspx"
            >
        </asp:HyperLink> 
        <br/>     
    </div>  


</asp:Content>
