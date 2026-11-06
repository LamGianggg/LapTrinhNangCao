using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using QuanLySach.BUS;
using QuanLySach.DAL;

namespace QuanLySach.GUI
{
    internal static class Program
    {
        /// <summary>
        /// Điểm khởi nhập chính của ứng dụng.
        /// Áp dụng mẫu thiết kế Dependency Injection theo lý thuyết slide "3. Dependence Injection":
        /// - Bước 1: Cài đặt thư viện Microsoft.Extensions.DependencyInjection
        /// - Bước 2 & 3: Định nghĩa và cài đặt Interfaces (ISachDAL, ITheLoaiDAL, ISachBUS, ITheLoaiBUS)
        /// - Bước 4: Refactor các lớp để phụ thuộc vào Interface (Loose Coupling)
        /// - Bước 5: Đăng ký dịch vụ vào DI Container (ServiceCollection)
        /// - Bước 6: Inject dịch vụ vào Form qua Constructor
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Khởi tạo CSDL MySQL và nạp dữ liệu ban đầu nếu chưa có
            try
            {
                using var db = new QLTVDBContext();
                db.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message + "\nVui lòng kiểm tra lại dịch vụ MySQL và chuỗi kết nối trong App.config.",
                                "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Thiết lập DI Container theo lý thuyết Dependency Injection
            var services = new ServiceCollection();
            ConfigureServices(services);
            using var serviceProvider = services.BuildServiceProvider();

            // Lấy instance của Form1 đã được tiêm các dịch vụ BUS qua Constructor
            var mainForm = serviceProvider.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Đăng ký các dịch vụ tầng DAL
            services.AddScoped<ITheLoaiDAL, TheLoaiDAL>();
            services.AddScoped<ISachDAL, SachDAL>();

            // Đăng ký các dịch vụ tầng BUS
            services.AddScoped<ITheLoaiBUS, TheLoaiBUS>();
            services.AddScoped<ISachBUS, SachBUS>();

            // Đăng ký Form1
            services.AddTransient<Form1>();
        }
    }
}
