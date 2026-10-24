using System;
using System.Linq;
using QuanLySach.Models;

namespace QuanLySach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Quản Lý Sách - Code First Demo ===");
            try
            {
                using var db = new QLTVDBContext();
                Console.WriteLine("Database Connection Initialized.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
