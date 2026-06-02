using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using ZavodCementa;

namespace ZavodCementa
{
    public partial class Form1 : Form
    {
        private WarehouseControl warehouseControl;
        private DeliveryControl deliveryControl;
        private ClientsControl clientsControl;
        private EmployeesControl employeesControl;

        public Form1()
        {
            InitializeComponent();
            this.listBoxTabs.SelectedIndexChanged += ListBoxTabs_SelectedIndexChanged;
            this.listBoxTabs.SelectedIndex = 0;
        }

        private void ListBoxTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTab = this.listBoxTabs.SelectedItem.ToString();
            this.panelRight.Controls.Clear();

            switch (selectedTab)
            {
                case "Склад":
                    if (warehouseControl == null)
                        warehouseControl = new WarehouseControl();
                    warehouseControl.Dock = DockStyle.Fill;
                    this.panelRight.Controls.Add(warehouseControl);
                    break;
                case "Доставка":
                    if (deliveryControl == null)
                        deliveryControl = new DeliveryControl();
                    deliveryControl.Dock = DockStyle.Fill;
                    this.panelRight.Controls.Add(deliveryControl);
                    break;
                case "Клиенты":
                    if (clientsControl == null)
                        clientsControl = new ClientsControl();
                    clientsControl.Dock = DockStyle.Fill;
                    this.panelRight.Controls.Add(clientsControl);
                    break;
                case "Сотрудники":
                    if (employeesControl == null)
                        employeesControl = new EmployeesControl();
                    employeesControl.Dock = DockStyle.Fill;
                    this.panelRight.Controls.Add(employeesControl);
                    break;
            }
        }
    }

    // ==================== КОНТРОЛ СКЛАДА ====================
    public class WarehouseControl : UserControl
    {
        private FlowLayoutPanel flowLayout;
        private List<Material> materials;

        public WarehouseControl()
        {
            InitializeMaterials();
            SetupUI();
        }

        private void InitializeMaterials()
        {
            materials = new List<Material>
            {
                new Material { Name = "Цемент М500", Quantity = 3200, MaxQuantity = 5000, Unit = "кг", Color = Color.SteelBlue },
                new Material { Name = "Песок", Quantity = 1800, MaxQuantity = 3000, Unit = "кг", Color = Color.SandyBrown },
                new Material { Name = "Гравий", Quantity = 4200, MaxQuantity = 5000, Unit = "кг", Color = Color.DarkGray },
                new Material { Name = "Глина", Quantity = 950, MaxQuantity = 2000, Unit = "кг", Color = Color.SaddleBrown },
                new Material { Name = "Добавка (пластификатор)", Quantity = 340, MaxQuantity = 500, Unit = "л", Color = Color.LightGreen }
            };
        }

        private void SetupUI()
        {
            this.BackColor = Color.WhiteSmoke;
            flowLayout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            this.Controls.Add(flowLayout);

            foreach (var mat in materials)
            {
                var panel = CreateMaterialPanel(mat);
                flowLayout.Controls.Add(panel);
            }
        }

        private Panel CreateMaterialPanel(Material mat)
        {
            Panel mainPanel = new Panel
            {
                Height = 95,
                Margin = new Padding(5),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Width = 800  // Фиксированная ширина панели
            };

            // Используем горизонтальную раскладку
            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5),
                WrapContents = false
            };

            // ========== ЛЕВАЯ ЧАСТЬ: КВАДРАТ С БУКВОЙ ==========
            Panel leftPanel = new Panel
            {
                Width = 70,
                Height = 70,
                Margin = new Padding(0, 5, 10, 5)  // Добавил отступ справа
            };

            PictureBox picture = new PictureBox
            {
                Size = new Size(48, 48),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Image = GenerateMaterialImage(mat.Name, mat.Color),
                BackColor = Color.Transparent
            };
            // Центрируем картинку внутри левой панели
            picture.Location = new Point((leftPanel.Width - picture.Width) / 2, (leftPanel.Height - picture.Height) / 2);
            leftPanel.Controls.Add(picture);

            // ========== ПРАВАЯ ЧАСТЬ: ИНФОРМАЦИЯ ==========
            Panel rightPanel = new Panel
            {
                Width = 700,  // Увеличенная ширина
                Height = 70,
                Margin = new Padding(0, 5, 0, 5)
            };

            // Название материала (увеличено)
            Label lblName = new Label
            {
                Text = mat.Name,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, 0),
                MinimumSize = new Size(200, 0)  // Минимальная ширина для длинных названий
            };

            // ProgressBar (растянут на всю ширину)
            ProgressBar bar = new ProgressBar
            {
                Maximum = 100,
                Value = (int)((double)mat.Quantity / mat.MaxQuantity * 100),
                Width = rightPanel.Width - 100,  // Оставляем место для кнопок
                Height = 25,
                Location = new Point(0, 28)
            };

            // Количество
            Label lblQuantity = new Label
            {
                Text = $"{mat.Quantity} / {mat.MaxQuantity} {mat.Unit}",
                AutoSize = true,
                Location = new Point(0, 56),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkGreen
            };

            // Кнопка "+"
            Button btnPlus = new Button
            {
                Text = "+",
                Location = new Point(rightPanel.Width - 70, 28),
                Size = new Size(32, 25),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnPlus.Click += (s, e) =>
            {
                if (mat.Quantity + 100 <= mat.MaxQuantity)
                    mat.Quantity += 100;
                else
                    mat.Quantity = mat.MaxQuantity;

                bar.Value = (int)(mat.Quantity / mat.MaxQuantity * 100);
                lblQuantity.Text = $"{mat.Quantity} / {mat.MaxQuantity} {mat.Unit}";
            };

            // Кнопка "-"
            Button btnMinus = new Button
            {
                Text = "-",
                Location = new Point(rightPanel.Width - 35, 28),
                Size = new Size(32, 25),
                BackColor = Color.LightCoral,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnMinus.Click += (s, e) =>
            {
                if (mat.Quantity - 100 >= 0)
                    mat.Quantity -= 100;
                else
                    mat.Quantity = 0;

                bar.Value = (int)(mat.Quantity / mat.MaxQuantity * 100);
                lblQuantity.Text = $"{mat.Quantity} / {mat.MaxQuantity} {mat.Unit}";
            };

            rightPanel.Controls.Add(lblName);
            rightPanel.Controls.Add(bar);
            rightPanel.Controls.Add(lblQuantity);
            rightPanel.Controls.Add(btnPlus);
            rightPanel.Controls.Add(btnMinus);

            // Добавляем левую и правую панели
            flowPanel.Controls.Add(leftPanel);
            flowPanel.Controls.Add(rightPanel);

            // Обработчик изменения размера для адаптации
            mainPanel.Resize += (s, e) =>
            {
                if (mainPanel.Width > 400)
                {
                    rightPanel.Width = mainPanel.Width - leftPanel.Width - 30;
                    bar.Width = rightPanel.Width - 80;
                    btnPlus.Location = new Point(rightPanel.Width - 70, 28);
                    btnMinus.Location = new Point(rightPanel.Width - 35, 28);
                }
            };

            mainPanel.Controls.Add(flowPanel);
            return mainPanel;
        }

        private Image GenerateMaterialImage(string name, Color backColor)
        {
            Bitmap bmp = new Bitmap(48, 48);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(backColor);
                g.DrawRectangle(Pens.Black, 0, 0, 47, 47);
                string firstLetter = name.Length > 0 ? name[0].ToString() : "?";
                using (Font font = new Font("Arial", 20, FontStyle.Bold))
                {
                    SizeF textSize = g.MeasureString(firstLetter, font);
                    float x = (48 - textSize.Width) / 2;
                    float y = (48 - textSize.Height) / 2;
                    g.DrawString(firstLetter, font, Brushes.White, x, y);
                }
            }
            return bmp;
        }

        private class Material
        {
            public string Name { get; set; }
            public double Quantity { get; set; }
            public double MaxQuantity { get; set; }
            public string Unit { get; set; }
            public Color Color { get; set; }
        }
    }

    // ==================== КОНТРОЛ ДОСТАВКИ ====================
    public class DeliveryControl : UserControl
    {
        private DataGridView dataGridView;
        private BindingSource bindingSource;
        private DataTable dataTable;
        private Button btnSave;

        public DeliveryControl()
        {
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = true,
                BackgroundColor = Color.White
            };

            btnSave = new Button
            {
                Text = "Сохранить изменения",
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightGreen
            };
            btnSave.Click += BtnSave_Click;

            bindingSource = new BindingSource();
            dataGridView.DataSource = bindingSource;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                RowStyles = { new RowStyle(SizeType.Percent, 100F), new RowStyle(SizeType.Absolute, 35F) }
            };
            mainPanel.Controls.Add(dataGridView, 0, 0);
            mainPanel.Controls.Add(btnSave, 0, 1);
            this.Controls.Add(mainPanel);
        }

        private void LoadData()
        {
            dataTable = DatabaseHelper.GetDataTable("SELECT Id, Sender, Recipient, Product, Volume, Deadline FROM Deliveries");
            if (dataTable.Columns.Contains("Id"))
                dataTable.Columns["Id"].ColumnMapping = MappingType.Hidden;
            bindingSource.DataSource = dataTable;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.EndEdit();
                bindingSource.EndEdit();

                using var connection = DatabaseHelper.GetConnection();
                connection.Open();
                using var adapter = new SQLiteDataAdapter("SELECT * FROM Deliveries", connection);
                var commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                adapter.InsertCommand = commandBuilder.GetInsertCommand();
                adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                adapter.Update((DataTable)bindingSource.DataSource);

                MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // ==================== КОНТРОЛ КЛИЕНТОВ ====================
    public class ClientsControl : UserControl
    {
        private DataGridView dataGridView;
        private BindingSource bindingSource;
        private DataTable dataTable;
        private Button btnSave;

        public ClientsControl()
        {
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = true,
                BackgroundColor = Color.White
            };

            btnSave = new Button
            {
                Text = "Сохранить изменения",
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightGreen
            };
            btnSave.Click += BtnSave_Click;

            bindingSource = new BindingSource();
            dataGridView.DataSource = bindingSource;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                RowStyles = { new RowStyle(SizeType.Percent, 100F), new RowStyle(SizeType.Absolute, 35F) }
            };
            mainPanel.Controls.Add(dataGridView, 0, 0);
            mainPanel.Controls.Add(btnSave, 0, 1);
            this.Controls.Add(mainPanel);
        }

        private void LoadData()
        {
            dataTable = DatabaseHelper.GetDataTable("SELECT Id, Name, Phone, Address, UUID FROM Clients");
            if (dataTable.Columns.Contains("Id"))
                dataTable.Columns["Id"].ColumnMapping = MappingType.Hidden;
            bindingSource.DataSource = dataTable;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.EndEdit();
                bindingSource.EndEdit();

                using var connection = DatabaseHelper.GetConnection();
                connection.Open();
                using var adapter = new SQLiteDataAdapter("SELECT * FROM Clients", connection);
                var commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                adapter.InsertCommand = commandBuilder.GetInsertCommand();
                adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                adapter.Update((DataTable)bindingSource.DataSource);

                MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // ==================== КОНТРОЛ СОТРУДНИКОВ ====================
    public class EmployeesControl : UserControl
    {
        private DataGridView dataGridView;
        private BindingSource bindingSource;
        private DataTable dataTable;
        private Button btnSave;

        public EmployeesControl()
        {
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = true,
                BackgroundColor = Color.White
            };

            btnSave = new Button
            {
                Text = "Сохранить изменения",
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = Color.LightGreen
            };
            btnSave.Click += BtnSave_Click;

            bindingSource = new BindingSource();
            dataGridView.DataSource = bindingSource;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                RowStyles = { new RowStyle(SizeType.Percent, 100F), new RowStyle(SizeType.Absolute, 35F) }
            };
            mainPanel.Controls.Add(dataGridView, 0, 0);
            mainPanel.Controls.Add(btnSave, 0, 1);
            this.Controls.Add(mainPanel);
        }

        private void LoadData()
        {
            dataTable = DatabaseHelper.GetDataTable("SELECT Id, Name, Phone, Position, Salary, Quota FROM Employees");
            if (dataTable.Columns.Contains("Id"))
                dataTable.Columns["Id"].ColumnMapping = MappingType.Hidden;
            bindingSource.DataSource = dataTable;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.EndEdit();
                bindingSource.EndEdit();

                using var connection = DatabaseHelper.GetConnection();
                connection.Open();
                using var adapter = new SQLiteDataAdapter("SELECT * FROM Employees", connection);
                var commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                adapter.InsertCommand = commandBuilder.GetInsertCommand();
                adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                adapter.Update((DataTable)bindingSource.DataSource);

                MessageBox.Show("Данные успешно сохранены!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}