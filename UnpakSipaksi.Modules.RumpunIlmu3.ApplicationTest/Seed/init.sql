DROP TABLE IF EXISTS `rumpun_ilmu3`;
CREATE TABLE `rumpun_ilmu3` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_rumpun_ilmu2` int(11) NOT NULL,
  `nama` varchar(200) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_rumpun_ilmu2` (`id_rumpun_ilmu2`) 
) ENGINE=InnoDB AUTO_INCREMENT=115 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;