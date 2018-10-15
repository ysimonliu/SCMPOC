using SCMPOC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMPOC
{
    class Program
    {
        private static List<Order> orders = new List<Order>();
        private static List<Person> persons = new List<Person>();

        static void Main(string[] args)
        {
            string input = string.Empty;
            while (true)
            {
                Console.WriteLine("Please enter number to perform an action:");
                Console.WriteLine("1. Create a person");
                Console.WriteLine("2. Create an order");
                Console.WriteLine("3. List all persons");
                Console.WriteLine("4. View suborder");
                input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        CreatePerson();
                        break;
                    case "2":
                        CreateOrder();
                        break;
                    case "3":
                        ListPersons();
                        break;
                    case "4":
                        ViewSuborder();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Invalid input!!!");
                        break;
                }
            }
        }

        private static void ViewSuborder()
        {
            string input;
            Console.Write("Enter the Guid of Order you wish to view:");
            input = Console.ReadLine();
            var suborders = orders.Where(o => o.Id.Equals(Guid.Parse(input))).FirstOrDefault().SubOrders;

            int i = 0;
            foreach(var suborder in suborders)
            {
                Console.WriteLine($"{i} - {suborder.FriendlyString()}");
                i++;
            }

            Console.Write("Choose the suborder which you want to view the price:");
            input = Console.ReadLine();
            var suborderToView = suborders[int.Parse(input)];

            while (true)
            {
                Console.Write("Enter viewer (Enter Id), Enter q to quit:");
                input = Console.ReadLine();
                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase)) break;

                try
                {
                    Console.WriteLine($"Price for this order is {suborderToView.GetPrice(Guid.Parse(input))}");
                }
                catch (UnauthorizedException)
                {
                    Console.WriteLine($"You are NOT allowed to view the order");
                }
                catch
                {
                    Console.WriteLine($"InternalError!!!");
                }
            }

            
        }

        private static void ListPersons()
        {
            Console.WriteLine();
            if (persons.Count == 0)
            {
                Console.WriteLine("No persons created yet.");
            }
            else
            {
                persons.ForEach(p => Console.WriteLine($"ID: {p.Id}, Name:{p.Name}"));
            }

            Console.WriteLine();
        }

        private static void CreateOrder()
        {
            Console.WriteLine();
            Console.Write("Enter name:");
            string input = Console.ReadLine();
            Order order = new Order(input);
            orders.Add(order);
            Console.WriteLine($"Order created:. Name: {order.Name}, ID: {order.Id}");
            Console.WriteLine();
            while(true)
            {
                Console.Write("Create a suborder for this order? (y/n)");
                input = Console.ReadLine();
                if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.Write("Enter suborder price:");
                    input = Console.ReadLine();
                    double price = Double.Parse(input);

                    Console.Write("Enter Buyer (Enter ID):");
                    input = Console.ReadLine();
                    Person buyer = persons.Where(p => p.Id.Equals(Guid.Parse(input))).FirstOrDefault();

                    Console.Write("Enter Supplier (Enter ID):");
                    input = Console.ReadLine();
                    Person supplier = persons.Where(p => p.Id.Equals(Guid.Parse(input))).FirstOrDefault();

                    var personsWhoCanViewPrice = new List<Person>();
                    while (true)
                    {
                        Console.Write("Enter a person who can view the price of this suborder (Enter ID), Enter q to quit:");
                        input = Console.ReadLine();
                        if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase)) break;
                        personsWhoCanViewPrice.Add(persons.Where(p => p.Id.Equals(Guid.Parse(input))).FirstOrDefault());
                    }

                    order.CreateNewSubOrder(price, buyer, supplier, personsWhoCanViewPrice);
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"Order created. Order ID: {order.Id}");
            Console.WriteLine();
        }

        private static void CreatePerson()
        {
            Console.WriteLine();
            Console.Write("Enter name:");
            string input = Console.ReadLine();
            Person person = new Person(input);
            persons.Add(person);
            Console.WriteLine($"Person created:. Name: {person.Name}, ID: {person.Id}");
            Console.WriteLine();
        }
    }
}
