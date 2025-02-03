# Sipaksi Versi 2
ini merupakan prototype aplikasi sipaksi terbaru menggunakan teknologi <b>NET Core 8 API</b> yang dimana akan di integrasikan dengan aplikasi mobile. 

### Tujuan
1. merubah teknologi
2. merubah alur yg cukup besar menjadi lebih kecil
3. modularisasi yang dimana kedepannya bisa jadi microservice jika ingin di pisah per modul
4. penerapan clean architecture = agar rapih, cqrs = pemisahan database dalam write & read (CDC sudah ada  adaptor uji coba bisa tinggal implementasi ke mongo/redis) dan event source = untuk melihat perubahan event yg terjadi
5. loose coupling dan hight cohesion yang lebih nyata. uji coba pada aplikasi siamida dan sipaksi dalam penerapan clean architecture dan cqrs masih dibilang 50% berhasil (maintenance) & 50% gagal (masih terlihat techical bukan domain, jadi antar domain sangat coupling)

### Task
1. pembuatan modularisasi
    <br>1.1 uji coba pembuatan modul
    <br>1.1.1 koneksi database module
    <br>1.1.2 crud per modul
    <br>1.1.3 config per modul
    <br>1.1.4 relasional & join beda koneksi database
2. migrasi semua fitur dosen, lppm, fakultas & auditor
3. penerapan clean architecture
4. penerapan cqrs
    <br>4.1 penyimpanan data ke struktural (sql)
    <br>4.2 penyimpanan data tidak struktural (no-sql)
5. penerapan event sourcing
6. authentication
    <br>6.1 datbase simak
    <br>6.2 migrasi ke keyclock
7. docker
8. reduce image & solve CVE image docker