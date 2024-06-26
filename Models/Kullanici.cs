using System.ComponentModel.DataAnnotations;


public class Kullanici
{
    [Key]
    public long? ID { get; set; }
    public string Isim { get; set; }
    public string Email { get; set; }
    public string Sifre { get; set; }
}
