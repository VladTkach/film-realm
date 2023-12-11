import { Injectable } from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {UserDto} from "../../models/user/user-dto";

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private readonly authRoutePrefix = '/api/admin';
  constructor(private http: HttpInternalService) { }

  public getAdmins() {
    return this.http.getRequest<UserDto[]>(`${this.authRoutePrefix}/all-admin`);
  }

  public getUsersByName(userName: string) {
    return this.http.getRequest<UserDto[]>(`${this.authRoutePrefix}/all-user/${userName}`);
  }

  public promoteAdmin(adminId: number) {
    return this.http.postRequest<UserDto>(`${this.authRoutePrefix}/add-admin/${adminId}`, {});
  }

  public deleteAdmin(adminId: number){
    return this.http.deleteRequest(`${this.authRoutePrefix}/all-admin/${adminId}`);
  }
}
