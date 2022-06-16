<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QLD._Default" %>

<%@ Register Assembly="DevExpress.XtraReports.v16.1.Web, Version=16.1.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="title" >
        <asp:Label ID="title" runat="server" Text="QUẢN LÝ ĐIỂM SINH VIÊN" ></asp:Label>
    </h1>

   <div id="table-content">
        <p class="titleSelectTable">
            <asp:Label ID="chooseTables" runat="server" Font-Bold="true" Text="Chọn bảng tham gia vào báo cáo"></asp:Label>
        </p>
        <div class="selectTable" >
            <asp:CheckBoxList ID="CheckBoxListTable" runat="server" ForeColor="Black" OnSelectedIndexChanged="checkBoxListIndexTable" RepeatDirection="Horizontal" RepeatLayout="Table">
            </asp:CheckBoxList>
        </div>
   </div>

    <div id="column-Content" >
        <asp:Panel ID="PanelChooseRow" runat="server" > 
            <asp:Label ID="LabelChooseRow" runat="server" Text="Chọn các cột tham gia vào báo cáo" Font-Bold="True" ></asp:Label>
            <br />
            
            <div class="selectColumn">
                <asp:CheckBoxList ID="CheckBoxListColumn" runat="server" OnSelectedIndexChanged="CheckBoxListIndexColumn" RepeatDirection="Horizontal" RepeatLayout="Table">
                </asp:CheckBoxList>
            </div>
            <br />
        </asp:Panel>
    </div>
    <br />
    <div id="query-content">
        <asp:Panel ID="PanelGridViewColumn" runat="server"  >
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" Height="16px" ForeColor="#333333" GridLines="None"   >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <%--<asp:CheckBox ID="CheckAll" AutoPostBack="true" runat="server" Text="Chọn tất cả" />--%>
                            <asp:Label ID="orderSelect" runat="server" Text="Thứ tự Select"></asp:Label>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <div class="columnOrder">
                                <asp:CheckBox ID="CheckBoxOrder" runat="server" Checked="False" />
                                <span class="textOrder"></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Chọn hàm" >
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownListFunction" AutoPostBack="true" runat="server">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                              <%--  <asp:ListItem Text="SORT ASC" Value="ASC"></asp:ListItem>
                                <asp:ListItem Text="SORT DESC" Value="DESC"></asp:ListItem>--%>
                                <asp:ListItem Text="COUNT" Value="COUNT"></asp:ListItem>
                                <asp:ListItem Text="MIN" Value="MIN"></asp:ListItem>                                     
                                <asp:ListItem Text="MAX" Value="MAX"></asp:ListItem>
                                <asp:ListItem Text="SUM" Value="SUM"></asp:ListItem>
                                <asp:ListItem Text="AVG" Value="AVG"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>           
              
                   
                    <asp:TemplateField HeaderText="Điều Kiện">                        
                            <ItemTemplate>
                                <div class="condition">
                                    <asp:TextBox ID="TextBoxDieuKien" placeholder="vd: >5 hoặc !='T01'" runat="server" ></asp:TextBox>
                                </div>
                             </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Đổi tên cột">
                        <ItemTemplate>
                            <div class="renameColumn">
                                <asp:TextBox ID="TextBoxRename" runat="server"></asp:TextBox>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Left" Font-Bold="True"/>
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White"/>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"/>
                <RowStyle ForeColor="#333333" BackColor="#F7F6F3"/>
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="true"/>
                <SortedAscendingCellStyle BackColor="#007DBB"/>
                <SortedAscendingHeaderStyle BackColor="#506C8C"/>
                <SortedAscendingCellStyle BackColor="#007DBB"/>
                <SortedDescendingCellStyle BackColor="#FFFDF8"/>
                <SortedDescendingHeaderStyle BackColor="#6F8DAE"/>
            </asp:GridView>
            <br />
            <%--<asp:Label ID="lbltxt" runat="server"></asp:Label>--%>

            <asp:Button ID="ButtonQuery" runat="server" OnClick="ButtonQuery_Click" Text="Tạo QUERY" />
            <span><small>Click here</small></span>
            <br />
            
            <br />
            <div style="display:block; width: 96%; margin: 0 auto;">
                <asp:Label ID="LabelMess" runat="server" ></asp:Label>
                <div class="copyText">
                    <p href="#" onclick="copyToClipboard('#MainContent_LabelMess');">Copy Text</p>

                </div>
            </div>
            <br />
            <asp:Panel ID="Panel1" runat="server">
                <asp:TextBox ID="fixSelect" runat="server" placeholder="Hãy copy đoạn truy vấn muốn in báo cáo và dán vào đây" Height="70px" Width="100%" TextMode="MultiLine" Rows="3" ></asp:TextBox>
            </asp:Panel>
              <br />
            <div class="orderSelect">
                <asp:Label ID="OrderChecked" runat="server" Height="50px" Width="100%"></asp:Label>
            </div>
            <div class="orderSelectChange">
                <small>Đã được sắp xếp</small>
                <asp:Label ID="total" runat="server" ></asp:Label>
                <div class="copyText" style="margin-left:20px;">
                    <p href="#" onclick="copyToClipboard('#MainContent_total');">Copy Text</p>

                </div>
            </div>
            <div class="selectText">
                <asp:Label ID="selectText" runat="server" ></asp:Label>
            </div>        
            
            <br />
            <%--<asp:Button ID="Button1" runat="server" OnClick="dcmm" Text="Button" />--%>
            <br />       
        </asp:Panel>
        <br />
        <div class="infoReport">
            <asp:Panel ID="Panel2" runat="server">
                <div>
                    <span>Tiêu đề báo cáo: </span>
                    <asp:TextBox ID="titleTexbox" placeholder="Nhập tiêu đề in hoa" runat="server"></asp:TextBox>
                </div>
                <div>
                    <span>Người tạo báo cáo: </span>
                    <asp:TextBox ID="Producer" runat="server"></asp:TextBox>
                </div>
            </asp:Panel>
        </div>
        <div id="scanRp" style="display:block; ">
                
                <br />
                <asp:Button ID="ButtonReport" OnClick="CreateReport" OnClientClick="target ='_blank';" runat="server" Text="In báo cáo" />
        </div>
        <%--<div class="reportPage">   
            <dx:ASPxWebDocumentViewer ID="ASPxWebDocumentViewer1" runat="server">
            </dx:ASPxWebDocumentViewer>
        </div>--%>
    </div>
    <script>
        function copyToClipboard(element) {
            var $temp = $("<input>");
            $("body").append($temp);
            $temp.val($(element).text()).select();
            document.execCommand("copy");
            $temp.remove();
        }
    </script>
    
</asp:Content>  

