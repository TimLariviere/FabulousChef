module FabulousChef.Pages.DashboardPage

open System
open Fabulous.Tracing
open FabulousChef.Models
open FabulousChef
open FabulousChef.Components.DishCell

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open Xamarin.Forms

type Model =
    { Chef: Chef
      SelectedDishType: DishType }
    
type Msg =
    | SelectDishType of DishType

let init () =
    { Chef = Data.getChefById (ChefId 1)
      SelectedDishType = MainDish }
    
let update (msg: Msg) (model: Model) =
    model
    
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
        "Starters", Starter
        "Main dishes", MainDish
        "Desserts", Dessert
        "Meats", Meat
    ]
    
    View.StackLayout(
        orientation = StackOrientation.Horizontal,
        children = [
            for (name, dishType) in dishTypes do 
                View.RadioButton(
                    groupName = "DishType",
                    content = Content.fromString name,
                    isChecked = (model.SelectedDishType = dishType),
                    checkedChanged = fun _ -> dispatch (SelectDishType dishType)
                )
        ]
    )
    
let dishesListView model =
    View.CollectionView([
        for dish in model.Chef.Dishes do
            if dish.Type = model.SelectedDishType then
                View.DishCell(dish.Id)
    ])
    
let view model dispatch =
    View.ContentPage(
        useSafeArea = false,
        backgroundColor = Colors.appBackground (),
        content = View.Grid(
            rowdefs = [ Auto; Auto; Star ],
            coldefs = [ Absolute 50.; Star ],
            children = [
                (headerView model)
                    .ColumnSpan(2)
            
                (dishTypeSelectorView model dispatch)
                    .Row(1)
                    .ColumnSpan(2)
            
                (dishesListView model)
                    .Row(2)
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
