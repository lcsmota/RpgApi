using System.Text.Json.Serialization;

namespace RpgApi.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }

    public int RpgClassId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RpgClass RpgClass { get; set; }
}
