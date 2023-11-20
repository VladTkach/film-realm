import { Injectable } from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {BehaviorSubject, tap} from "rxjs";
import {UserDto} from "../../models/user/user-dto";
import {UserLoginDto} from "../../models/user/user-login-dto";
import {UserAuthDto} from "../../models/user/user-auth-dto";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authRoutePrefix = '/api/auth';
  private currentUserSource = new BehaviorSubject<UserDto | null>(null);
  public currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpInternalService) { }

  public login(userLoginDto: UserLoginDto){
    return this.http.postRequest<UserAuthDto>(`${this.authRoutePrefix}/login`, userLoginDto)
      .pipe(tap( (userAuth) => {
        this.setCurrentUser(userAuth);
      }))
  }

  public logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  public setCurrentUser(userAuthDto: UserAuthDto) {
    const roles = this.getDecodedToken(userAuthDto.token.accessToken.token).role;
    Array.isArray(roles);
    console.log(roles);

    localStorage.setItem('user', JSON.stringify(userAuthDto));
    this.currentUserSource.next(userAuthDto.user);
  }

  getDecodedToken(token: string){
    return JSON.parse(atob(token.split('.')[1]))
  }
}
