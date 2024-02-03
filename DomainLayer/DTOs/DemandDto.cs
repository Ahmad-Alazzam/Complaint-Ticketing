using Common.Enum;

public class DemandDto
{
    public int Id { get; set; }
    public string DemandTextAr { get; set; }
    public string DemandTextEn { get; set; }
    public string DemandText => SelectedLanguage == Language.Arabic ? DemandTextAr : DemandTextEn;
    public Language SelectedLanguage { get; set; } = Language.Arabic;
}