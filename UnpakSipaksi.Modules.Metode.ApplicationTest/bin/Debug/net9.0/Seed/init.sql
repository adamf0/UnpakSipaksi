DROP TABLE IF EXISTS `metode`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `metode` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(36) DEFAULT NULL,
  `id_akurasi_penelitian` int(11) NOT NULL,
  `id_kejelasan_pembagian_tugas_tim` int(11) NOT NULL,
  `id_kesesuaian_waktu_rab_luaran_fasilitas` int(11) NOT NULL,
  `id_potensi_ketercapaian_luaran_dijanjikan` int(11) NOT NULL,
  `id_model_feasibility_study` int(11) NOT NULL,
  `id_kesesuaian_tkt` int(11) NOT NULL,
  `id_kredibilitas_mitra_dukungan` int(11) NOT NULL,
  `nilai` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_akurasi_penelitian` (`id_akurasi_penelitian`,`id_kejelasan_pembagian_tugas_tim`,`id_kesesuaian_waktu_rab_luaran_fasilitas`,`id_potensi_ketercapaian_luaran_dijanjikan`,`id_model_feasibility_study`,`id_kesesuaian_tkt`,`id_kredibilitas_mitra_dukungan`),
  KEY `id_akurasi_penelitian_2` (`id_akurasi_penelitian`,`id_kejelasan_pembagian_tugas_tim`,`id_kesesuaian_waktu_rab_luaran_fasilitas`,`id_potensi_ketercapaian_luaran_dijanjikan`,`id_model_feasibility_study`,`id_kesesuaian_tkt`,`id_kredibilitas_mitra_dukungan`),
  KEY `id_kejelasan_pembagian_tugas_tim` (`id_kejelasan_pembagian_tugas_tim`),
  KEY `id_kesesuaian_tkt` (`id_kesesuaian_tkt`),
  KEY `id_kesesuaian_waktu_rab_luaran_fasilitas` (`id_kesesuaian_waktu_rab_luaran_fasilitas`),
  KEY `id_kredibilitas_mitra_dukungan` (`id_kredibilitas_mitra_dukungan`),
  KEY `id_model_feasibility_study` (`id_model_feasibility_study`),
  KEY `id_potensi_ketercapaian_luaran_dijanjikan` (`id_potensi_ketercapaian_luaran_dijanjikan`) 
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;