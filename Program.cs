using FoodDelivery.Business;
using FoodDelivery.Business.Models;
using FoodDelivery.Data;
using System;
using System.Collections.Generic;

namespace FoodDelivery.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IFoodRepository foodRepository = new InMemoryFoodRepository(); 
            FoodService foodService = new FoodService(foodRepository);

            IOrderRepository orderRepository = new InMemoryOrderRepository(); 
            OrderService orderService = new OrderService(orderRepository);

            while (true)
            {
                Console.WriteLine("Welcome to food delivery system !");
                Console.WriteLine("-------------------");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. View available foods");
                Console.WriteLine("2. Place an order");
                Console.WriteLine("3. Exit");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        DisplayAvailableFoods(foodService);
                        break;
                    case "2":
                        PlaceOrder(foodService, orderService);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void DisplayAvailableFoods(FoodService foodService)
        {
            Console.WriteLine("Available Foods:");
            Console.WriteLine("---------");
            foreach (var food in foodService.GetAllFoods())
            {
                Console.WriteLine($"ID: {food.Id}, Name: {food.Name}, Price: {food.Price:C}");
            }
        }

        static void PlaceOrder(FoodService foodService, OrderService orderService)
        {
            Console.Write("Enter your name: ");
            string customerName = Console.ReadLine();

            DisplayAvailableFoods(foodService);
            Console.Write("Enter food ID: ");
            int foodId;
            while (!int.TryParse(Console.ReadLine(), out foodId))
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
            }

            Console.Write("Enter quantity: ");
            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid quantity.");
            }

            Order order = new Order
            {
                CustomerName = customerName,
                FoodId = foodId,
                Quantity = quantity
            };

            orderService.PlaceOrder(order);
            Console.WriteLine("Order placed successfully.");
        }
    }
}