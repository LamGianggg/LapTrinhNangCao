using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLySinhVien.Entity;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        private readonly IStudentRepository repository;
        private bool updatingStudentId;
        private bool existingStudent;
        private bool bindingStudents;
        private bool isLoaded;

        public Form1() : this(new InMemoryStudentRepository())
        {
        }

        public Form1(IStudentRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
            InitializeComponent();
            ConfigureExistingControls();

            // Đăng ký các sự kiện
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button6.Click += button6_Click;
        }

        private void ConfigureExistingControls()
        {
            Text = "Ứng dụng quản lý sinh viên";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);

            // ==========================================
            // THIẾT LẬP THỨ TỰ TAB: TRÊN XUỐNG DƯỚI, TRÁI QUA PHẢI
            // ==========================================
            // Cột 1 (trái, trên xuống dưới): Mã SV -> Ngày sinh -> Email
            textBox1.TabIndex = 0;
            dateTimePicker1.TabIndex = 1;
            textBox4.TabIndex = 2;

            // Cột 2 (giữa, trên xuống dưới): Họ tên -> Giới tính -> Điện thoại
            textBox7.TabIndex = 3;
            radioButton1.TabIndex = 4;
            radioButton2.TabIndex = 5;
            textBox2.TabIndex = 6;

            // Cột 3 (phải, trên xuống dưới): Lớp học -> Điểm -> Trạng thái
            comboBox1.TabIndex = 7;
            numericUpDown1.TabIndex = 8;
            comboBox2.TabIndex = 9;

            // Các nút hành động bên dưới thông tin sinh viên
            button3.TabIndex = 10;
            button4.TabIndex = 11;
            button5.TabIndex = 12;
            button6.TabIndex = 13;

            // Khu vực tìm kiếm / lọc
            textBox5.TabIndex = 14;
            comboBox3.TabIndex = 15;
            textBox6.TabIndex = 16;
            button1.TabIndex = 17;
            button2.TabIndex = 18;

            // Danh sách sinh viên
            dataGridView1.TabIndex = 19;

            // ==========================================
            // CẤU HÌNH ĐỊNH DẠNG CÁC ĐIỀU KHIỂN NHẬP
            // ==========================================
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 10;
            numericUpDown1.DecimalPlaces = 1;
            numericUpDown1.Increment = 0.1m;

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new object[] { "Đang học", "Nghỉ học", "Bảo lưu" });

            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

            // ==========================================
            // CẤU HÌNH CÁC NÚT BẤM (TEXT, MÀU SẮC NHƯ MẪU)
            // ==========================================
            button3.Text = "Thêm";
            button3.BackColor = Color.FromArgb(40, 167, 69);
            button3.ForeColor = Color.White;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            button4.Text = "Sửa";
            button4.BackColor = Color.FromArgb(23, 110, 185);
            button4.ForeColor = Color.White;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;

            button5.Text = "Xóa";
            button5.BackColor = Color.FromArgb(220, 53, 69);
            button5.ForeColor = Color.White;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;

            button6.Text = "Làm mới";
            button6.BackColor = Color.FromArgb(108, 117, 125);
            button6.ForeColor = Color.White;
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 0;

            button1.Text = "Tìm kiếm";
            button1.BackColor = Color.FromArgb(23, 110, 185);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            button2.Text = "Hiển thị tất cả";
            button2.BackColor = Color.FromArgb(240, 240, 240);
            button2.ForeColor = Color.Black;

            // ==========================================
            // CẤU HÌNH DATAGRIDVIEW
            // ==========================================
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;

            label3.Font = new Font(label3.Font, FontStyle.Bold);
            textBox3.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                isLoaded = false;

                // Lấy về danh sách lớp học hiển thị combobox
                BindClasses();

                // Lấy về danh sinh viên hiển thị data gridview
                RefreshStudentGrid();

                // Khởi tạo trạng thái form trống
                ClearStudentFields(true);
                existingStudent = false;

                // Thiết lập cho các button có giá trị enable phù hợp (Nhập: true, Sửa/Xóa: false)
                UpdateActionButtons();

                // Bỏ chọn dòng mặc định trên grid để form sẵn sàng nhập mới
                dataGridView1.ClearSelection();

                // Con trỏ thiết lập mặc định ở txtMaSV
                BeginInvoke(new Action(() => textBox1.Focus()));

                isLoaded = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể tải dữ liệu: " + exception.Message,
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindClasses()
        {
            var classes = repository.GetClasses().ToList();

            // ComboBox chọn lớp cho sinh viên
            comboBox1.DataSource = null;
            comboBox1.DisplayMember = "TenLop";
            comboBox1.ValueMember = "MaLop";
            comboBox1.DataSource = classes;
            comboBox1.SelectedIndex = -1;

            // ComboBox lọc lớp trong khu vực tìm kiếm (có tùy chọn "Tất cả lớp")
            var filterClasses = new List<LopHoc>
            {
                new LopHoc { MaLop = "", TenLop = "Tất cả lớp" }
            };
            filterClasses.AddRange(classes);

            comboBox3.DataSource = null;
            comboBox3.DisplayMember = "TenLop";
            comboBox3.ValueMember = "MaLop";
            comboBox3.DataSource = filterClasses;
            comboBox3.SelectedIndex = 0;
        }

        private void RefreshStudentGrid()
        {
            ApplyStudentFilter();
        }

        private void SetGridHeader(string columnName, string headerText)
        {
            if (dataGridView1.Columns.Contains(columnName))
            {
                dataGridView1.Columns[columnName].HeaderText = headerText;
            }
        }

        private void ApplyStudentFilter()
        {
            var students = repository.GetStudents().AsEnumerable();
            var keyword = textBox5.Text.Trim();
            if (keyword.Length > 0)
            {
                students = students.Where(student =>
                    ContainsText(student.MaSV, keyword) ||
                    ContainsText(student.HoTen, keyword) ||
                    ContainsText(student.Email, keyword) ||
                    ContainsText(student.DienThoai, keyword));
            }

            if (comboBox3.SelectedValue != null)
            {
                var selectedClass = Convert.ToString(comboBox3.SelectedValue);
                if (!string.IsNullOrEmpty(selectedClass))
                {
                    students = students.Where(student => string.Equals(student.MaLop, selectedClass, StringComparison.OrdinalIgnoreCase));
                }
            }

            decimal minimumScore;
            if (decimal.TryParse(textBox6.Text.Trim(), out minimumScore) && minimumScore > 0)
            {
                students = students.Where(student => student.Diem >= minimumScore);
            }

            var rows = students.Select(student => new
            {
                student.MaSV,
                student.HoTen,
                NgaySinh = student.NgaySinh.ToString("dd/MM/yyyy"),
                student.GioiTinh,
                student.Email,
                student.DienThoai,
                Diem = student.Diem.ToString("0.0"),
                Lop = student.LopHoc == null ? string.Empty : student.LopHoc.TenLop,
                student.TrangThai
            }).ToList();

            var wasBinding = bindingStudents;
            bindingStudents = true;
            try
            {
                dataGridView1.DataSource = rows;
            }
            finally
            {
                bindingStudents = wasBinding;
            }

            // Định dạng tiêu đề cột theo hình giao diện
            SetGridHeader("MaSV", "Mã SV");
            SetGridHeader("HoTen", "Họ và tên");
            SetGridHeader("NgaySinh", "Ngày sinh");
            SetGridHeader("GioiTinh", "Giới tính");
            SetGridHeader("Email", "Email");
            SetGridHeader("DienThoai", "Điện thoại");
            SetGridHeader("Diem", "Điểm");
            SetGridHeader("Lop", "Lớp");
            SetGridHeader("TrangThai", "Trạng thái");

            label3.Text = string.Format("Tổng số: {0} sinh viên", rows.Count);
            textBox3.Text = rows.Count.ToString();
        }

        private static bool ContainsText(string value, string keyword)
        {
            return value != null && value.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        // ==========================================
        // SỰ KIỆN KHI NGƯỜI DÙNG NHẬP MÃ SINH VIÊN
        // ==========================================
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (updatingStudentId)
            {
                return;
            }

            var maSV = textBox1.Text.Trim();
            if (maSV.Length == 0)
            {
                existingStudent = false;
                ClearStudentFields(false);
                UpdateActionButtons();
                return;
            }

            try
            {
                var student = repository.FindStudent(maSV);
                if (student != null)
                {
                    // Nếu mã sinh viên tồn tại:
                    // - Lấy thông tin của sinh viên hiển thị tương ứng lên các điều khiển còn lại
                    // - Disable chức năng nhập, enable chức năng sửa, xóa
                    existingStudent = true;
                    DisplayStudent(student);
                }
                else
                {
                    // Chưa tồn tại:
                    // - Xóa giá trị các điều khiển textbox
                    // - Enable chức năng nhập, disable chức năng sửa, xóa
                    existingStudent = false;
                    ClearStudentFields(false);
                }

                UpdateActionButtons();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể tra cứu mã sinh viên: " + exception.Message,
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayStudent(SinhVien student)
        {
            textBox7.Text = student.HoTen ?? string.Empty;
            dateTimePicker1.Value = student.NgaySinh.Date < dateTimePicker1.MinDate
                ? dateTimePicker1.MinDate
                : student.NgaySinh.Date > dateTimePicker1.MaxDate ? dateTimePicker1.MaxDate : student.NgaySinh.Date;
            textBox4.Text = student.Email ?? string.Empty;
            textBox2.Text = student.DienThoai ?? string.Empty;
            comboBox2.Text = student.TrangThai ?? string.Empty;
            numericUpDown1.Value = Math.Min(numericUpDown1.Maximum, Math.Max(numericUpDown1.Minimum, student.Diem));
            radioButton1.Checked = string.Equals(student.GioiTinh, "Nam", StringComparison.OrdinalIgnoreCase);
            radioButton2.Checked = string.Equals(student.GioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase);
            comboBox1.SelectedValue = student.MaLop;
            textBox1.ReadOnly = false;
        }

        private void ClearStudentFields(bool clearStudentId)
        {
            if (clearStudentId)
            {
                updatingStudentId = true;
                textBox1.Clear();
                updatingStudentId = false;
            }

            textBox7.Clear();
            dateTimePicker1.Value = DateTime.Today < dateTimePicker1.MinDate ? dateTimePicker1.MinDate :
                DateTime.Today > dateTimePicker1.MaxDate ? dateTimePicker1.MaxDate : DateTime.Today;
            textBox4.Clear();
            textBox2.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox2.Text = string.Empty;
            numericUpDown1.Value = numericUpDown1.Minimum;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedIndex = -1;
            textBox1.ReadOnly = false;
        }

        private void UpdateActionButtons()
        {
            // Nếu sinh viên đã tồn tại: disable chức năng nhập, enable chức năng sửa, xóa
            // Nếu chưa tồn tại: enable chức năng nhập, disable chức năng sửa, xóa
            button3.Enabled = !existingStudent;
            button4.Enabled = existingStudent;
            button5.Enabled = existingStudent;
            button6.Enabled = true;
        }

        private SinhVien ReadStudentFromForm()
        {
            var selectedClass = comboBox1.SelectedValue;
            return new SinhVien
            {
                MaSV = textBox1.Text.Trim(),
                HoTen = textBox7.Text.Trim(),
                NgaySinh = dateTimePicker1.Value.Date,
                GioiTinh = radioButton1.Checked ? "Nam" : radioButton2.Checked ? "Nữ" : null,
                Email = textBox4.Text.Trim(),
                DienThoai = textBox2.Text.Trim(),
                TrangThai = string.IsNullOrWhiteSpace(comboBox2.Text) ? "Đang học" : comboBox2.Text.Trim(),
                Diem = numericUpDown1.Value,
                MaLop = selectedClass != null ? Convert.ToString(selectedClass) : null
            };
        }

        private static bool ValidateStudent(SinhVien student)
        {
            IList<System.ComponentModel.DataAnnotations.ValidationResult> validationResults;
            if (student.Validate(out validationResults))
            {
                return true;
            }

            var message = string.Join(Environment.NewLine, validationResults.Select(result => result.ErrorMessage));
            MessageBox.Show(message, "Thông tin chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // ==========================================
        // CÁC CHỨC NĂNG TÌM KIẾM VÀ LỌC
        // ==========================================
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ApplyStudentFilter();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể lọc danh sách sinh viên: " + exception.Message,
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
            textBox6.Clear();
            comboBox3.SelectedIndex = 0;
            try
            {
                ApplyStudentFilter();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể tải danh sách sinh viên: " + exception.Message,
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // CHỨC NĂNG NHẬP / THÊM MỚI
        // ==========================================
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var student = ReadStudentFromForm();
                if (!ValidateStudent(student))
                {
                    return;
                }

                if (repository.FindStudent(student.MaSV) != null)
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại trong hệ thống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                repository.AddStudent(student);
                existingStudent = true;
                DisplayStudent(student);
                RefreshStudentGrid();
                UpdateActionButtons();
                MessageBox.Show("Đã thêm sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể thêm sinh viên: " + exception.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // CHỨC NĂNG SỬA (CHỨC NĂNG NGUY HIỂM - CÓ XÁC THỰC)
        // ==========================================
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã sinh viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác thực trước khi thực hiện chức năng nguy hiểm (ghi đè dữ liệu)
            var confirmResult = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn cập nhật thông tin sinh viên [{0}] không?", textBox1.Text.Trim()),
                "Xác nhận sửa thông tin",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var student = ReadStudentFromForm();
                if (!ValidateStudent(student))
                {
                    return;
                }

                repository.UpdateStudent(student);
                existingStudent = true;
                DisplayStudent(student);
                RefreshStudentGrid();
                UpdateActionButtons();
                MessageBox.Show("Đã cập nhật thông tin sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể cập nhật sinh viên: " + exception.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // CHỨC NĂNG XÓA (CHỨC NĂNG NGUY HIỂM - CÓ XÁC THỰC)
        // ==========================================
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác thực trước khi thực hiện chức năng nguy hiểm (xóa vĩnh viễn)
            var confirmResult = MessageBox.Show(
                string.Format("Bạn có chắc chắn muốn xóa sinh viên [{0}] không?\nHành động này không thể hoàn tác!", textBox1.Text.Trim()),
                "Xác nhận xóa sinh viên",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

            try
            {
                repository.DeleteStudent(textBox1.Text.Trim());
                existingStudent = false;
                ClearStudentFields(true);
                RefreshStudentGrid();
                UpdateActionButtons();
                dataGridView1.ClearSelection();
                textBox1.Focus();
                MessageBox.Show("Đã xóa sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể xóa sinh viên: " + exception.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // CHỨC NĂNG LÀM MỚI
        // ==========================================
        private void button6_Click(object sender, EventArgs e)
        {
            // Xóa trống các thuộc tính trên form
            existingStudent = false;
            ClearStudentFields(true);

            // Enable button Nhập, Disable button sửa xóa
            UpdateActionButtons();

            // Chuyển tiêu điểm về điều khiển txtMaSV
            dataGridView1.ClearSelection();
            textBox1.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SyncSelectedStudentFromGrid();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoaded && !bindingStudents)
            {
                SyncSelectedStudentFromGrid();
            }
        }

        private void SyncSelectedStudentFromGrid()
        {
            if (bindingStudents || dataGridView1.CurrentRow == null || !dataGridView1.Columns.Contains("MaSV"))
            {
                return;
            }

            var value = dataGridView1.CurrentRow.Cells["MaSV"].Value;
            if (value == null || value == DBNull.Value)
            {
                return;
            }

            var maSV = Convert.ToString(value).Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                return;
            }

            updatingStudentId = true;
            textBox1.Text = maSV;
            updatingStudentId = false;

            var student = repository.FindStudent(maSV);
            existingStudent = student != null;
            if (student != null)
            {
                DisplayStudent(student);
            }

            UpdateActionButtons();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label1_Click_2(object sender, EventArgs e)
        {
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}
