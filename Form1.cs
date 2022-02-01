using FastReport;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsFRep.Model;
using WindowsFormsFRep.Service;
using System.Drawing;
using Equin.ApplicationFramework;

namespace WindowsFormsFRep
{
    public partial class Form1 : Form
    {
        List<Fields> result;

        public readonly DataBaseAccessService dataBaseAccessService = new DataBaseAccessService();
        public BindingListView<Fields> _fields;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBaseAccessService dataBaseAccessService = new DataBaseAccessService();
            result = dataBaseAccessService.SelectFieldsFromDateBase(dateTimePicker1.Value.ToString("dd'/'MM'/'yyyy"), dateTimePicker2.Value.ToString("dd'/'MM'/'yyyy"));
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            _fields = new BindingListView<Fields>(dataBaseAccessService.SelectFieldsFromDateBase(dateTimePicker1.Value.ToString("dd'/'MM'/'yyyy"), dateTimePicker2.Value.ToString("dd'/'MM'/'yyyy")));
            dataGridView.DataSource = _fields;

            Font F = new Font("Arial", 11);
            int nc = dataGridView.ColumnCount;
            if (nc > 0)
            {
                // установить шрифт и цвет заголовка
                dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Red;
                dataGridView.ColumnHeadersDefaultCellStyle.Font = F;

                dataGridView.Columns[0].HeaderText = "Тип продукции";
                dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridView.Columns[1].HeaderText = "Наименование продукции";
                                            
                dataGridView.Columns[2].DefaultCellStyle.Font = F;
                dataGridView.Columns[2].HeaderText = "Количество проб";
            }
        }
    }
}
