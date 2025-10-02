DROP TABLE IF EXISTS `referensi`;
CREATE TABLE `referensi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_kebaruan_referensi` int(11) NOT NULL,
  `id_relevansi_kualitas_referensi` int(11) NOT NULL,
  `nilai` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_kebaruan_referensi` (`id_kebaruan_referensi`,`id_relevansi_kualitas_referensi`),
  KEY `id_kebaruan_referensi_2` (`id_kebaruan_referensi`,`id_relevansi_kualitas_referensi`),
  KEY `id_relevansi_kualitas_referensi` (`id_relevansi_kualitas_referensi`) 
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;