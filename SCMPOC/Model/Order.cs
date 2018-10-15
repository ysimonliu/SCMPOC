using System;
using System.Collections.Generic;

namespace SCMPOC.Model
{
    public class Order
    {
        public Order(string name)
        {
            this.Name = name;
            this.Id = Guid.NewGuid();
            this.SubOrders = new List<SubOrder>();
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public List<SubOrder> SubOrders { get; private set; }

        public void CreateNewSubOrder(double price, Person buyer, Person supplier, List<Person> personsCanViewPrice)
        {
            // TODO: check if SubOrders not null, last in the SubOrder's supplier has to match new SubOrder's buyer
            SubOrder subOrder = new SubOrder(this, price, buyer, supplier, personsCanViewPrice);
            this.SubOrders.Add(subOrder);
        }
    }
}