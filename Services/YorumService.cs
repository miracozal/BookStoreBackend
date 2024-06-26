﻿using BookStore.Models;
using System.Data;
using System.Data.SqlClient;

public class YorumService
{

    public static string YorumEkle(Yorum yorum, string connectionString)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_YorumEkle", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@KitapID", yorum.KitapID);
            cmd.Parameters.AddWithValue("@KullaniciID", yorum.KullaniciID);
            cmd.Parameters.AddWithValue("@YorumMetni", yorum.YorumMetni);
            cmd.Parameters.AddWithValue("@Puan", yorum.Puan);

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                return "Yorum başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }
    }

    public static List<Yorum> YorumlariGetir(int kitapID, string connectionString)
    {
        List<Yorum> yorumlar = new List<Yorum>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_YorumlariGetir", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@KitapID", kitapID);

            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Yorum yorum = new Yorum
                    {
                        ID = reader.GetInt32(0),
                        KitapID = reader.GetInt32(1),
                        KullaniciID = reader.GetInt32(2),
                        YorumMetni = reader.GetString(3),
                        Puan = reader.GetInt32(4)
                    };
                    yorumlar.Add(yorum);
                }
            }
        }
        return yorumlar;
    }
}
