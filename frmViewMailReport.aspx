<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmViewMailReport.aspx.cs"
    Inherits="AdminTool.frmViewMailReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ListRowGrid
        {
            border-bottom: 10px #fff solid;
            background-color: #efefef;
            height: 50px;
            margin-left: 10px;
        }
        
        .ListRowGrid td
        {
            padding-left: 15px;
            vertical-align: middle;
        }
        
        .ListHeaderGrid
        {
            background-color: #BDC9D6;
            height: 50px;
        }
        
        .ListHeaderGrid th
        {
            padding-left: 15px;
            vertical-align: middle;
        }
        .style1
        {
            width: 48px;
        }
        .style2
        {
            float: left;
            width: 535px;
        }
        .style3
        {
            height: 86px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 20px 20px; border: 1px solid #4090fd;">
        <div style="border-bottom: 1px solid #4090fd;">
        <div>
                <table width="100%">
                    <tr>
                        <td style="vertical-align: middle;" class="style1">
                            <asp:ImageButton ID="ImageGoBack" runat="server" ImageUrl="~/Images/goBack.png" OnClientClick="if(!ValidateSearch()) return false;"
                                Style="height: 30px; width: 30px; vertical-align: middle; margin: 5px;" ValidationGroup="text"
                                align="left" OnClick="ImageGoBack_Click" />
                        </td>
                           <td style="vertical-align: middle; margin:1%;" class="style2">
                               <asp:Label ID="lblHeader" runat="server" Text="MAIL REPORT" Style="font-weight: bold; text-align:left;" Font-Size="18" Font-Names="Forum"></asp:Label>
                            </td>
                          <td style="vertical-align: middle; float: right; margin:1%;">
                            <asp:TextBox ID="txtSearchBox" placeholder="Search here" runat="server" Style="width: 200px;
                                height: 25px; vertical-align: middle;" MaxLength="50" CssClass="form-control" AutoPostBack="true" EnableViewState="true" OnTextChanged="txtSearchBox_TextChanged"
                                BorderColor="#bbd3e9" />
                            <asp:ImageButton ID="btnSearch" ValidationGroup="text" runat="server" ImageUrl="~/Images/search_User.png"
                                Style="height: 30px; width: 30px; vertical-align: middle; display: none;" OnClientClick="if(!ValidateSearch()) return false;"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                 <asp:HiddenField ID="hdnSearchTxt" runat="server" />
            </div>
        </div>
        <div style="padding: 20px;">
            <table width="100%">
                 <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                    <td colspan="2" align="right">
                        <div id="DivExport" runat="server">
                            <asp:Button runat="server" ID="ImgExportToExcel" Text="Export EXCEL" OnClick="ImgExportToExcel_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToCSV" Text="Export CSV" OnClick="ImgExportToCSV_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                            <asp:Button runat="server" ID="ImgExportToPDF" Text="Export PDF" OnClick="ImgExportToPDF_Click"
                                CssClass="btn" EnableViewState="false" Width="125" CausesValidation="true" ValidationGroup="Group1"
                                BackColor="#3E75CD" ForeColor="White" />
                        </div>
                    </td>
                </tr>

                 <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div>
                         <asp:Label ID="Label1" runat="server" Text="Select Subject List : "></asp:Label>
                                <asp:DropDownList ID="ListOfAllSubjectList" runat="server">
                                <asp:ListItem Text="All"></asp:ListItem>
                                </asp:DropDownList>
                                 <asp:Label ID="Label3" runat="server" Style="margin-left: 1%; margin-right: 1%" Text="Select Status : "></asp:Label>
                                <asp:DropDownList ID="StatusOfReport" runat="server">
                                 <asp:ListItem Text="All"></asp:ListItem>
                                  <asp:ListItem Text="Is Submitted into CRM"></asp:ListItem>
                                   <asp:ListItem Text="Is Split Completed"></asp:ListItem>
                                </asp:DropDownList>
                       </div>
                       </td>
                   </tr>
                    <tr>
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                   <tr>
                    <td colspan="5" class="style3">
                        <asp:Label ID="Label4" runat="server" Text="Start Date : "></asp:Label>
                        <asp:TextBox runat="server" ID="StartDate" Width="147px" ></asp:TextBox>
                        <%-- <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>--%>
                         <%-- <obout:Calendar runat="server" id="Calendar1" DatePickerMode="true" TextBoxId="txtDate">
                          </obout:Calendar>    
                        --%> 
                        <asp:Label ID="Label5" runat="server" Style="margin-left: 1%; margin-right: 1%" Text="End Date : "></asp:Label>
                        <asp:TextBox runat="server" ID="EndDate"></asp:TextBox>
                        <%-- <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>--%>


                    </td>
                    </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div id="GroupDetails" runat="server" width="100%">
                            <asp:GridView ID="GridViewMailReport" runat="server" Width="101%" AllowPaging="True"
                                AutoGenerateColumns="False" GridLines="None" DataKeyNames="mailId" PageSize="10" OnPageIndexChanging="GridViewMailReport_PageIndexChanging">
                                <HeaderStyle CssClass="ListHeaderGrid" HorizontalAlign="Left" BorderColor="#bbd3e9"
                                    BackColor="#e5eef6" />
                                <RowStyle CssClass="ListRowGrid" />
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" Height="40" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            S No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiddenUserId" runat="server" Value='<%# Eval("mailId") %>'>
                                            </asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Mail Content
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMailContentBody" runat="server" Text='<%# Eval("mail_content_body") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Mail Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMailActualDateTime" runat="server" Text='<%# Eval("mail_actual_date_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Split Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsSplitContentCompleted" runat="server" Text='<%# Eval("is_split_content_completed") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            CRM Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsInfoSubmittedIntoCrm" runat="server" Text='<%# Eval("is_info_submited_into_crm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            CRM Id
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRecordId" runat="server" Text='<%# Eval("record_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="minWidth" />
                                        <HeaderTemplate>
                                            Submit Status
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrmSubmitDescription" runat="server" Text='<%# Eval("crm_submit_description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="Label2" runat="server" Text="No Record Found" ForeColor="Red"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
           </div>
        </div>
 </asp:Content>
