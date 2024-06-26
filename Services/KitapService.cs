using BookStore.Models;
using System.Data.SqlClient;

namespace BookStore.Services
{
    public class KitapService
    {
        public static string KitapEkle(Kitap kitap, string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("SP_KitapEkle", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Baslik", kitap.Baslik);
                    command.Parameters.AddWithValue("@Yazar", kitap.Yazar);
                    command.Parameters.AddWithValue("@YayinYili", kitap.YayinYili);
                    command.Parameters.AddWithValue("@Fiyat", kitap.Fiyat);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return "Kitap başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }


        public static string KitapGuncelle(Kitap kitap, string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("SP_KitapGuncelle", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ID", kitap.ID);
                    command.Parameters.AddWithValue("@Baslik", kitap.Baslik);
                    command.Parameters.AddWithValue("@Yazar", kitap.Yazar);
                    command.Parameters.AddWithValue("@YayinYili", kitap.YayinYili);
                    command.Parameters.AddWithValue("@Fiyat", kitap.Fiyat);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return "Kitap başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }

        public static List<Kitap> Kitaplar(string connectionString)
        {
            List<Kitap> kitaplar = new List<Kitap>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SP_Kitaplar", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var kitap = new Kitap
                        {
                            ID = (long)reader["ID"],
                            Baslik = reader["Baslik"].ToString(),
                            Yazar = reader["Yazar"].ToString(),
                            YayinYili = (int)reader["YayinYili"],
                            Fiyat = (decimal)reader["Fiyat"]
                        };

                        kitaplar.Add(kitap);
                    }
                }
            }
            return kitaplar;
        }

        public static string KitapSil(int id, string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand("SP_KitapSil", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ID", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return "Kitap başarıyla silindi.";
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }
    }
}
