<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmCreateSMS.aspx.cs" Inherits="AdminTool.frmCreateSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 149px;
            height: 59px;
        }
        .style2
        {
            width: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
         <div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style2">
            <asp:ImageButton ID="ImageGoBack" runat="server" ImageUrl="~/Images/goBack.png" Style="height: 30px;
                width: 30px; vertical-align: middle; margin: 5px;" ValidationGroup="text" align="left" />
                </td>
                  <td style="vertical-align: middle; float:left; margin:1%;">
                       <asp:Label ID="lblHeader" runat="server" Text="SMS Information" Style="font-weight: bold; text-align:left;"  Font-Size="18" Font-Names="Forum">
                       </asp:Label>
                  </td>
                   </tr>
                </table>
        </div>
        </div>
        <div style="padding: 20px;">
            <table width="100%">
                <tr>
                    <td colspan="3">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td colspan="2" align="right" class="style1">
                        <div id="DivExport" runat="server" style="text-align: right; width: 138px;">
                            <asp:Button ID="SaveSMSDetail" runat="server" Text="Save" CssClass="btn" EnableViewState="false"
                                CausesValidation="true" Width="107px" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button ID="CancelSMS" runat="server" Text="Cancel" CssClass="btn" EnableViewState="false"
                                CausesValidation="true" BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblTemplatemsg" AssociatedControlID="dropdownTemplatemsg"
                        CssClass="col-md-8 control-label">Template Message</asp:Label>
                    <div class="col-md-12">
                        <asp:DropDownList ID="dropdownTemplatemsg" runat="server" BorderColor="#bbd3e9" Style="background-color: #e5eef6">
                            <asp:ListItem Text="None"></asp:ListItem>
                            <%--<asp:ListItem Text="manager"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblSubject" AssociatedControlID="tbSubject" CssClass="col-md-8 control-label">Subject</asp:Label>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="tbSubject" ValidationGroup="Group1" CssClass="form-control"
                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbSubject"
                            ValidationGroup="Group1" ErrorMessage="The field is required." />
                    </div>
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblLead" AssociatedControlID="tbLead" CssClass="col-md-8 control-label">Lead</asp:Label>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="tbLead" ValidationGroup="Group1" CssClass="form-control"
                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbLead"
                            ValidationGroup="Group1" ErrorMessage="The field is required." />
                    </div>
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblClient" AssociatedControlID="tbClient" CssClass="col-md-8 control-label">Client</asp:Label>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="tbClient" ValidationGroup="Group1" CssClass="form-control"
                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbClient"
                            ValidationGroup="Group1" ErrorMessage="The field is required." />
                    </div>
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblMessage" AssociatedControlID="tbMessage" CssClass="col-md-8 control-label">Message</asp:Label>
                    <div class="col-md-12">
                        <asp:TextBox runat="server" ID="tbMessage" ValidationGroup="Group1" CssClass="form-control"
                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbMessage"
                            ValidationGroup="Group1" ErrorMessage="The field is required." />
                    </div>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblEmailOptOut" AssociatedControlID="chkEmailOptOut"
                            CssClass="col-md-8 control-label">Email opt out</asp:Label>
                        <asp:CheckBox runat="server" ID="chkEmailOptOut" />
                    </td>
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblStatus" AssociatedControlID="dropdownStatus" CssClass="col-md-8 control-label">Status</asp:Label>
                    <div class="col-md-12">
                        <asp:DropDownList ID="dropdownStatus" runat="server" BorderColor="#bbd3e9" Style="background-color: #e5eef6">
                            <asp:ListItem Text="None"></asp:ListItem>
                            <asp:ListItem Text="Pending"></asp:ListItem>
                            <asp:ListItem Text="Sent"></asp:ListItem>
                            <asp:ListItem Text="Failed"></asp:ListItem>
                            <asp:ListItem Text="Received"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblAdditionalInformation" CssClass="col-md-8 control-label">Additional Information</asp:Label>
                    &nbsp;
                </tr>
                <tr>
                    <asp:Label runat="server" ID="lblSMSOwner" AssociatedControlID="dropdownSMSOwner"
                        CssClass="col-md-8 control-label">SMS Owner</asp:Label>
                    <div class="col-md-12">
                        <asp:DropDownList ID="dropdownSMSOwner" runat="server" BorderColor="#bbd3e9" Style="background-color: #e5eef6">
                        </asp:DropDownList>
                    </div>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
