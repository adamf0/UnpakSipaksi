DROP TABLE IF EXISTS `kategori_skema`;
CREATE TABLE `kategori_skema` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `nama` text NOT NULL,
  `min` int(11) DEFAULT NULL,
  `max` int(11) DEFAULT NULL,
  `old_rule` VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT '{"operation": "and","rules": {}}',
  `rule` VARCHAR(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT '[]',
  `keyName` text DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;