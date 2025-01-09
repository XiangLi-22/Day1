using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DAL要引用IDAL。因为DAL中的类要实现IDAL中的接口
// DAL要引用Model。因为DAL类中方法的形参有使用到实体
namespace DAL
{
    public class StudentDAL : IStudent
    {
        /// <summary>
        /// 学生列表，将来使用数据库时，就不需要了。
        /// </summary>
        public static List<Student> Students { get; set; } = new List<Student>() {
            new Student(){ Id = 1,Name = "张三", Age = 20},
            new Student(){ Id = 2,Name = "李四", Age = 21}
        };
        public bool Add(Student model)
        {
            try
            {
                if (Students.Count > 0)
                {
                    Student lastStudent = Students[Students.Count - 1];
                    model.Id = lastStudent.Id + 1;
                }

                Students.Add(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Student GetModel(int id)
        {
            // 拉姆达表达，形参列表只有一个参数时，可以省略小括号
            // 拉姆达表达，方法体中只有一行语句时，可以省略{}和;和return
            //Students.Find((s) => { return s.Id == id; });
            return Students.Find(s => s.Id == id);
        }

        public bool Remove(int id)
        {
            return Remove(GetModel(id));
        }

        public bool Remove(Student model)
        {
            try
            {
                Students.Remove(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Student> Search(string name)
        {
            return Students.FindAll(s => s.Name.Contains(name));
        }

        public bool Update(Student model)
        {
            // 得到要修改的学生在原列表中的索引
            int i = Students.FindIndex(s => s.Id == model.Id);
            if (i != -1)
            {
                Students[i].Id = model.Id;
                Students[i].Name = model.Name;
                Students[i].Age = model.Age;
                return true;
            }
            return false;
        }
    }
}
