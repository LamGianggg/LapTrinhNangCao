using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLySach.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    TenLoai = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.MaLoai);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    MaSach = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    TenSach = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaLoai = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.MaSach);
                    table.ForeignKey(
                        name: "FK_Sach_TheLoai_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "TheLoai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TheLoai",
                columns: new[] { "MaLoai", "TenLoai" },
                values: new object[,]
                {
                    { "TL01", "Công nghệ thông tin" },
                    { "TL02", "Kinh tế - Quản trị" },
                    { "TL03", "Văn học - Nghệ thuật" }
                });

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "MaSach", "DonGia", "MaLoai", "NgayNhap", "SoLuong", "TenSach" },
                values: new object[,]
                {
                    { "S001", 85000m, "TL01", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Lập trình C# nâng cao" },
                    { "S002", 120000m, "TL01", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, "Cơ sở dữ liệu và EF Core" },
                    { "S003", 75000m, "TL02", new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Kinh tế học vi mô" },
                    { "S004", 95000m, "TL02", new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, "Quản trị doanh nghiệp" },
                    { "S005", 60000m, "TL03", new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Truyện Kiều chú giải" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sach_MaLoai",
                table: "Sach",
                column: "MaLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "TheLoai");
        }
    }
}
