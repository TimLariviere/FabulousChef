module FabulousChef.Components.DishCell

open System
open FabulousChef.Models
open FabulousChef
open FabulousChef.PancakeViewExtensions

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
    
let formatTime (time: TimeSpan) =
    if time.TotalHours > 1. then
        sprintf "%i HR %i MIN" time.Hours time.Minutes
    elif time.TotalHours = 1. then
        sprintf "1 HR"
    else
        sprintf "%i MIN" time.Minutes

let view model dispatch =
    View.Grid(
        margin = Thickness(0., 0., 0., 20.),
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
                            View.ImageButton(
                                source = Image.fromFont(
                                     FontImageSource(
                                         FontFamily = Fonts.Icomoon,
                                         Glyph = Fonts.IcomoonConstants.Heart,
                                         Color = Colors.uncheckedHeartColor ()
                                     )
                                ),
                                height = 40.,
                                width = 40.,
                                margin = Thickness(0., 0., 0., -80.)
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
                            if model.AverageReviews <= 5. then
                                Fonts.IcomoonConstants.ok (FontSize.fromValue 20.)
                            else
                                Fonts.IcomoonConstants.fire (FontSize.fromValue 20.)
                                
                            View.Label(
                                formattedText = View.FormattedString(
                                    spans = [
                                        View.Span(
                                            text = sprintf "%.2f" model.AverageReviews,
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
                    View.StackLayout(
                        height = 40.,
                        margin = Thickness(0., 20., 0., 0.),
                        children = [
                            View.PancakeView(
                                cornerRadius = CornerRadius(10.),
                                backgroundColor = Colors.preparationTimeBackground (),
                                padding = Thickness(10.),
                                width = 150.,
                                horizontalOptions = LayoutOptions.Start,
                                content = View.Label(
                                    text = formatTime model.Dish.Recipe.PreparationTime,
                                    textColor = Colors.preparationTimeForeground (),
                                    fontFamily = Fonts.OpenSansBold,
                                    fontSize = FontSize.fromValue 16.,
                                    horizontalTextAlignment = TextAlignment.Center
                                )
                            )
                        ]
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
            key = id.ToString(),
            content = Component.forProgramWithArgs (program, id)
        )