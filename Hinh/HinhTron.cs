using System;

namespace ExampleCAdvance.IHinh
{
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
}
