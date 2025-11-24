using WebApplication.System;
using WebApplication.Classes;
using Xunit;

namespace WebApplication.Tests
{
    public class Sys_Add_Tests
    {
        [Fact]
        public void AddStudent_true()
        {
            var sys = new CoursesSystem();

            var result = sys.add_student("Stewie", "Kill_Lois", 10, 1);

            Assert.True(result);
            Assert.Contains("Stewie Kill_Lois", sys.get_all_students());
        }

        [Fact]
        public void AddTeacher_true()
        {
            var sys = new CoursesSystem();

            var result = sys.add_teacher("T-90M", "Proriv", 5);

            Assert.True(result);
            Assert.Contains("T-90M Proriv", sys.get_all_teachers());
        }

        [Fact]
        public void AddOnlineCourse_true_add()
        {
            var sys = new CoursesSystem();
            var student = sys.add_student("Ezio", "Auditore", 153262, 4);
            var teacher = sys.add_teacher("Tamplier", "Tamplierov", 42153);

            var result = sys.add_online_course("OOPs", "https://www.youtube.com/watch?v=dQw4w9WgXcQ&list=RDdQw4w9WgXcQ&start_radio=1", student, teacher);

            Assert.True(result);
            Assert.Contains("OOPs", sys.get_all_courses());
        }
    }
}
