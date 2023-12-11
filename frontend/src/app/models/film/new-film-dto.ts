export interface NewFilmDto{
  name: string
  description: string
  year: number
  poster: File
  resourceUrl: string
  genresId: number[]
  actorsId: number[]
}
