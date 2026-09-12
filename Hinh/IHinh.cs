using System;

namespace ExampleCAdvance
{
    public interface IHinh
    {
        double getDienTich();
        double getChuVi();
    }

    public class HinhTron : IHinh
    {
        private double banKinh;

        public HinhTron(double banKinh)
        {
            this.banKinh = banKinh;
        }

        public double getDienTich()
        {
            return Math.PI * banKinh * banKinh;
        }

        public double getChuVi()
        {
            return 2 * Math.PI * banKinh;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nhap ban kinh: ");
            double r = double.Parse(Console.ReadLine());

            HinhTron hinhTron = new HinhTron(r);

            Console.WriteLine("Dien tich = " + hinhTron.getDienTich());
            Console.WriteLine("Chu vi = " + hinhTron.getChuVi());
        }
    }
}
