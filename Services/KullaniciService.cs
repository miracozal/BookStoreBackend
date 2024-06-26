using BookStore.Models;
using BookStore.Services;
using System.Data.SqlClient;

public class KullaniciService
{
    public static string YorumEkle(Yorum yorum, string connectionString)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SP_YorumEkle", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@KitapID", yorum.KitapID);
                command.Parameters.AddWithValue("@KullaniciID", yorum.KullaniciID);
                command.Parameters.AddWithValue("@YorumMetni", yorum.YorumMetni);
                command.Parameters.AddWithValue("@Puan", yorum.Puan);

                connection.Open();
                command.ExecuteNonQuery();
            }
            return "Yorum başarıyla eklendi.";
        }
        catch (Exception ex)
        {
            return $"Hata oluştu: {ex.Message}";
        }
    }
    public static void KullaniciEkle(Kullanici kullanici, string connectionString)
    {
        string hashedPassword = LoginService.HashPassword(kullanici.Sifre);

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("SP_KullaniciEkle", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Isim", kullanici.Isim);
            command.Parameters.AddWithValue("@Email", kullanici.Email);
            command.Parameters.AddWithValue("@Sifre", hashedPassword);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
