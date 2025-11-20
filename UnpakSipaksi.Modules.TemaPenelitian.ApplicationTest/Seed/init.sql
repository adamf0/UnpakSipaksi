DROP TABLE IF EXISTS `bidang_fokus_penelitian_tema`;
CREATE TABLE `bidang_fokus_penelitian_tema` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_bidang_fokus_penelitian` int(11) NOT NULL,
  `nama` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_bidang_fokus_penelitian` (`id_bidang_fokus_penelitian`) 
);