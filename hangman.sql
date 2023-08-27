DROP DATABASE IF EXISTS hangman;
CREATE DATABASE hangman;
USE  hangman;





CREATE TABLE woerter(
    
    id INT AUTO_INCREMENT,
    begriff VARCHAR(60),
    hinweis VARCHAR(100),
   
    PRIMARY KEY (id)

);



