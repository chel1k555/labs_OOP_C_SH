using System.Collections.Generic;

namespace WebApplication.Classes{
    public class Online : Course
    {
        private string zoom { get; set;}

        public Online(string name, string link) : base(name)
        {
            zoom = link;
        }
    }

}
