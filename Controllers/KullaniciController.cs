using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("[controller]")]
public class KullaniciController : ControllerBase
{
    private readonly IOptions<ConfigurationSettings> _configurationSettings;
    public KullaniciController(IOptions<ConfigurationSettings> config)
    {
        _configurationSettings = config;
    }

    [HttpPost("YorumEkle")]
    public IActionResult YorumEkle([FromBody] Yorum yorum)
    {
        var result = KullaniciService.YorumEkle(yorum, _configurationSettings.Value.ConnectionString);
        if (!result.Contains("Hata"))
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost("KullaniciEkle")]
    public ActionResult KullaniciEkle([FromBody] Kullanici kullanici)
    {
        try
        {
            KullaniciService.KullaniciEkle(kullanici, _configurationSettings.Value.ConnectionString);
            return Ok("Kullanýcý kaydý baþarýyla tamamlandý !");
        }
        catch (Exception ex)
        {
            return BadRequest($"Kullanýcý kaydedilemedi: {ex.Message}");
        }
    }
}
