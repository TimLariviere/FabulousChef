module FabulousChef.Fonts

open Xamarin.Forms

let [<Literal>] OpenSansRegular = "OpenSans-Regular"
let [<Literal>] OpenSansSemibold = "OpenSans-Semibold"
let [<Literal>] Icomoon = "Icomoon"

[<assembly: ExportFont("OpenSans-Regular.ttf", Alias = OpenSansRegular)>]
[<assembly: ExportFont("OpenSans-Semibold.ttf", Alias = OpenSansSemibold)>]
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