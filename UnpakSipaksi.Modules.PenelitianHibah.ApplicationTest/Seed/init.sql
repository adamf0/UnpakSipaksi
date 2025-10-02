DROP TABLE IF EXISTS `penelitian_internal_anggota_dosen`;
CREATE TABLE `penelitian_internal_anggota_dosen` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_pdp` int(11) NOT NULL,
  `NIDN` varchar(50) NOT NULL,
  `status` tinyint(1) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pdp` (`id_pdp`) 
) ENGINE=InnoDB AUTO_INCREMENT=4035 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP TABLE IF EXISTS `penelitian_internal_anggota_non_dosen`;
CREATE TABLE `penelitian_internal_anggota_non_dosen` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_pdp` int(11) NOT NULL,
  `nim` varchar(50) DEFAULT NULL,
  `bukti_mbkm` text DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pdp` (`id_pdp`) 
) ENGINE=InnoDB AUTO_INCREMENT=2266 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP TABLE IF EXISTS `penelitian_internal_anggota_non_dosen2`;
CREATE TABLE `penelitian_internal_anggota_non_dosen2` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_pdp` int(11) NOT NULL,
  `nomorIdentitas` varchar(255) DEFAULT NULL,
  `nama` varchar(255) DEFAULT NULL,
  `afiliasi` varchar(255) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `penelitian_internal_anggota_non_dosen_ibfk_1_copy` (`id_pdp`) 
) ENGINE=InnoDB AUTO_INCREMENT=763 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP TABLE IF EXISTS `penelitian_internal_luaran`;
CREATE TABLE `penelitian_internal_luaran` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_pdp` int(11) DEFAULT NULL,
  `id_pdp_kategori` int(11) DEFAULT NULL,
  `id_pdp_kategori_luaran` int(11) DEFAULT NULL,
  `keterangan` text DEFAULT NULL,
  `link` text DEFAULT NULL,
  `jenis` enum('','wajib','tambahan') DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pdp` (`id_pdp`),
  KEY `id_pdp_kategori` (`id_pdp_kategori`),
  KEY `id_pdp_kategori_luaranpdp_kategori_luaran` (`id_pdp_kategori_luaran`) 
) ENGINE=InnoDB AUTO_INCREMENT=5150 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP TABLE IF EXISTS `penelitian_internal_dokumen_pendukung`;
CREATE TABLE `penelitian_internal_dokumen_pendukung` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_pdp` int(11) NOT NULL,
  `file_mitra` text DEFAULT NULL,
  `link` text DEFAULT NULL,
  `kategori` text NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pdp` (`id_pdp`) 
) ENGINE=InnoDB AUTO_INCREMENT=1275 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP TABLE IF EXISTS `penelitian_internal_dokumen_kontrak`;
CREATE TABLE `penelitian_internal_dokumen_kontrak` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_pdp` int(11) DEFAULT NULL,
  `file_kontrak` text DEFAULT NULL,
  `link_kontrak` text DEFAULT NULL,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pdp` (`id_pdp`) 
) ENGINE=InnoDB AUTO_INCREMENT=341 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP TABLE IF EXISTS `penelitian_internal_substansi_usulan`;
CREATE TABLE `penelitian_internal_substansi_usulan` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_pdp` int(11) NOT NULL,
  `file` text DEFAULT NULL,
  `link` text DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_pdp` (`id_pdp`) 
) ENGINE=InnoDB AUTO_INCREMENT=1903 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;