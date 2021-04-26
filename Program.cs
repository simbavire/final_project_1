using System;
// dotnet add package MySql.Data
using MySql.Data;
using MySql.Data.MySqlClient;

namespace sql_workwith
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load the connector class and create a connection to the database.

            string connString = "server=localhost;port=3306;database=Webshop;user=newuser;password=pard!";
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            MySqlCommand cmd;
            int rowsAffected;

            // Add 3 records at each table from C# code.

            cmd = new MySqlCommand("INSERT INTO Categories (`name`) VALUES " +
                "('Laptops'), ('Headphones'), ('Speakers')", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"INSERT INTO Categories: {rowsAffected} rows affected.");

            cmd = new MySqlCommand(@"
INSERT INTO Suppliers (`name`, contact, telephoneNumber, emailAddress) VALUES
    ('Big Distributor Inc.', 'John Doe', '+37122345345', 'john.doe@big-distributor.com'),
    ('Pear', 'Jane Appleseed', '+37127288388', 'j.apple@pear.com'),
    ('The Other Company', 'Adam J Peterson', '+37129001002', 'apet@other.com')", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"INSERT INTO Suppliers: {rowsAffected} rows affected.");

            cmd = new MySqlCommand(@"
INSERT INTO Products (`name`, `description`, price, warrantyPeriod, categoryId, supplierId) VALUES
    ('Icer 13 inch Ultra HD Portable Laptop', 'The perfect every-day companion for your on-the-go business needs!', 1497.77, 36, 1, 1),
    ('EarPods Pro', 'Redefine listening to music with the new EarPods Pro.', 499.00, 18, 2, 2),
    ('Yahama Floor Speakers', 'Sound quality you can trust.', 650.00, 18, 3, 3)", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"INSERT INTO Products: {rowsAffected} rows affected.");

            cmd = new MySqlCommand(@"
INSERT INTO Orders(customerName, customerSurname, customerEmailAddress, customerTelephoneNumber, `status`) VALUES
    ('Eric', 'Rosen', 'e.rosen@gmail.com', '+37126263636', 'done'),
    ('Alyssa', 'Kane', 'alyssa@yahoo.com', '+37122009372', 'entered'),
    ('Bruno', 'Jewel', 'bruno@jewel.net', '+37127001001', 'canceled')", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"INSERT INTO Orders: {rowsAffected} rows affected.");

            cmd = new MySqlCommand("INSERT INTO OrderProducts (orderId, productId, quantity) VALUES " +
                "(1, 1, 1), (1, 2, 2), (2, 3, 1)", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"INSERT INTO OrderProducts: {rowsAffected} rows affected.");

            // Try to update some records from C# code.

            cmd = new MySqlCommand("UPDATE Products SET price = 399 WHERE id = 2", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"UPDATE Products: {rowsAffected} rows affected.");

            cmd = new MySqlCommand("UPDATE Categories SET `name` = 'Portable Computers' WHERE id = 1", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"UPDATE Categories: {rowsAffected} rows affected.");

            cmd = new MySqlCommand("UPDATE Orders SET `status` = 'in processing' WHERE id = 2", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"UPDATE Orders: {rowsAffected} rows affected.");

            // Try to delete one record from C# code.

            cmd = new MySqlCommand("DELETE FROM Orders WHERE id = 3", conn);
            rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine($"DELETE FROM Orders: {rowsAffected} rows affected.");

            Console.WriteLine();

            // Try to build 3 simple SELECT statements with WHERE, ORDER BY from C# code

            cmd = new MySqlCommand("SELECT id, price, `name` FROM Products " +
                "WHERE price > 500 ORDER BY price ASC", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("id \t| price \t| name (FROM Products)");
                while (reader.Read())
                {
                    uint id = (uint)reader[0];
                    decimal price = (decimal)reader[1];
                    string name = (string)reader[2];

                    Console.WriteLine($"{id} \t| {price} \t| {name}");
                }
                Console.WriteLine();
            }

            cmd = new MySqlCommand("SELECT orderId, productId, quantity FROM OrderProducts " +
                "WHERE quantity < 5 ORDER BY orderId, productId", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("orderId | productId \t| quantity (FROM OrderProducts)");
                while (reader.Read())
                {
                    uint orderId = (uint)reader[0];
                    uint productId = (uint)reader[1];
                    uint quantity = (uint)reader[2];

                    Console.WriteLine($"{orderId} \t| {productId} \t\t| {quantity}");
                }
                Console.WriteLine();
            }

            cmd = new MySqlCommand(@"
SELECT id, customerName, customerSurname, `status` FROM Orders
    WHERE `status` != 'canceled' AND `status` != 'entered'
    ORDER BY `status`, customerSurname, customerName", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("id \t| name \t| surname \t| status (FROM Orders)");
                while (reader.Read())
                {
                    uint id = (uint)reader[0];
                    string customerName = (string)reader[1];
                    string customerSurname = (string)reader[2];
                    string status = (string)reader[3];

                    Console.WriteLine($"{id} \t| {customerName} \t| {customerSurname} \t| {status}");
                }
                Console.WriteLine();
            }

            // Try to build 3 advanced SELECT statements with JOIN from C# code.

            cmd = new MySqlCommand("SELECT Products.`name`, Suppliers.`name` FROM Products " +
                "JOIN Suppliers ON Suppliers.id = Products.supplierId", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("productName \t| supplierName");
                while (reader.Read())
                {
                    string productName = (string)reader[0];
                    string supplierName = (string)reader[1];

                    Console.WriteLine($"{productName} \t| {supplierName}");
                }
                Console.WriteLine();
            }

            cmd = new MySqlCommand("SELECT Products.`name`, Categories.`name` FROM Products " +
                "JOIN Categories ON Categories.id = Products.categoryId", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("productName \t| categoryName");
                while (reader.Read())
                {
                    string productName = (string)reader[0];
                    string categoryName = (string)reader[1];

                    Console.WriteLine($"{productName} \t| {categoryName}");
                }
                Console.WriteLine();
            }

            cmd = new MySqlCommand(@"
SELECT Orders.customerName, OrderProducts.quantity, Products.`name`
    FROM Orders
    JOIN OrderProducts ON OrderProducts.orderId = Orders.id
    JOIN Products ON Products.id = OrderProducts.productId", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                Console.WriteLine("name \t| qty \t| product");
                while (reader.Read())
                {
                    string customerName = (string)reader[0];
                    uint quantity = (uint)reader[1];
                    string productName = (string)reader[2];

                    Console.WriteLine($"{customerName} \t| {quantity} \t| {productName}");
                }
                Console.WriteLine();
            }

            conn.Close();
        }
    }
}

