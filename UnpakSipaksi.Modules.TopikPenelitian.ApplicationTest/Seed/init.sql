DROP TABLE IF EXISTS `bidang_fokus_penelitian_tema_topik`;
CREATE TABLE `bidang_fokus_penelitian_tema_topik` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_bidang_fokus_penelitian_tema` int(11) NOT NULL,
  `nama` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_bidang_fokus_penelitian_tema` (`id_bidang_fokus_penelitian_tema`) 
);