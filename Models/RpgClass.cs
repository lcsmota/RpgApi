using System.Text.Json.Serialization;

namespace RpgApi.Models;

public class RpgClass
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Character> Characters { get; set; } = new List<Character>();
}
