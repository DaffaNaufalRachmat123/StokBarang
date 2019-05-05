using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace StokBarangApp
{
    public class SqlCommand
    {
        public MySqlConnection connection;
        public SqlCommand()
        {
            string strConnect = "Server = " + Properties.Settings.Default.database_server + "; port = " + Properties.Settings.Default.mysql_port + "; database = " + Properties.Settings.Default.database_name + "; Uid = " + Properties.Settings.Default.username_db + "; Pwd = " + Properties.Settings.Default.password_db + ";";
            connection = new MySqlConnection(strConnect);
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }
        #region barang_area
        public void save_barang(string nama_produk , string jenis_produk , string merek_produk , int harga_beli , int harga_barang , int jumlah_produk , int jumlah_beli, int total_penjualan , string kadaluarsa , string tanggal_beli , string kode_supplier)
        {
            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO stok_barang (nama_produk , jenis_produk , merek_produk , harga_beli,  harga_produk , jumlah_produk , jumlah_beli , total_penjualan , tanggal_beli, tanggal_kadaluarsa , kode_supplier) VALUES (?,?,?,?,?,?,?,?,?,?,?);", connection))
                {
                    command.Parameters.AddWithValue("nama_produk", nama_produk);
                    command.Parameters.AddWithValue("jenis_produk", jenis_produk);
                    command.Parameters.AddWithValue("merek_produk", merek_produk);
                    command.Parameters.AddWithValue("harga_beli", harga_beli);
                    command.Parameters.AddWithValue("harga_barang", harga_barang);
                    command.Parameters.AddWithValue("jumlah_produk", jumlah_produk);
                    command.Parameters.AddWithValue("jumlah_beli", jumlah_beli);
                    command.Parameters.AddWithValue("total_penjualan", total_penjualan);
                    command.Parameters.AddWithValue("tanggal_beli", tanggal_beli);
                    command.Parameters.AddWithValue("tanggal_kadaluarsa", kadaluarsa);
                    command.Parameters.AddWithValue("kode_supplier", kode_supplier);
                    int responseCode = command.ExecuteNonQuery();
                    if(responseCode == 1)
                    {
                        MessageBox.Show("[+]Data Tersimpan[+]", "Message", MessageBoxButtons.OK , MessageBoxIcon.Information);
                    } else
                    {
                        MessageBox.Show("[-]Data Tak Tersimpan[-]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void update_barang(string nama_produk , string jenis_produk , string merek_produk ,int harga_beli , int harga_barang , int jumlah_produk , int jumlah_beli, int total_penjualan , string tanggal_beli, string kadaluarsa , string kode_supplier, int id)
        {
            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("UPDATE stok_barang SET nama_produk = ? , jenis_produk = ? , merek_produk = ? , harga_beli = ?, harga_produk = ?, jumlah_produk = ?, jumlah_beli = ?, total_penjualan = ? , tanggal_beli = ? , tanggal_kadaluarsa = ? , kode_supplier = ? WHERE id = ?", connection))
                {
                    command.Parameters.AddWithValue("nama_produk", nama_produk);
                    command.Parameters.AddWithValue("jenis_produk", jenis_produk);
                    command.Parameters.AddWithValue("merek_produk", merek_produk);
                    command.Parameters.AddWithValue("harga_beli", harga_beli);
                    command.Parameters.AddWithValue("harga_produk", harga_barang);
                    command.Parameters.AddWithValue("jumlah_produk", jumlah_produk);
                    command.Parameters.AddWithValue("jumlah_beli", jumlah_beli);
                    command.Parameters.AddWithValue("total_penjualan", total_penjualan);
                    command.Parameters.AddWithValue("tanggal_beli", tanggal_beli);
                    command.Parameters.AddWithValue("tanggal_kadaluarsa", kadaluarsa);
                    command.Parameters.AddWithValue("kode_supplier", kode_supplier);
                    command.Parameters.AddWithValue("id", id);
                    int responseCode = command.ExecuteNonQuery();
                    if (responseCode == 1)
                        MessageBox.Show("[!]Data Berhasil Diperbarui[!]", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("[-]Data Tak Berhasil Diperbarui[-]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void Delete(int id)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("DELETE FROM stok_barang WHERE id = ?", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    int responseCode = await command.ExecuteNonQueryAsync();
                    if (responseCode == 1)
                        MessageBox.Show("[+]Data Terhapus[+]", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("[-]Gagal Terhapus[-]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<DataTable> tampil_barang()
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM stok_barang", connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if(dt != null)
                        {
                            return dt;
                        } else
                        {
                            return null;
                        }
                    }
                }
            } catch
            {
                return null;
            }
        }
        #endregion
        #region users_area
        public int Login(string username , string password)
        {
            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username = ? AND password = ?", connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("password", password);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt.Rows.Count;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
        public async void Register(string username , string password)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO users (username , password) VALUES (?,?)", connection))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);
                    int responseCode = await cmd.ExecuteNonQueryAsync();
                    if(responseCode == 1)
                    {
                        MessageBox.Show("[+]Berhasi Registrasi[+]", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    } else
                    {
                        MessageBox.Show("[-]Registrasi Gagal[-]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region stok_penjualan
        public async Task<int> check_data(string nama_produk)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM stok_penjualan WHERE nama_produk = ?", connection))
                {
                    using(MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table.Rows.Count;
                    }
                }
            } catch
            {
                return 0;
            }
        }
        public void save_penjualan(string nama_pembeli , int total_harga , string nama_produk , string jenis_produk , string merek_produk , int jumlah_penjualan , int harga_produk , string penjualan , int total_untung)
        {
            try
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO stok_penjualan (nama_pembeli,total_harga,nama_produk,jenis_produk,merek_produk,jumlah_penjualan,harga_produk,penjualan,total_untung) VALUES (?,?,?,?,?,?,?,?,?)", connection))
                {
                    cmd.Parameters.AddWithValue("nama_pembeli", nama_pembeli);
                    cmd.Parameters.AddWithValue("total_harga", total_harga);
                    cmd.Parameters.AddWithValue("nama_produk", nama_produk);
                    cmd.Parameters.AddWithValue("jenis_produk", jenis_produk);
                    cmd.Parameters.AddWithValue("merek_produk", merek_produk);
                    cmd.Parameters.AddWithValue("jumlah_penjualan", jumlah_penjualan);
                    cmd.Parameters.AddWithValue("harga_produk", harga_produk);
                    cmd.Parameters.AddWithValue("penjualan", penjualan);
                    cmd.Parameters.AddWithValue("total_untung", total_untung);
                    int code = cmd.ExecuteNonQuery();
                    if (code == 1)
                        MessageBox.Show("[+]Data Masuk Ke Penjualan[+]", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("[-]Data Tak Masuk Ke Penjualan[-]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<DataTable> tampil_penjualan()
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM stok_penjualan ORDER BY penjualan ASC", connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dta = new DataTable();
                        adapter.Fill(dta);
                        return dta;
                    }
                }
            } catch
            {
                return null;
            }
        }
        public async void delete_penjualan(int id)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("DELETE FROM stok_penjualan WHERE id = ?", connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    int code = await command.ExecuteNonQueryAsync();
                    if (code == 1)
                        MessageBox.Show("[-]Data Terhapus[-]", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("[!]Data Tak Terhapus[!]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region User
        public async void save_user(string username , string password)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO users (username , password) VALUES (?,?)", connection))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", new Encryption("StokBarangApp").encrypt(password));
                    int responseCode = await cmd.ExecuteNonQueryAsync();
                    if (responseCode == 1)
                        MessageBox.Show("User Berhasil Ditambahkan", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("User Gagal Ditambahkan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void update_user(string username , string password , int id)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand command = new MySqlCommand("UPDATE users SET username = ? , password = ? WHERE id = ?", connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("password", new Encryption("StokBarangApp").encrypt(password));
                    command.Parameters.AddWithValue("id", id);
                    int code = await command.ExecuteNonQueryAsync();
                    if (code == 1)
                        MessageBox.Show("User Berhasil Diubah", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("User Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void delete_user(int id)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM users WHERE id = ?", connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    int code = await cmd.ExecuteNonQueryAsync();
                    if (code == 1)
                        MessageBox.Show("User Terhapus", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("User Gagal Terhapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<DataTable> tampil_user()
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM users", connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
        #endregion
        #region Supplier
        public async Task<DataTable> getAllSupplierCodes()
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT kode_supplier FROM supplier", connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            } catch
            {
                return null;
            }
        }
        public async void save_supplier(string kode_supplier , string nama , string alamat , string telepon , string email , string tanggal_daftar)
        {
            try
            {
                await connection.OpenAsync();
                using(MySqlCommand cmd = new MySqlCommand("INSERT INTO supplier VALUES(?,?,?,?,?,?)" , connection))
                {
                    cmd.Parameters.AddWithValue("kode_supplier", kode_supplier);
                    cmd.Parameters.AddWithValue("nama_produk", nama);
                    cmd.Parameters.AddWithValue("alamat_supplier", alamat);
                    cmd.Parameters.AddWithValue("telepon_supplier", telepon);
                    cmd.Parameters.AddWithValue("email_supplier", email);
                    cmd.Parameters.AddWithValue("tanggal_daftar", tanggal_daftar);
                    int code = await cmd.ExecuteNonQueryAsync();
                    if (code == 1)
                        MessageBox.Show("Data Tersimpan", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Gagal Tersimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async void update_supplier(string nama, string alamat, string telepon, string email, string tanggal_daftar, string kode_lama)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("UPDATE supplier SET nama_supplier = '" + nama + "' , alamat_supplier = '" + alamat + "' , telepon_supplier = '" + telepon + "', email_supplier = '" + email + "', tanggal_daftar = '" + tanggal_daftar + "' WHERE kode_supplier = '" + kode_lama + "'" , connection))
                {
                    int code = await cmd.ExecuteNonQueryAsync();
                    if (code == 1)
                        MessageBox.Show("Data Terupdate", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Gagal Terupdate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<DataTable> tampil_supplier()
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM supplier", connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count == 0)
                            return null;
                        else
                            return table;
                    }
                }
            } catch
            {
                return null;
            }
        }
        public async void delete_supplier(string kode_supplier)
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("DELETE FROM supplier WHERE kode_supplier = ?", connection))
                {
                    cmd.Parameters.AddWithValue("kode_supplier", kode_supplier);
                    int code = await cmd.ExecuteNonQueryAsync();
                    if (code == 1)
                        MessageBox.Show("Data Terhapus", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Gagal Terhapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<DataTable> load_supplier_code()
        {
            try
            {
                await connection.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand("SELECT kode_supplier FROM supplier", connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dta = new DataTable();
                        adapter.Fill(dta);
                        return dta;
                    }
                }
            } catch
            {
                return null;
            }
        }
        #endregion
    }
}
