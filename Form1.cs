using FastReport;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsFRep.Model;
using WindowsFormsFRep.Service;

namespace WindowsFormsFRep
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBaseAccessService dataBaseAccessService = new DataBaseAccessService();
            List<Fields> result = dataBaseAccessService.SelectFieldsFromDateBase(dateTimePicker1.Value.ToString("dd'/'MM'/'yyyy"), dateTimePicker2.Value.ToString("dd'/'MM'/'yyyy"));

            Report report = new Report();
            report.RegisterData(result, "fields");
            //report.Design();
            report.Load(@"c:\Users\user\source\repos\WindowsFormsFRep\Rest1.frx");
            report.Prepare();
            FastReport.Export.Image.ImageExport export = new FastReport.Export.Image.ImageExport();
            //if (export.ShowDialog())
            export.Export(report, @"c:\Users\user\source\repos\WindowsFormsFRep\Rest.jpg");
            report.Show();
        }
    }
}
