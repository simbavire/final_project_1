CREATE SCHEMA IF NOT EXISTS `WebShop` DEFAULT CHARACTER SET utf8;

CREATE TABLE IF NOT EXISTS `WebShop`.`Categories` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

INSERT INTO Categories (`name`) VALUES
('dresses'), ('shoes'), ('underwear');

CREATE TABLE IF NOT EXISTS `WebShop`.`Suppliers` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(80) NOT NULL,
  `contact` VARCHAR(80) NOT NULL,
  `telephoneNumber` VARCHAR(45) NOT NULL,
  `emailAddress` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

INSERT INTO `Suppliers` (`name`, `contact`, `telephoneNumber`, `emailAddress`) VALUES
('D&A', 'Ceraukste, distr. Bauska', '+371 26658866', 'da@bauska.lv'),
('Līga L', 'Stacijas 3 str., Bauska', '+371 25566992', 'ligal@inbox.lv');

CREATE TABLE IF NOT EXISTS `WebShop`.`Orders` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `customerName` VARCHAR(45) NOT NULL,
  `customerSurname` VARCHAR(45) NOT NULL,
  `customerEmailAddress` VARCHAR(45) NOT NULL,
  `customerTelephoneNumber` VARCHAR(45) NOT NULL,
  `status` ENUM('entered', 'in processing', 'canceled', 'done') NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;

INSERT INTO `Orders` (`customerName`, `customerSurname`, `customerEmailAddress`, `customerTelephoneNumber`, `status`) VALUES
('Inga', 'Bērziņa', 'inga@tvnet.lv', '+371 22556699', 'entered'),
('Zanda', 'Sauša', 'zanda@nkll.lv', '+333 5554448841', 'done'),
('Jančuks', 'Zaķis', 'zakis@k.lv', '+371 22336655', 'done');

CREATE TABLE IF NOT EXISTS `WebShop`.`Products` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `description` VARCHAR(140) NOT NULL,
  `price` DECIMAL(9,2) UNSIGNED NOT NULL,
  `warrantyPeriod` INT UNSIGNED NOT NULL DEFAULT 0,
  `categoryId` INT UNSIGNED NOT NULL,
  `supplierId` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Products_Suppliers1_idx` (`supplierId` ASC) VISIBLE,
  INDEX `fk_Products_Categories_idx` (`categoryId` ASC) VISIBLE,
  CONSTRAINT `fk_Products_Categories`
    FOREIGN KEY (`categoryId`)
    REFERENCES `WebShop`.`Categories` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `fk_Products_Suppliers`
    FOREIGN KEY (`supplierId`)
    REFERENCES `WebShop`.`Suppliers` (`id`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT)
ENGINE = InnoDB;

INSERT INTO Products (`name`, `description`, `price`,  `warrantyPeriod`, `categoryId`, `supplierId` ) VALUES
('dresses'), ('shoes'), ('underwear');




