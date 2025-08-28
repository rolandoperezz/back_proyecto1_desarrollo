namespace BasketApi.Dtos;

public class SaveMatchRequest
{
    // obligatorios
    public string HomeName { get; set; } = default!;
    public string AwayName { get; set; } = default!;
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }

    // opcionales
    public int? HomeFouls { get; set; }
    public int? AwayFouls { get; set; }
    public int? QuarterDurationSec { get; set; }
    public int? QuartersPlayed { get; set; }
    public string? HomeColorHex { get; set; }
    public string? AwayColorHex { get; set; }
    public string? ExtraJson { get; set; }     // libre: stats por cuarto, etc.
}
