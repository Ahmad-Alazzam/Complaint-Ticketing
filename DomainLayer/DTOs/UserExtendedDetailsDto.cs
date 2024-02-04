using Common.Enum;

public class UserExtendedDetailsDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public UserTypeEnum UserType { get; set; }
    public DateTime DateOfBirth { get; set; }
}