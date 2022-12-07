namespace RpgApi.DTOs;

public class CharacterForUpdateDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public double Height { get; set; }
    public double Weight { get; set; }

    public int RpgClassId { get; set; }
}
