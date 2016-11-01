<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmUserDetailViewScreen.aspx.cs" Inherits="AdminTool.frmUserDetailViewScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1 {
            height: 18px;
        }

        .style7 {
            width: 474px;
        }

        .style8 {
            width: 47px;
        }
    </style>

    <script type="text/javascript">
        function crmClickEvent() {
            var doc = document.getElementById("tblCRMSetting")
            if (doc.style.display == "none") {
                document.getElementById("tblCRMSetting").style.display = "block";
                document.getElementById("tblAccountSetting").style.display = "none";
                document.getElementById("tblSMSSetting").style.display = "none";
                document.getElementById("<%= ImgCrmSetting.ClientID %>").value = "Account Setting";
                document.getElementById("<%= ImgSmsSetting.ClientID %>").value = "SMS Setting";
            } else {
                document.getElementById("tblCRMSetting").style.display = "none";
                document.getElementById("tblSMSSetting").style.display = "none";
                document.getElementById("tblAccountSetting").style.display = "block";
                document.getElementById("<%= ImgCrmSetting.ClientID %>").value = "CRM Setting";
            }
            document.getElementById("lblerrmsg").style.display = "none";
        }

        function smsClickEvent() {
            var doc = document.getElementById("tblSMSSetting")
            if (doc.style.display == "none") {
                document.getElementById("tblSMSSetting").style.display = "block";
                document.getElementById("tblCRMSetting").style.display = "none";
                document.getElementById("tblAccountSetting").style.display = "none";
                document.getElementById("<%= ImgCrmSetting.ClientID %>").value = "CRM Setting";
                document.getElementById("<%= ImgSmsSetting.ClientID %>").value = "Account Setting";
            } else {
                document.getElementById("tblCRMSetting").style.display = "none";
                document.getElementById("tblSMSSetting").style.display = "none";
                document.getElementById("tblAccountSetting").style.display = "block";
                document.getElementById("<%= ImgCrmSetting.ClientID %>").value = "CRM Setting";
                document.getElementById("<%= ImgSmsSetting.ClientID %>").value = "SMS Setting";
            }
            document.getElementById("lblerrmsg").style.display = "none";
        }


        function onCheckChange() {
            
            if (document.getElementById("<%= chkIsUseDefault.ClientID %>").checked) {
                document.getElementById("<%=tbSmsUserID.ClientID %>").setAttribute("disabled",true);
                document.getElementById("<%=tbSmsUserPassword.ClientID %>").setAttribute("disabled", true);
                document.getElementById("<%=tbAppKey.ClientID %>").setAttribute("disabled", true);
                document.getElementById("<%=tbAppSecretKey.ClientID %>").setAttribute("disabled", true);
            }
            else {
                document.getElementById("<%=tbSmsUserID.ClientID %>").removeAttribute("disabled");
                document.getElementById("<%=tbSmsUserPassword.ClientID %>").removeAttribute("disabled");
                document.getElementById("<%=tbAppKey.ClientID %>").removeAttribute("disabled");
                document.getElementById("<%=tbAppSecretKey.ClientID %>").removeAttribute("disabled");
            }
        }

        function ShowSmsPasswordClick() {
            if (document.getElementById("<%= tbSmsUserPassword.ClientID %>").type == "password")
                document.getElementById("<%= tbSmsUserPassword.ClientID %>").type = "Text";
            else
                document.getElementById("<%= tbSmsUserPassword.ClientID %>").type = "Password";
        }

        function ShowCrmPasswordClick() {
            if (document.getElementById("<%= tbGmailPassword.ClientID %>").type == "password")
                document.getElementById("<%= tbGmailPassword.ClientID %>").type = "Text";
            else
                document.getElementById("<%= tbGmailPassword.ClientID %>").type = "Password";
        }

        function ShowPortalPasswordClick() {
            if (document.getElementById("<%= tbPassword.ClientID %>").type == "password")
                document.getElementById("<%= tbPassword.ClientID %>").type = "Text";
            else
                document.getElementById("<%= tbPassword.ClientID %>").type = "Password";
        }

        function HideTextBox(ddlId) {
            var selectedValue = document.getElementById("<%= dropExistingEntry.ClientID %>").value;
            if (selectedValue == 2) {
                document.getElementById("<%= lblUpdateNote.ClientID %>").style.display = "block";
            } else {
                document.getElementById("<%= lblUpdateNote.ClientID %>").style.display = "none";
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
    <div style="margin: 20px 20px; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style8">
                            <asp:ImageButton ID="ImageGoBack" ValidationGroup="text" runat="server" ImageUrl="~/Images/goBack.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px;" OnClick="ImageGoBack_Click"
                                align="left" />
                        </td>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="General Account Settings" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 10px;">
            <table width="100%">
                <tr>
                    <td class="style1" colspan="3">
                        <div id="DivExport" runat="server" style="text-align: right;">
                            <asp:Button ID="ImgCrmSetting" runat="server" Text="CRM Setting" CssClass="btn"
                                OnClientClick="crmClickEvent();return false;" Width="163px" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button ID="ImgSmsSetting" runat="server" Text="SMS Setting" CssClass="btn"
                                OnClientClick="smsClickEvent();return false;" Width="163px" BackColor="#3E75CD" ForeColor="White" />

                            <asp:Button ID="ImgViewSubject" runat="server" Text="View Subject" CssClass="btn"
                                EnableViewState="false" CausesValidation="true" ValidationGroup="Group1" OnClick="ImgViewSubject_Click"
                                Width="163px" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button ID="ImgViewLeadColumnHeader" runat="server" Text="View Lead Columns"
                                CssClass="btn" EnableViewState="false" CausesValidation="true" ValidationGroup="Group1"
                                OnClick="ImgViewLeadColumnHeader_Click" BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button ID="ImgSync" runat="server" Text=" Force Sync" CssClass="btn" EnableViewState="false"
                                CausesValidation="true" ValidationGroup="Group1" OnClick="ImgTestApi_Click" BackColor="#3E75CD"
                                ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td id="lblerrmsg">&nbsp;
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td id="tblCRMSetting" style="display: none;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblGmailId" AssociatedControlID="tbGmailId" Style="vertical-align: middle;" CssClass="col-md-6 control-label ">Gmail Id : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbGmailId" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblGmailPassword" AssociatedControlID="tbGmailPassword" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Gmail Password : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbGmailPassword" ValidationGroup="Group1" CssClass="form-control" TextMode="Password"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnlShowPortalPassword" runat="server" class="col-md-5" Style="margin-bottom: 10%;" OnClientClick="ShowCrmPasswordClick();return false;">ShowPassword</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblConfigurationToken" AssociatedControlID="tbConfigurationToken" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Configuration Token : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbConfigurationToken" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblexistingEvent" AssociatedControlID="tbPassword" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Existing Entry : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="dropExistingEntry" CssClass="form-control" BorderColor="#bbd3e9" onchange="HideTextBox(this);" Style="background-color: #e5eef6" OnSelectedIndexChanged="dropExistingEntry_SelectedIndexChanged" />
                                    </div>
                                </td>
                                <td style="vertical-align: middle; margin-top: 2%;">
                                    <asp:Label runat="server" ID="lblUpdateNote" Style="vertical-align: middle; display: none; font-size: 14px; color: red" CssClass="col-md-12 control-label">Note : Existing info will be update only on behalf of your Lead email column value.</asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3">&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div id="SaveCRMinfoDiv" visible="true" runat="server">
                                        <asp:Button ID="btnSaveCRM" runat="server" Text="Save" CssClass="btn" EnableViewState="false"
                                            Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="btnSaveCRM_Click"
                                            BackColor="#3E75CD" ForeColor="White" />
                                        &nbsp;<asp:Button ID="btnSaveCRMCancel" runat="server" Text="Cancel" CssClass="btn"
                                            Width="125" CausesValidation="true" OnClientClick="crmClickEvent();" BackColor="#3E75CD"
                                            ForeColor="White" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td id="tblSMSSetting" style="display: none;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblSmsUserID" AssociatedControlID="tbSmsUserID" Style="vertical-align: middle;" CssClass="col-md-6 control-label ">Sms UserId : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbSmsUserID" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblSmsUserPassword" AssociatedControlID="tbSmsUserPassword" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Sms Password : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbSmsUserPassword" ValidationGroup="Group1" CssClass="form-control" TextMode="Password"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkShowSmsPassword" runat="server" class="col-md-5" Style="margin-bottom: 10%;" OnClientClick="ShowSmsPasswordClick();return false;">ShowPassword</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblAppKey" AssociatedControlID="tbAppKey" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Sms AppKey : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbAppKey" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblAppSecretKey" AssociatedControlID="tbAppSecretKey" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Sms AppSecretKey : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbAppSecretKey" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblSmsConfigurationToken" AssociatedControlID="tbSmsConfigurationToken" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Configuration Token : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbSmsConfigurationToken" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblSmsFrom" AssociatedControlID="tbSmsFrom" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Sms From : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbSmsFrom" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblModuleName" AssociatedControlID="tbModuleName" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Module Name : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbModuleName" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblUseDefault" AssociatedControlID="chkIsUseDefault" Style="vertical-align: middle;" CssClass="col-md-6 control-label">Use Default Credantial : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:CheckBox ID="chkIsUseDefault" runat="server" onChange="onCheckChange(); return false;" AutoPostBack="false" Checked="false" Width="70%" CssClass="txtbox"></asp:CheckBox>
                                        <span>&nbsp;</span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <div id="Div2" visible="true" runat="server">
                                        <asp:Button ID="btnSmsSave" runat="server" Text="Save" CssClass="btn" EnableViewState="false"
                                            Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="btnSmsSave_Click"
                                            BackColor="#3E75CD" ForeColor="White" />
                                        &nbsp;<asp:Button ID="btnSmsCancel" runat="server" Text="Cancel" CssClass="btn"
                                            Width="125" CausesValidation="true" OnClientClick="smsClickEvent();" BackColor="#3E75CD"
                                            ForeColor="White" />                                         
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>



                    <td id="tblAccountSetting">
                        <table>
                            <asp:HiddenField ID="hiddenpassword" runat="server" Value='<%# Eval("password") %>'></asp:HiddenField>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblFirstName" AssociatedControlID="tbFirstName" Style="vertical-align: middle;" CssClass="col-md-5 control-label ">First Name : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbFirstName" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbFirstName"
                                            ValidationGroup="Group1" ErrorMessage="The field is required." />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblLastName" AssociatedControlID="tbLastName" Style="vertical-align: middle;" CssClass="col-md-5 control-label">Last Name : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbLastName" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbLastName"
                                            ValidationGroup="Group1" ErrorMessage="The field is required." />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblEmail" AssociatedControlID="tbEmail" Style="vertical-align: middle;" CssClass="col-md-5 control-label">Portal Email : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbEmail" ValidationGroup="Group1" CssClass="form-control"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbEmail"
                                            ValidationGroup="Group1" ErrorMessage="The field is required." />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblChangePassword" AssociatedControlID="tbPassword" Style="vertical-align: middle;" CssClass="col-md-5 control-label">Portal Password : </asp:Label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="tbPassword" CssClass="form-control" TextMode="Password"
                                            BorderColor="#bbd3e9" Style="background-color: #e5eef6" autocomplete="off" />
                                    </div>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkPortalPassword" runat="server" class="col-md-5" OnClientClick="ShowPortalPasswordClick();return false;">ShowPassword</asp:LinkButton>
                                </td>
                            </tr>

                            <tr>
                                <td>&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <div id="UpdateDiv" visible="false" runat="server">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn" EnableViewState="false"
                                            Width="125" CausesValidation="false" OnClick="btnUpdate_Click1" BackColor="#3E75CD"
                                            ForeColor="White" />
                                        <asp:Button ID="btnUpdateCancel" runat="server" Text="Cancel" CssClass="btn" Width="125"
                                            CausesValidation="true" OnClick="btnSaveCancel_Click" BackColor="#3E75CD" ForeColor="White" />
                                    </div>
                                    <div id="AddNewUser" visible="false" runat="server">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" EnableViewState="false"
                                            Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="btnSave_Click"
                                            BackColor="#3E75CD" ForeColor="White" />
                                        &nbsp;<asp:Button ID="btnSaveCancel" runat="server" Text="Cancel" CssClass="btn"
                                            Width="125" CausesValidation="true" OnClick="btnSaveCancel_Click" BackColor="#3E75CD"
                                            ForeColor="White" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>

            </table>
        </div>
    </div>
</asp:Content>
