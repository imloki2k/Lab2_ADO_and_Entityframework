using Lab2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDatatoForm();

        }

        void LoadDatatoForm()
        {
            cbSubjects.Items.Add("All Subjects");
            using (var context = new APContext())
            {
                foreach (var s in context.Subjects)
                {
                    string subject = s.SubjectCode + "";
                    cbSubjects.Items.Add(subject);
                    cbSubject.Items.Add(subject);
                }
                cbSubjects.SelectedIndex = 0;
            }


            cbInstructors.Items.Add("All Instructors");
            using (var context = new APContext())
            {
                foreach (var i in context.Instructors)
                {
                    string instructorName = i.InstructorLastName + " " + i.InstructorMidName + " " + i.InstructorFirstName;
                    cbInstructor.Items.Add(instructorName);
                    cbInstructors.Items.Add(instructorName);

                }
                cbInstructors.SelectedIndex = 0;
            }

            using (var context = new APContext())
            {
                foreach (var t in context.Terms)
                {
                    string term = t.TermName + "";
                    cbTerm.Items.Add(term);
                }
            }

            using (var context = new APContext())
            {
                foreach (var c in context.Campuses)
                {
                    string campus = c.CampusName + "";
                    cbCampus.Items.Add(campus);
                }
            }
        }

        void loadDataToDGV()
        {
            using (var context = new APContext())
            {
                if (cbSubjects.SelectedIndex == 0 && cbInstructors.SelectedIndex != 0)
                {
                    dataGridView.DataSource = context.Courses.Select(
                    x => new
                    {
                        CourseID = x.CourseId,
                        CourseCode = x.CourseCode,
                        Description = x.CourseDescription,
                        Subject = x.Subject.SubjectCode,
                        Instructor = x.Instructor.InstructorLastName + " " + x.Instructor.InstructorMidName + " " + x.Instructor.InstructorFirstName,
                        Term = x.Term.TermName,
                        Campus = x.Campus.CampusName,
                    }).Where(x => x.Instructor == cbInstructors.SelectedItem)
                    .ToList();
                }
                else if (cbSubjects.SelectedIndex != 0 && cbInstructors.SelectedIndex == 0)
                {
                    dataGridView.DataSource = context.Courses.Select(
                    x => new
                    {
                        CourseID = x.CourseId,
                        CourseCode = x.CourseCode,
                        Description = x.CourseDescription,
                        Subject = x.Subject.SubjectCode,
                        Instructor = x.Instructor.InstructorLastName + " " + x.Instructor.InstructorMidName + " " + x.Instructor.InstructorFirstName,
                        Term = x.Term.TermName,
                        Campus = x.Campus.CampusName,
                    }).Where(x => x.Subject == cbSubjects.SelectedItem)
                    .ToList();
                }
                else if (cbSubjects.SelectedIndex == 0 && cbInstructors.SelectedIndex == 0)
                {
                    dataGridView.DataSource = context.Courses.Select(
                    x => new
                    {
                        CourseID = x.CourseId,
                        CourseCode = x.CourseCode,
                        Description = x.CourseDescription,
                        Subject = x.Subject.SubjectCode,
                        Instructor = x.Instructor.InstructorLastName + " " + x.Instructor.InstructorMidName + " " + x.Instructor.InstructorFirstName,
                        Term = x.Term.TermName,
                        Campus = x.Campus.CampusName,
                    })
                    .ToList();
                }
                else
                {
                    dataGridView.DataSource = context.Courses.Select(
                 x => new
                 {
                     CourseID = x.CourseId,
                     CourseCode = x.CourseCode,
                     Description = x.CourseDescription,
                     Subject = x.Subject.SubjectCode,
                     Instructor = x.Instructor.InstructorLastName + " " + x.Instructor.InstructorMidName + " " + x.Instructor.InstructorFirstName,
                     Term = x.Term.TermName,
                     Campus = x.Campus.CampusName,
                 }).Where(x => x.Subject == cbSubjects.SelectedItem && x.Instructor == cbInstructors.SelectedItem)
                 .ToList();
                }
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
            tCourses.Text = "";
            tDescription.Text = "";
            cbSubject.Text = "";
            cbInstructor.Text = "";
            cbTerm.Text = "";
            cbCampus.Text = "";
        }


        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];

                tCourses.Text = row.Cells[1].Value.ToString();
                tDescription.Text = row.Cells[2].Value.ToString();
                cbSubject.SelectedItem = row.Cells[3].Value.ToString();
                cbInstructor.SelectedItem = row.Cells[4].Value.ToString();
                cbTerm.SelectedItem = row.Cells[5].Value.ToString();
                cbCampus.SelectedItem = row.Cells[6].Value.ToString();
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {

            using (var context = new APContext())
            {
                int sID = 0;
                int iID = 0;
                int tID = 0;
                int cID = 0;

                foreach (var s in context.Subjects)
                {
                    if (s.SubjectCode == cbSubject.Text)
                    {
                        sID = s.SubjectId;
                    }
                }
                foreach (var i in context.Instructors)
                {
                    string instructorName = i.InstructorLastName + " " + i.InstructorMidName + " " + i.InstructorFirstName;
                    if (instructorName == cbInstructor.Text)
                    {
                        iID = i.InstructorId;
                    }
                }
                foreach (var t in context.Terms)
                {
                    if (t.TermName == cbTerm.Text)
                    {
                        tID = t.TermId;
                    }
                }
                foreach (var c in context.Campuses)
                {
                    if (c.CampusName == cbCampus.Text)
                    {
                        cID = c.CampusId;
                    }
                }
                
                if(dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    int courseID = Convert.ToInt32(row.Cells[0].Value);
                    Course course = context.Courses.FirstOrDefault(x => x.CourseId == courseID);
                    course.CourseCode = tCourses.Text;
                    course.CourseDescription = tDescription.Text;
                    course.SubjectId = sID;
                    course.InstructorId = iID;
                    course.TermId = tID;
                    course.CampusId = cID;
                    context.SaveChanges();
                    MessageBox.Show("Edit successful!");
                }
                loadDataToDGV();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int subjectID = 0;
            int instructorID = 0;
            int termID = 0;
            int campusID = 0;
            using (var context = new APContext())
            {
                foreach (var s in context.Subjects)
                {
                    if (s.SubjectCode == cbSubject.Text)
                    {
                        subjectID = s.SubjectId;
                    }
                }
                foreach (var i in context.Instructors)
                {
                    string instructorName = i.InstructorLastName + " " + i.InstructorMidName + " " + i.InstructorFirstName;
                    if (instructorName == cbInstructor.Text)
                    {
                        instructorID = i.InstructorId;
                    }
                }
                foreach (var t in context.Terms)
                {
                    if (t.TermName == cbTerm.Text)
                    {
                        termID = t.TermId;
                    }
                }
                foreach (var c in context.Campuses)
                {
                    if (c.CampusName == cbCampus.Text)
                    {
                        campusID = c.CampusId;
                    }
                }

                context.Courses.Add(new Course(
                    tCourses.Text,
                    tDescription.Text,
                    subjectID,
                    instructorID,
                    termID,
                    campusID));

                context.SaveChanges();
                MessageBox.Show("Add successful!");
                loadDataToDGV();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            using (var context = new APContext())
            {
                if(dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView.SelectedRows[0];
                    int courseId = Convert.ToInt32(row.Cells[0].Value.ToString());
                    Course course = context.Courses.FirstOrDefault(c => c.CourseId == courseId);
                    context.Courses.Remove(course);
                    MessageBox.Show("Delete successful!");
                }
                context.SaveChanges();
                loadDataToDGV();
            }
        }

    }

}
