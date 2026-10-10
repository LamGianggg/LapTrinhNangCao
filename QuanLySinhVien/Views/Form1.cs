using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLySinhVien.BUL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        private readonly SinhVienBUL sinhVienBUL;
        private readonly LopBus lopBus;
        private ErrorProvider errorProvider;

        private bool updatingStudentId;
        private bool existingStudent;
        private bool bindingStudents;
        private bool isLoaded;

        public Form1()
        {
            sinhVienBUL = new SinhVienBUL();
            lopBus = new LopBus();

            InitializeComponent();

            errorProvider = new ErrorProvider();
            errorProvider.ContainerControl = this;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            ConfigureExistingControls();

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button6.Click += button6_Click;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // Note: textBox1.TextChanged is already wired in Form1.Designer.cs
        }

        private void ConfigureExistingControls()
        {
            Text = "\u1ee8ng d\u1ee5ng qu\u1ea3n l\u00fd sinh vi\u00ean";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 10;
            numericUpDown1.DecimalPlaces = 1;
            numericUpDown1.Increment = 0.1m;

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new object[] { "\u0110ang h\u1ecdc", "Ngh\u1ec9 h\u1ecdc", "B\u1ea3o l\u01b0u" });

            button3.Text = "Th\u00eam";
            button3.BackColor = Color.FromArgb(40, 167, 69);
            button3.ForeColor = Color.White;
            button3.FlatStyle = FlatStyle.Flat;

            button4.Text = "S\u1eeda";
            button4.BackColor = Color.FromArgb(23, 110, 185);
            button4.ForeColor = Color.White;
            button4.FlatStyle = FlatStyle.Flat;

            button5.Text = "X\u00f3a";
            button5.BackColor = Color.FromArgb(220, 53, 69);
            button5.ForeColor = Color.White;
            button5.FlatStyle = FlatStyle.Flat;

            button6.Text = "L\u00e0m m\u1edbi";
            button6.BackColor = Color.FromArgb(108, 117, 125);
            button6.ForeColor = Color.White;
            button6.FlatStyle = FlatStyle.Flat;

            button1.Text = "T\u00ecm ki\u1ebfm";
            button1.BackColor = Color.FromArgb(23, 110, 185);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;

            button2.Text = "Hi\u1ec3n th\u1ecb t\u1ea5t c\u1ea3";

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
                BindClasses();
                RefreshStudentGrid();
                ClearStudentFields(true);
                existingStudent = false;
                UpdateActionButtons();
                dataGridView1.ClearSelection();
                BeginInvoke(new Action(() => textBox1.Focus()));
                isLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindClasses()
        {
            var classes = lopBus.GetAllLopHoc();

            comboBox1.DataSource = null;
            comboBox1.DisplayMember = "TenLop";
            comboBox1.ValueMember = "MaLop";
            comboBox1.DataSource = classes.ToList();
            comboBox1.SelectedIndex = -1;

            var filterClasses = new List<LopHoc> { new LopHoc { MaLop = "", TenLop = "-- T\u1ea5t c\u1ea3 l\u1edbp --" } };
            filterClasses.AddRange(classes);
            comboBox3.DataSource = null;
            comboBox3.DisplayMember = "TenLop";
            comboBox3.ValueMember = "MaLop";
            comboBox3.DataSource = filterClasses;
            comboBox3.SelectedIndex = 0;
        }

        // Khi chon lop tren comboBox3 -> hien thi sinh vien cua lop do
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
            try
            {
                var maLop = Convert.ToString(comboBox3.SelectedValue);
                List<SinhVien> result;
                if (string.IsNullOrEmpty(maLop))
                    result = sinhVienBUL.GetAllSinhVien();
                else
                    result = sinhVienBUL.GetSinhVienByMaLop(maLop);
                DisplayStudentsOnGrid(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayStudentsOnGrid(IEnumerable<SinhVien> students)
        {
            var rows = students.Select(sv => new
            {
                sv.MaSV, sv.HoTen,
                NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy"),
                sv.GioiTinh, sv.Email, sv.DienThoai,
                Diem = sv.Diem.ToString("0.0"),
                Lop = sv.LopHoc != null ? sv.LopHoc.TenLop : sv.MaLop,
                sv.TrangThai
            }).ToList();

            var was = bindingStudents;
            bindingStudents = true;
            try { dataGridView1.DataSource = rows; }
            finally { bindingStudents = was; }

            SetH("MaSV","M\u00e3 SV"); SetH("HoTen","H\u1ecd t\u00ean"); SetH("NgaySinh","Ng\u00e0y sinh");
            SetH("GioiTinh","Gi\u1edbi t\u00ednh"); SetH("Email","Email"); SetH("DienThoai","\u0110i\u1ec7n tho\u1ea1i");
            SetH("Diem","\u0110i\u1ec3m"); SetH("Lop","L\u1edbp"); SetH("TrangThai","Tr\u1ea1ng th\u00e1i");

            label3.Text = string.Format("T\u1ed5ng: {0} SV", rows.Count);
            textBox3.Text = rows.Count.ToString();
        }

        private void SetH(string c, string t)
        {
            if (dataGridView1.Columns.Contains(c)) dataGridView1.Columns[c].HeaderText = t;
        }

        private void RefreshStudentGrid() { DisplayStudentsOnGrid(sinhVienBUL.GetAllSinhVien()); }

        // Khi nhap ma SV vao textBox1
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (updatingStudentId) return;
            var maSV = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                existingStudent = false;
                ClearStudentFields(false);
                UpdateActionButtons();
                return;
            }
            try
            {
                var sv = sinhVienBUL.GetSinhVienById(maSV);
                if (sv != null) { existingStudent = true; DisplayStudent(sv); }
                else { existingStudent = false; ClearStudentFields(false); }
                UpdateActionButtons();
            }
            catch { }
        }

        private void DisplayStudent(SinhVien sv)
        {
            textBox7.Text = sv.HoTen ?? "";
            dateTimePicker1.Value = sv.NgaySinh.Date < dateTimePicker1.MinDate ? dateTimePicker1.MinDate :
                sv.NgaySinh.Date > dateTimePicker1.MaxDate ? dateTimePicker1.MaxDate : sv.NgaySinh.Date;
            textBox4.Text = sv.Email ?? "";
            textBox2.Text = sv.DienThoai ?? "";
            comboBox2.Text = sv.TrangThai ?? "";
            numericUpDown1.Value = Math.Min(numericUpDown1.Maximum, Math.Max(numericUpDown1.Minimum, sv.Diem));
            radioButton1.Checked = string.Equals(sv.GioiTinh, "Nam", StringComparison.OrdinalIgnoreCase);
            radioButton2.Checked = string.Equals(sv.GioiTinh, "N\u1eef", StringComparison.OrdinalIgnoreCase);
            comboBox1.SelectedValue = sv.MaLop;
        }

        private void ClearStudentFields(bool clearId)
        {
            ClearErrors();
            if (clearId) { updatingStudentId = true; textBox1.Clear(); updatingStudentId = false; }
            textBox7.Clear();
            dateTimePicker1.Value = DateTime.Today;
            textBox4.Clear();
            textBox2.Clear();
            comboBox2.SelectedIndex = -1; comboBox2.Text = "";
            numericUpDown1.Value = numericUpDown1.Minimum;
            radioButton1.Checked = false; radioButton2.Checked = false;
            comboBox1.SelectedIndex = -1;
        }

        private void UpdateActionButtons()
        {
            button3.Enabled = !existingStudent;
            button4.Enabled = existingStudent;
            button5.Enabled = existingStudent;
            button6.Enabled = true;
        }

        private SinhVien ReadStudentFromForm()
        {
            return new SinhVien
            {
                MaSV = textBox1.Text.Trim(),
                HoTen = textBox7.Text.Trim(),
                NgaySinh = dateTimePicker1.Value.Date,
                GioiTinh = radioButton1.Checked ? "Nam" : radioButton2.Checked ? "N\u1eef" : null,
                Email = textBox4.Text.Trim(),
                DienThoai = textBox2.Text.Trim(),
                TrangThai = string.IsNullOrWhiteSpace(comboBox2.Text) ? "\u0110ang h\u1ecdc" : comboBox2.Text.Trim(),
                Diem = numericUpDown1.Value,
                MaLop = comboBox1.SelectedValue != null ? Convert.ToString(comboBox1.SelectedValue) : null
            };
        }

        // ============ VALIDATION VIA ERRORPROVIDER ============
        private void ClearErrors()
        {
            errorProvider.SetError(textBox1, "");
            errorProvider.SetError(textBox7, "");
            errorProvider.SetError(textBox4, "");
            errorProvider.SetError(textBox2, "");
            errorProvider.SetError(comboBox1, "");
            errorProvider.SetError(radioButton1, "");
            errorProvider.SetError(numericUpDown1, "");
        }

        private bool ValidateStudentWithErrorProvider(SinhVien sv)
        {
            ClearErrors();
            IList<ValidationResult> results;
            if (sv.Validate(out results)) return true;

            foreach (var vr in results)
            {
                var field = vr.MemberNames.FirstOrDefault() ?? "";
                switch (field)
                {
                    case "MaSV": errorProvider.SetError(textBox1, vr.ErrorMessage); break;
                    case "HoTen": errorProvider.SetError(textBox7, vr.ErrorMessage); break;
                    case "Email": errorProvider.SetError(textBox4, vr.ErrorMessage); break;
                    case "DienThoai": errorProvider.SetError(textBox2, vr.ErrorMessage); break;
                    case "MaLop": errorProvider.SetError(comboBox1, vr.ErrorMessage); break;
                    case "GioiTinh": errorProvider.SetError(radioButton1, vr.ErrorMessage); break;
                    case "Diem": errorProvider.SetError(numericUpDown1, vr.ErrorMessage); break;
                    default: errorProvider.SetError(textBox1, vr.ErrorMessage); break;
                }
            }
            return false;
        }

        // ============ THEM ============
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var sv = ReadStudentFromForm();
                if (!ValidateStudentWithErrorProvider(sv)) return;
                sinhVienBUL.AddSinhVien(sv);
                existingStudent = true;
                RefreshStudentGrid();
                UpdateActionButtons();
                ClearErrors();
                MessageBox.Show("Th\u00eam sinh vi\u00ean th\u00e0nh c\u00f4ng!", "Th\u00f4ng b\u00e1o", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        // ============ SUA ============
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;
            var confirm = MessageBox.Show("X\u00e1c nh\u1eadn c\u1eadp nh\u1eadt?", "X\u00e1c nh\u1eadn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;
            try
            {
                var sv = ReadStudentFromForm();
                if (!ValidateStudentWithErrorProvider(sv)) return;
                sinhVienBUL.UpdateSinhVien(sv);
                RefreshStudentGrid();
                UpdateActionButtons();
                ClearErrors();
                MessageBox.Show("C\u1eadp nh\u1eadt th\u00e0nh c\u00f4ng!", "Th\u00f4ng b\u00e1o", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ============ XOA ============
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;
            var confirm = MessageBox.Show("X\u00e1c nh\u1eadn x\u00f3a?", "X\u00e1c nh\u1eadn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                sinhVienBUL.DeleteSinhVien(textBox1.Text.Trim());
                existingStudent = false;
                ClearStudentFields(true);
                RefreshStudentGrid();
                UpdateActionButtons();
                textBox1.Focus();
                MessageBox.Show("X\u00f3a th\u00e0nh c\u00f4ng!", "Th\u00f4ng b\u00e1o", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ============ LAM MOI ============
        private void button6_Click(object sender, EventArgs e)
        {
            existingStudent = false;
            ClearStudentFields(true);
            UpdateActionButtons();
            dataGridView1.ClearSelection();
            textBox1.Focus();
        }

        // ============ TIM KIEM ============
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var kw = textBox5.Text.Trim();
                var maLop = Convert.ToString(comboBox3.SelectedValue);
                decimal minDiem = 0;
                decimal.TryParse(textBox6.Text.Trim(), out minDiem);
                DisplayStudentsOnGrid(sinhVienBUL.FilterStudents(kw, maLop, minDiem));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "L\u1ed7i", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ============ HIEN THI TAT CA ============
        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Clear(); textBox6.Clear();
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
            RefreshStudentGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SyncSelectedStudentFromGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoaded && !bindingStudents) SyncSelectedStudentFromGrid();
        }

        private void SyncSelectedStudentFromGrid()
        {
            if (bindingStudents || dataGridView1.CurrentRow == null || !dataGridView1.Columns.Contains("MaSV")) return;
            var val = dataGridView1.CurrentRow.Cells["MaSV"].Value;
            if (val == null || val == DBNull.Value) return;
            var maSV = Convert.ToString(val).Trim();
            if (string.IsNullOrEmpty(maSV)) return;
            updatingStudentId = true;
            textBox1.Text = maSV;
            updatingStudentId = false;
            var sv = sinhVienBUL.GetSinhVienById(maSV);
            existingStudent = (sv != null);
            if (sv != null) DisplayStudent(sv);
            UpdateActionButtons();
        }

        private void groupBox1_Enter(object sender, EventArgs e) {}
        private void label1_Click_2(object sender, EventArgs e) {}
        private void label3_Click_1(object sender, EventArgs e) {}
        private void groupBox3_Enter(object sender, EventArgs e) {}
        private void label1_Click(object sender, EventArgs e) {}
        private void label1_Click_1(object sender, EventArgs e) {}
        private void label3_Click(object sender, EventArgs e) {}
    }
}
