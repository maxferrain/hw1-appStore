using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleStore
{
    internal class Program
    {

        static Queue<int> Queue = new Queue<int>();
        static bool flag = true;
        //static AtistContext context = new AtistContext();
        static Context context = new Context();


        static void Main(string[] args)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Console.WriteLine("Веедите одну из команд: 1 - первый запрос" + Environment.NewLine
                + "2 - второй запрос" + Environment.NewLine + "3 - третий запрос" + Environment.NewLine 
                + "4 - остановить программу" + Environment.NewLine);
            Task process = Processing();

        }


        static async Task Processing()
        {
            Task queryTask = Query();
            while (flag)
            {
                string caseSwitch = Console.ReadLine();
                switch (caseSwitch)
                {
                    case "1":
                        Queue.Enqueue(1);
                        break;
                    case "2":
                        Queue.Enqueue(2);
                        break;
                    case "3":
                        Queue.Enqueue(3);
                        break;
                    case "4":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("wrong command");
                        break;
                }
            }
            await queryTask;
        }


        static async Task Query()
        {
            context.Technologies.Add(new Technology()
            {
                processorName = "M1",
                yearIssue = 2020
            });
            context.Technologies.Add(new Technology()
            {
                processorName = "M1 Pro",
                yearIssue = 2021
            });

            context.Products.Add(new Product()
            {
                itemName = "MacBook",
                itemCost = 2500,
                version = "Pro",
                diagonal = 16
            });
            context.Products.Add(new Product()
            {
                itemName = "iPhone",
                itemCost = 1200,
                version = "Pro Max",
                diagonal = 6
            });
            Product prod1 = new Product()
            {
                itemName = "iPad",
                itemCost = 1200,
                version = "Pro",
                diagonal = 11
            };
            context.Products.Add(new Product()
            {
                itemName = "MacBook",
                itemCost = 2000,
                version = "Pro",
                diagonal = 13
            });
            context.Products.Add(new Product()
            {
                itemName = "MacBook",
                itemCost = 1800,
                version = "Air",
                diagonal = 13
            });
            context.Stores.Add(new Store()
            {
                address = "Lake Grove, Smith Haven",
                name = "Lake store",
                quantity = 100
            });
            context.Stores.Add(new Store()
            {
                address = "Bronx, The Mall at Bay Plaza",
                name = "Bay Mall",
                quantity = 83
            });

            context.SaveChanges();
            context.Dispose();

            while (flag)
            {
                int query = 0;
                try
                {
                    await Task.Delay(1000);
                    query = Queue.Dequeue();
                }
                catch
                {
                    Console.WriteLine(Environment.NewLine + "Empty Queue");
                }
                if (query == 1)
                {
                    await Task.Delay(2500);
                    Console.WriteLine(Environment.NewLine + "Answer: " + query + Environment.NewLine);
                    // Price limit
                    // sql-query
                    // Select * from Products where Products.itemCost <= 2000

                    IQueryable<Product> product = from pr in context.Products
                                                  where pr.itemCost <= 2000
                                                  select pr;

                    List<Product> list = product.ToList();
                    Console.WriteLine($"Product with a price below 2000USD:");
                    foreach (Product products in product)
                        Console.WriteLine($"{products.itemName} Price - {products.itemCost}");
                }
                if (query == 2)
                {
                    await Task.Delay(3500);
                    Console.WriteLine(Environment.NewLine + "Answer: " + query + Environment.NewLine);
                    // Diagonal Search
                    // sql-query
                    //Select * from Store.Product where Store.Product.diagonal = '13'

                    IQueryable<Product> product2 = from pr in context.Products
                                                   where pr.version == "Pro"
                                                   select pr;

                    List<Product> list2 = product2.ToList();
                    Console.WriteLine($"Pro Products:");
                    foreach (Product products in product2)
                        Console.WriteLine($"ID {products.id} " +
                            $"Item: {products.itemName}{products.version} {products.diagonal}' " +
                            $"Price: {products.itemCost}");
 
                }
                if (query == 3)
                {
                    await Task.Delay(3500);
                    Console.WriteLine(Environment.NewLine + "Answer: " + query + Environment.NewLine);

                    // SQL - All pruducts with processor M1
                    //Select * from Products join Technologies T on T.id = Products.'Technologyid' where processorName = 'M1'


                    // products by all stores

                    IQueryable<Store> product3 = from st in context.Stores
                                                 where st.Product.Contains(prod1)
                                                 select st;

                    List<Store> list3 = product3.ToList();
                    Console.WriteLine($"Products by all stores:");
                    foreach (Store store in product3)
                        Console.WriteLine($"ID {store.id} " +
                            $"Item: {store.address}");

                }


                // aggregation query (SQL) - count of products with each type of processors

                //select processorName, sum(Technologies.id) SUM from Technologies
                //inner join Products P on Technologies.id = P.Technologyid
                //group by P.Technologyid
            }
        }
    }
}
