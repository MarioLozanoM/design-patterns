CREATE TABLE Products (
    Id INT PRIMARY KEY,
    Name VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10, 2),
    Rating FLOAT
);
GO

INSERT INTO Products (Id, Name, Category, Price, Rating) VALUES
(1, 'Laptop', 'Electronics', 999.99, 4.5),
(2, 'Smartphone', 'Electronics', 499.99, 4.7),
(3, 'Tablet', 'Electronics', 299.99, 4.3),
(4, 'Headphones', 'Accessories', 199.99, 4.6),
(5, 'Smartwatch', 'Wearables', 249.99, 4.2),
(6, 'Camera', 'Electronics', 799.99, 4.8),
(7, 'Monitor', 'Electronics', 299.99, 4.4),
(8, 'Keyboard', 'Accessories', 49.99, 4.1),
(9, 'Mouse', 'Accessories', 29.99, 4.0),
(10, 'Charger', 'Accessories', 19.99, 3.9);
GO