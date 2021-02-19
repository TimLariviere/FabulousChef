module FabulousChef.Components.DishCell

open FabulousChef.Models
open FabulousChef

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

type Model =
    { Dish: Dish
      IsFavorite: bool
      AverageReviews: float }
    
type Msg =
    | ToggleLike of bool
    
let init dishId =
    let dish = Data.getDishById dishId
    let averageReviews =
        if dish.Reviews.Length = 0 then
            5.
        else
            dish.Reviews
            |> List.map float
            |> List.average
    
    { Dish = dish
      IsFavorite = false
      AverageReviews = averageReviews }
    
let update msg model =
    match msg with
    | ToggleLike value -> { model with IsFavorite = value } : Model

let view model dispatch =
    let reviewFontImageSource =
        if model.AverageReviews <= 5. then
            FontImageSource(FontFamily = Fonts.Icomoon, Glyph = Fonts.IcomoonConstants.Ok)
        else
            FontImageSource(FontFamily = Fonts.Icomoon, Glyph = Fonts.IcomoonConstants.Fire)
    
    View.Grid([
        View.ContentView(
            background = View.SolidColorBrush(Color.Red),
            content =
                View.CheckBox(
                    isChecked = model.IsFavorite,
                    checkedChanged = fun e -> dispatch (ToggleLike e.Value)
                )
        )
        
        View.StackLayout([
            View.Image(
                source = Image.fromPath model.Dish.Picture,
                height = 480.,
                width = 480.
            )
            View.Label(
                text = model.Dish.Name
            )
            View.StackLayout(
                orientation = StackOrientation.Horizontal,
                children = [
                    View.Image(
                        source = Image.fromFont reviewFontImageSource
                    )
                    View.Label(
                        formattedText = View.FormattedString(
                            spans = [
                                View.Span(
                                    text = model.AverageReviews.ToString()    
                                )
                                View.Span(
                                    text = model.Dish.Reviews.Length.ToString()    
                                )
                            ]
                        )
                    )
                ]
            )
            View.Label(
                text = model.Dish.Recipe.PreparationTime.ToString()    
            )
        ])
    ])
    
let program =
    XamarinFormsProgram.mkSimple init update view
    
type Fabulous.XamarinForms.View with
    static member inline DishCell(id) =
        View.ContentView(
            Component.forProgramWithArgs (program, id)
        )