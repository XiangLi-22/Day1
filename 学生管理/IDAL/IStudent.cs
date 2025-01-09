using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// IDAL类库引用了Model类库。因为IDAL中要使用Model类库中的模型
namespace IDAL
{
    public interface IStudent : IBase<Student>
    {
    }
}
