namespace QuanLySach.GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            grpThongTin = new System.Windows.Forms.GroupBox();
            dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            cboTheLoai = new System.Windows.Forms.ComboBox();
            txtDonGia = new System.Windows.Forms.TextBox();
            txtSoLuong = new System.Windows.Forms.TextBox();
            txtTenSach = new System.Windows.Forms.TextBox();
            txtMaSach = new System.Windows.Forms.TextBox();
            lblDonGia = new System.Windows.Forms.Label();
            lblSoLuong = new System.Windows.Forms.Label();
            lblNgayNhap = new System.Windows.Forms.Label();
            lblTenSach = new System.Windows.Forms.Label();
            lblMaSach = new System.Windows.Forms.Label();
            lblTheLoai = new System.Windows.Forms.Label();
            grpChucNang = new System.Windows.Forms.GroupBox();
            btnThoat = new System.Windows.Forms.Button();
            btnXoa = new System.Windows.Forms.Button();
            btnSua = new System.Windows.Forms.Button();
            btnThem = new System.Windows.Forms.Button();
            btnMoi = new System.Windows.Forms.Button();
            grpDanhSach = new System.Windows.Forms.GroupBox();
            dgvSach = new System.Windows.Forms.DataGridView();
            colMaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTheLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNgayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            errProvider = new System.Windows.Forms.ErrorProvider(components);
            grpThongTin.SuspendLayout();
            grpChucNang.SuspendLayout();
            grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errProvider).BeginInit();
            SuspendLayout();
            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(dtpNgayNhap);
            grpThongTin.Controls.Add(cboTheLoai);
            grpThongTin.Controls.Add(txtDonGia);
            grpThongTin.Controls.Add(txtSoLuong);
            grpThongTin.Controls.Add(txtTenSach);
            grpThongTin.Controls.Add(txtMaSach);
            grpThongTin.Controls.Add(lblDonGia);
            grpThongTin.Controls.Add(lblSoLuong);
            grpThongTin.Controls.Add(lblNgayNhap);
            grpThongTin.Controls.Add(lblTenSach);
            grpThongTin.Controls.Add(lblMaSach);
            grpThongTin.Controls.Add(lblTheLoai);
            grpThongTin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            grpThongTin.Location = new System.Drawing.Point(12, 12);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new System.Drawing.Size(640, 200);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin sách";
            // 
            // dtpNgayNhap
            // 
            dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            dtpNgayNhap.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpNgayNhap.Location = new System.Drawing.Point(110, 145);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new System.Drawing.Size(180, 25);
            dtpNgayNhap.TabIndex = 3;
            // 
            // cboTheLoai
            // 
            cboTheLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTheLoai.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            cboTheLoai.FormattingEnabled = true;
            cboTheLoai.Location = new System.Drawing.Point(110, 30);
            cboTheLoai.Name = "cboTheLoai";
            cboTheLoai.Size = new System.Drawing.Size(500, 25);
            cboTheLoai.TabIndex = 0;
            // 
            // txtDonGia
            // 
            txtDonGia.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtDonGia.Location = new System.Drawing.Point(400, 145);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.Size = new System.Drawing.Size(210, 25);
            txtDonGia.TabIndex = 5;
            // 
            // txtSoLuong
            // 
            txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtSoLuong.Location = new System.Drawing.Point(400, 107);
            txtSoLuong.Name = "txtSoLuong";
            txtSoLuong.Size = new System.Drawing.Size(210, 25);
            txtSoLuong.TabIndex = 4;
            // 
            // txtTenSach
            // 
            txtTenSach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtTenSach.Location = new System.Drawing.Point(110, 107);
            txtTenSach.MaxLength = 150;
            txtTenSach.Name = "txtTenSach";
            txtTenSach.Size = new System.Drawing.Size(180, 25);
            txtTenSach.TabIndex = 2;
            // 
            // txtMaSach
            // 
            txtMaSach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtMaSach.Location = new System.Drawing.Point(110, 68);
            txtMaSach.MaxLength = 20;
            txtMaSach.Name = "txtMaSach";
            txtMaSach.Size = new System.Drawing.Size(500, 25);
            txtMaSach.TabIndex = 1;
            // 
            // lblDonGia
            // 
            lblDonGia.AutoSize = true;
            lblDonGia.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblDonGia.Location = new System.Drawing.Point(325, 148);
            lblDonGia.Name = "lblDonGia";
            lblDonGia.Size = new System.Drawing.Size(57, 17);
            lblDonGia.TabIndex = 5;
            lblDonGia.Text = "Đơn giá:";
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblSoLuong.Location = new System.Drawing.Point(325, 110);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new System.Drawing.Size(64, 17);
            lblSoLuong.TabIndex = 4;
            lblSoLuong.Text = "Số lượng:";
            // 
            // lblNgayNhap
            // 
            lblNgayNhap.AutoSize = true;
            lblNgayNhap.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblNgayNhap.Location = new System.Drawing.Point(20, 148);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Size = new System.Drawing.Size(76, 17);
            lblNgayNhap.TabIndex = 3;
            lblNgayNhap.Text = "Ngày nhập:";
            // 
            // lblTenSach
            // 
            lblTenSach.AutoSize = true;
            lblTenSach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblTenSach.Location = new System.Drawing.Point(20, 110);
            lblTenSach.Name = "lblTenSach";
            lblTenSach.Size = new System.Drawing.Size(62, 17);
            lblTenSach.TabIndex = 2;
            lblTenSach.Text = "Tên sách:";
            // 
            // lblMaSach
            // 
            lblMaSach.AutoSize = true;
            lblMaSach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblMaSach.Location = new System.Drawing.Point(20, 71);
            lblMaSach.Name = "lblMaSach";
            lblMaSach.Size = new System.Drawing.Size(61, 17);
            lblMaSach.TabIndex = 1;
            lblMaSach.Text = "Mã sách:";
            // 
            // lblTheLoai
            // 
            lblTheLoai.AutoSize = true;
            lblTheLoai.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            lblTheLoai.Location = new System.Drawing.Point(20, 33);
            lblTheLoai.Name = "lblTheLoai";
            lblTheLoai.Size = new System.Drawing.Size(57, 17);
            lblTheLoai.TabIndex = 0;
            lblTheLoai.Text = "Thể loại:";
            // 
            // grpChucNang
            // 
            grpChucNang.Controls.Add(btnThoat);
            grpChucNang.Controls.Add(btnXoa);
            grpChucNang.Controls.Add(btnSua);
            grpChucNang.Controls.Add(btnThem);
            grpChucNang.Controls.Add(btnMoi);
            grpChucNang.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            grpChucNang.Location = new System.Drawing.Point(670, 12);
            grpChucNang.Name = "grpChucNang";
            grpChucNang.Size = new System.Drawing.Size(202, 200);
            grpChucNang.TabIndex = 1;
            grpChucNang.TabStop = false;
            grpChucNang.Text = "Chức năng";
            // 
            // btnThoat
            // 
            btnThoat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnThoat.Location = new System.Drawing.Point(20, 158);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new System.Drawing.Size(162, 30);
            btnThoat.TabIndex = 4;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // btnXoa
            // 
            btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnXoa.Location = new System.Drawing.Point(20, 125);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new System.Drawing.Size(162, 30);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnSua.Location = new System.Drawing.Point(20, 92);
            btnSua.Name = "btnSua";
            btnSua.Size = new System.Drawing.Size(162, 30);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnThem
            // 
            btnThem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnThem.Location = new System.Drawing.Point(20, 59);
            btnThem.Name = "btnThem";
            btnThem.Size = new System.Drawing.Size(162, 30);
            btnThem.TabIndex = 1;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnMoi
            // 
            btnMoi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnMoi.Location = new System.Drawing.Point(20, 26);
            btnMoi.Name = "btnMoi";
            btnMoi.Size = new System.Drawing.Size(162, 30);
            btnMoi.TabIndex = 0;
            btnMoi.Text = "Mới";
            btnMoi.UseVisualStyleBackColor = true;
            btnMoi.Click += btnMoi_Click;
            // 
            // grpDanhSach
            // 
            grpDanhSach.Controls.Add(dgvSach);
            grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            grpDanhSach.Location = new System.Drawing.Point(12, 225);
            grpDanhSach.Name = "grpDanhSach";
            grpDanhSach.Size = new System.Drawing.Size(860, 370);
            grpDanhSach.TabIndex = 2;
            grpDanhSach.TabStop = false;
            grpDanhSach.Text = "Danh sách sách";
            // 
            // dgvSach
            // 
            dgvSach.AllowUserToAddRows = false;
            dgvSach.AllowUserToDeleteRows = false;
            dgvSach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSach.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colMaSach, colTenSach, colTheLoai, colNgayNhap, colSoLuong, colDonGia });
            dgvSach.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dgvSach.Location = new System.Drawing.Point(3, 21);
            dgvSach.MultiSelect = false;
            dgvSach.Name = "dgvSach";
            dgvSach.ReadOnly = true;
            dgvSach.RowHeadersWidth = 35;
            dgvSach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvSach.Size = new System.Drawing.Size(854, 346);
            dgvSach.TabIndex = 0;
            dgvSach.CellClick += dgvSach_CellClick;
            // 
            // colMaSach
            // 
            colMaSach.DataPropertyName = "MaSach";
            colMaSach.FillWeight = 85F;
            colMaSach.HeaderText = "Mã sách";
            colMaSach.Name = "colMaSach";
            colMaSach.ReadOnly = true;
            // 
            // colTenSach
            // 
            colTenSach.DataPropertyName = "TenSach";
            colTenSach.FillWeight = 160F;
            colTenSach.HeaderText = "Tên sách";
            colTenSach.Name = "colTenSach";
            colTenSach.ReadOnly = true;
            // 
            // colTheLoai
            // 
            colTheLoai.DataPropertyName = "TenLoai";
            colTheLoai.FillWeight = 110F;
            colTheLoai.HeaderText = "Thể loại";
            colTheLoai.Name = "colTheLoai";
            colTheLoai.ReadOnly = true;
            // 
            // colNgayNhap
            // 
            colNgayNhap.DataPropertyName = "NgayNhapFormatted";
            colNgayNhap.FillWeight = 85F;
            colNgayNhap.HeaderText = "Ngày nhập";
            colNgayNhap.Name = "colNgayNhap";
            colNgayNhap.ReadOnly = true;
            // 
            // colSoLuong
            // 
            colSoLuong.DataPropertyName = "SoLuong";
            colSoLuong.FillWeight = 60F;
            colSoLuong.HeaderText = "Số lượng";
            colSoLuong.Name = "colSoLuong";
            colSoLuong.ReadOnly = true;
            // 
            // colDonGia
            // 
            colDonGia.DataPropertyName = "DonGiaFormatted";
            colDonGia.FillWeight = 80F;
            colDonGia.HeaderText = "Đơn giá";
            colDonGia.Name = "colDonGia";
            colDonGia.ReadOnly = true;
            // 
            // errProvider
            // 
            errProvider.ContainerControl = this;
            // 
            // Form1
            // 
            AcceptButton = btnThem;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnThoat;
            ClientSize = new System.Drawing.Size(884, 611);
            Controls.Add(grpDanhSach);
            Controls.Add(grpChucNang);
            Controls.Add(grpThongTin);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quản lý sách – {Mã sinh viên} – {Họ tên sinh viên}";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            grpChucNang.ResumeLayout(false);
            grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSach).EndInit();
            ((System.ComponentModel.ISupportInitialize)errProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.Label lblMaSach;
        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.GroupBox grpChucNang;
        private System.Windows.Forms.Button btnMoi;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.DataGridView dgvSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTheLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.ErrorProvider errProvider;
    }
}
