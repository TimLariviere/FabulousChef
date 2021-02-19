module FabulousChef.Pages.DashboardPage

open System
open Fabulous.Tracing
open FabulousChef.Models
open FabulousChef

open FabulousChef.PancakeViewExtensions
open FabulousChef.Components.DishCell
open FabulousChef.Components.DishTypeRadioButton

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

type Model =
    { Chef: Chef
      SelectedDishType: DishType
      Dishes: Dish list  }
    
type Msg =
    | SelectDishType of DishType

let init () =
    let chef = Data.getChefById (ChefId 1)
    let selectedDishType = MainDish
    { Chef = chef
      SelectedDishType = selectedDishType
      Dishes = chef.Dishes |> List.filter (fun d -> d.Type = selectedDishType) }
    
let update (msg: Msg) (model: Model) =
    match msg with
    | SelectDishType dishType ->
        let dishes = model.Chef.Dishes |> List.filter (fun d -> d.Type = dishType)
        { model with SelectedDishType = dishType; Dishes = dishes }
    
let headerView model =
    View.StackLayout(
        key = "header",
        spacing = 0.,
        children = [
            View.StackLayout(
                key = "header",
                orientation = StackOrientation.Horizontal,
                padding = Thickness(15., 60., 15., 15.),
                spacing = 20.,
                children = [
                    View.ImageButton(
                        source = Image.fromFont(
                            FontImageSource(
                                FontFamily = Fonts.Icomoon,
                                Glyph = Fonts.IcomoonConstants.Menu,
                                Color = Colors.textForeground ()
                            )
                        ),
                        height = 30.,
                        width = 30.,
                        verticalOptions = LayoutOptions.Start
                    )
                    
                    View.StackLayout(
                        horizontalOptions = LayoutOptions.StartAndExpand,
                        spacing = 0.,
                        children = [
                            View.Label(
                                text = "Meet your Chef",
                                textColor = Colors.textForeground (),
                                fontSize = FontSize.fromValue 18.,
                                fontFamily = Fonts.OpenSansRegular
                            )
                            
                            View.Label(
                                text = $"{model.Chef.FirstName} {model.Chef.LastName}",
                                textColor = Colors.textForeground (),
                                fontSize = FontSize.fromValue 22.,
                                fontFamily = Fonts.OpenSansSemibold
                            )
                        ]
                    )
                    
                    View.Image(
                        source = Image.fromPath model.Chef.Picture,
                        aspect = Aspect.AspectFill,
                        height = 52.,
                        width = 52.,
                        clip = View.EllipseGeometry(
                            center = Point(26., 26.),
                            radiusX = 26.,
                            radiusY = 26.
                        )
                    )
                ]
            )
            View.ContentView(
                height = 1.,
                backgroundColor = Colors.separatorColor ()
            )
        ]
    )
    
    
let dishTypeSelectorView model dispatch =
    let dishTypes = [
        "MEAT", Meat
        "DESSERTS", Dessert
        "MAIN DISH", MainDish
        "STARTERS", Starter
    ]
    
    View.PancakeView(
        backgroundColor = Colors.dishTypeSelectorBackground (),
        cornerRadius = CornerRadius(0., 20., 0., 20.),
        margin = Thickness(0., 0., 15., 0.),
        verticalOptions = LayoutOptions.Center,
        padding = Thickness(5., 0.),
        content = View.StackLayout(
            spacing = 0.,
            children = [
                for (name, dishType) in dishTypes do 
                    View.DishTypeRadioButton(
                        groupName = "DishType",
                        content = Content.fromString name,
                        isChecked = (model.SelectedDishType = dishType),
                        checkedChanged = (fun _ -> dispatch (SelectDishType dishType))
                    )
            ]
         )
    )
    
let dishesListView model =
    if model.Dishes.Length = 0 then
        View.Label(
            text = sprintf "No %A available" model.SelectedDishType,
            horizontalOptions = LayoutOptions.Center,
            verticalOptions = LayoutOptions.Center
        )
    else
        View.CollectionView([
            for dish in model.Dishes do
                View.DishCell(dish.Id)
        ])
    
let view model dispatch =
    View.ContentPage(
        useSafeArea = false,
        backgroundColor = Colors.appBackground (),
        content = View.Grid(
            rowdefs = [ Auto; Star ],
            coldefs = [ Absolute 75.; Star ],
            children = [
                (headerView model)
                    .ColumnSpan(2)
            
                (dishTypeSelectorView model dispatch)
                    .Row(1)
            
                (dishesListView model)
                    .Row(1)
                    .Column(1)
            ]
        )
    )
    
let program =
    XamarinFormsProgram.mkSimple init update view
#if DEBUG
    |> Program.withTraceLevel TraceLevel.Debug
    |> Program.withConsoleTrace
#endif

type Fabulous.XamarinForms.View with
    static member inline DashboardPage() = Component.forProgram program
