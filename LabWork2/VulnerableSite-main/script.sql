CREATE DATABASE lab_security;
USE lab_security;
CREATE TABLE users (
 id INT AUTO_INCREMENT PRIMARY KEY,
 username VARCHAR(255) NOT NULL,
 password VARCHAR(255) NOT NULL,
 email VARCHAR(255) NOT NULL
);
CREATE TABLE comments (
 id INT AUTO_INCREMENT PRIMARY KEY,
 username VARCHAR(255) NOT NULL,
 comment TEXT NOT NULL
);
-- Добавляем пользователя для тестирования
INSERT INTO users (username, password, email) VALUES
('admin', 'password', 'example@mail.ru');
INSERT INTO users (username, password, email) VALUES
('user', 'password', 'other@mail.ru');
CREATE USER 'lab_security'@'localhost' identified with
mysql_native_password BY 'lab_security';
GRANT ALL PRIVILEGES ON lab_security.* TO
'lab_security'@'localhost';