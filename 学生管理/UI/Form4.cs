using DAL;
using Model;
using System;
using System.Windows.Forms;

namespace UI
{
    public partial class Form4 : Form
    {
        // 编辑的学生信息
        ClassRoom editClassRoom = null;
        // 编辑标识 false添加,true编辑
        bool isEdit = false;
        // 业务逻辑层
        ClassRoomDAL classRoomDAL = new ClassRoomDAL();
        public Form4(bool _isEdit, ClassRoom _editClassRoom)
        {
            InitializeComponent();
            this.isEdit = _isEdit;
            this.editClassRoom = _editClassRoom;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // 编辑
            if (isEdit)
            {
                this.Text = "编辑班级";
                this.btnSave.Text = "保存";

                // 编辑的班级信息先展示
                textBox1.Text = editClassRoom.ClassRoomName;
            }
            else
            {
                this.Text = "添加班级";
                this.btnSave.Text = "添加";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 简单验证一下
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("班级名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 编辑
            if (isEdit)
            {
                // 给编辑学生重新赋值
                editClassRoom.ClassRoomName = textBox1.Text;

                if (classRoomDAL.Update(editClassRoom))
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
                ClassRoom model = new ClassRoom()
                {
                    ClassRoomId = 1,
                    ClassRoomName = textBox1.Text
                };
                bool result = classRoomDAL.Add(model);
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
