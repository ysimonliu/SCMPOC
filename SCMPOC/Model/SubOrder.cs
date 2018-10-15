using System;
using System.Collections.Generic;
using System.Linq;

namespace SCMPOC.Model
{
    public class SubOrder
    {
        public SubOrder(
            Order parent,
            double price,
            Person buyer,
            Person supplier,
            List<Person> personsCanViewPrice)
        {
            this.Parent = parent;
            this.Price = price;
            this.Buyer = buyer;
            this.Supplier = supplier;
            this.PersonsCanViewPrice = personsCanViewPrice;
        }

        private Order Parent { get; set; }

        private double Price { get; set; }

        private List<Person> PersonsCanViewPrice { get; set; }

        public Person Supplier { get; private set; }

        public Person Buyer { get; private set; }

        public double GetPrice(Guid onBehalfOf)
        {
            if (!PersonsCanViewPrice.Select(p => p.Id).Contains(onBehalfOf))
            {
                throw new UnauthorizedException($"UNAUTHORIZED: User of ID {onBehalfOf} is not authorized to view the price of this SubOrder " +
                    $"(Order:{this.Parent.Name}, Supplier:{this.Supplier}, Buyer:{this.Buyer})");
            }

            return Price;
        }

        public string FriendlyString()
        {
            return $"SubOrder - Supplier: {this.Supplier.Name}, Buyer: {this.Buyer.Name}";
        }
    }
}