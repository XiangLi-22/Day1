using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClassRoomDAL : IClassRoom
    {
        public static List<ClassRoom> ClassRooms { get; set; } = new List<ClassRoom>(){
            new ClassRoom(){ ClassRoomId = 1,ClassRoomName = "上位机"},
            new ClassRoom(){ ClassRoomId = 2,ClassRoomName = "Java"}
        };
        public bool Add(ClassRoom model)
        {
            try
            {
                if (ClassRooms.Count > 0)
                {
                    ClassRoom lastClassRoom = ClassRooms[ClassRooms.Count - 1];
                    model.ClassRoomId = lastClassRoom.ClassRoomId + 1;
                }
                ClassRooms.Add(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ClassRoom GetModel(int classRoomId)
        {
            return ClassRooms.Find(s => s.ClassRoomId == classRoomId);
        }

        public bool Remove(int classRoomId)
        {
            return Remove(GetModel(classRoomId));
        }

        public bool Remove(ClassRoom model)
        {
            try
            {
                ClassRooms.Remove(model);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ClassRoom> Search(string classRoomName)
        {
            return ClassRooms.FindAll(s => s.ClassRoomName.Contains(classRoomName));
        }

        public bool Update(ClassRoom model)
        {
            int i = ClassRooms.FindIndex(s => s.ClassRoomId == model.ClassRoomId);
            if (i != -1)
            {
                ClassRooms[i].ClassRoomId = model.ClassRoomId;
                ClassRooms[i].ClassRoomName = model.ClassRoomName;
                return true;
            }
            return false;
        }
    }
}
