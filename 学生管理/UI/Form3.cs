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
    public partial class Form3 : Form
    {
        // 业务逻辑层
        ClassRoomDAL classRoomDAL = new ClassRoomDAL();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            BindDataGridView(ClassRoomDAL.ClassRooms);
        }

        private void BindDataGridView(List<ClassRoom> list)
        {
            dataGridView1.DataSource = list.ToList();

            dataGridView1.Columns["ClassRoomId"].HeaderText = "编号";
            dataGridView1.Columns["ClassRoomName"].HeaderText = "班级名称";

            // 先创建操作列
            if (!dataGridView1.Columns.Contains("OperateColumn"))
            {
                DataGridViewLinkColumn column1 = new DataGridViewLinkColumn();
                column1.HeaderText = "操作";
                column1.Name = "OperateColumn";
                column1.Width = 150;
                dataGridView1.Columns.Add(column1);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(false, null);
            DialogResult dr = form4.ShowDialog();
            if (dr == DialogResult.OK)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ClassRoom> list;
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                list = classRoomDAL.Search(txtName.Text);
            }
            else
            {
                list = ClassRoomDAL.ClassRooms;
            }

            BindDataGridView(list);
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 保证绘制的文本按钮在数据行上，e.ColumnIndex > 0网格拥有列  e.RowIndex > 0网格拥有行
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                // 进一步缩小绘制范围，在操作列
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "操作")
                {
                    // 1。准备文字的格式化
                    StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;//设置重绘入单元格的字体样式
                    sf.Alignment = StringAlignment.Center; // 文本水平居中对齐
                    sf.LineAlignment = StringAlignment.Center; // 文本垂直居中对齐

                    // 2。重绘单元格边框   Cell单元格  Bound边界
                    e.PaintBackground(e.CellBounds, false);

                    // 3。定义字体
                    Font font = new Font("宋体", 9);

                    // 4。准备文本绘制区域
                    SizeF sizeEdit = e.Graphics.MeasureString("编辑", font);// 文本大小
                    SizeF sizeDel = e.Graphics.MeasureString("删除", font);


                    // 绘制文本占两个文本宽度比率
                    float fEdit = sizeEdit.Width / (sizeEdit.Width + sizeDel.Width);
                    float fDel = sizeDel.Width / (sizeEdit.Width + sizeDel.Width);

                    // 矩形区域：单元格的一半区域
                    RectangleF rectEdit = new RectangleF(
                        e.CellBounds.Left,
                        e.CellBounds.Top, e.CellBounds.Width * fEdit, e.CellBounds.Height);

                    RectangleF rectDel = new RectangleF(
                        rectEdit.Right,
                        e.CellBounds.Top, e.CellBounds.Width * fDel, e.CellBounds.Height);

                    // 5。添加背景
                    RectangleF rectEdit2 = new RectangleF(e.CellBounds.Left + 5, e.CellBounds.Top + 5, e.CellBounds.Width * fEdit - 5, e.CellBounds.Height - 10);
                    e.Graphics.FillRectangle(Brushes.Aqua, rectEdit2);

                    // e.Graphics拿图形绘制单元（拿画布，画板）
                    // DrawString()画板拥有某一个具体的功能，画文本的。Draw绘制，画
                    // 参数1：文本信息 参数2：文本字体 参数3：文本画笔（颜色，粗细） 参数4：画的文本放置的位置（点）
                    e.Graphics.DrawString("编辑", font, Brushes.Black, rectEdit, sf);
                    e.Graphics.DrawString("删除", font, Brushes.Red, rectDel, sf);

                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "操作")
                {
                    // 1。当前鼠标在当前单元格中的坐标
                    Point curPosition = e.Location;

                    Graphics g = dataGridView1.CreateGraphics();
                    Font font = new Font("宋体", 9);
                    // 操作列总宽高
                    SizeF sizeEdit = g.MeasureString("编辑", font);// 文本大小
                    SizeF sizeDel = g.MeasureString("删除", font);

                    // 绘制文本占两个文本宽度比率
                    float fEdit = sizeEdit.Width / (sizeEdit.Width + sizeDel.Width);
                    float fDel = sizeDel.Width / (sizeEdit.Width + sizeDel.Width);

                    // 矩形区域：单元格的一半区域
                    Rectangle rectTotal = new Rectangle(0, 0, this.dataGridView1.Columns[e.ColumnIndex].Width, this.dataGridView1.Rows[e.RowIndex].Height); // 操作列某行对应的单元格宽高

                    // 2。最主要拿到编辑和删除这两个区域。目的：判断鼠标点击的位置是否在此区域内
                    RectangleF rectEdit = new RectangleF(
                        rectTotal.Left, rectTotal.Top, rectTotal.Width * fEdit, rectTotal.Height);
                    RectangleF rectDel = new RectangleF(
                        rectEdit.Right, rectTotal.Top, rectTotal.Width * fDel, rectTotal.Height);

                    if (rectEdit.Contains(curPosition))//编辑
                        Edit(e.RowIndex);
                    else if (rectDel.Contains(curPosition))//删除
                        Remove(e.RowIndex);

                }
            }
        }

        private void Remove(int rowIndex)
        {
            int id = (int)dataGridView1.Rows[rowIndex].Cells["ClassRoomId"].Value;

            DialogResult dr = MessageBox.Show($"你确定要删除id={id}此条数据吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                if (classRoomDAL.Remove(id))
                {
                    btnSearch.PerformClick();
                }
            }
        }

        private void Edit(int rowIndex)
        {
            int id = (int)dataGridView1.Rows[rowIndex].Cells["ClassRoomId"].Value;
            string name = dataGridView1.Rows[rowIndex].Cells["ClassRoomName"].Value.ToString();

            // 准备要编辑的班级信息
            ClassRoom editClassRoom = new ClassRoom()
            {
                ClassRoomId = id,
                ClassRoomName = name,
            };
            // 打开编辑界面，把编辑信息传递到新窗体
            Form4 form4 = new Form4(true, editClassRoom);
            DialogResult dr = form4.ShowDialog();
            if (dr == DialogResult.OK)
            {
                btnSearch.PerformClick();
            }
        }
    }
}
