DROP TABLE IF EXISTS `pengumuman`;
CREATE TABLE `pengumuman` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `uuid` VARCHAR(36) DEFAULT NULL,
  `isi` TEXT NOT NULL,
  `file` VARCHAR(500) DEFAULT NULL,
  `url` VARCHAR(1000) DEFAULT NULL,
  `type` VARCHAR(20) NOT NULL DEFAULT 'pengumuman', 
  `type_target` VARCHAR(20) NOT NULL DEFAULT 'all', 
  `nidn` VARCHAR(50) DEFAULT NULL,
  `kode_fakultas` CHAR(9) DEFAULT NULL,
  `created_at` DATETIME DEFAULT NULL,
  `updated_at` DATETIME DEFAULT NULL,
  `type_expire` VARCHAR(20) DEFAULT 'no expire',
  `tanggal_awal` DATETIME DEFAULT NULL,
  `tanggal_akhir` DATETIME DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
