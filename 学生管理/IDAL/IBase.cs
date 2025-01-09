using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    // 基接口：业务逻辑中使用的其他接口，都继承此接口（都在此接口的基础上进行扩展）
    public interface IBase<T>
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>布尔值</returns>
        bool Add(T model);

        /// <summary>
        /// 根据实体Id删除一个实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>布尔值</returns>
        bool Remove(int id);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>布尔值</returns>
        bool Remove(T model);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>布尔值</returns>
        bool Update(T model);

        /// <summary>
        /// 根据名称搜索多个实体
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>列表</returns>
        List<T> Search(string name);

        /// <summary>
        /// 根据实体Id得到一个实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>一个实体</returns>
        T GetModel(int id);
    }
}
