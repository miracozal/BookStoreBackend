using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Kitap
    {
        [Key]
        public long? ID { get; set; }
        public string Baslik { get; set; }
        public string Yazar { get; set; }
        public int YayinYili { get; set; }
        public decimal Fiyat { get; set; }
        public decimal? OrtalamaPuan { get; set; }
    }
}
