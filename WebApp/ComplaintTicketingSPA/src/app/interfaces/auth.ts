export interface User {
    id: number;
    userName: string;
    password: string;
    userDetails: UserExtendedDetails;
  }
  
  export interface UserExtendedDetails {
    name: string;
    email: string;
    phoneNumber: string;
    dateOfBirth: Date;
  }