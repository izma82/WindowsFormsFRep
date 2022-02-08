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
            //вариант для другого запроса
            // result = dataBaseAccessService.SelectFieldsFromDateBase(dateTimePicker1.Value.ToString("dd'/'MM'/'yyyy"), dateTimePicker2.Value.ToString("dd'/'MM'/'yyyy"));
            result = dataBaseAccessService.SelectFieldsFromDateBase();

            //вывод в fastreport
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
            //вариант для другого запроса
            //_fields = new BindingListView<Fields>(dataBaseAccessService.SelectFieldsFromDateBase(dateTimePicker1.Value.ToString("dd'/'MM'/'yyyy"), dateTimePicker2.Value.ToString("dd'/'MM'/'yyyy")));

            //1-й вариант 
            _fields = new BindingListView<Fields>(dataBaseAccessService.SelectFieldsFromDateBase());
            dataGridView.DataSource = _fields;

            //2-й вариант
            //this._fields = new BindingListView<Fields>(dataBaseAccessService.SelectFieldsFromDateBase());
            //this.bindingSource1.DataSource = this._fields;
            //this.dataGridView.DataSource = this.bindingSource1;

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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Удалить строку?", "Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int currentRow = dataGridView.CurrentRow.Index; //текущий индекс строки                                                                                                                            
                string selectedCalc = dataGridView.Rows[currentRow].Cells[0].Value.ToString();
                int deleteRow = dataBaseAccessService.DeleteFromDatabase(selectedCalc);
                _fields.DataSource.RemoveAt(currentRow);  
                _fields.Refresh();
                dataGridView.Refresh();
                ShowMessageBox("Строка была успешно удалена");
            }
            else
            {
                ShowMessageBox("Строка не была удалена");
            }     
        }

        public void ShowMessageBox(string message)
        {
            string caption = "delete";
            MessageBox.Show(
        message,
        caption,
        MessageBoxButtons.OKCancel,
        MessageBoxIcon.Information
        );
        }
    }
}
