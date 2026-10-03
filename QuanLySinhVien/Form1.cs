using System;
using System.Collections.Generic;
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

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button6.Click += button6_Click;
        }

        private void ConfigureExistingControls()
        {
            Text = "Quản lý sinh viên";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = System.Drawing.Color.WhiteSmoke;

            textBox1.TabIndex = 0;
            textBox1.Width = 125;
            textBox7.TabIndex = 1;
            textBox7.Width = 170;
            comboBox1.TabIndex = 2;
            comboBox1.Width = 155;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            dateTimePicker1.TabIndex = 3;
            dateTimePicker1.Width = 120;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            radioButton1.TabIndex = 4;
            radioButton2.TabIndex = 5;
            numericUpDown1.TabIndex = 6;
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 10;
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = 0.25m;
            textBox4.TabIndex = 7;
            textBox4.Width = 120;
            textBox2.TabIndex = 8;
            textBox2.Width = 150;
            comboBox2.TabIndex = 9;
            comboBox2.Width = 155;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new object[] { "Đang học", "Nghỉ học" });

            button3.Text = "Thêm";
            button3.Width = 80;
            button3.Location = new System.Drawing.Point(330, 167);
            button4.Text = "Cập nhật";
            button4.Width = 95;
            button4.Location = new System.Drawing.Point(418, 167);
            button5.Text = "Xóa";
            button5.Width = 80;
            button5.Location = new System.Drawing.Point(521, 167);
            button6.Text = "Làm mới";
            button6.Width = 100;
            button6.Location = new System.Drawing.Point(609, 167);

            textBox5.TabIndex = 0;
            comboBox3.TabIndex = 1;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox6.TabIndex = 2;
            button2.Text = "Bỏ lọc";
            button2.TabIndex = 3;
            button1.Text = "Tìm kiếm";
            button1.TabIndex = 4;
            textBox3.ReadOnly = true;
            textBox3.TabStop = false;

            groupBox1.TabStop = false;
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.TabStop = false;
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.TabStop = false;
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;

            groupBox1.TabIndex = 0;
            button3.TabIndex = 1;
            button4.TabIndex = 2;
            button5.TabIndex = 3;
            button6.TabIndex = 4;
            groupBox2.TabIndex = 5;
            groupBox3.TabIndex = 6;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                BindClasses();
                RefreshStudentGrid();
                ClearStudentFields(false);
                UpdateActionButtons();
                BeginInvoke(new Action(() => textBox1.Focus()));
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể tải dữ liệu sinh viên: " + exception.Message,
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindClasses()
        {
            var classes = repository.GetClasses().ToList();
            comboBox1.DataSource = null;
            comboBox1.DisplayMember = "TenLop";
            comboBox1.ValueMember = "MaLop";
            comboBox1.DataSource = classes;
            comboBox1.SelectedIndex = -1;

            comboBox3.DataSource = null;
            comboBox3.DisplayMember = "TenLop";
            comboBox3.ValueMember = "MaLop";
            comboBox3.DataSource = classes.ToList();
            comboBox3.SelectedIndex = -1;
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
                    ContainsText(student.MaSV, keyword) || ContainsText(student.HoTen, keyword));
            }

            if (comboBox3.SelectedValue != null)
            {
                var selectedClass = Convert.ToString(comboBox3.SelectedValue);
                students = students.Where(student => string.Equals(student.MaLop, selectedClass, StringComparison.OrdinalIgnoreCase));
            }

            decimal minimumScore;
            if (decimal.TryParse(textBox6.Text.Trim(), out minimumScore))
            {
                students = students.Where(student => student.Diem >= minimumScore);
            }

            var rows = students.Select(student => new
            {
                student.MaSV,
                student.HoTen,
                student.NgaySinh,
                student.GioiTinh,
                student.Email,
                student.DienThoai,
                student.TrangThai,
                student.Diem,
                LopHoc = student.LopHoc == null ? string.Empty : student.LopHoc.TenLop
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
            SetGridHeader("MaSV", "Mã sinh viên");
            SetGridHeader("HoTen", "Họ và tên");
            SetGridHeader("NgaySinh", "Ngày sinh");
            SetGridHeader("GioiTinh", "Giới tính");
            SetGridHeader("Email", "Email");
            SetGridHeader("DienThoai", "Điện thoại");
            SetGridHeader("TrangThai", "Trạng thái");
            SetGridHeader("Diem", "Điểm");
            SetGridHeader("LopHoc", "Lớp học");
            textBox3.Text = rows.Count.ToString();
        }

        private static bool ContainsText(string value, string keyword)
        {
            return value != null && value.IndexOf(keyword, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

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
                existingStudent = student != null;
                if (student == null)
                {
                    ClearStudentFields(false);
                }
                else
                {
                    DisplayStudent(student);
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
            radioButton1.Checked = string.Equals(student.GioiTinh, radioButton1.Text, StringComparison.OrdinalIgnoreCase);
            radioButton2.Checked = string.Equals(student.GioiTinh, radioButton2.Text, StringComparison.OrdinalIgnoreCase);
            comboBox1.SelectedValue = student.MaLop;
            textBox1.ReadOnly = true;
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
            comboBox2.Text = string.Empty;
            numericUpDown1.Value = numericUpDown1.Minimum;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedIndex = -1;
            textBox1.ReadOnly = false;
        }

        private void UpdateActionButtons()
        {
            var hasId = !string.IsNullOrWhiteSpace(textBox1.Text);
            button3.Enabled = hasId && !existingStudent;
            button4.Enabled = existingStudent;
            button5.Enabled = existingStudent;
        }

        private SinhVien ReadStudentFromForm()
        {
            var selectedClass = comboBox1.SelectedValue;
            if (selectedClass == null)
            {
                throw new InvalidOperationException("Vui lòng chọn lớp học.");
            }

            return new SinhVien
            {
                MaSV = textBox1.Text.Trim(),
                HoTen = textBox7.Text.Trim(),
                NgaySinh = dateTimePicker1.Value.Date,
                GioiTinh = radioButton1.Checked ? radioButton1.Text : radioButton2.Checked ? radioButton2.Text : null,
                Email = textBox4.Text.Trim(),
                DienThoai = textBox2.Text.Trim(),
                TrangThai = comboBox2.Text.Trim(),
                Diem = numericUpDown1.Value,
                MaLop = Convert.ToString(selectedClass)
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
            comboBox3.SelectedIndex = -1;
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var student = ReadStudentFromForm();
                if (!ValidateStudent(student))
                {
                    return;
                }

                repository.AddStudent(student);
                existingStudent = true;
                DisplayStudent(student);
                RefreshStudentGrid();
                UpdateActionButtons();
                MessageBox.Show("Đã thêm sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể thêm sinh viên: " + exception.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("Đã cập nhật thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể cập nhật sinh viên: " + exception.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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
            }
            catch (Exception exception)
            {
                MessageBox.Show("Không thể xóa sinh viên: " + exception.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            existingStudent = false;
            ClearStudentFields(true);
            UpdateActionButtons();
            textBox1.Focus();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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

            updatingStudentId = true;
            textBox1.Text = Convert.ToString(value);
            updatingStudentId = false;
            var student = repository.FindStudent(textBox1.Text);
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
    }
}