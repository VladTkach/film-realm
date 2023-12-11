import {GenreDto} from "../genre/genre-dto";
import {ActorDto} from "../actor/actor-dto";

export interface FilmDto{
  id: number
  name: string
  description?: string
  year: number
  posterUrl: string
  resourceUrl: string
  genres: GenreDto[]
  actors: ActorDto[]
}
