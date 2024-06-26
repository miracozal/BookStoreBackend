using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Yorum
    {
        [Key]
        public long? ID { get; set; }
        public int KitapID { get; set; }
        public int KullaniciID { get; set; }
        public string YorumMetni { get; set; }
        public int Puan { get; set; }
    }

}
