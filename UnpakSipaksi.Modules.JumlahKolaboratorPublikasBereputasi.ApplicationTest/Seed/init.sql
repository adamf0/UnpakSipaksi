DROP TABLE IF EXISTS `jumlah_kolaborator_publikasi_bereputasi`;
CREATE TABLE `jumlah_kolaborator_publikasi_bereputasi` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `name` text NOT NULL,
  `bobot_pdp` int(11) NOT NULL DEFAULT 0,
  `bobot_terapan` int(11) NOT NULL DEFAULT 0,
  `bobot_kerjasama` int(11) NOT NULL DEFAULT 0,
  `bobot_penelitian_dasar` int(11) NOT NULL DEFAULT 0,
  `skor` int(11) NOT NULL DEFAULT 0,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;