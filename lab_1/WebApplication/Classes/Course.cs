using System.Collections.Generic;

namespace WebApplication.Classes{
    public abstract class Course
    {
        public string Name { get; set; }
        private List<Student> Students = new List<Student>();
        private List<Teacher> Teachers = new List<Teacher>();

        public Course(string name)
        {
            Name = name;
        }

        public Student std(int i)
        {
            return Students[i];
        }

        public Teacher tch(int i)
        {
            return Teachers[i];
        }

        public int count_st(Course crs)
        {
            return crs.Students.Count();
        }

        public int count_tch(Course crs)
        {
            return crs.Teachers.Count();
        }

        public void add_stud_in_crs(Student std)
        {
            if (!Students.Contains(std))
            {
                Students.Add(std);
            }
            if (!std.has(this))
            {
                std.add_course_st(this);
            }
        }

        public void add_teach_in_crs(Teacher tch)
        {
            if (!Teachers.Contains(tch))
            {
                Teachers.Add(tch);
            }
            if (!tch.has(this))
            {
                tch.add_course_tch(this);
            }            
        }        

        public void delete_st(Student st)
        {
            Students.Remove(st);
        }

        public void delete_teach(Teacher tch)
        {
            Teachers.Remove(tch);
        }
        
        public void delete_all_st()
        {
            Students.Clear();
        }
        
        public void delete_all_teach()
        {
            Teachers.Clear();
        }
    }

}
