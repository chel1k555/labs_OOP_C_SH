using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Classes;

namespace WebApplication.System
{
    public class CoursesSystem
    {
        private List<Student> students = new List<Student>();
        private List<Teacher> teachers = new List<Teacher>();
        private List<Course> courses = new List<Course>();


        public void cout_all_courses()
        {
            if(courses.Count() == 0)
            {
                Console.WriteLine("Zero courses added before");
                return;
            }
            for(int i = 0; i < courses.Count(); ++i)
            {
                Console.WriteLine($"{i}. {courses[i].Name}\n");
            }
        }

        public void cout_all_teachers()
        {
            if(teachers.Count() == 0)
            {
                Console.WriteLine("Zero teachers added before");
                return;
            }
            for(int i = 0; i < teachers.Count(); ++i)
            {
                Console.WriteLine($"{i}. {teachers[i].Name} {teachers[i].SurName}, Courses: {teachers[i].course_count()}");
            }
        }

        public void cout_all_students()
        {
            if(students.Count() == 0)
            {
                Console.WriteLine("Zero students added before");
                return;
            }
            for(int i = 0; i < students.Count(); ++i)
            {
                Console.WriteLine($"{i}. {students[i].Name} {students[i].SurName}, Semestr: {students[i].Semestr}, Courses: {students[i].course_count()}");
            } 
        }

        public void cout_courses_in_st(Student st)
        {
            if (st.course_count() == 0)
            {
                Console.WriteLine($"Zero courses added for student {st.Name} {st.SurName}");
                return;
            }
            for(int i = 0; i < st.course_count(); ++i)
            {
                Console.WriteLine($"{i}. {st.csr(i).Name}\n");
            }

        }

        public void cout_all_courses_in_tch(Teacher tch)
        {
            if(tch.course_count() == 0)
            {
                Console.WriteLine($"Zero courses added for teacher {tch.Name} {tch.SurName}");
                return;
            }
            for(int i = 0; i < tch.course_count(); ++i)
            {
                Console.WriteLine($"{i}. {tch.csr(i).Name}");
            }
        }

        public void cout_st_in_crs(Course crs)
        {
            if(crs.count_st(crs) == 0)
            {
                Console.WriteLine("In this course zero students");
                return;
            }
            for(int i = 0; i < crs.count_st(crs); ++i)
            {
                Console.WriteLine($"{i}. {crs.std(i).Name} {crs.std(i).SurName}, semestr: {crs.std(i).Semestr}");
            }
        }

        public void cout_tch_in_crs(Course crs)
        {
            if(crs.count_tch(crs) == 0)
            {
                Console.WriteLine("In this course zero teachers");
                return;
            }
            for(int i = 0; i < crs.count_tch(crs); ++i)
            {
                Console.WriteLine($"{i}. {crs.tch(i).Name} {crs.tch(i).SurName}");
            }            
        }

        public List<string> get_all_courses()
        {
            if (courses.Count == 0)
                return new List<string> { "Zero courses added before" };

            return courses
                .Select((c, i) => $"{i}. {c.Name}")
                .ToList();
        }

        public List<string> get_all_teachers()
        {
            if (teachers.Count == 0)
                return new List<string> { "Zero teachers added before" };

            return teachers
                .Select((t, i) =>
                    $"{i}. {t.Name} {t.SurName}, Courses: {t.course_count()}")
                .ToList();
        }

        public List<string> get_all_students()
        {
            if (students.Count == 0)
                return new List<string> { "Zero students added before" };

            return students
                .Select((s, i) =>
                    $"{i}. {s.Name} {s.SurName}, Semestr: {s.Semestr}, Courses: {s.course_count()}")
                .ToList();
        }

        public List<string> get_courses_in_student(Student st)
        {
            if (st.course_count() == 0)
                return new List<string> { $"Zero courses added for student {st.Name} {st.SurName}" };

            return Enumerable.Range(0, st.course_count())
                            .Select(i => $"{i}. {st.csr(i).Name}")
                            .ToList();
        }

        public List<string> get_all_courses_in_teacher(Teacher tch)
        {
            if (tch.course_count() == 0)
                return new List<string> { $"Zero courses added for teacher {tch.Name} {tch.SurName}" };

            return Enumerable.Range(0, tch.course_count())
                            .Select(i => $"{i}. {tch.csr(i).Name}")
                            .ToList();
        }

        public List<string> get_students_in_course(Course crs)
        {
            if (crs.count_st(crs) == 0)
                return new List<string> { "In this course zero students" };

            return Enumerable.Range(0, crs.count_st(crs))
                            .Select(i =>
                                $"{i}. {crs.std(i).Name} {crs.std(i).SurName}, semestr: {crs.std(i).Semestr}")
                            .ToList();
        }

        public List<string> get_teachers_in_course(Course crs)
        {
            if (crs.count_tch(crs) == 0)
                return new List<string> { "In this course zero teachers" };

            return Enumerable.Range(0, crs.count_tch(crs))
                            .Select(i =>
                                $"{i}. {crs.tch(i).Name} {crs.tch(i).SurName}")
                            .ToList();
        }

        public bool add_online_course(string name, string link, Student std, Teacher tch)
        {
            if (!students.Any(s => s.Id == std.Id))
            {
                Console.WriteLine($"System cannot find student (id: {std.Id}, Name: {std.Name})");
                return false;
            }
            if (!teachers.Any(t => t.Id == tch.Id))
            {
                Console.WriteLine($"System cannot find teacher (id: {tch.Id}, Name: {tch.Name})");
                return false;
            }
            if (courses.Any(c => c.Name == name))
            {
                Console.WriteLine($"System already have course (Name: {name})");
                return false;
            }
            Online crs = new Online(name, link);
            crs.add_stud_in_crs(std);
            crs.add_teach_in_crs(tch);
            courses.Add(crs);
            return true;
        }

