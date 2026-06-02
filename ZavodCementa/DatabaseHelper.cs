using System.Data;
using System.Data.SQLite;

namespace ZavodCementa
{
    public static class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=cement_plant.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public static void InitializeDatabase()
        {
            using var connection = GetConnection();
            connection.Open();

            // Таблица клиентов
            string createClientsTable = @"
                CREATE TABLE IF NOT EXISTS Clients (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Phone TEXT NOT NULL,
                    Address TEXT NOT NULL,
                    UUID TEXT NOT NULL UNIQUE
                )";
            using var cmdClients = new SQLiteCommand(createClientsTable, connection);
            cmdClients.ExecuteNonQuery();

            // Таблица сотрудников
            string createEmployeesTable = @"
                CREATE TABLE IF NOT EXISTS Employees (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Phone TEXT NOT NULL,
                    Position TEXT NOT NULL,
                    Salary TEXT NOT NULL,
                    Quota TEXT NOT NULL
                )";
            using var cmdEmployees = new SQLiteCommand(createEmployeesTable, connection);
            cmdEmployees.ExecuteNonQuery();

            // Таблица доставок
            string createDeliveriesTable = @"
                CREATE TABLE IF NOT EXISTS Deliveries (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Sender TEXT NOT NULL,
                    Recipient TEXT NOT NULL,
                    Product TEXT NOT NULL,
                    Volume TEXT NOT NULL,
                    Deadline TEXT NOT NULL
                )";
            using var cmdDeliveries = new SQLiteCommand(createDeliveriesTable, connection);
            cmdDeliveries.ExecuteNonQuery();

            // Таблица поставщиков (НОВАЯ)
            string createSuppliersTable = @"
                CREATE TABLE IF NOT EXISTS Suppliers (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ContactPerson TEXT NOT NULL,
                    Phone TEXT NOT NULL,
                    Email TEXT,
                    Address TEXT,
                    Material TEXT NOT NULL
                )";
            using var cmdSuppliers = new SQLiteCommand(createSuppliersTable, connection);
            cmdSuppliers.ExecuteNonQuery();

            // Таблица оборудования (НОВАЯ)
            string createEquipmentTable = @"
                CREATE TABLE IF NOT EXISTS Equipment (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Type TEXT NOT NULL,
                    Status TEXT NOT NULL,
                    PurchaseDate TEXT,
                    ResponsibleEmployee TEXT
                )";
            using var cmdEquipment = new SQLiteCommand(createEquipmentTable, connection);
            cmdEquipment.ExecuteNonQuery();
        }

        public static DataTable GetDataTable(string sql)
        {
            var dataTable = new DataTable();
            using var connection = GetConnection();
            connection.Open();
            using var adapter = new SQLiteDataAdapter(sql, connection);
            adapter.Fill(dataTable);
            return dataTable;
        }

        public static int ExecuteNonQuery(string sql, SQLiteParameter[] parameters = null)
        {
            using var connection = GetConnection();
            connection.Open();
            using var command = new SQLiteCommand(sql, connection);
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }
    }
}