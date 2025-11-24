using System;
using System.Linq;
using WebApplication.System;
using WebApplication.Classes;
using Xunit;

namespace Tests
{
    public class CoursesSystemTests
    {
        private CoursesSystem CreateSystem()
        {
            return new CoursesSystem();
        }

        [Fact]
        public void AddStudent_true()
        {
            var sys = CreateSystem();

            bool added = sys.add_student("Stewie", "Kill_Lois", 21352, 2);

            Assert.True(added);
            Assert.Contains("Stewie Kill_Lois", sys.get_all_students()[0]);
        }

        [Fact]
        public void AddStudent_false()
        {
            var sys = CreateSystem();

            sys.add_student("Ezio", "Auditore", 26624, 2);
            bool added = sys.add_student("Wolter", "White", 26624, 3);

            Assert.False(added);
        }

        
//----------------------------------------------------------------------
        

        [Fact]
        public void AddTeacher_true()
        {
            var sys = CreateSystem();

            bool added = sys.add_teacher("Ogo", "Elki-palki", 10);

            Assert.True(added);
            Assert.Contains("Ogo Elki-palki", sys.get_all_teachers()[0]);
        }

        [Fact]
        public void AddTeacher_false()
        {
            var sys = CreateSystem();

            sys.add_teacher("Anton", "Stoit-Tam", 10);
            bool added = sys.add_teacher("Bad", "Comed", 10);

            Assert.False(added);
        }

 
//----------------------------------------------------------------------


        [Fact]
        public void AddOnlineCourse_true()
        {
            var sys = CreateSystem();
            sys.add_student("Zombie", "Zombiev", 3246, 2);
            sys.add_teacher("Rastenie", "Protiv", 522);

            bool added = sys.add_online_course(";)", "https://youtu.be/dQw4w9WgXcQ?si=tTxZ79NGAMGmOLCo", 
                new Student("Zombie", "Zombiev", 3246, 2), 
                new Teacher("Rastenie", "Protiv", 522));

            Assert.True(added);
            Assert.Contains(";)", sys.get_all_courses()[0]);
        }

        [Fact]
        public void AddOnlineCourse_false()
        {
            var sys = CreateSystem();
            sys.add_student("War", "Thunder", 253264, 2);
            sys.add_teacher("Turmsek", "Pivo", 324);

            sys.add_online_course("Math", "https://youtu.be/dQw4w9WgXcQ?si=tTxZ79NGAMGmOLCo",
                new Student("War", "Thunder", 253264, 2),
                new Teacher("Turmsek", "Pivo", 324));

            bool added = sys.add_online_course("Math", "https://youtu.be/dQw4w9WgXcQ?si=tTxZ79NGAMGmOLCo",
                new Student("War", "Thunder", 253264, 2),
                new Teacher("Turmsek", "Pivo", 324));

            Assert.False(added);
        }


//----------------------------------------------------------------------
        

        [Fact]
        public void DeleteCourse_true()
        {
            var sys = CreateSystem();
            sys.add_student("Russkiy", "Rus", 1252, 2);
            sys.add_teacher("Pfoo", "Yasher", 1326);
            sys.add_online_course("Anglllll", "no link aaaaaaaaaaaaaaa.com", 
                new Student("Russkiy", "Rus", 1252, 2),
                new Teacher("Pfoo", "Yasher", 1326));

            bool deleted = sys.delete_course("Anglllll");

            Assert.True(deleted);
            Assert.Equal("Zero courses added before", sys.get_all_courses().First());
        }

        [Fact]
        public void DeleteCourse_false()
        {
            var sys = CreateSystem();

            bool deleted = sys.delete_course("Unknown");

            Assert.False(deleted);
        }

        
//----------------------------------------------------------------------

        
        [Fact]
        public void DeleteStudent_true()
        {
            var sys = CreateSystem();
            sys.add_student("Ya", "Ustal", 67, 52);

            bool ok = sys.delete_stud("Ya", 67);

            Assert.True(ok);
            Assert.Equal("Zero students added before", sys.get_all_students().First());
        }