        public bool add_offline_course(string name, string add, string aud, Student std, Teacher tch)
        {
            if (!students.Any(s => s.Id == std.Id))
            {
                Console.WriteLine($"System cannot find student (id: {std.Id}, Name: {std.Name})");
                return false;
            }
            if (!teachers.Any(t => t.Id == tch.Id))
            {
                Console.WriteLine($"System cannot find teacher (id: {tch.Id}, Name: {tch.Name})");
                return false;
            }
            if (courses.Any(c => c.Name == name))
            {
                Console.WriteLine($"System already have course (Name: {name})");
                return false;
            }
            Offline crs = new Offline(name, add, aud);
            crs.add_stud_in_crs(std);
            crs.add_teach_in_crs(tch);
            courses.Add(crs);
            return true;
        }

        public bool delete_course(string name)
        {
            if (!courses.Any(c => c.Name == name))
            {
                Console.WriteLine($"System cannot find course (Name: {name})");
                return false;
            }
            var course = courses.FirstOrDefault(c => c.Name == name);
            course.delete_all_st();
            course.delete_all_teach();
            courses.Remove(course);
            return true;
        }

        public bool delete_teach(string name, int id)
        {
            if (!teachers.Any(t => t.Id == id))
            {
                Console.WriteLine($"System cannot find this teacher id: {id}");
                return false;
            }
            var teach = teachers.FirstOrDefault(t => t.Id == id && t.Name == name);
            teach.delete_tch_from_all_crs();
            teachers.Remove(teach);
            return true;
        }

        public bool delete_teach_from_course(string tname, int tid, string cname)
        {
            if (!teachers.Any(t => t.Id == tid))
            {
                Console.WriteLine($"System cannot find this teacher id: {tid}");
                return false;
            }
            if (!courses.Any(c => c.Name == cname))
            {
                Console.WriteLine($"System cannot find this course name: {cname}");
                return false;
            }
            var crs = courses.FirstOrDefault(c => c.Name == cname);
            var teach = teachers.FirstOrDefault(t => t.Id == tid && t.Name == tname);
            crs.delete_teach(teach);
            teach.delete_crs_in_tch(crs);
            return true;
        }

        public bool delete_stud(string name, int id)
        {
            if (!students.Any(t => t.Id == id))
            {
                Console.WriteLine($"System cannot find this student id: {id}");
                return false;
            }
            var st = students.FirstOrDefault(s => s.Id == id && s.Name == name);
            st.delete_st_from_all_crs();
            students.Remove(st);
            return true;
        }
        
        public bool delete_stud_from_course(string sname, int sid, string cname)
        {
            if (!students.Any(t => t.Id == sid))
            {
                Console.WriteLine($"System cannot find this student id: {sid}");
                return false;
            }
            if (!courses.Any(c => c.Name == cname))
            {
                Console.WriteLine($"System cannot find this course name: {cname}");
                return false;
            }
            var crs = courses.FirstOrDefault(c => c.Name == cname);
            var stud = students.FirstOrDefault(s => s.Name == sname && s.Id == sid);
            crs.delete_st(stud);
            stud.delete_crs_in_st(crs);
            return true;
        }

        public bool add_t_on_course(string name, string surnm, int id, string cname)
        {
            if(id < 0)
            {
                Console.WriteLine("ID cannot be negative");
                return false;
            }
            if(!teachers.Any(t => t.Name == name && t.Id == id))
            {
                this.add_teacher(name, surnm, id);
            }
            var crs = courses.FirstOrDefault(c => c.Name == cname);
            var teach = teachers.FirstOrDefault(t => t.Id == id && t.Name == name);
            crs.add_teach_in_crs(teach);
            return true;
        }

        public bool add_s_on_course(string name, string surnm, int id, int sem, string cname)
        {
            if(id < 0)
            {
                Console.WriteLine("ID cannot be negative");
                return false;
            }
            if(sem < 0)
            {
                Console.WriteLine("Semestr cannot be negative");
                return false;
            }
            if(!students.Any(s => s.Name == name && s.Id == id))
            {
                this.add_student(name, surnm, id, sem);
            }
            var crs = courses.FirstOrDefault(c => c.Name == cname);
            var st = students.FirstOrDefault(s => s.Id == id && s.Name == name);
            crs.add_stud_in_crs(st);
            return true;
        }        

        public bool add_teacher(string name, string surnm, int id)
        {
            if(id < 0)
            {
                Console.WriteLine("ID cannot be negative");
                return false;
            }
            if (teachers.Any(t => t.Id == id))
            {
                Console.WriteLine($"Teacher with Id: {id} already in system");
                return false;
            }
            Teacher tch = new Teacher(name, surnm, id);
            teachers.Add(tch);
            return true;
        }

        public bool add_student(string name, string surnm, int id, int sem)
        {
            if(id < 0)
            {
                Console.WriteLine("ID cannot be negative");
                return false;
            }
            if(sem < 0)
            {
                Console.WriteLine("Semestr cannot be negative");
                return false;
            }
            if (students.Any(s => s.Id == id))
            {
                Console.WriteLine($"Student with Id: {id} already in system");
                return false;
            }
            Student std = new Student(name, surnm, id, sem);
            students.Add(std);
            return true;
        }  
    }
}