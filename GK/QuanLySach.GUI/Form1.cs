using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuanLySach.BUS;
using QuanLySach.Entity;

namespace QuanLySach.GUI
{
    public partial class Form1 : Form
    {
        private readonly ISachBUS _sachBUS;
        private readonly ITheLoaiBUS _theLoaiBUS;
        private List<SachViewModel> _danhSachSach = new List<SachViewModel>();

        /// <summary>
        /// Constructor hỗ trợ Dependency Injection theo lý thuyết slide "3. Dependence Injection".
        /// </summary>
        public Form1(ISachBUS sachBUS, ITheLoaiBUS theLoaiBUS)
        {
            InitializeComponent();
            _sachBUS = sachBUS ?? throw new ArgumentNullException(nameof(sachBUS));
            _theLoaiBUS = theLoaiBUS ?? throw new ArgumentNullException(nameof(theLoaiBUS));
        }

        /// <summary>
        /// Constructor mặc định hỗ trợ Visual Studio Form Designer.
        /// </summary>
        public Form1() : this(new SachBUS(), new TheLoaiBUS())
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTheLoai();
                LoadDanhSachSach();
                ResetInputState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nạp danh sách thể loại vào ComboBox (Câu 4a)
        /// </summary>
        private void LoadTheLoai()
        {
            var dsTheLoai = _theLoaiBUS.GetAll();
            cboTheLoai.DataSource = dsTheLoai;
            cboTheLoai.DisplayMember = "TenLoai";
            cboTheLoai.ValueMember = "MaLoai";
        }

        /// <summary>
        /// Nạp danh sách sách dưới dạng ViewModel vào DataGridView (LINQ Projection - Bài 3, Slide 13)
        /// </summary>
        private void LoadDanhSachSach()
        {
            _danhSachSach = _sachBUS.GetAllViewModels();
            dgvSach.DataSource = _danhSachSach;
        }

        /// <summary>
        /// Làm sạch các thông báo lỗi trên ErrorProvider (Bài 2, Slide 25 & Bài 3, Slide 281)
        /// </summary>
        private void ClearErrors()
        {
            errProvider.SetError(txtMaSach, string.Empty);
            errProvider.SetError(txtTenSach, string.Empty);
            errProvider.SetError(cboTheLoai, string.Empty);
            errProvider.SetError(dtpNgayNhap, string.Empty);
            errProvider.SetError(txtSoLuong, string.Empty);
            errProvider.SetError(txtDonGia, string.Empty);
        }

        /// <summary>
        /// Thiết lập trạng thái ban đầu của các controls (Câu 1c)
        /// </summary>
        private void ResetInputState()
        {
            ClearErrors();

            txtMaSach.Clear();
            txtTenSach.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();
            dtpNgayNhap.Value = DateTime.Now;

            cboTheLoai.SelectedIndex = -1;

            txtMaSach.ReadOnly = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            dgvSach.ClearSelection();
            dgvSach.CurrentCell = null;

            txtMaSach.Focus();
        }

        /// <summary>
        /// Khi chọn dòng hợp lệ trên DataGridView (Câu 1c & Câu 4a)
        /// </summary>
        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSach.Rows.Count) return;

            ClearErrors();

            var row = dgvSach.Rows[e.RowIndex];
            string? maSach = row.Cells["colMaSach"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(maSach)) return;

            var sach = _danhSachSach.FirstOrDefault(s => s.MaSach == maSach);
            if (sach != null)
            {
                txtMaSach.Text = sach.MaSach;
                txtTenSach.Text = sach.TenSach;
                dtpNgayNhap.Value = sach.NgayNhap;
                txtSoLuong.Text = sach.SoLuong.ToString();
                txtDonGia.Text = sach.DonGia.ToString("0.##");
                cboTheLoai.SelectedValue = sach.MaLoai;

                txtMaSach.ReadOnly = true;
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            ResetInputState();
        }

