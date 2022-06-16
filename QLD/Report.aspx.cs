
using DevExpress.XtraReports.Data;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

namespace QLD
{
    public partial class Report : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string qr = Session["test"].ToString();
            
            XtraReport xtraRP = new XtraReport();
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            String connect = "Data Source=DESKTOP-6O3U2OP;Initial Catalog=Quanlydiem;User ID=sa;Password=123";
            cnn.ConnectionString = connect;
            cnn.Open();
            DataSet dt = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(qr, cnn);
            da.Fill(dt);
            xtraRP.DataSource = dt;
            InitBands(xtraRP);
            InitDetailsBaseXRTable(xtraRP, dt);
            ASPxWebDocumentViewer2.OpenReport(xtraRP);
            
        }
        
        public void InitBands(XtraReport rep)
        {
            DetailBand detail = new DetailBand();
            PageHeaderBand pageHeader = new PageHeaderBand();
            ReportHeaderBand reportHeader = new ReportHeaderBand();
            ReportFooterBand reportFooter = new ReportFooterBand();

            reportHeader.HeightF = 40;
            detail.HeightF = 20;
            reportFooter.HeightF = 380;
            pageHeader.HeightF = 20;
            rep.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] { reportHeader, detail, pageHeader, reportFooter });
        }
        public void InitDetailsBaseXRTable(XtraReport rep, DataSet ds)
        {
            ds = ((DataSet)rep.DataSource);
            int colCount = ds.Tables[0].Columns.Count;
            int colWidth = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right)) / colCount;
            rep.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
            XRLabel title = new XRLabel();
            title.Text =Session["title"].ToString();         
            title.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            title.ForeColor = Color.DarkBlue;
            title.Font = new Font("Tahoma", 20, FontStyle.Bold, GraphicsUnit.Pixel);
            title.Width = Convert.ToInt32(rep.PageWidth - 50);

            // Create a table to represent headers
            XRTable tableHeader = new XRTable();
            tableHeader.Height = 40;
            tableHeader.BackColor = Color.Gray;
            tableHeader.ForeColor = Color.White;
            tableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            tableHeader.Font = new Font("Tahoma", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            tableHeader.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right));
            tableHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100.0F);
            XRTableRow headerRow = new XRTableRow();
            headerRow.Width = tableHeader.Width;
            tableHeader.Rows.Add(headerRow);
            tableHeader.BeginInit();

            /*Create a table to display data*/
            XRTable tableDetail = new XRTable();
            tableDetail.Height = 20;
            tableDetail.Width = (rep.PageWidth - (rep.Margins.Left + rep.Margins.Right));
            tableDetail.Font = new Font("Tahoma", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            XRTableRow detailRow = new XRTableRow();
            detailRow.Width = tableDetail.Width;
            tableDetail.Rows.Add(detailRow);
            tableDetail.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 5, 5, 100.0F);
            tableDetail.BeginInit();
            /*Create table cells, fill the header cells with text, bind the cells to data*/
            for (int i = 0; i < colCount; i++)
            {
                XRTableCell headerCell = new XRTableCell();
                headerCell.Text = ds.Tables[0].Columns[i].Caption;
                XRTableCell detailCell = new XRTableCell();
                detailCell.DataBindings.Add("Text", null, ds.Tables[0].Columns[i].Caption);
                if (i == 0)
                {
                    headerCell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                }
                else
                {
                    headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                }

                if (i == 0)
                {
                    headerCell.Width = 50;
                    detailCell.Width = 50;
                    detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                }
                else if (i == 1)
                {
                    headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    headerCell.Width = 130;
                    detailCell.Width = 130;
                }
                else if (i == 2)
                {
                    headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    headerCell.Width = 70;
                    detailCell.Width = 70;
                }
                else if (i == 4)
                {
                    headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    headerCell.Width = 145;
                    detailCell.Width = 145;
                }
                else if (i == 5)
                {
                    headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    headerCell.Width = 115;
                    detailCell.Width = 115;
                }
                else
                {
                    headerCell.Width = colWidth;
                    detailCell.Width = colWidth;
                }
                detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;

                /*Place the cells into the corresponding tables*/
                headerRow.Cells.Add(headerCell);
                detailRow.Cells.Add(detailCell);
            }
            XRLabel date_time = new XRLabel();
            date_time.Text ="Ngày tạo báo cáo: "+ DateTime.Now.ToShortDateString();
            date_time.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
            date_time.ForeColor = Color.Orange;
            date_time.Font = new Font("Tahoma", 13, FontStyle.Bold, GraphicsUnit.Pixel);
            date_time.Width = Convert.ToInt32(rep.PageWidth - 50);

            XRLabel producer = new XRLabel();
            producer.Text = "Người tạo báo cáo: "+ Session["Producer"].ToString(); 
            producer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            producer.ForeColor = Color.Orange;
            producer.Font = new Font("Tahoma", 13, FontStyle.Bold, GraphicsUnit.Pixel);
            producer.Width = Convert.ToInt32(rep.PageWidth - 50);


            tableHeader.EndInit();
            tableDetail.EndInit();
            /*Place the table onto a report's Detail band*/
            rep.Bands[BandKind.ReportHeader].Controls.Add(title);  
            rep.Bands[BandKind.PageHeader].Controls.Add(tableHeader);
            rep.Bands[BandKind.Detail].Controls.Add(tableDetail);
            rep.Bands[BandKind.ReportFooter].Controls.Add(date_time);
            rep.Bands[BandKind.ReportFooter].Controls.Add(producer);


        }


    }
}