using System.Collections.Generic;

namespace WebApplication.Classes{
    public class Student
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Semestr { get; set; }
        public int Id { get; private set; }
        private List<Course> Courses = new List<Course>();

        public Student(string name, string sur, int id, int semestr)
        {
            Name = name;
            Semestr = semestr;
            if (id < 0)
            {
                throw new ArgumentException("ID cannot be negative");
            }
            Id = id;
            SurName = sur;
        }

        public void add_course_st(Course crs)
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

        public void delete_st_from_all_crs()
        {
            for(int i = 0; i < Courses.Count(); ++i)
            {
                Courses[i].delete_st(this);
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

        public void delete_crs_in_st(Course crs)
        {
            Courses.Remove(crs);
            return;
        }

    }

}
