namespace FabulousChef

open Fabulous
open Fabulous.XamarinForms
open FabulousChef.Pages
open FabulousChef.Pages.DashboardPage
open FabulousChef.Pages.DishPage
open FabulousChef.Models
open Xamarin.Forms

module App = 
    type Model =
        { SelectedDishId: DishId option }

    type Msg = 
        | NavigateToDish of DishId
        | NavigateBack

    let init () =
        { SelectedDishId = None }

    let update (msg: Msg) (model: Model) =
        match msg with
        | NavigateToDish dishId ->
            { model with SelectedDishId = Some dishId }
                
        | NavigateBack ->
            { model with SelectedDishId = None }

    let view (model: Model) dispatch =
        View.Application(
            View.NavigationPage(
                useSafeArea = false,
                barBackgroundColor = Colors.appBackground (),
                barTextColor = Colors.textForeground (),
                popped = (fun _ -> dispatch NavigateBack),
                pages = [
                    View.DashboardPage(
                        onExternalMsg = fun extMsg ->
                            match extMsg with
                            | DashboardPage.ExternalMsg.NavigateToDish dishId ->
                                dispatch (NavigateToDish dishId)
                    )
                    
                    match model.SelectedDishId with
                    | None -> ()
                    | Some dishId -> View.DishPage(dishId)
                ]
            )
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