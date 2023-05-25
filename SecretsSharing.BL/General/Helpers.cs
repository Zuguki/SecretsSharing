namespace SecretsSharing.BL.General;

public class Helpers
{
    public static int? StringToIntDef(string str, int? def)
    {
        return int.TryParse(str, out var value) 
            ? value 
            : def;
    }

    public static Guid? StringToGuidDef(string str, Guid? def = null)
    {
        return Guid.TryParse(str, out var value) 
            ? value 
            : def;
    }
}