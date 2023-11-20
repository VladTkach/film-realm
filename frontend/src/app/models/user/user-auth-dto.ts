import {UserDto} from "./user-dto";
import {Token} from "../auth/token";

export interface UserAuthDto {
  user: UserDto
  token: Token
}
