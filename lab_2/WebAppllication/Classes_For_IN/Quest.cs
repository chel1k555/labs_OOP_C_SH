using System.Collections.Generic;

namespace WebApplication.Classes_For_IN
{
    public class Quest : Item
    {
        public bool Ques {get; private set;}
        public Quest(int id, string name, Rare rar, bool ques) : base(id, false, name, rar, true)
        {
            Ques = ques;
        }
    }
}