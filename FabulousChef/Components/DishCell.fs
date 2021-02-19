module FabulousChef.Components.DishCell

open System
open FabulousChef.Models
open FabulousChef

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
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
            7.
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
    
let formatTime (time: TimeSpan) =
    if time.TotalHours > 1. then
        sprintf "%i HR %i MIN" time.Hours time.Minutes
    elif time.TotalHours = 1. then
        sprintf "1 HR"
    else
        sprintf "%i MIN" time.Minutes

let view model dispatch =
    View.Grid(
        margin = Thickness(0., 0., 0., 15.),
        children = [
            View.ContentView(
                backgroundColor = Colors.controlBackground (),
                margin = Thickness(0., 90., 0., 0.)
            )
            
            View.StackLayout(
                padding = Thickness(20., 0., 20., 20.),
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
                            View.ImageButton(
                                source = Image.fromFont(
                                     FontImageSource(
                                         FontFamily = Fonts.Icomoon,
                                         Glyph = Fonts.IcomoonConstants.Heart
                                     )
                                ),
                                height = 40.,
                                width = 40.,
                                margin = Thickness(0., 0., 0., -70.)
                            )
                        ]
                    )
                    View.Label(
                        text = model.Dish.Name,
                        fontSize = FontSize.fromValue 20.,
                        fontFamily = Fonts.MontserratSemibold,
                        margin = Thickness(0., 10., 0., 0.)
                    )
                    View.StackLayout(
                        orientation = StackOrientation.Horizontal,
                        children = [
                            View.Label(
                                formattedText = View.FormattedString(
                                    spans = [
                                        View.Span(
                                            text =
                                                (if model.AverageReviews <= 5. then
                                                    Fonts.IcomoonConstants.Ok
                                                else
                                                    Fonts.IcomoonConstants.Fire),
                                            fontFamily = Fonts.Icomoon,
                                            textColor = Colors.averageReviewTextForeground ()
                                        )
                                        View.Span(
                                            text = sprintf " %.2f" model.AverageReviews,
                                            fontFamily = Fonts.OpenSansSemibold,
                                            textColor = Colors.averageReviewTextForeground ()
                                        )
                                        View.Span(
                                            text = sprintf " (%i)" model.Dish.Reviews.Length,
                                            fontFamily = Fonts.OpenSansLight,
                                            textColor = Colors.reviewCountTextForeground ()
                                        )
                                    ]
                                ),
                                fontSize = FontSize.fromValue 16.
                            )
                        ]
                    )
                    View.Label(
                        text = formatTime model.Dish.Recipe.PreparationTime,
                        textColor = Colors.preparationTimeForeground (),
                        fontFamily = Fonts.OpenSansBold,
                        fontSize = FontSize.fromValue 16.,
                        backgroundColor = Colors.preparationTimeBackground (),
                        horizontalTextAlignment = TextAlignment.Center,
                        width = 150.,
                        height = 40.,
                        padding = Thickness(10.),
                        horizontalOptions = LayoutOptions.Start,
                        margin = Thickness(0., 20., 0., 0.)
                    )
                ]
            )
        ]
    )
    
let program =
    XamarinFormsProgram.mkSimple init update view
    
type Fabulous.XamarinForms.View with
    static member inline DishCell(id) =
        View.ContentView(
            Component.forProgramWithArgs (program, id)
        )