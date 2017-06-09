CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(50), rating INT);
CREATE TABLE clients (id INT IDENTITY(1,1), name VARCHAR(50), stylist_id INT);
