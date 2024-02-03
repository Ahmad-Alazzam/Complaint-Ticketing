using Common.Enum;

public class ComplaintDto
{
    public int Id { get; set; }
    public string ComplaintTextAr { get; set; }
    public string ComplaintTextEn { get; set; }
    public string ComplaintText => SelectedLanguage == Language.Arabic ? ComplaintTextAr : ComplaintTextEn;
    public Language SelectedLanguage { get; set; }
    public List<DemandDto> Demands { get; set; }
    public string AttachmentFilePath { get; set; }
    public DateTime SubmissionDate { get; set; }
}