        /// <summary>
        /// Kiểm tra dữ liệu nhập liệu ở tầng GUI và đánh dấu lỗi trực quan bằng ErrorProvider (Bài 2, Slide 25)
        /// </summary>
        private bool ValidateGuiInput(out int soLuong, out decimal donGia)
        {
            ClearErrors();
            bool isValid = true;
            soLuong = 0;
            donGia = 0;

            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                errProvider.SetError(txtMaSach, "Mã sách không được để trống!");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtTenSach.Text))
            {
                errProvider.SetError(txtTenSach, "Tên sách không được để trống!");
                isValid = false;
            }

            if (cboTheLoai.SelectedIndex == -1 || cboTheLoai.SelectedValue == null)
            {
                errProvider.SetError(cboTheLoai, "Vui lòng chọn thể loại cho sách!");
                isValid = false;
            }

            if (dtpNgayNhap.Value.Date > DateTime.Now.Date)
            {
                errProvider.SetError(dtpNgayNhap, "Ngày nhập không được lớn hơn ngày hiện tại!");
                isValid = false;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out soLuong) || soLuong <= 0)
            {
                errProvider.SetError(txtSoLuong, "Số lượng phải là số nguyên lớn hơn 0!");
                isValid = false;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out donGia) || donGia <= 0)
            {
                errProvider.SetError(txtDonGia, "Đơn giá phải là số lớn hơn 0!");
                isValid = false;
            }

            return isValid;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateGuiInput(out int soLuong, out decimal donGia))
            {
                MessageBox.Show("Dữ liệu nhập vào chưa hợp lệ. Vui lòng kiểm tra lại các ô được đánh dấu đỏ!",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sachMoi = new Sach
                {
                    MaSach = txtMaSach.Text.Trim(),
                    TenSach = txtTenSach.Text.Trim(),
                    NgayNhap = dtpNgayNhap.Value.Date,
                    SoLuong = soLuong,
                    DonGia = donGia,
                    MaLoai = cboTheLoai.SelectedValue?.ToString() ?? string.Empty
                };

                // Thao tác qua BUS (Câu 4b)
                _sachBUS.Add(sachMoi);

                MessageBox.Show("Thêm sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachSach();
                ResetInputState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi thêm sách", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng chọn cuốn sách cần sửa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateGuiInput(out int soLuong, out decimal donGia))
            {
                MessageBox.Show("Dữ liệu nhập vào chưa hợp lệ. Vui lòng kiểm tra lại các ô được đánh dấu đỏ!",
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sachCapNhat = new Sach
                {
                    MaSach = txtMaSach.Text.Trim(), // Giữ nguyên khóa chính (Câu 4c)
                    TenSach = txtTenSach.Text.Trim(),
                    NgayNhap = dtpNgayNhap.Value.Date,
                    SoLuong = soLuong,
                    DonGia = donGia,
                    MaLoai = cboTheLoai.SelectedValue?.ToString() ?? string.Empty
                };

                // Thao tác qua BUS (Câu 4b)
                _sachBUS.Update(sachCapNhat);

                MessageBox.Show("Cập nhật thông tin sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachSach();
                ResetInputState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật sách", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSach = txtMaSach.Text.Trim();
            if (string.IsNullOrWhiteSpace(maSach))
            {
                MessageBox.Show("Vui lòng chọn cuốn sách cần xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hộp thoại xác nhận trước khi xóa (Câu 4d)
            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sách '{txtTenSach.Text}' (Mã: {maSach}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // Thao tác qua BUS (Câu 4b)
                _sachBUS.Delete(maSach);

                MessageBox.Show("Xóa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachSach();
                ResetInputState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi xóa sách", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Gọi Close() để kích hoạt FormClosing, tránh hiển thị hộp thoại xác nhận 2 lần (Câu 4d)
            this.Close();
        }

        /// <summary>
        /// Xác nhận khi đóng bằng nút Thoát, nút X hoặc phím Esc (Câu 4d)
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát khỏi chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
