// See https://aka.ms/new-console-template for more information
using System;

class Currency
{
    public string Name { get; set; }
    public double ExRate { get; set; }

    public Currency() { }

    public Currency(string name, double exRate)
    {
        Name = name;
        ExRate = exRate;
    }

    public Currency(Currency other)
    {
        Name = other.Name;
        ExRate = other.ExRate;
    }

    public override string ToString()
    {
        return $"{Name} ({ExRate} UAH)";
    }
}

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Cost { get; set; }
    public int Quantity { get; set; }
    public string Producer { get; set; }
    public double Weight { get; set; }
    public Currency Currency { get; set; }

    public Product() { }

    public Product(string name, double price, string cost, int quantity, string producer, double weight, Currency currency)
    {
        Name = name;
        Price = price;
        Cost = cost;
        Quantity = quantity;
        Producer = producer;
        Weight = weight;
        Currency = currency;
    }

    public Product(Product other)
    {
        Name = other.Name;
        Price = other.Price;
        Cost = other.Cost;
        Quantity = other.Quantity;
        Producer = other.Producer;
        Weight = other.Weight;
        Currency = new Currency(other.Currency);
    }

    public double GetPriceInUAH()
    {
        return Price * Currency.ExRate;
    }

    public double GetWeightInPounds()
    {
        // 1 кілограм = 2.20462 фунта
        return Weight * 2.20462;
    }

    public override string ToString()
    {
        return $"{Name} ({Quantity} pcs, {Price} {Cost}, {Weight} kg)";
    }
}

class Program
{
    static Product[] CreateProducts(int n)
    {
        Product[] products = new Product[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter details for product {i + 1}:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Cost (e.g., USD): ");
            string cost = Console.ReadLine();
            Console.Write("Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Producer: ");
            string producer = Console.ReadLine();
            Console.Write("Weight (kg): ");
            double weight = Convert.ToDouble(Console.ReadLine());

            Console.Write("Currency Name: ");
            string currencyName = Console.ReadLine();
            Console.Write("Exchange Rate: ");
            double exchangeRate = Convert.ToDouble(Console.ReadLine());

            Currency currency = new Currency(currencyName, exchangeRate);
            products[i] = new Product(name, price, cost, quantity, producer, weight, currency);
        }

        return products;
    }

    static void DisplayProduct(Product product)
    {
        Console.WriteLine(product.ToString());
    }

    static void DisplayAllProducts(Product[] products)
    {
        foreach (var product in products)
        {
            DisplayProduct(product);
        }
    }

    static void Main(string[] args)
    {
        int choice;
        Product[] products = null;

        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create Products");
            Console.WriteLine("2. Display a Product");
            Console.WriteLine("3. Display All Products");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the number of products: ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    products = CreateProducts(n);
                    break;

                case 2:
                    if (products != null && products.Length > 0)
                    {
                        Console.Write("Enter the index of the product to display: ");
                        int index = Convert.ToInt32(Console.ReadLine());
                        if (index >= 0 && index < products.Length)
                        {
                            DisplayProduct(products[index]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid index");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No products created yet.");
                    }
                    break;

                case 3:
                    if (products != null && products.Length > 0)
                    {
                        DisplayAllProducts(products);
                    }
                    else
                    {
                        Console.WriteLine("No products created yet.");
                    }
                    break;

                case 4:
                    Console.WriteLine("Exiting the program.");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }

        } while (choice != 4);
    }
}

