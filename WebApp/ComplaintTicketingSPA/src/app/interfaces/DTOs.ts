export class ComplaintDto {
  Id: number;
  ComplaintTextAr: string;
  ComplaintTextEn: string;
  ComplaintText: string;
  SelectedLanguage: Language;
  Demands: DemandDto[];
  UserId: number;
  AttachmentFilePath: string;
  SubmissionDate: Date;

  constructor(
    Id: number = 0,
    ComplaintTextAr: string = '',
    ComplaintTextEn: string = '',
    ComplaintText: string = '',
    SelectedLanguage: Language = Language.Arabic,
    Demands: DemandDto[] = [],
    UserId: number = 0,
    AttachmentFilePath: string = '',
    SubmissionDate: Date = new Date()
  ) {
    this.Id = Id;
    this.ComplaintTextAr = ComplaintTextAr;
    this.ComplaintTextEn = ComplaintTextEn;
    this.ComplaintText = ComplaintText;
    this.SelectedLanguage = SelectedLanguage;
    this.Demands = Demands;
    this.UserId = UserId;
    this.AttachmentFilePath = AttachmentFilePath;
    this.SubmissionDate = SubmissionDate;
  }
}

export class DemandDto {
  Id: number;
  DemandTextAr: string;
  DemandTextEn: string;
  DemandText: string;
  SelectedLanguage: Language;

  constructor(
    Id: number = 0,
    DemandTextAr: string = '',
    DemandTextEn: string = '',
    DemandText: string = '',
    SelectedLanguage: Language = Language.Arabic
  ) {
    this.Id = Id;
    this.DemandTextAr = DemandTextAr;
    this.DemandTextEn = DemandTextEn;
    this.DemandText = DemandText;
    this.SelectedLanguage = SelectedLanguage;
  }
}

export class UserDto {
  Id: number;
  UserName: string;
  Password: string;
  UserType: UserTypeEnum;
  UserDetails: UserExtendedDetailsDto;

  constructor(
    Id: number = 0,
    UserName: string = '',
    Password: string = '',
    UserType: UserTypeEnum = UserTypeEnum.User,
    UserDetails: UserExtendedDetailsDto = new UserExtendedDetailsDto()
  ) {
    this.Id = Id;
    this.UserName = UserName;
    this.Password = Password;
    this.UserType = UserType;
    this.UserDetails = UserDetails;
  }
}

export class UserExtendedDetailsDto {
  Name: string;
  Email: string;
  PhoneNumber: string;
  DateOfBirth: Date;

  constructor(
    Name: string = '',
    Email: string = '',
    PhoneNumber: string = '',
    DateOfBirth: Date = new Date()
  ) {
    this.Name = Name;
    this.Email = Email;
    this.PhoneNumber = PhoneNumber;
    this.DateOfBirth = DateOfBirth;
  }
}
  
export enum Language {
    Arabic,
    English
  }

export enum UserTypeEnum {
    User,
    Administrator
  }

export enum ComplaintStatus
  {
      UnderReview,
      Approveed,
      Rejected
  }
