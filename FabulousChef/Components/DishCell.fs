module FabulousChef.Components.DishCell

open System
open FabulousChef.Models
open FabulousChef
open FabulousChef.Controls.LikeButton
open FabulousChef.Controls.PreparationTimeLabel
open FabulousChef.Controls.ReviewsSummary
open FabulousChef.PancakeViewExtensions

open Fabulous.XamarinForms
open Xamarin.Forms

type Model =
    { Dish: Dish
      IsFavorite: bool
      AverageReviews: float }
    
type Msg =
    | ToggleLike of bool
    | CellTapped
    
type ExternalMsg =
    | Tapped
    
let init dishId =
    let dish = Data.getDishById dishId
    let averageReviews =
        if dish.Reviews.Length = 0 then
            5.
        else
            dish.Reviews
            |> List.map float
            |> List.average
    
    let model =
        { Dish = dish
          IsFavorite = false
          AverageReviews = averageReviews }
    model, [], []
    
let update msg model =
    match msg with
    | ToggleLike value ->
        let newModel : Model = { model with IsFavorite = value }
        newModel, [], []
        
    | CellTapped ->
        model, [], [Tapped]

let view model dispatch =
    View.Grid(
        margin = Thickness(0., 0., 0., 20.),
        gestureRecognizers = [
            View.TapGestureRecognizer(
                command = fun () -> dispatch CellTapped
            )
        ],
        children = [
            View.PancakeView(
                backgroundColor = Colors.controlBackground (),
                cornerRadius = CornerRadius(30., 0., 30., 0.),
                margin = Thickness(0., 90., 0., 0.)
            )
            
            View.StackLayout(
                padding = Thickness(20., 0., 30., 20.),
                children = [
                    View.StackLayout(
                        orientation = StackOrientation.Horizontal,
                        children = [
                            View.Image(
                                source = Image.fromPath model.Dish.Picture,
                                height = 180.,
                                width = 180.,
                                horizontalOptions = LayoutOptions.StartAndExpand
                            )
                            View.LikeButton(
                                margin = Thickness(0., 0., 0., -80.),
                                height = 40.,
                                width = 40.
                            )
                        ]
                    )
                    View.Label(
                        text = model.Dish.Name,
                        fontSize = FontSize.fromValue 20.,
                        fontFamily = Fonts.MontserratSemibold,
                        margin = Thickness(0., 10., 0., 0.)
                    )
                    View.ReviewsSummary(
                        averageReviews = model.AverageReviews,
                        reviewsCount = model.Dish.Reviews.Length
                    )
                    View.StackLayout(
                        height = 40.,
                        margin = Thickness(0., 20., 0., 0.),
                        children = [
                            View.PreparationTimeLabel(
                                preparationTime = model.Dish.Recipe.PreparationTime
                            )
                        ]
                    )
                ]
            )
        ]
    )
    
let program =
    XamarinFormsProgram.mkComponent init update view
    
type Fabulous.XamarinForms.View with
    static member inline DishCell(id, onExternalMsg) =
        View.ContentView(
            key = id.ToString(),
            content = Component.forProgramWithArgsAndExternalMsg(program, id, onExternalMsg)
        )