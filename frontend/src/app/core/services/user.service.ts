import { Injectable } from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {UpdateUsernameDto} from "../../models/user/update-username-dto";
import {UserDto} from "../../models/user/user-dto";
import {UpdatePasswordDto} from "../../models/user/update-password-dto";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly authRoutePrefix = '/api/user';
  constructor(private http: HttpInternalService) { }

  public updateUserName(updateUserNameDto: UpdateUsernameDto){
    return this.http.putRequest<UserDto>(`${this.authRoutePrefix}/update-username`, updateUserNameDto);
  }

  public updatePassword(updatePasswordDto: UpdatePasswordDto){
    return this.http.putRequest<UserDto>(`${this.authRoutePrefix}/update-password`, updatePasswordDto);
  }
}
