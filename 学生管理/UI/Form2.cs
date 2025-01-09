using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Form2 : Form
    {
        // 编辑的学生信息
        Student editStudent = null;
        // 编辑标识 false添加,true编辑
        bool isEdit = false;
        // 业务逻辑层
        StudentDAL studentDAL = new StudentDAL();
        public Form2(bool _isEdit, Student _editStudent)
        {
            InitializeComponent();
            this.isEdit = _isEdit;
            this.editStudent = _editStudent;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 编辑
            if (isEdit)
            {
                this.Text = "编辑学生";
                this.btnSave.Text = "保存";

                // 编辑的学生信息先展示
                textBox1.Text = editStudent.Name;
                numericUpDown1.Value = editStudent.Age;
            }
            else
            {
                this.Text = "添加学生";
                this.btnSave.Text = "添加";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 简单验证一下
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("学生姓名不能为空！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            // 编辑
            if (isEdit)
            {
                // 给编辑学生重新赋值
                editStudent.Name = textBox1.Text;
                editStudent.Age = (int)numericUpDown1.Value;

                if (studentDAL.Update(editStudent))
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                Student model = new Student()
                {
                    Id = 1,
                    Name = textBox1.Text,
                    Age = (int)numericUpDown1.Value
                };
                bool result = studentDAL.Add(model);
                if (result)
                {
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                };
            }

            // 设置对话框结果，目的在Form1中打开Form2时能够拿到此结果，判断是否添加和编辑成功。
            DialogResult = DialogResult.OK;
        }


    }
}
