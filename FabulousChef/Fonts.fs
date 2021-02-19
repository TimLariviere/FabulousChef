module FabulousChef.Fonts

open Xamarin.Forms

let [<Literal>] OpenSansLight = "OpenSans-Light"
let [<Literal>] OpenSansRegular = "OpenSans-Regular"
let [<Literal>] OpenSansSemibold = "OpenSans-Semibold"
let [<Literal>] OpenSansBold = "OpenSans-Bold"
let [<Literal>] MontserratSemibold = "Montserrat-Semibold"
let [<Literal>] Icomoon = "Icomoon"

[<assembly: ExportFont("OpenSans-Light.ttf", Alias = OpenSansLight)>]
[<assembly: ExportFont("OpenSans-Regular.ttf", Alias = OpenSansRegular)>]
[<assembly: ExportFont("OpenSans-Semibold.ttf", Alias = OpenSansSemibold)>]
[<assembly: ExportFont("OpenSans-Bold.ttf", Alias = OpenSansBold)>]
[<assembly: ExportFont("Montserrat-Semibold.ttf", Alias = MontserratSemibold)>]
[<assembly: ExportFont("icomoon.ttf", Alias = Icomoon)>]
do ()

module IcomoonConstants =
    let [<Literal>] BellRing = "\ue900"
    let [<Literal>] Bookmark = "\ue901"
    let [<Literal>] CardStrike = "\ue902"
    let [<Literal>] Comments = "\ue903"
    let [<Literal>] DownArrow = "\ue904"
    let [<Literal>] Fire = "\ue905"
    let [<Literal>] Heart = "\ue909"
    let [<Literal>] Ok = "\ue90a"
    let [<Literal>] RecipeBook = "\ue90b"
    let [<Literal>] Search = "\ue90c"
    let [<Literal>] ShoppingCart = "\ue90d"
    let [<Literal>] UpArrow = "\ue90e"
    let [<Literal>] Back = "\ue90f"
    let [<Literal>] Menu = "\ue910"
    
    open Fabulous.XamarinForms
    
    let fire fontSize =
        View.Grid([
            View.Label(
                text = "\ue905",
                textColor = Color.FromHex("#FF6536"),
                fontFamily = Icomoon,
                fontSize = fontSize
            )
            View.Label(
                text = "\ue906",
                textColor = Color.FromHex("#FF421D"),
                fontFamily = Icomoon,
                fontSize = fontSize
            )
            View.Label(
                text = "\ue907",
                textColor = Color.FromHex("#FBBF00"),
                fontFamily = Icomoon,
                fontSize = fontSize
            )
            View.Label(
                text = "\ue908",
                textColor = Color.FromHex("#FFA900"),
                fontFamily = Icomoon,
                fontSize = fontSize
            )
        ])
    
    let ok fontSize =
        View.Label(
            text = Ok,
            textColor = Color.FromHex("#FFCC00"),
            fontFamily = Icomoon,
            fontSize = fontSize
        )