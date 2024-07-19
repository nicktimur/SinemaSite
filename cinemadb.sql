CREATE DATABASE cinemadb;
USE cinemadb;

CREATE TABLE Kullanici (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    isim VARCHAR(255),
    soyisim VARCHAR(255),
    kullanici_adi VARCHAR(255) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    sifre VARCHAR(255) NOT NULL,
    kullanici_tipi INT NOT NULL DEFAULT 0,
    olusturulma_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    aktif_mi BOOL,
    silinme_tarihi TIMESTAMP NULL DEFAULT NULL,
    guncelleme_tarihi TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    son_aktif_tarih TIMESTAMP NULL DEFAULT NULL
);


CREATE TABLE Sinema (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    isim VARCHAR(255),
    konum VARCHAR(255),
    olusturulma_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    silinme_tarihi TIMESTAMP NULL DEFAULT NULL,
    guncelleme_tarihi TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE Salon (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    toplam_koltuk INTEGER,
    salon_mumarasi INTEGER,
    salon_tipi ENUM('Standart', 'IMAX', '4DX') NOT NULL,
    sinema_id BIGINT,
    olusturulma_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    silinme_tarihi TIMESTAMP NULL DEFAULT NULL,
    guncelleme_tarihi TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (sinema_id) REFERENCES Sinema(id)
);

CREATE TABLE Film (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    isim VARCHAR(255),
    sure INTEGER,
    olusturulma_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    silinme_tarihi TIMESTAMP NULL DEFAULT NULL,
    guncelleme_tarihi TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP    
);

CREATE TABLE Gosterim (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    salon_id BIGINT,
    film_id BIGINT,
	sunum_tarihi DATETIME,
    olusturulma_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    silinme_tarihi TIMESTAMP NULL DEFAULT NULL,
    guncelleme_tarihi TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (salon_id) REFERENCES Salon(id),
    FOREIGN KEY (film_id) REFERENCES Film(id)
);


CREATE TABLE Tickets (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    gosterim_id BIGINT,
	sutun INTEGER,
    satır  INTEGER,
    musteri_id BIGINT,
    satin_alim_tarihi DATE,
    olusturulma_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    silinme_tarihi TIMESTAMP NULL DEFAULT NULL,
    guncelleme_tarihi TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (gosterim_id) REFERENCES Gosterim(id),
    FOREIGN KEY (musteri_id) REFERENCES Kullanici(id)
);