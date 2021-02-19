module FabulousChef.Models

open System

type DishRecipe =
    { Steps: string list
      PreparationTime: TimeSpan }
    
type DishType =
    | Starter
    | MainDish
    | Dessert
    | Meat
    
type DishId = DishId of int

type Dish =
    { Id: DishId
      Name: string
      Picture: string
      Type: DishType
      IsFavorite: bool
      Recipe: DishRecipe
      Reviews: int list }
    
type ChefId = ChefId of int
    
type Chef =
    { Id: ChefId
      FirstName: string
      LastName: string
      Picture: string
      Dishes: Dish list }
    
type Cart =
    { Dishes: Dish list }