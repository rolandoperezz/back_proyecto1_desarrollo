namespace BasketApi.Models;

public class MatchResult
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // Equipos
    public string HomeName { get; set; } = default!;
    public string AwayName { get; set; } = default!;

    // Marcadores finales
    public int HomeScore { get; set; }
    public int AwayScore { get; set; }

    // Faltas (totales de equipo)
    public int HomeFouls { get; set; }
    public int AwayFouls { get; set; }

    // Configuración del juego
    public int QuarterDurationSec { get; set; }   // 600 = 10 min
    public int QuartersPlayed { get; set; }       // 4 típicamente

    // Colores (hex) opcionales para referencia
    public string? HomeColorHex { get; set; }     // "#2563EB"
    public string? AwayColorHex { get; set; }     // "#EF4444"

    // Fecha/hora de cierre (UTC)
    public DateTime EndedAtUtc { get; set; } = DateTime.UtcNow;

    // (Opcional) JSON con datos extra (p. ej., puntos por cuarto)
    public string? ExtraJson { get; set; }
}
