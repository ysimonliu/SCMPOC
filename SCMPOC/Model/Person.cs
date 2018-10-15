using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMPOC.Model
{
    public class Person
    {
        public Person(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Guid Id;

        public string Name;
    }
}
