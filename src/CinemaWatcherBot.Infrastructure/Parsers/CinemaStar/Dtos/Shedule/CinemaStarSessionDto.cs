namespace CinemaWatcherBot.Infrastructure.Parsers.CinemaStar.Dtos.Shedule;

public class CinemaStarSessionDto
{
    public int Id { get; set; }
    public string BusinessDate { get; set; }
    public string Showtime { get; set; }
    public string CloseDate { get; set; }
    public string Format { get; set; } = string.Empty;
    public bool Disabled { get; set; }
    public int StandardPrice { get; set; }
}