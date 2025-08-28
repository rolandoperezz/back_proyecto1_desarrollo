using BasketApi.Models;

namespace BasketApi.Dtos;

public class MatchResultDto
{
    public Guid Id { get; set; }
    public string HomeName { get; set; } = default!;
    public string AwayName { get; set; } = default!;
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }
    public int HomeFouls { get; set; }
    public int AwayFouls { get; set; }
    public int QuarterDurationSec { get; set; }
    public int QuartersPlayed { get; set; }
    public string? HomeColorHex { get; set; }
    public string? AwayColorHex { get; set; }
    public DateTime EndedAtUtc { get; set; }
    public string? ExtraJson { get; set; }

    public static MatchResultDto FromEntity(MatchResult e) => new()
    {
        Id = e.Id,
        HomeName = e.HomeName,
        AwayName = e.AwayName,
        HomeScore = e.HomeScore,
        AwayScore = e.AwayScore,
        HomeFouls = e.HomeFouls,
        AwayFouls = e.AwayFouls,
        QuarterDurationSec = e.QuarterDurationSec,
        QuartersPlayed = e.QuartersPlayed,
        HomeColorHex = e.HomeColorHex,
        AwayColorHex = e.AwayColorHex,
        EndedAtUtc = e.EndedAtUtc,
        ExtraJson = e.ExtraJson
    };
}