        [Fact]
        public void DeleteStudent_false()
        {
            var sys = CreateSystem();

            bool ok = sys.delete_stud("aaaaaaaaaaaa", 100);

            Assert.False(ok);
        }


//----------------------------------------------------------------------
        

        [Fact]
        public void DeleteTeacher_true()
        {
            var sys = CreateSystem();
            sys.add_teacher("Ya", "Ustal", 67);

            bool ok = sys.delete_teach("Ya", 67);

            Assert.True(ok);
            Assert.Equal("Zero teachers added before", sys.get_all_teachers().First());
        }

        [Fact]
        public void DeleteTeacher_false()
        {
            var sys = CreateSystem();

            bool ok = sys.delete_teach("qwteywrew", 2134);

            Assert.False(ok);
        }


//----------------------------------------------------------------------
   

        [Fact]
        public void AddStudentOnCourse_true()
        {
            var sys = CreateSystem();
            sys.add_student("Ya", "Ustal", 67, 52);
            sys.add_teacher("Tut", "Tozhe", 2345);

            sys.add_online_course("OOOOOOOOOOP", "htwa link",
                new Student("Ya", "Ustal", 67, 52),
                new Teacher("Tut", "Tozhe", 2345));

            bool ok = sys.add_s_on_course("Ya", "Ustal", 67, 52, "OOOOOOOOOOP");

            Assert.True(ok);
            var list = sys.get_students_in_course(
                new Online("OOOOOOOOOOP","htwa link")
            );
            Assert.True(list.Count >= 2);
        }

        [Fact]
        public void AddTeacherOnCourse_true()
        {
            var sys = CreateSystem();
            sys.add_student("Ya", "Ustal", 67, 52);
            sys.add_teacher("Tut", "Tozhe", 2345);

            sys.add_online_course("OOOOOOOOOOP", "htwa link",
                new Student("Ya", "Ustal", 67, 52),
                new Teacher("Tut", "Tozhe", 2345));

            bool ok = sys.add_t_on_course("Tut", "Tozhe", 2345, "OOOOOOOOOOP");

            Assert.True(ok);
        }


//----------------------------------------------------------------------


        [Fact]
        public void DeleteStudentFromCourse_true()
        {
            var sys = CreateSystem();
            sys.add_student("Ya", "Ustal", 67, 52);
            sys.add_teacher("Tut", "Tozhe", 2345);

            sys.add_online_course("OOOOOOOOOOP", "htwa link",
                new Student("Ya", "Ustal", 67, 52),
                new Teacher("Tut", "Tozhe", 2345));

            bool ok = sys.delete_stud_from_course("Ya", 67, "OOOOOOOOOOP");

            Assert.True(ok);
            var list = sys.get_students_in_course(new Online("OOOOOOOOOOP","htwa link"));
            Assert.Contains("zero", list[0].ToLower());
        }

        [Fact]
        public void DeleteTeacherFromCourse_true()
        {
            var sys = CreateSystem();
            sys.add_student("Ya", "Ustal", 67, 52);
            sys.add_teacher("Tut", "Tozhe", 2345);

            sys.add_online_course("OOOOOOOOOOP", "htwa link",
                new Student("Ya", "Ustal", 67, 52),
                new Teacher("Tut", "Tozhe", 2345));

            bool ok = sys.delete_teach_from_course("Tut", 2345, "OOOOOOOOOOP");

            Assert.True(ok);
        }

        
//----------------------------------------------------------------------
        

        [Fact]
        public void GetAllStudents_zero()
        {
            var sys = CreateSystem();

            var list = sys.get_all_students();

            Assert.Single(list);
            Assert.Equal("Zero students added before", list[0]);
        }

        [Fact]
        public void GetAllCourses_zero()
        {
            var sys = CreateSystem();

            var list = sys.get_all_courses();

            Assert.Single(list);
            Assert.Equal("Zero courses added before", list[0]);
        }
    }
}
