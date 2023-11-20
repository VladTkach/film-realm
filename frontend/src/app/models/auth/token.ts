import {AccessToken} from "./access-token";

export interface Token {
  accessToken: AccessToken
  refreshToken: string
}
