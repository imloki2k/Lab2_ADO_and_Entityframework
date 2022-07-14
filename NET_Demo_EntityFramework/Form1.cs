using NET_Demo_EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NET_Demo_EntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void LoadDataForDGV()
        {
            using (var context = new APContext())
            {
                dataGridView1.DataSource = context.Instructors
                    .Select(x => new {
                        x.InstructorId,
                        x.InstructorFirstName,
                        x.InstructorLastName,
                        x.Department.DepartmentName
                    })
                    .ToList();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataForDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var context = new APContext())
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int InsId = Convert.ToInt32(row.Cells[0].Value);
                    Instructor i = context.Instructors.FirstOrDefault(x => x.InstructorId == InsId);
                    context.Instructors.Remove(i);
                }
                context.SaveChanges();
                LoadDataForDGV();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            using(var context = new APContext())
            {
                dataGridView1.DataSource = context.Instructors
                    .Where(x => x.InstructorFirstName.Contains(name) || (x.InstructorLastName.Contains(name)))
                    .Select(x => new
                    {
                        x.InstructorId,
                        x.InstructorFirstName,
                        x.InstructorLastName,
                        x.Department.DepartmentName
                    })
                    .ToList();
                    
            }
        }
    }
}
