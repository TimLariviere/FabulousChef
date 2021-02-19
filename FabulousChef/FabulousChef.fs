namespace FabulousChef

open Fabulous
open Fabulous.XamarinForms
open FabulousChef.Pages.DashboardPage
open Xamarin.Forms

module App = 
    type Model =
        { X: bool }

    type Msg = 
        | NoMsg

    let init () =
        { X = true }

    let update (msg: Msg) (model: Model) =
        model

    let view (model: Model) dispatch =
        View.Application(
            View.DashboardPage()
        )

    let program =
        XamarinFormsProgram.mkSimple init update view
#if DEBUG
        |> Program.withConsoleTrace
#endif

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
        |> XamarinFormsProgram.run app