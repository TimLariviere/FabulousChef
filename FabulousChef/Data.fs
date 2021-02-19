module FabulousChef.Data

open System
open Models

let data =
    [ { Id = ChefId 1
        FirstName = "Alexender"
        LastName = "Doe"
        Picture = "chef-alexender-doe.jpg"
        Dishes = [
            { Id = DishId 1
              Name = "Italian Grill Fish"
              Picture = "dish-italian-grill-fish.png"
              Type = MainDish
              IsFavorite = false
              Recipe =
                  { Steps = []
                    PreparationTime = TimeSpan(0, 45, 0) }
              Reviews = [] }
            
            { Id = DishId 2
              Name = "Shrimp Bowl Waba Grill"
              Picture = "dish-shrimp-bowl-waba-grill.png"
              Type = MainDish
              IsFavorite = true
              Recipe =
                  { Steps = []
                    PreparationTime = TimeSpan(1, 45, 0) }
              Reviews = [] }
        ] } ]

let getDishById (id: DishId) =
    data
    |> List.collect (fun c -> c.Dishes)
    |> List.find (fun d -> d.Id = id)
    
let getChefById (id: ChefId) =
    data
    |> List.find (fun c -> c.Id = id)