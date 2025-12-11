using System.Collections.Generic;

namespace WebApplication.Classes {
    public class Teacher
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Id { get; private set; }
        private List<Course> Courses = new List<Course>();

        public Teacher(string name, string surn, int id)
        {
            Name = name;
            SurName = surn;
            if (id < 0)
            {
                throw new ArgumentException("ID cannot be negative");
            }
            Id = id;
        }

        public void add_course_tch(Course crs)
        {
            Courses.Add(crs);
            return;
        }

        public bool has(Course crs)
        {
            if (Courses.Contains(crs))
            {
                return true;
            }
            return false;
        }        

        public void delete_tch_from_all_crs()
        {
            for(int i = 0; i < Courses.Count(); ++i)
            {
                Courses[i].delete_teach(this);
            }
            Courses.Clear();
        }

        public int course_count()
        {
            return Courses.Count();
        }

        public Course csr(int i)
        {
            return Courses[i];
        }

        public void delete_crs_in_tch(Course crs)
        {
            Courses.Remove(crs);
            return;
        }        

    }

}
