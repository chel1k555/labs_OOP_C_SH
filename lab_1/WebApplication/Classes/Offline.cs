using System.Collections.Generic;

namespace WebApplication.Classes{
    public class Offline : Course
    {
        private string adress { get; set; }
        private string auditore { get; set; }

        public Offline(string name, string ad, string audi) : base(name)
        {
            adress = ad;
            auditore = audi;
        }
    }

}
