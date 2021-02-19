module FabulousChef.Colors

open Xamarin.Forms

let choose light dark =
    match Application.Current.RequestedTheme with
    | OSAppTheme.Light | OSAppTheme.Unspecified -> light
    | OSAppTheme.Dark -> dark
    | theme -> failwith $"Unknown AppTheme {theme}"

let appBackground () = choose Color.White (Color.FromHex("#242424"))
let controlBackground () = choose (Color.FromHex("#9A9A9A")) (Color.FromHex("#2E2E2E"))
let textForeground () = choose (Color.FromHex("#242424")) Color.White
let averageReviewTextForeground () = Color.FromHex("#FFCC00")
let reviewCountTextForeground () = Color.FromHex("#5A5A5A")
let preparationTimeBackground () = Color.FromHex("#FAC800")
let preparationTimeForeground () = Color.FromHex("#171717")
let separatorColor () = Color.FromHex("#707070")