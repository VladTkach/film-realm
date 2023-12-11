import {Injectable} from '@angular/core';
import {HttpInternalService} from "./http-internal.service";
import {BehaviorSubject, tap} from "rxjs";
import {UserDto} from "../../models/user/user-dto";
import {UserLoginDto} from "../../models/user/user-login-dto";
import {UserAuthDto} from "../../models/user/user-auth-dto";
import {Token} from "../../models/auth/token";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authRoutePrefix = '/api/auth';
  private readonly roleItem = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
  private currentUserSource = new BehaviorSubject<UserDto | null>(null);
  public currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpInternalService) { }

  public login(userLoginDto: UserLoginDto){
    return this.http.postRequest<UserAuthDto>(`${this.authRoutePrefix}/login`, userLoginDto)
      .pipe(tap( (userAuth) => {
        this.setCurrentUser(userAuth.user);
        this.setToken(userAuth.token);
      }))
  }

  public logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }

  public setCurrentUser(userDto: UserDto) {
    const token = this.getAccessToken();
    if (!token){
      return;
    }
    userDto.role = this.getDecodedToken(token)[this.roleItem];
    this.currentUserSource.next(userDto);
  }

  public getAccessToken(){
    const tokenString = localStorage.getItem('token');
    if (!tokenString) {
      return null;
    }
    const token: Token = JSON.parse(tokenString);

    return token.accessToken.token;
  }

  private setToken(token: Token) {
    localStorage.setItem('token', JSON.stringify(token));
  }

  getDecodedToken(token: string){
    return JSON.parse(atob(token.split('.')[1]))
  }
}
