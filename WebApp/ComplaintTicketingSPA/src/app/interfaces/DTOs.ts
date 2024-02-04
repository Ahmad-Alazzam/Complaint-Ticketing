export interface ComplaintDto {
    Id: number;
    ComplaintTextAr: string;
    ComplaintTextEn: string;
    ComplaintText: string;
    SelectedLanguage: Language;
    Demands: DemandDto[];
    UserId: number;
    AttachmentFilePath: string;
    SubmissionDate: Date;
  }
  
  export interface DemandDto {
    Id: number;
    DemandTextAr: string;
    DemandTextEn: string;
    DemandText: string; // This property is derived based on the SelectedLanguage
    SelectedLanguage: Language;
  }

  export interface UserDto {
    Id: number;
    UserName: string;
    Password: string;
    UserType: UserTypeEnum ,
    UserDetails: UserExtendedDetailsDto;
  }
  
  export interface UserExtendedDetailsDto {
    Name: string;
    Email: string;
    PhoneNumber: string;
    DateOfBirth: Date;
  }
  
  export enum Language {
    Arabic,
    English
  }

  export enum UserTypeEnum {
    User,
    Administrator
  }