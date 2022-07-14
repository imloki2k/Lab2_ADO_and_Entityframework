using Lab2_DAO.DataAccess;
using Lab2_DAO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_DAO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDataToForm();
            loadDataToDGV();
        }


        private void LoadDataToForm()
        {
            cbSubjects.Items.Add("All Subjects");
            foreach(var s in SubjectDAO.GetAllSubjects())
            {
                cbSubjects.Items.Add(s.SubjectCode);
                cbSubject.Items.Add(s.SubjectCode);
            }
            cbSubjects.SelectedIndex = 0;


            cbInstructors.Items.Add("All Instructors");
            foreach(var i in InstructorDAO.GetInstructors())
            {
                cbInstructor.Items.Add(i.InstructorFirstName + " " + i.InstructorMidName + " " + i.InstructorLastName);
                cbInstructors.Items.Add(i.InstructorFirstName + " " + i.InstructorMidName + " " + i.InstructorLastName);
            }
            cbInstructors.SelectedIndex = 0;

            foreach(var t in TermDAO.GetAllTerms())
            {
                cbTerm.Items.Add(t.TermName);
            }

            foreach (var c in CampusDAO.GetAllCampuses())
            {
                cbCampus.Items.Add(c.CampusName);
            }

        }

        public void loadDataToDGV()
        {
            if(cbSubjects.SelectedIndex == 0 && cbInstructors.SelectedIndex == 0)
            {
                dataGridView1.DataSource = CourseDAO.GetAllCourses();
            }else if(cbSubjects.SelectedIndex != 0 && cbInstructors.SelectedIndex == 0)
            {
                dataGridView1.DataSource = CourseDAO.GetCoursesBySubjectCode(cbSubjects.Text);
            }else if(cbSubjects.SelectedIndex == 0 && cbInstructors.SelectedIndex != 0)
            {
                dataGridView1.DataSource = CourseDAO.GetCoursesByInstructorName(cbInstructors.SelectedIndex);
            }
        }

        private void cbSubjects_SelectedValueChanged(object sender, EventArgs e)
        {
            loadDataToDGV();
        }

        private void cbInstructors_SelectedValueChanged(object sender, EventArgs e)
        {
            loadDataToDGV();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tCourseCode.Text = "";
            tDescription.Text = "";
            cbSubject.Text = "";
            cbInstructor.Text = "";
            cbTerm.Text = "";
            cbCampus.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int cid = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
            int k = CourseDAO.DeleteCourseById(cid);
            if(k == 0)
            {
                MessageBox.Show("Delete Failed");
            }
            else
            {
                dataGridView1.DataSource = CourseDAO.GetAllCourses();
                MessageBox.Show("Delete Successful");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                tCourseCode.Text = row.Cells[1].Value.ToString();
                tDescription.Text = row.Cells[2].Value.ToString();
                cbSubject.SelectedItem = row.Cells[3].Value.ToString();
                cbInstructor.SelectedItem = row.Cells[4].Value.ToString();
                cbTerm.SelectedItem = row.Cells[5].Value.ToString();
                cbCampus.SelectedItem = row.Cells[6].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string code = tCourseCode.Text;
            string description = tDescription.Text;
            int sid = SubjectDAO.GetSubjectBySubjecCode(cbSubject.Text).SubjectId;
            int iid = cbInstructor.SelectedIndex + 1;
            int tid = TermDAO.GetTermByTermName(cbTerm.Text).TermId;
            int cid = CampusDAO.GetCampusByCampusName(cbCampus.Text).CampusId;
            Course c = new Course(code,description,sid,iid,tid,cid);
            int k = CourseDAO.InsertCourse(c);
            if(k == 0)
            {
                MessageBox.Show("Add Failed");
            }
            else
            {
                dataGridView1.DataSource = CourseDAO.GetAllCourses();
                MessageBox.Show("Add Successful");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
            string code = tCourseCode.Text;
            string description = tDescription.Text;
            int sid = SubjectDAO.GetSubjectBySubjecCode(cbSubject.Text).SubjectId;
            int iid = cbInstructor.SelectedIndex + 1;
            int tid = TermDAO.GetTermByTermName(cbTerm.Text).TermId;
            int cid = CampusDAO.GetCampusByCampusName(cbCampus.Text).CampusId;
            Course c = new Course(code, description, sid, iid, tid, cid);
            int k = CourseDAO.EditCourse(c, id);
            if (k == 0)
            {
                MessageBox.Show("Edit Failed");
            }
            else
            {
                dataGridView1.DataSource = CourseDAO.GetAllCourses();
                MessageBox.Show("Edit Successful");
            }
        }
    }
}
