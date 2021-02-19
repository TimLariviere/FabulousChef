module FabulousChef.Colors

open Xamarin.Forms

let choose light dark =
    match Application.Current.RequestedTheme with
    | OSAppTheme.Light | OSAppTheme.Unspecified -> light
    | OSAppTheme.Dark -> dark
    | theme -> failwith $"Unknown AppTheme {theme}"

let appBackground () = choose Color.White (Color.FromHex("#242424"))
let controlBackground () = choose (Color.FromHex("#329A9A9A")) (Color.FromHex("#FF2E2E2E"))
let textForeground () = choose (Color.FromHex("#242424")) Color.White
let averageReviewTextForeground () = Color.FromHex("#FFCC00")
let reviewCountTextForeground () = Color.FromHex("#5A5A5A")
let preparationTimeBackground () = Color.FromHex("#FAC800")
let preparationTimeForeground () = Color.FromHex("#171717")
let separatorColor () = Color.FromHex("#707070")
let uncheckedHeartColor () = choose (Color.FromHex("#FFCC00")) Color.White

let unselectedDishTypeBackground () = Color.Transparent
let unselectedDishTypeForeground () = textForeground ()
let selectedDishTypeBackground () = Color.Black
let selectedDishTypeForeground () = Color.White
let dishTypeSelectorBackground () = choose (Color.FromHex("#16171717")) (Color.FromHex("#FF171717"))