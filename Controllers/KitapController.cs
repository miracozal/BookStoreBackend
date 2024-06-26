using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BookStore.Controllers
{
    public class KitapController : Controller
    {
        private readonly IOptions<ConfigurationSettings> _configurationSettings;
        public KitapController(IOptions<ConfigurationSettings> config)
        {
            _configurationSettings = config;
        }
        [HttpPost("KitapEkle")]
        public ActionResult KitapEkle([FromBody] Kitap kitap)
        {
            var result = KitapService.KitapEkle(kitap, _configurationSettings.Value.ConnectionString);
            if (!result.Contains("Hata"))
            {
                return Ok(new { message = "Kitap başarıyla eklendi." });
            }
            else
            {
                return BadRequest(new { error = "Kitap eklenemedi." });
            }
        }


        [HttpPost("KitapGuncelle")]
        [Authorize]
        public ActionResult<string> KitapGuncelle([FromBody] Kitap kitap)
        {
            var result = KitapService.KitapGuncelle(kitap, _configurationSettings.Value.ConnectionString);
            if (!result.Contains("Hata"))
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new { error = result });
            }
        }

        [HttpGet("Kitaplar")]
        [Authorize]
        public ActionResult<List<Kitap>> Kitaplar()
        {
            var result = KitapService.Kitaplar(_configurationSettings.Value.ConnectionString);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Ok(null);
            }
        }

        [HttpDelete("KitapSil/{id}")]
        [Authorize]
        public ActionResult KitapSil(int id)
        {
            var result = KitapService.KitapSil(id, _configurationSettings.Value.ConnectionString);
            if (!result.Contains("Hata"))
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new { error = result });
            }
        }

        [HttpPost("YorumEkle")]
        [Authorize]
        public ActionResult YorumEkle([FromBody] Yorum yorum)
        {
            var result = YorumService.YorumEkle(yorum, _configurationSettings.Value.ConnectionString);
            if (!result.Contains("Hata"))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("YorumlariGetir/{kitapID}")]
        [Authorize]
        public ActionResult<List<Yorum>> YorumlariGetir(int kitapID)
        {
            var yorumlar = YorumService.YorumlariGetir(kitapID, _configurationSettings.Value.ConnectionString);
            return Ok(yorumlar);
        }
    }
}
