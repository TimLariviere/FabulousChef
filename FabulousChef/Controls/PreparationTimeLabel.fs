module FabulousChef.Controls.PreparationTimeLabel

open Fabulous.XamarinForms
open FabulousChef.PancakeViewExtensions
open Xamarin.Forms
open FabulousChef
open System

let formatTime (time: TimeSpan) =
    if time.TotalHours > 1. then
        sprintf "%i HR %i MIN" time.Hours time.Minutes
    elif time.TotalHours = 1. then
        sprintf "1 HR"
    else
        sprintf "%i MIN" time.Minutes

type Fabulous.XamarinForms.View with
    static member inline PreparationTimeLabel(?preparationTime) =
        let preparationTime = match preparationTime with Some x -> x | None -> TimeSpan.Zero
        
        View.PancakeView(
            cornerRadius = CornerRadius(10.),
            backgroundColor = Colors.preparationTimeBackground (),
            padding = Thickness(10.),
            width = 125.,
            horizontalOptions = LayoutOptions.Start,
            content = View.Label(
                text = formatTime preparationTime,
                textColor = Colors.preparationTimeForeground (),
                fontFamily = Fonts.OpenSansBold,
                fontSize = FontSize.fromValue 16.,
                horizontalTextAlignment = TextAlignment.Center
            )
        )