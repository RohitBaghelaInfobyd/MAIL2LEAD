<%@ Page Title="Content Split Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="frmMailContentSplitInfo.aspx.cs" Inherits="AdminTool.frmMailContentSplitInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListRowGrid {
            border-bottom: 10px #fff solid;
            background-color: #efefef;
            height: 50px;
            margin-left: 10px;
        }

            .ListRowGrid td {
                padding-left: 15px;
                vertical-align: middle;
            }

        .ListHeaderGrid {
            background-color: #BDC9D6;
            height: 50px;
        }

            .ListHeaderGrid th {
                padding-left: 15px;
                vertical-align: middle;
            }

        .commondClass {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; margin-bottom: 6%; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
            <div>
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style2">
                            <asp:ImageButton ID="ImageGoBack3" ValidationGroup="text" runat="server" ImageUrl="~/Images/goBack.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px;"
                                OnClick="ImageGoBack3_Click" align="left" />
                        </td>
                        <td style="vertical-align: middle; float: left; margin: 1%;">
                            <asp:Label ID="lblHeader" runat="server" Text="Manage Template Content Split Information" Style="font-weight: bold; text-align: left;"
                                Font-Size="18" Font-Names="Forum"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="padding: 20px;">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Selected Subject : " Font-Bold="true"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="dropDpownListOfAllSubjectList" runat="server" OnSelectedIndexChanged="dropDpownListOfAllSubjectList_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td colspan="3" style="text-align: right">
                        <div id="DivExport" runat="server">
                            <asp:Button ID="ImgAddSplitInfo" runat="server" Text="Add Split Info" CssClass="btn"
                                Width="125" CausesValidation="true" ValidationGroup="Group1"
                                OnClick="ImgAddNewSplitInfo_Click" BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="pnlAddNewInfo" runat="server" Visible="false" Style="padding-top: 10px;">
                            <table>
                                <tr>
                                    <td>
                                        <div>
                                            <asp:Panel ID="PnlAddNewSplitInfo" runat="server">
                                                <asp:Label runat="server" Text="Select Lead Column : "></asp:Label>
                                                <asp:DropDownList ID="MailToLeadColumnHeader" runat="server">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="tbStartText" placeholder="Start Text To Split" ValidationGroup="text"
                                                    runat="server" onkeyup="OnsearchtextChanged();" MaxLength="50" class="form-control"
                                                    BorderColor="#bbd3e9" Style="display: inline-block" />
                                                <asp:TextBox ID="tbEndText" placeholder="End Text To Split" ValidationGroup="text"
                                                    runat="server" onkeyup="OnsearchtextChanged();"
                                                    MaxLength="50" class="form-control" BorderColor="#bbd3e9" Style="display: inline-block" />
                                                <asp:Button ID="imgBtnAddNew" runat="server" Text="Add" CssClass="btn"
                                                    Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="imgBtnAddNew_Click"
                                                    BackColor="#3E75CD" ForeColor="White" />
                                                <asp:Button ID="imgBtnCancel" runat="server" Text="Cancel" CssClass="btn"
                                                    Width="125" CausesValidation="true" ValidationGroup="Group1" OnClick="imgBtnCancel_Click"
                                                    BackColor="#3E75CD" ForeColor="White" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: .5em; padding-bottom: .5em">
                                        <asp:Panel ID="PnlisValueSplitInfo" runat="server">
                                            <asp:Label ID="lblIsValueSplit" runat="server" Text="Split Value : "></asp:Label>
                                            <asp:CheckBox ID="chkisValueSplit" runat="server" Checked="false" AutoPostBack="true"
                                                EnableViewState="true" OnCheckedChanged="chkIsValueSplitInfo_Clicked"></asp:CheckBox>
                                            <asp:Label ID="lblSplitFrom" runat="server" Text="&nbsp;&nbsp;&nbsp;  Split By : "></asp:Label>
                                            <asp:DropDownList ID="dropdownIsValueSplit" runat="server" AutoPostBack="true" EnableViewState="true"
                                                OnSelectedIndexChanged="dropdownIsValueSplit_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtValueToSplit" placeholder="Enter char to split value" ValidationGroup="text"
                                                runat="server" Width="240px" MaxLength="50" class="form-control" Style="display: inline-block" BorderColor="#bbd3e9" />
                                            <asp:Label ID="lblIndex" runat="server" Style="margin-left: 1%; margin-right: 1%" Text="Split Index :" />
                                            <asp:DropDownList ID="dropdownValueIndex" runat="server" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: .5em; padding-bottom: .5em">
                                        <asp:Panel ID="pnlIsDefaultValue" runat="server">
                                            <asp:Label ID="lblIsDefaultValue" runat="server" Text="Default Value : "></asp:Label>
                                            <asp:CheckBox ID="checkIsDefaultValue" runat="server" Checked="false" AutoPostBack="true"
                                                EnableViewState="true" OnCheckedChanged="chkDefaultValueCheck_Clicked"></asp:CheckBox>

                                            <asp:Label ID="lblValuetype" Style="margin-left: 2%;" runat="server" Text="Value type : "></asp:Label>
                                            <asp:DropDownList ID="dropdownIsDefaultValue" runat="server" AutoPostBack="true"
                                                EnableViewState="true" OnSelectedIndexChanged="dropdownIsDefaultValue_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtDefaultValue" placeholder="Enter default value" ValidationGroup="text"
                                                runat="server" Style="display: inline-block" Width="240px"
                                                MaxLength="50" class="form-control" BorderColor="#bbd3e9" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td colspan="4">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%" style="border: 1px solid #4090fd;">
                            <asp:GridView ID="GridSplitDetail" runat="server" EnableViewState="true" Width="100%" AutoGenerateColumns="False"
                                GridLines="None" DataKeyNames="Id" OnRowEditing="GridSplitDetail_RowEditing" OnRowUpdating="GridSplitDetail_RowUpdating"
                                OnRowDeleting="GridSplitDetail_RowDeleting">
                                <HeaderStyle CssClass="ListHeaderGrid" HorizontalAlign="Left" BorderColor="#bbd3e9"
                                    BackColor="#e5eef6" />
                                <RowStyle CssClass="ListRowGrid" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" Height="40" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            S No.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            <asp:HiddenField ID="hiddenSplitId" runat="server" Value='<%# Eval("id") %>'></asp:HiddenField>
                                            <asp:HiddenField ID="hiddenColumnHeaderId" runat="server" Value='<%# Eval("columnHeaderId") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Lead Column
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadColumnHeader" runat="server" Text='<%# Eval("leadColumnHeader") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Start Text
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartText" runat="server" Text='<%# Eval("startText") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            End Text
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndText" runat="server" Text='<%# Eval("endText") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Split Value 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:CheckBox ID="lblIsValueSplit1" runat="server" Checked='<%# Eval("IsValueSplit").ToString().Equals("1") %>' Width="70%" Enabled="false" CssClass="txtbox"></asp:CheckBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Split By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsValueSplit" runat="server" Text='<%# Eval("SplitType") %>' Width="70%"
                                                CssClass="txtbox"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Split Value
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSplitValueText" runat="server" Text='<%# Eval("splitValueText") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Split Index
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="dropdownSplitIndex" runat="server" Text='<%# Eval("SplitIndex") %>'
                                                Width="70%" CssClass="txtbox"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            IsDefault Value
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display: inline-block;">
                                                <asp:CheckBox ID="lblisHaveDefaultValue" runat="server" Checked='<%# Eval("isHaveDefaultValue").ToString().Equals("1") %>' Width="70%" Enabled="false" CssClass="txtbox"></asp:CheckBox>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Default Value Type
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldefaultValueType" runat="server" Text='<%# Eval("defaultValueType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Default Value
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldefaultValue" runat="server" Text='<%# Eval("defaultValue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/Images/edit.png" CancelImageUrl="~/Images/cancel_new.png"
                                        DeleteImageUrl="~/Images/delete.png" UpdateImageUrl="~/Images/save.png" ShowCancelButton="false"
                                        ShowDeleteButton="true" ShowEditButton="true" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Right"
                                        HeaderText="Action">
                                        <ItemStyle HorizontalAlign="center" Width="100px"></ItemStyle>
                                        <HeaderStyle CssClass="commondClass" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
    </div>
</asp:Content>
