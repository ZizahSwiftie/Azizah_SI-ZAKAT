## 🛡️ Skenario Pengujian Keamanan: SQL Injection (UCP 2 Task)

Sebagai pemenuhan tugas UCP 2, celah keamanan **SQL Injection (SQLi)** sengaja diterapkan pada **Form Login** dengan mengubah query aman menjadi query dinamis konvensional.

### 1. Titik Celah Keamanan (Vulnerability Code)
Pada `FormLogin.cs`, parameter input langsung digabungkan ke dalam string query secara mentah tanpa menggunakan parameter ADO.NET:
```csharp
string query = "SELECT * FROM Tabel_Warga WHERE nama = '" + txtUsername.Text + "' AND password = '" + txtPassword.Text + "'";
SELECT * FROM Tabel_Warga WHERE nama = 'admin' OR '1'='1' AND password = 'xyz'

### 📋 Status Tugas UCP 2
- [x] Stored Procedure (Insert, Update, Delete, Search)
- [x] Data Binding View & Binding Navigator
- [x] Dokumentasi Skenario SQL Injection