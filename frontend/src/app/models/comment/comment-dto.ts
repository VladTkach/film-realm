import {UserDto} from "../user/user-dto";

export interface CommentDto{
  id: number
  text: string
  user: UserDto
}
